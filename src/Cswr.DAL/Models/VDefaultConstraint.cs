using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class VDefaultConstraint
    {
        public string TableName { get; set; }
        public string ConstraintName { get; set; }
        public string ColumnName { get; set; }
        public string DefaultClause { get; set; }
    }
}
