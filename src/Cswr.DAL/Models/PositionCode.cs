using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class PositionCode
    {
        public PositionCode()
        {
            SportPositionStats = new HashSet<SportPositionStat>();
            SportPositions = new HashSet<SportPosition>();
            SupplementalSheets = new HashSet<SupplementalSheet>();
        }

        public string PositionCode1 { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public virtual ICollection<SportPositionStat> SportPositionStats { get; set; }
        public virtual ICollection<SportPosition> SportPositions { get; set; }
        public virtual ICollection<SupplementalSheet> SupplementalSheets { get; set; }
    }
}
