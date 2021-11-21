using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class SportSeasonPlayerSeasonStat
    {
        public string SportCode { get; set; }
        public string SeasonCode { get; set; }
        public int PlayerId { get; set; }
        public string StatCode { get; set; }
        public double? StatValue { get; set; }

        public virtual Player Player { get; set; }
        public virtual SeasonCode SeasonCodeNavigation { get; set; }
        public virtual SportCode SportCodeNavigation { get; set; }
        public virtual StatCode StatCodeNavigation { get; set; }
    }
}
