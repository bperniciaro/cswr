using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class SupplementalSource
    {
        public SupplementalSource()
        {
            SportSeasonSuppPlayerReviews = new HashSet<SportSeasonSuppPlayerReview>();
            SupplementalSheets = new HashSet<SupplementalSheet>();
        }

        public int SupplementalSourceId { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<SportSeasonSuppPlayerReview> SportSeasonSuppPlayerReviews { get; set; }
        public virtual ICollection<SupplementalSheet> SupplementalSheets { get; set; }
    }
}
