using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsPositionCode
{
    /// <summary>
    /// The code representing a position of some sport.
    /// </summary>
    public string PositionCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    /// <summary>
    /// The standare abbreviation associated with some position.
    /// </summary>
    public string Abbreviation { get; set; } = null!;

    public virtual ICollection<SheetsSportPositionStat> SheetsSportPositionStats { get; set; } = new List<SheetsSportPositionStat>();

    public virtual ICollection<SheetsSportPosition> SheetsSportPositions { get; set; } = new List<SheetsSportPosition>();

    public virtual ICollection<SheetsSupplementalSheet> SheetsSupplementalSheets { get; set; } = new List<SheetsSupplementalSheet>();
}
