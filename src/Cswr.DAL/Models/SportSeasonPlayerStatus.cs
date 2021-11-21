using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class SportSeasonPlayerStatus
    {
        public int PlayerStatusId { get; set; }
        public string SportCode { get; set; }
        public string SeasonCode { get; set; }
        public int PlayerId { get; set; }
        public string StatusCode { get; set; }
        public string SuppInfo { get; set; }
        public int? Count { get; set; }
        public bool Approved { get; set; }
        public bool Archived { get; set; }
        public string CreatedByUsername { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public string ModifiedByUsername { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }

        public virtual Player Player { get; set; }
        public virtual SeasonCode SeasonCodeNavigation { get; set; }
        public virtual SportCode SportCodeNavigation { get; set; }
        public virtual PlayerStatusCode StatusCodeNavigation { get; set; }
    }
}
