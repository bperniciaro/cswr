using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsSportPositionStat
{
    public string SportCode { get; set; } = null!;

    public string PositionCode { get; set; } = null!;

    public string StatCode { get; set; } = null!;

    public byte Seqno { get; set; }

    public virtual SheetsPositionCode PositionCodeNavigation { get; set; } = null!;

    public virtual SheetsSportCode SportCodeNavigation { get; set; } = null!;

    public virtual SheetsStatCode StatCodeNavigation { get; set; } = null!;
}
