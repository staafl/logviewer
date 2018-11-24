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
        public string ColumnName { get; set; }
        public string Regex { get; set; }
        public string GroupsJoin { get; set; }
        public string MatchesJoin { get; set; }
        public string FieldIndex { get; set; }
        public string FieldDelimiter { get; set; }
        public string RuleName { get; set; }
        public string Color { get; set; }
    }

    public class RowRule
    {
        public string RuleName { get; set; }
        public string IncludeRegex { get; set; }
        public string ExcludeRegex { get; set; }
    }
}
