using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsSportSeasonSuppPlayerReview
{
    public string SportCode { get; set; } = null!;

    public string SeasonCode { get; set; } = null!;

    public int SupplementalSourceId { get; set; }

    public int PlayerId { get; set; }

    public string ReviewUrl { get; set; } = null!;

    public virtual SheetsPlayer Player { get; set; } = null!;

    public virtual SheetsSeasonCode SeasonCodeNavigation { get; set; } = null!;

    public virtual SheetsSportCode SportCodeNavigation { get; set; } = null!;

    public virtual SheetsSupplementalSource SupplementalSource { get; set; } = null!;
}
