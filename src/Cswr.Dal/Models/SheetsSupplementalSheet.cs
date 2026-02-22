using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsSupplementalSheet
{
    public int SupplementalSheetId { get; set; }

    public string SeasonCode { get; set; } = null!;

    public int SupplementalSourceId { get; set; }

    public string SportCode { get; set; } = null!;

    public string PositionCode { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public string? Url { get; set; }

    public virtual SheetsPositionCode PositionCodeNavigation { get; set; } = null!;

    public virtual SheetsSeasonCode SeasonCodeNavigation { get; set; } = null!;

    public virtual ICollection<SheetsSupplementalSheetItem> SheetsSupplementalSheetItems { get; set; } = new List<SheetsSupplementalSheetItem>();

    public virtual SheetsSportCode SportCodeNavigation { get; set; } = null!;

    public virtual SheetsSupplementalSource SupplementalSource { get; set; } = null!;
}
