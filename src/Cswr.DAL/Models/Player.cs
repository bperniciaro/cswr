using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class Player
    {
        public Player()
        {
            AdpplayerLogs = new HashSet<AdpplayerLog>();
            CheatSheetItems = new HashSet<CheatSheetItem>();
            SportSeasonPlayerSeasonStats = new HashSet<SportSeasonPlayerSeasonStat>();
            SportSeasonPlayerStatuses = new HashSet<SportSeasonPlayerStatus>();
            SportSeasonPlayerTeams = new HashSet<SportSeasonPlayerTeam>();
            SportSeasonPlayerWeeklyStats = new HashSet<SportSeasonPlayerWeeklyStat>();
            SportSeasonSuppPlayerReviews = new HashSet<SportSeasonSuppPlayerReview>();
            SupplementalSheetItems = new HashSet<SupplementalSheetItem>();
        }

        public int PlayerId { get; set; }
        public string SportCode { get; set; }
        public string PositionCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string TeamCode { get; set; }
        public short Number { get; set; }
        public DateTime FirstYear { get; set; }
        public int? StatMapId { get; set; }
        public bool Retired { get; set; }
        public DateTime? BirthDate { get; set; }
        public string TwitterUsername { get; set; }

        public virtual SportCode SportCodeNavigation { get; set; }
        public virtual ICollection<AdpplayerLog> AdpplayerLogs { get; set; }
        public virtual ICollection<CheatSheetItem> CheatSheetItems { get; set; }
        public virtual ICollection<SportSeasonPlayerSeasonStat> SportSeasonPlayerSeasonStats { get; set; }
        public virtual ICollection<SportSeasonPlayerStatus> SportSeasonPlayerStatuses { get; set; }
        public virtual ICollection<SportSeasonPlayerTeam> SportSeasonPlayerTeams { get; set; }
        public virtual ICollection<SportSeasonPlayerWeeklyStat> SportSeasonPlayerWeeklyStats { get; set; }
        public virtual ICollection<SportSeasonSuppPlayerReview> SportSeasonSuppPlayerReviews { get; set; }
        public virtual ICollection<SupplementalSheetItem> SupplementalSheetItems { get; set; }
    }
}
