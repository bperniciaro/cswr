using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsCheatSheetItem
{
    public int CheatSheetId { get; set; }

    /// <summary>
    /// The ID of the respective player, or NULL if the item is a tier-template
    /// </summary>
    public int PlayerId { get; set; }

    public short Seqno { get; set; }

    public bool? SleeperTag { get; set; }

    public bool? BustTag { get; set; }

    public bool? InjuredTag { get; set; }

    public string Note { get; set; } = null!;

    public virtual SheetsCheatSheet CheatSheet { get; set; } = null!;

    public virtual SheetsPlayer Player { get; set; } = null!;
}
