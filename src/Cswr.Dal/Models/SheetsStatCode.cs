using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsStatCode
{
    /// <summary>
    /// A code representing some statistic.
    /// </summary>
    public string StatCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Abbreviation { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<SheetsSportPositionStat> SheetsSportPositionStats { get; set; } = new List<SheetsSportPositionStat>();

    public virtual ICollection<SheetsSportSeasonPlayerSeasonStat> SheetsSportSeasonPlayerSeasonStats { get; set; } = new List<SheetsSportSeasonPlayerSeasonStat>();

    public virtual ICollection<SheetsSportSeasonPlayerWeeklyStat> SheetsSportSeasonPlayerWeeklyStats { get; set; } = new List<SheetsSportSeasonPlayerWeeklyStat>();

    public virtual ICollection<SheetsCheatSheet> CheatSheets { get; set; } = new List<SheetsCheatSheet>();
}
