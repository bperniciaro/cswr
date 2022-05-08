using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class SportSeasonSuppPlayerReview
    {
        public string SportCode { get; set; }
        public string SeasonCode { get; set; }
        public int SupplementalSourceId { get; set; }
        public int PlayerId { get; set; }
        public string ReviewUrl { get; set; }

        public virtual Player Player { get; set; }
        public virtual SeasonCode SeasonCodeNavigation { get; set; }
        public virtual SportCode SportCodeNavigation { get; set; }
        public virtual SupplementalSource SupplementalSource { get; set; }
    }
}
