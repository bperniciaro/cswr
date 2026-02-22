using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsSportSeason
{
    public string SportCode { get; set; } = null!;

    public string SeasonCode { get; set; } = null!;

    public bool CurrentSeason { get; set; }

    public bool SeasonStarted { get; set; }

    public bool? SeasonEnded { get; set; }

    public bool? SomeStatsLoaded { get; set; }

    public virtual SheetsSeasonCode SeasonCodeNavigation { get; set; } = null!;

    public virtual SheetsSportCode SportCodeNavigation { get; set; } = null!;
}
