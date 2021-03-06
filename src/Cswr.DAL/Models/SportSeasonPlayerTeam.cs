using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class SportSeasonPlayerTeam
    {
        public string SportCode { get; set; }
        public string SeasonCode { get; set; }
        public int PlayerId { get; set; }
        public string TeamCode { get; set; }

        public virtual Player Player { get; set; }
        public virtual SeasonCode SeasonCodeNavigation { get; set; }
        public virtual SportCode SportCodeNavigation { get; set; }
        public virtual TeamCode TeamCodeNavigation { get; set; }
    }
}
