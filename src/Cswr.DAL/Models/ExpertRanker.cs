using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class ExpertRanker
    {
        public int ExpertRankerId { get; set; }
        public string Name { get; set; }
        public string WebsiteName { get; set; }
        public string WebsiteUrl { get; set; }
    }
}
