using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsTeamCode
{
    /// <summary>
    /// The standard team abbreviation for a team, such as NOS for New Orleans Saints
    /// </summary>
    public string TeamCode { get; set; } = null!;

    public string SportCode { get; set; } = null!;

    /// <summary>
    /// The city which represents the particular team.
    /// </summary>
    public string City { get; set; } = null!;

    /// <summary>
    /// The mascot for the particular team, such as the &apos;Saints&apos;.
    /// </summary>
    public string Mascot { get; set; } = null!;

    /// <summary>
    /// The abbreviation associated with a team.
    /// </summary>
    public string Abbreviation { get; set; } = null!;

    public int? StatMapId { get; set; }

    public virtual ICollection<SheetsSportSeasonPlayerTeam> SheetsSportSeasonPlayerTeams { get; set; } = new List<SheetsSportSeasonPlayerTeam>();

    public virtual ICollection<SheetsSportTeamSeasonBye> SheetsSportTeamSeasonByes { get; set; } = new List<SheetsSportTeamSeasonBye>();
}
