using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsSupplementalSheetItem
{
    public int SupplementalSheetId { get; set; }

    public int PlayerId { get; set; }

    public int Seqno { get; set; }

    public bool? SleeperTag { get; set; }

    public bool? BustTag { get; set; }

    public string Note { get; set; } = null!;

    public virtual SheetsPlayer Player { get; set; } = null!;

    public virtual SheetsSupplementalSheet SupplementalSheet { get; set; } = null!;
}
