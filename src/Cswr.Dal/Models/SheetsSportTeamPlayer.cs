using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsSportTeamPlayer
{
    public string SeasonCode { get; set; } = null!;

    public string SportCode { get; set; } = null!;

    public string TeamCode { get; set; } = null!;

    public int PlayerId { get; set; }
}
