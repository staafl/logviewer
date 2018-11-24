using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace logviewer
{
    public partial class Form1 : Form
    {
        private string file_;
        private Rules rules_;

        public Form1()
        {
            InitializeComponent();
            dgv.CellClick += Dgv_CellClick;
            dgv.AllowDrop = true;
            dgv.DragEnter += Dgv_DragEnter;
            dgv.DragDrop += Dgv_DragDrop;
        }

        private void Dgv_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Dgv_DragDrop(object sender, DragEventArgs e)
        {
            this.file_ = ((string[])e.Data.GetData(DataFormats.FileDrop)).First();
            this.Text = this.file_;
            if (this.rules_ == null)
            {
                ShowRulesForm();
            }
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 && e.RowIndex == -1)
            {
                ShowRulesForm();
            }
        }

        private void ShowRulesForm()
        {
            using (var rulesForm = new RulesForm(this.rules_))
            {
                rulesForm.ShowDialog();
                this.LoadData(rules: rulesForm.Rules);
            }
        }

        private void LoadData(string file = null, Rules rules = null)
        {
            this.file_ = file ?? this.file_;
            this.rules_ = rules ?? this.rules_;
            rules = this.rules_;
            file = this.file_;

            if (file == null ||
                rules_?.ColumnRules == null ||
                !File.Exists(file_))
            {
                return;
            }

            this.dgv.Rows.Clear();
            foreach (var column in this.dgv.Columns.OfType<DataGridViewColumn>().ToArray())
            {
                if (column != columnLineNumber)
                {
                    dgv.Columns.Remove(column);
                }
            }

            IEnumerable<string> text = File.ReadLines(file);

            var newColumns = rules.ColumnRules.GroupBy(x => x.ColumnName);

            foreach (var newColumn in newColumns)
            {
                var column = new DataGridViewColumn();
                column.Name = newColumn.Key;
                column.HeaderText = newColumn.Key;
                column.CellTemplate = new DataGridViewTextBoxCell();
                column.ReadOnly = true;
                this.dgv.Columns.Add(column);
            }

            int rowIndex = 0;
            foreach (var row in text)
            {
                rowIndex += 1;
                var dgvRow = dgv.Rows[dgv.Rows.Add()];
                dgvRow.Cells[0].Value = rowIndex;
                bool anyMatches = false;
                int columnIndex = 0;
                foreach (var newColumn in newColumns)
                {
                    columnIndex += 1;
                    foreach (var rule in newColumn)
                    {
                        if (string.IsNullOrEmpty(rule.Regex))
                        {
                            continue;
                        }
                        foreach (var match in Regex.Matches(row, rule.Regex).OfType<Match>())
                        {
                            anyMatches = true;
                            dgvRow.Cells[columnIndex].Value += match.Groups.Count > 1 ?
                                string.Join(rule.RegexJoin ?? " ", match.Groups.OfType<Group>().Skip(1)) :
                                match.Value;
                        }
                    }
                }
                if (!anyMatches)
                {
                    dgv.Rows.Remove(dgvRow);
                }
            }
        }
    }
}
