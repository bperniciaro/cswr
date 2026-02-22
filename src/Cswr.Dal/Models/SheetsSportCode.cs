using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsSportCode
{
    /// <summary>
    /// Code describing the different leagues which are available.
    /// </summary>
    public string SportCode { get; set; } = null!;

    public string? SportName { get; set; }

    /// <summary>
    /// The name of the particular league.
    /// </summary>
    public string LeagueName { get; set; } = null!;

    /// <summary>
    /// An abbreviation associated with the respective league.
    /// </summary>
    public string LeagueAbbreviation { get; set; } = null!;

    public virtual ICollection<SheetsCheatSheet> SheetsCheatSheets { get; set; } = new List<SheetsCheatSheet>();

    public virtual ICollection<SheetsPlayer> SheetsPlayers { get; set; } = new List<SheetsPlayer>();

    public virtual ICollection<SheetsSportPositionStat> SheetsSportPositionStats { get; set; } = new List<SheetsSportPositionStat>();

    public virtual ICollection<SheetsSportPosition> SheetsSportPositions { get; set; } = new List<SheetsSportPosition>();

    public virtual ICollection<SheetsSportSeasonPlayerSeasonStat> SheetsSportSeasonPlayerSeasonStats { get; set; } = new List<SheetsSportSeasonPlayerSeasonStat>();

    public virtual ICollection<SheetsSportSeasonPlayerStatus> SheetsSportSeasonPlayerStatuses { get; set; } = new List<SheetsSportSeasonPlayerStatus>();

    public virtual ICollection<SheetsSportSeasonPlayerTeam> SheetsSportSeasonPlayerTeams { get; set; } = new List<SheetsSportSeasonPlayerTeam>();

    public virtual ICollection<SheetsSportSeasonPlayerWeeklyStat> SheetsSportSeasonPlayerWeeklyStats { get; set; } = new List<SheetsSportSeasonPlayerWeeklyStat>();

    public virtual ICollection<SheetsSportSeasonSuppPlayerReview> SheetsSportSeasonSuppPlayerReviews { get; set; } = new List<SheetsSportSeasonSuppPlayerReview>();

    public virtual ICollection<SheetsSportSeason> SheetsSportSeasons { get; set; } = new List<SheetsSportSeason>();

    public virtual ICollection<SheetsSportSetting> SheetsSportSettings { get; set; } = new List<SheetsSportSetting>();

    public virtual ICollection<SheetsSportTeamSeasonBye> SheetsSportTeamSeasonByes { get; set; } = new List<SheetsSportTeamSeasonBye>();

    public virtual ICollection<SheetsSupplementalSheet> SheetsSupplementalSheets { get; set; } = new List<SheetsSupplementalSheet>();

    public virtual ICollection<SheetsPlayerStatusCode> StatusCodes { get; set; } = new List<SheetsPlayerStatusCode>();
}
