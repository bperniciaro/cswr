using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class Adpcalculation
    {
        public Adpcalculation()
        {
            AdpplayerLogs = new HashSet<AdpplayerLog>();
        }

        public int AdpcalculationId { get; set; }
        public string SeasonCode { get; set; }
        public string SportCode { get; set; }
        public string PositionCode { get; set; }
        public DateTime CalcTimestamp { get; set; }
        public int? TotalSheetsConsidered { get; set; }
        public int? Last24Sheets { get; set; }
        public int? TimespanInDays { get; set; }
        public int? Last48Sheets { get; set; }
        public int? Last72Sheets { get; set; }

        public virtual ICollection<AdpplayerLog> AdpplayerLogs { get; set; }
    }
}
