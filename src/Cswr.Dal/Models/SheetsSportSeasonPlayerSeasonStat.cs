using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsSportSeasonPlayerSeasonStat
{
    public string SportCode { get; set; } = null!;

    public string SeasonCode { get; set; } = null!;

    public int PlayerId { get; set; }

    public string StatCode { get; set; } = null!;

    public double? StatValue { get; set; }

    public virtual SheetsPlayer Player { get; set; } = null!;

    public virtual SheetsSeasonCode SeasonCodeNavigation { get; set; } = null!;

    public virtual SheetsSportCode SportCodeNavigation { get; set; } = null!;

    public virtual SheetsStatCode StatCodeNavigation { get; set; } = null!;
}
