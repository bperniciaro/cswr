using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class TeamCode
    {
        public TeamCode()
        {
            SportSeasonPlayerTeams = new HashSet<SportSeasonPlayerTeam>();
            SportTeamSeasonByes = new HashSet<SportTeamSeasonBye>();
        }

        public string TeamCode1 { get; set; }
        public string SportCode { get; set; }
        public string City { get; set; }
        public string Mascot { get; set; }
        public string Abbreviation { get; set; }
        public int? StatMapId { get; set; }

        public virtual ICollection<SportSeasonPlayerTeam> SportSeasonPlayerTeams { get; set; }
        public virtual ICollection<SportTeamSeasonBye> SportTeamSeasonByes { get; set; }
    }
}
