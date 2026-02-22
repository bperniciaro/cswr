using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsPlayerStatusCode
{
    public string StatusCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool SuppInfoRequired { get; set; }

    public string? SuppInfoLabel { get; set; }

    public string? SuppInfoExample { get; set; }

    public string? SuppInfoInstructions { get; set; }

    public bool CountRequired { get; set; }

    public string? CountLabel { get; set; }

    public int? CountExample { get; set; }

    public string? CountInstructions { get; set; }

    public int Seqno { get; set; }

    public int Priority { get; set; }

    public bool Dynamic { get; set; }

    public virtual ICollection<SheetsSportSeasonPlayerStatus> SheetsSportSeasonPlayerStatuses { get; set; } = new List<SheetsSportSeasonPlayerStatus>();

    public virtual ICollection<SheetsSportCode> SportCodes { get; set; } = new List<SheetsSportCode>();
}
