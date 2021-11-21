using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class PlayerStatusCode
    {
        public PlayerStatusCode()
        {
            SportSeasonPlayerStatuses = new HashSet<SportSeasonPlayerStatus>();
            SportStatusCodes = new HashSet<SportStatusCode>();
        }

        public string StatusCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool SuppInfoRequired { get; set; }
        public string SuppInfoLabel { get; set; }
        public string SuppInfoExample { get; set; }
        public string SuppInfoInstructions { get; set; }
        public bool CountRequired { get; set; }
        public string CountLabel { get; set; }
        public int? CountExample { get; set; }
        public string CountInstructions { get; set; }
        public int Seqno { get; set; }
        public int Priority { get; set; }
        public bool Dynamic { get; set; }

        public virtual ICollection<SportSeasonPlayerStatus> SportSeasonPlayerStatuses { get; set; }
        public virtual ICollection<SportStatusCode> SportStatusCodes { get; set; }
    }
}
