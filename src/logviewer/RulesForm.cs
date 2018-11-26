using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        static Dictionary<
            Expression<Func<ColumnRule, string>>,
            Func<RulesForm, DataGridViewColumn>>
                columnRuleToColumn =
                    new Dictionary<
                        Expression<Func<ColumnRule, string>>,
                        Func<RulesForm, DataGridViewColumn>>
        {
            {cr => cr.RuleName, rf => rf.columnRuleName },
            {cr => cr.ColumnName, rf => rf.columnColumnName },
            {cr => cr.Regex, rf => rf.columnRegex },
            {cr => cr.Color, rf => rf.columnColor },
            {cr => cr.FieldDelimiter, rf => rf.columnFieldDelimiter },
            {cr => cr.FieldIndex, rf => rf.columnFieldIndex },
        };

        static Dictionary<
            Expression<Func<RowRule, string>>,
            Func<RulesForm, DataGridViewColumn>>
                rowRuleToColumn =
                    new Dictionary<
                        Expression<Func<RowRule, string>>,
                        Func<RulesForm, DataGridViewColumn>>
        {
            {rr => rr.RuleName, rf => rf.rowRulesColumnRuleName },
            {rr => rr.IncludeRegex, rf => rf.rowRulesColumnIncludeRegex },
            {rr => rr.ExcludeRegex, rf => rf.rowRulesColumnExcludeRegex },
        };

        public Rules Rules { get; private set; }

        public RulesForm(Rules rules)
        {
            InitializeComponent();

            SetRules(rules);
            dgvColumnRules.AllowDrop = true;
            dgvColumnRules.DragEnter += Dgv_DragEnter;
            dgvColumnRules.DragDrop += Dgv_DragDrop;
            this.KeyDown += RulesForm_KeyDown;
            this.KeyPreview = true;
        }

        private void RulesForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && !e.Shift && !e.Alt && e.KeyCode == Keys.O)
            {
                AskForFile();
            }
        }

        private void Dgv_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Dgv_DragDrop(object sender, DragEventArgs e)
        {
            string file = ((string[])e.Data.GetData(DataFormats.FileDrop)).First();
            ImportRules(file);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Rules = GetRules();
            this.DialogResult = DialogResult.OK;
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            AskForFile();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            using (var ofd = new SaveFileDialog())
            {
                DialogResult result = ofd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string file = ofd.FileName;
                    try
                    {
                        var rules = Newtonsoft.Json.JsonConvert.SerializeObject(GetRules(),
                            Newtonsoft.Json.Formatting.Indented);
                        File.WriteAllText(file, rules);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex + "");
                    }
                }
            }
        }


        private void AskForFile()
        {
            using (var ofd = new OpenFileDialog())
            {
                DialogResult result = ofd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string file = ofd.FileName;
                    ImportRules(file);
                }
            }
        }

        private void SetRules(Rules rules)
        {
            SetRules(
                columnRuleToColumn,
                dgvColumnRules,
                rules?.ColumnRules);

            SetRules(
                rowRuleToColumn,
                dgvRowRules,
                rules?.RowRules);
        }

        void SetRules<TRule>(
            Dictionary<
                Expression<Func<TRule, string>>,
                Func<RulesForm, DataGridViewColumn>> dict,
            DataGridView dgv,
            TRule[] rules)
        {
            dgv.Rows.Clear();

            if (rules == null)
            {
                return;
            }

            foreach (TRule rule in rules)
            {
                var row = dgv.Rows[dgv.Rows.Add()];
                foreach (var kvp in dict)
                {
                    row.Cells[kvp.Value(this).Index].Value =
                        ((PropertyInfo)(((MemberExpression)kvp.Key.Body).Member)).GetValue(rule);
                    //                            kvp.Key.Compile().Invoke(rule);
                }
            }
        }

        TRule[] GetRules<TRule>(
            Dictionary<
                Expression<Func<TRule, string>>,
                Func<RulesForm, DataGridViewColumn>> dict,
            DataGridView dgv) where TRule : new()
        {
            var rules = new List<TRule>();

            foreach (var row in dgv.Rows.OfType<DataGridViewRow>())
            {
                var rule = new TRule();
                foreach (var kvp in dict)
                {
                    ((PropertyInfo)((MemberExpression)kvp.Key.Body).Member).SetValue(
                        rule,
                        row.Cells[kvp.Value(this).Index].Value + "");
                }

                rules.Add(rule);
            }

            return rules.ToArray();
        }

        private Rules GetRules()
        {
            var columnRuleNames = new HashSet<string>();
            var rowRuleNames = new HashSet<string>();
            var random = new Random();
            var columnRules = GetRules(columnRuleToColumn, this.dgvColumnRules)
                .Where(cr => cr.Regex.Length > 0 || cr.FieldDelimiter.Length > 0)
                .ToArray();
            var rowRules = GetRules(rowRuleToColumn, this.dgvRowRules)
                .Where(rr => rr.IncludeRegex.Length > 0 || rr.ExcludeRegex.Length > 0)
                .ToArray();

            foreach (var cr in columnRules)
            {
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
                    cr.RuleName = name;

                    while (!columnRuleNames.Add(cr.RuleName))
                    {
                        cr.RuleName += ((char)('a' + random.Next(26))) + "";
                    }
                }
            }

            foreach (var rr in rowRules)
            {
                if (rr.RuleName.Length == 0)
                {
                    do
                    {
                        rr.RuleName += ((char)('a' + random.Next(26))) + "";
                    }
                    while (!rowRuleNames.Add(rr.RuleName));
                }
            }

            return new Rules
            {
                ColumnRules = columnRules,
                RowRules = rowRules
            };
        }

        private void ImportRules(string file)
        {
            try
            {
                var rules = Newtonsoft.Json.JsonConvert.DeserializeObject<Rules>(
                    File.ReadAllText(file),
                    new Newtonsoft.Json.JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new CamelCaseNamingStrategy()
                        }
                    });
                this.SetRules(rules);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
            }
        }
    }
}
