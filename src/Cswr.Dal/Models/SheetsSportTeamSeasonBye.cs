using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsSportTeamSeasonBye
{
    public string SportCode { get; set; } = null!;

    public string SeasonCode { get; set; } = null!;

    public string TeamCode { get; set; } = null!;

    public int? ByeWeek { get; set; }

    public virtual SheetsSeasonCode SeasonCodeNavigation { get; set; } = null!;

    public virtual SheetsSportCode SportCodeNavigation { get; set; } = null!;

    public virtual SheetsTeamCode TeamCodeNavigation { get; set; } = null!;
}
