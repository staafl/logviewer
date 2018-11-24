using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace logviewer
{
    public partial class RulesForm : Form
    {
        public Rules Rules { get; private set; }

        public RulesForm(Rules rules)
        {
            InitializeComponent();
            if (rules?.ColumnRules != null)
            {
                foreach (var rule in rules.ColumnRules)
                {
                    var row = dgvColumnRules.Rows[dgvColumnRules.Rows.Add()];
                    row.Cells[0].Value = rule.RuleName;
                    row.Cells[1].Value = rule.ColumnName;
                    row.Cells[2].Value = rule.Regex;
                }
            }
        }

        private void dgvColumnRules_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            var columnRules = new List<ColumnRule>();
            foreach (var row in dgvColumnRules.Rows.OfType<DataGridViewRow>())
            {
                var cr = new ColumnRule();
                cr.Regex = row.Cells[2].Value + "";
                if (cr.Regex.Length == 0)
                {
                    continue;
                }
                cr.RuleName = row.Cells[0].Value + "";
                cr.ColumnName = row.Cells[1].Value + "";
                if (cr.ColumnName.Length == 0)
                {
                    cr.ColumnName = cr.Regex;
                }
                if (cr.RuleName.Length == 0)
                {
                    cr.RuleName = cr.Regex;
                }
                columnRules.Add(cr);
            }
            this.Rules = new Rules { ColumnRules = columnRules.ToArray() };
            this.DialogResult = DialogResult.OK;
        }
    }
}
