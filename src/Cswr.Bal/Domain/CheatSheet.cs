namespace Cswr.Bal.Domain;

/// <summary>
/// Represents a specifi cheat sheet.
/// </summary>
public class CheatSheet
{
    private string userName = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="CheatSheet"/> class.
    /// </summary>
    /// <param name="cheatSheetID">The unique id of the cheat sheet.</param>
    /// <param name="username">The username of the user that created the cheat sheet.</param>
    /// <param name="seasonCode">The season on which the cheat sheet is based.</param>
    /// <param name="sportCode">The sport on which the cheat sheet is based.</param>
    /// <param name="sheetName">The name given to the cheat sheet by the user.</param>
    /// <param name="statsSeasonCode">The season code for which stats should be shown in a sheet.</param>
    /// <param name="created">The date and time that the cheat sheet was created.</param>
    /// <param name="lastUpdated">The date and time that the cheat sheet was last updated.</param>
    /// <param name="pprLeague">Indicates if the sheet is based on a PPR league.</param>
    /// <param name="auctionLeague">Indicates if the sheet is based on an auction league.</param>
    //public CheatSheet(int cheatSheetID, string username, string seasonCode, string sportCode, string sheetName, string statsSeasonCode, DateTime created, DateTime lastUpdated, bool? pprLeague, bool? auctionLeague)
    //{
    //    this.CheatSheetID = cheatSheetID;
    //    this.Username = username;
    //    this.SeasonCode = seasonCode;
    //    this.SportCode = sportCode;
    //    this.SheetName = sheetName;
    //    this.StatsSeasonCode = statsSeasonCode;
    //    this.Created = created;
    //    this.LastUpdated = lastUpdated;
    //    this.PprLeague = pprLeague;
    //    this.AuctionDraft = auctionLeague;
    //}

    /// <summary>
    /// Gets or sets the unique id for the sheet.
    /// </summary>
    public int CheatSheetId { get; set; }

    /// <summary>
    /// Gets or sets the username of the user who created the sheet.
    /// </summary>
    public string UserName
    {
        get
        {
            return this.userName.Trim();
        }

        set
        {
            this.userName = value;
        }
    }

    /// <summary>
    /// Gets or sets the season that the sheet is relevant.
    /// </summary>
    public string SeasonCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sport on which the sheet is based.
    /// </summary>
    public string SportCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name given to the cheat sheet.
    /// </summary>
    public string SheetName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the season on which to base the stats of the sheet.
    /// </summary>
    public string StatsSeasonCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the sheet is based on a PPR league.
    /// </summary>
    public bool? PprLeague { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the sheet's associated draft is auction-based.
    /// </summary>
    public bool? AuctionDraft { get; set; }

    /// <summary>
    /// Gets or sets a collection of cheat sheet items associated with the sheet.
    /// </summary>
    public List<CheatSheetItem> CheatSheetItems { get; set; } = new List<CheatSheetItem>();
}
