using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsCheatSheetPosition
{
    public int CheatSheetId { get; set; }

    public string PositionCode { get; set; } = null!;

    public virtual SheetsCheatSheet CheatSheet { get; set; } = null!;
}
