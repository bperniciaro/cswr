using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsPlayer
{
    public int PlayerId { get; set; }

    public string SportCode { get; set; } = null!;

    public string PositionCode { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string TeamCode { get; set; } = null!;

    public short Number { get; set; }

    public DateTime FirstYear { get; set; }

    public int? StatMapId { get; set; }

    public bool Retired { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? TwitterUsername { get; set; }

    public virtual ICollection<SheetsCheatSheetItem> SheetsCheatSheetItems { get; set; } = new List<SheetsCheatSheetItem>();

    public virtual ICollection<SheetsSportSeasonPlayerSeasonStat> SheetsSportSeasonPlayerSeasonStats { get; set; } = new List<SheetsSportSeasonPlayerSeasonStat>();

    public virtual ICollection<SheetsSportSeasonPlayerStatus> SheetsSportSeasonPlayerStatuses { get; set; } = new List<SheetsSportSeasonPlayerStatus>();

    public virtual ICollection<SheetsSportSeasonPlayerTeam> SheetsSportSeasonPlayerTeams { get; set; } = new List<SheetsSportSeasonPlayerTeam>();

    public virtual ICollection<SheetsSportSeasonPlayerWeeklyStat> SheetsSportSeasonPlayerWeeklyStats { get; set; } = new List<SheetsSportSeasonPlayerWeeklyStat>();

    public virtual ICollection<SheetsSportSeasonSuppPlayerReview> SheetsSportSeasonSuppPlayerReviews { get; set; } = new List<SheetsSportSeasonSuppPlayerReview>();

    public virtual ICollection<SheetsSupplementalSheetItem> SheetsSupplementalSheetItems { get; set; } = new List<SheetsSupplementalSheetItem>();

    public virtual SheetsSportCode SportCodeNavigation { get; set; } = null!;
}
