using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsSeasonCode
{
    public string SeasonCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<SheetsCheatSheet> SheetsCheatSheetSeasonCodeNavigations { get; set; } = new List<SheetsCheatSheet>();

    public virtual ICollection<SheetsCheatSheet> SheetsCheatSheetStatsSeasonCodeNavigations { get; set; } = new List<SheetsCheatSheet>();

    public virtual ICollection<SheetsSportSeasonPlayerSeasonStat> SheetsSportSeasonPlayerSeasonStats { get; set; } = new List<SheetsSportSeasonPlayerSeasonStat>();

    public virtual ICollection<SheetsSportSeasonPlayerStatus> SheetsSportSeasonPlayerStatuses { get; set; } = new List<SheetsSportSeasonPlayerStatus>();

    public virtual ICollection<SheetsSportSeasonPlayerTeam> SheetsSportSeasonPlayerTeams { get; set; } = new List<SheetsSportSeasonPlayerTeam>();

    public virtual ICollection<SheetsSportSeasonPlayerWeeklyStat> SheetsSportSeasonPlayerWeeklyStats { get; set; } = new List<SheetsSportSeasonPlayerWeeklyStat>();

    public virtual ICollection<SheetsSportSeasonSuppPlayerReview> SheetsSportSeasonSuppPlayerReviews { get; set; } = new List<SheetsSportSeasonSuppPlayerReview>();

    public virtual ICollection<SheetsSportSeason> SheetsSportSeasons { get; set; } = new List<SheetsSportSeason>();

    public virtual ICollection<SheetsSportTeamSeasonBye> SheetsSportTeamSeasonByes { get; set; } = new List<SheetsSportTeamSeasonBye>();

    public virtual ICollection<SheetsSupplementalSheet> SheetsSupplementalSheets { get; set; } = new List<SheetsSupplementalSheet>();
}
