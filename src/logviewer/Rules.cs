using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logviewer
{
    public class Rules
    {
        public ColumnRule[] ColumnRules { get; set; }
        public RowRule[] RowRules { get; set; }
    }

    public class ColumnRule
    {
        public string ColumnName { get; internal set; }
        public string Regex { get; internal set; }
        public string RegexJoin { get; internal set; }
        public string RuleName { get; internal set; }
    }

    public class RowRule
    {
    }
}
