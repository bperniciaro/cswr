using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class AdpplayerLog
    {
        public int AdpplayerLogId { get; set; }
        public string SportCode { get; set; }
        public string SeasonCode { get; set; }
        public int PlayerId { get; set; }
        public double Adp { get; set; }
        public DateTime CalcTimestamp { get; set; }
        public int AdpcalculationId { get; set; }

        public virtual Adpcalculation Adpcalculation { get; set; }
        public virtual Player Player { get; set; }
        public virtual SeasonCode SeasonCodeNavigation { get; set; }
        public virtual SportCode SportCodeNavigation { get; set; }
    }
}
