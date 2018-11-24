using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace logviewer
{
    public partial class RulesForm : Form
    {
        static Dictionary<Expression<Func<ColumnRule, object>>,
Func<RulesForm, DataGridViewColumn>> ruleToColumn = new Dictionary<Expression<Func<ColumnRule, object>>, Func<RulesForm, DataGridViewColumn>>
{
    {cr => cr.RuleName, rf => rf.columnRuleName },
    {cr => cr.ColumnName, rf => rf.columnColumnName },
    {cr => cr.Regex, rf => rf.columnRegex },
    {cr => cr.Color, rf => rf.columnColor },
    {cr => cr.FieldDelimiter, rf => rf.columnFieldDelimiter },
    {cr => cr.FieldIndex, rf => rf.columnFieldIndex },
};
        public Rules Rules { get; private set; }

        public RulesForm(Rules rules)
        {
            InitializeComponent();
            if (rules?.ColumnRules != null)
            {
                foreach (var rule in rules.ColumnRules)
                {
                    var row = dgvColumnRules.Rows[dgvColumnRules.Rows.Add()];
                    foreach (var kvp in ruleToColumn)
                    {
                        row.Cells[kvp.Value(this).Index].Value =
                            ((PropertyInfo)(((MemberExpression)kvp.Key.Body).Member)).GetValue(rule);
                        //                            kvp.Key.Compile().Invoke(rule);
                    }
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
            var ruleNames = new HashSet<string>();
            var random = new Random();
            foreach (var row in dgvColumnRules.Rows.OfType<DataGridViewRow>())
            {
                var cr = new ColumnRule();
                foreach (var kvp in ruleToColumn)
                {
                    ((PropertyInfo)((MemberExpression)kvp.Key.Body).Member).SetValue(
                        cr,
                        row.Cells[kvp.Value(this).Index].Value + "");
                }

                if (cr.Regex.Length == 0 && cr.FieldDelimiter.Length == 0)
                {
                    continue;
                }

                string name = cr.RuleName;
                if (name.Length == 0)
                {
                    if (cr.Regex.Length > 0)
                    {
                        name = cr.Regex;
                    }
                    else
                    {
                        name = "Field " + cr.FieldIndex;
                    }
                }
                if (cr.ColumnName.Length == 0)
                {
                    cr.ColumnName = name;
                }
                if (cr.RuleName.Length == 0)
                {
                    cr.RuleName = name + "_" + row.Index;
                    while (!ruleNames.Add(cr.RuleName))
                    {
                        cr.RuleName += ((char)('a' + random.Next(26))) + "";
                    }
                }

                columnRules.Add(cr);
            }
            this.Rules = new Rules { ColumnRules = columnRules.ToArray() };
            this.DialogResult = DialogResult.OK;
        }
    }
}
