using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class StatCode
    {
        public StatCode()
        {
            CheatSheetStats = new HashSet<CheatSheetStat>();
            SportPositionStats = new HashSet<SportPositionStat>();
            SportSeasonPlayerSeasonStats = new HashSet<SportSeasonPlayerSeasonStat>();
            SportSeasonPlayerWeeklyStats = new HashSet<SportSeasonPlayerWeeklyStat>();
        }

        public string StatCode1 { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CheatSheetStat> CheatSheetStats { get; set; }
        public virtual ICollection<SportPositionStat> SportPositionStats { get; set; }
        public virtual ICollection<SportSeasonPlayerSeasonStat> SportSeasonPlayerSeasonStats { get; set; }
        public virtual ICollection<SportSeasonPlayerWeeklyStat> SportSeasonPlayerWeeklyStats { get; set; }
    }
}
