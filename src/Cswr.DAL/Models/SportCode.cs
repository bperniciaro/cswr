using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class SportCode
    {
        public SportCode()
        {
            AdpplayerLogs = new HashSet<AdpplayerLog>();
            CheatSheets = new HashSet<CheatSheet>();
            Players = new HashSet<Player>();
            SportPositionStats = new HashSet<SportPositionStat>();
            SportPositions = new HashSet<SportPosition>();
            SportSeasonPlayerSeasonStats = new HashSet<SportSeasonPlayerSeasonStat>();
            SportSeasonPlayerStatuses = new HashSet<SportSeasonPlayerStatus>();
            SportSeasonPlayerTeams = new HashSet<SportSeasonPlayerTeam>();
            SportSeasonPlayerWeeklyStats = new HashSet<SportSeasonPlayerWeeklyStat>();
            SportSeasonSuppPlayerReviews = new HashSet<SportSeasonSuppPlayerReview>();
            SportSeasons = new HashSet<SportSeason>();
            SportSettings = new HashSet<SportSetting>();
            SportStatusCodes = new HashSet<SportStatusCode>();
            SportTeamSeasonByes = new HashSet<SportTeamSeasonBye>();
            SupplementalSheets = new HashSet<SupplementalSheet>();
        }

        public string SportCode1 { get; set; }
        public string SportName { get; set; }
        public string LeagueName { get; set; }
        public string LeagueAbbreviation { get; set; }

        public virtual ICollection<AdpplayerLog> AdpplayerLogs { get; set; }
        public virtual ICollection<CheatSheet> CheatSheets { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<SportPositionStat> SportPositionStats { get; set; }
        public virtual ICollection<SportPosition> SportPositions { get; set; }
        public virtual ICollection<SportSeasonPlayerSeasonStat> SportSeasonPlayerSeasonStats { get; set; }
        public virtual ICollection<SportSeasonPlayerStatus> SportSeasonPlayerStatuses { get; set; }
        public virtual ICollection<SportSeasonPlayerTeam> SportSeasonPlayerTeams { get; set; }
        public virtual ICollection<SportSeasonPlayerWeeklyStat> SportSeasonPlayerWeeklyStats { get; set; }
        public virtual ICollection<SportSeasonSuppPlayerReview> SportSeasonSuppPlayerReviews { get; set; }
        public virtual ICollection<SportSeason> SportSeasons { get; set; }
        public virtual ICollection<SportSetting> SportSettings { get; set; }
        public virtual ICollection<SportStatusCode> SportStatusCodes { get; set; }
        public virtual ICollection<SportTeamSeasonBye> SportTeamSeasonByes { get; set; }
        public virtual ICollection<SupplementalSheet> SupplementalSheets { get; set; }
    }
}
