using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class SeasonCode
    {
        public SeasonCode()
        {
            AdpplayerLogs = new HashSet<AdpplayerLog>();
            CheatSheetSeasonCodeNavigations = new HashSet<CheatSheet>();
            CheatSheetStatsSeasonCodeNavigations = new HashSet<CheatSheet>();
            SportSeasonPlayerSeasonStats = new HashSet<SportSeasonPlayerSeasonStat>();
            SportSeasonPlayerStatuses = new HashSet<SportSeasonPlayerStatus>();
            SportSeasonPlayerTeams = new HashSet<SportSeasonPlayerTeam>();
            SportSeasonPlayerWeeklyStats = new HashSet<SportSeasonPlayerWeeklyStat>();
            SportSeasonSuppPlayerReviews = new HashSet<SportSeasonSuppPlayerReview>();
            SportSeasons = new HashSet<SportSeason>();
            SportTeamSeasonByes = new HashSet<SportTeamSeasonBye>();
            SupplementalSheets = new HashSet<SupplementalSheet>();
        }

        public string SeasonCode1 { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AdpplayerLog> AdpplayerLogs { get; set; }
        public virtual ICollection<CheatSheet> CheatSheetSeasonCodeNavigations { get; set; }
        public virtual ICollection<CheatSheet> CheatSheetStatsSeasonCodeNavigations { get; set; }
        public virtual ICollection<SportSeasonPlayerSeasonStat> SportSeasonPlayerSeasonStats { get; set; }
        public virtual ICollection<SportSeasonPlayerStatus> SportSeasonPlayerStatuses { get; set; }
        public virtual ICollection<SportSeasonPlayerTeam> SportSeasonPlayerTeams { get; set; }
        public virtual ICollection<SportSeasonPlayerWeeklyStat> SportSeasonPlayerWeeklyStats { get; set; }
        public virtual ICollection<SportSeasonSuppPlayerReview> SportSeasonSuppPlayerReviews { get; set; }
        public virtual ICollection<SportSeason> SportSeasons { get; set; }
        public virtual ICollection<SportTeamSeasonBye> SportTeamSeasonByes { get; set; }
        public virtual ICollection<SupplementalSheet> SupplementalSheets { get; set; }
    }
}
