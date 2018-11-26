using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            dgv.RowHeaderMouseClick += Dgv_RowHeaderMouseClick;
            this.KeyDown += Form1_KeyDown;
            this.KeyPreview = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && !e.Shift && !e.Alt && e.KeyCode == Keys.O)
            {
                using (var ofd = new OpenFileDialog())
                {
                    DialogResult result = ofd.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        SetFile(ofd.FileName);
                    }
                }
            }
        }

        private void Dgv_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.file_ == null)
            {
                return;
            }
            string notepad2 =
                new[] {
                    @"%USER_BACK%\btsync\util\Notepad2\Notepad2.exe",
                    @"C:\Program Files\Notepad2\Notepad2.exe",
                    @"C:\Program Files (x86)\Notepad2\Notepad2.exe"

                }
                .Select(Environment.ExpandEnvironmentVariables)
                .FirstOrDefault(File.Exists);

            int line = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[0].Value);

            if (notepad2 != null)
            {
                Process.Start(notepad2, $"/g {line} \"{this.file_}\"");
                return;
            }

            string notepadPP =
                new[] {
                @"C:\Program Files\Notepad++\notepad++.exe",
                @"C:\Program Files (x86)\Notepad++\notepad++.exe"
                }.FirstOrDefault(File.Exists);

            if (notepadPP != null)
            {
                Process.Start(notepadPP, $"-n{line} \"{this.file_}\"");
                return;
            }
        }

        private void Dgv_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Dgv_DragDrop(object sender, DragEventArgs e)
        {
            string file = ((string[])e.Data.GetData(DataFormats.FileDrop)).First();
            SetFile(file);
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                DialogResult result = ofd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.SetFile(ofd.FileName);
                }
            }
        }



        private void SetFile(string file)
        {
            this.file_ = file;
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
            var rowRules = rules?.RowRules ?? new RowRule[0];

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
                bool rowOk = true;
                bool anyInclude = false;
                bool matchesAnyInclude = false;
                foreach (var rowRule in rowRules)
                {
                    if ((rowRule.IncludeRegex ?? "").Length != 0)
                    {
                        anyInclude = true;
                        if (Regex.IsMatch(row, rowRule.IncludeRegex))
                        {
                            matchesAnyInclude = true;
                        }
                    }
                    if ((rowRule.ExcludeRegex ?? "").Length != 0)
                    {
                        if (Regex.IsMatch(row, rowRule.ExcludeRegex))
                        {
                            rowOk = false;
                            break;
                        }
                    }
                }
                if (anyInclude && !matchesAnyInclude)
                {
                    continue;
                }
                if (!rowOk)
                {
                    continue;
                }
                var dgvRow = dgv.Rows[dgv.Rows.Add()];
                dgvRow.Cells[0].Value = rowIndex;
                bool anyMatches = false;
                int columnIndex = 0;
                foreach (var newColumn in newColumns)
                {
                    columnIndex += 1;
                    foreach (var rule in newColumn)
                    {
                        string regex = rule.Regex;
                        if (string.IsNullOrEmpty(rule.Regex))
                        {
                            regex = ".*";
                        }
                        string field = row;
                        if (!string.IsNullOrEmpty(rule.FieldDelimiter))
                        {
                            string fieldIndex = rule.FieldIndex + "";
                            if (fieldIndex.Length == 0)
                            {
                                fieldIndex = "0";
                            }
                            field = row.Split(new[] { rule.FieldDelimiter }, StringSplitOptions.None).Skip(Convert.ToInt32(fieldIndex)).FirstOrDefault();
                        }

                        foreach (var match in Regex.Matches(field, rule.Regex).OfType<Match>())
                        {
                            anyMatches = true;
                            if (!string.IsNullOrWhiteSpace(dgvRow.Cells[columnIndex].Value + ""))
                            {
                                dgvRow.Cells[columnIndex].Value += rule.MatchesJoin ?? " ";
                            }
                            dgvRow.Cells[columnIndex].Value += match.Groups.Count > 1 ?
                                string.Join(rule.GroupsJoin ?? " ", match.Groups.OfType<Group>().Skip(1)) :
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

        private void buttonRules_Click(object sender, EventArgs e)
        {
            ShowRulesForm();
        }
    }
}
