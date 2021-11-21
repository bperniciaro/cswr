using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class CheatSheet
    {
        public CheatSheet()
        {
            CheatSheetItems = new HashSet<CheatSheetItem>();
            CheatSheetPositions = new HashSet<CheatSheetPosition>();
            CheatSheetStats = new HashSet<CheatSheetStat>();
        }

        public int CheatSheetId { get; set; }
        public string Username { get; set; }
        public string SeasonCode { get; set; }
        public string SportCode { get; set; }
        public string SheetName { get; set; }
        public string StatsSeasonCode { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool? Pprleague { get; set; }
        public bool? AuctionDraft { get; set; }

        public virtual SeasonCode SeasonCodeNavigation { get; set; }
        public virtual SportCode SportCodeNavigation { get; set; }
        public virtual SeasonCode StatsSeasonCodeNavigation { get; set; }
        public virtual ICollection<CheatSheetItem> CheatSheetItems { get; set; }
        public virtual ICollection<CheatSheetPosition> CheatSheetPositions { get; set; }
        public virtual ICollection<CheatSheetStat> CheatSheetStats { get; set; }
    }
}
