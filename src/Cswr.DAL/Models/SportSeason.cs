using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class SportSeason
    {
        public string SportCode { get; set; }
        public string SeasonCode { get; set; }
        public bool CurrentSeason { get; set; }
        public bool SeasonStarted { get; set; }
        public bool? SeasonEnded { get; set; }
        public bool? SomeStatsLoaded { get; set; }

        public virtual SeasonCode SeasonCodeNavigation { get; set; }
        public virtual SportCode SportCodeNavigation { get; set; }
    }
}
