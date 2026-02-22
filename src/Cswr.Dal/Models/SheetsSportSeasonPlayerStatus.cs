using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsSportSeasonPlayerStatus
{
    public int PlayerStatusId { get; set; }

    public string SportCode { get; set; } = null!;

    public string SeasonCode { get; set; } = null!;

    public int PlayerId { get; set; }

    public string StatusCode { get; set; } = null!;

    public string? SuppInfo { get; set; }

    public int? Count { get; set; }

    public bool Approved { get; set; }

    public bool Archived { get; set; }

    public string CreatedByUsername { get; set; } = null!;

    public DateTime CreatedTimestamp { get; set; }

    public string? ModifiedByUsername { get; set; }

    public DateTime? ModifiedTimestamp { get; set; }

    public virtual SheetsPlayer Player { get; set; } = null!;

    public virtual SheetsSeasonCode SeasonCodeNavigation { get; set; } = null!;

    public virtual SheetsSportCode SportCodeNavigation { get; set; } = null!;

    public virtual SheetsPlayerStatusCode StatusCodeNavigation { get; set; } = null!;
}
