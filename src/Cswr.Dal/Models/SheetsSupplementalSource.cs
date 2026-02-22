using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsSupplementalSource
{
    public int SupplementalSourceId { get; set; }

    public string Abbreviation { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public virtual ICollection<SheetsSportSeasonSuppPlayerReview> SheetsSportSeasonSuppPlayerReviews { get; set; } = new List<SheetsSportSeasonSuppPlayerReview>();

    public virtual ICollection<SheetsSupplementalSheet> SheetsSupplementalSheets { get; set; } = new List<SheetsSupplementalSheet>();
}
