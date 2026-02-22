using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsCheatSheet
{
    public int CheatSheetId { get; set; }

    public string Username { get; set; } = null!;

    public string SeasonCode { get; set; } = null!;

    public string SportCode { get; set; } = null!;

    public string SheetName { get; set; } = null!;

    public string StatsSeasonCode { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime LastUpdated { get; set; }

    public bool? Pprleague { get; set; }

    public bool? AuctionDraft { get; set; }

    public virtual SheetsSeasonCode SeasonCodeNavigation { get; set; } = null!;

    public virtual ICollection<SheetsCheatSheetItem> SheetsCheatSheetItems { get; set; } = new List<SheetsCheatSheetItem>();

    public virtual ICollection<SheetsCheatSheetPosition> SheetsCheatSheetPositions { get; set; } = new List<SheetsCheatSheetPosition>();

    public virtual SheetsSportCode SportCodeNavigation { get; set; } = null!;

    public virtual SheetsSeasonCode StatsSeasonCodeNavigation { get; set; } = null!;

    public virtual ICollection<SheetsStatCode> StatCodes { get; set; } = new List<SheetsStatCode>();
}
