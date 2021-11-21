using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class SportTeamPlayer
    {
        public string SeasonCode { get; set; }
        public string SportCode { get; set; }
        public string TeamCode { get; set; }
        public int PlayerId { get; set; }
    }
}
