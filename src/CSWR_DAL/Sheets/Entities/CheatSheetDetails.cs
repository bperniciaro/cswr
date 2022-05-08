using System;

/// <summary>
/// Summary description for CheatSheet
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class CheatSheetDetails
  {
    public CheatSheetDetails(int cheatSheetID, string username, string seasonCode, string sportCode, string sheetName, string statSeasonCode,
                              DateTime created, DateTime lastUpdated, bool? pprLeague, bool? auctionDraft)
    {
      this.CheatSheetID = cheatSheetID;
      this.Username = username;
      this.SeasonCode = seasonCode;
      this.SportCode = sportCode;
      this.SheetName = sheetName;
      this.StatsSeasonCode = statSeasonCode;
      this.Created = created;
      this.LastUpdated = lastUpdated;
      this.PPRLeague = pprLeague;
      this.ActionDraft = auctionDraft;
    }

    // Cheat Sheet ID
    private int _cheatSheetID = 0;
    public int CheatSheetID
    {
      get { return _cheatSheetID; }
      set { _cheatSheetID = value; }
    }

    // Username
    private string _username = "";
    public string Username
    {
      get { return _username; }
      set { _username = value; }
    }


    // Season Code
    private string _seasonCode = "";
    public string SeasonCode
    {
      get { return _seasonCode; }
      set { _seasonCode = value; }
    }

    // Sport Code
    private string _sportCode = "";
    public string SportCode
    {
      get { return _sportCode; }
      set { _sportCode = value; }
    }

    // Sheet Name 
    private string _sheetName = "";
    public string SheetName
    {
      get { return _sheetName; }
      set { _sheetName = value; }
    }

    // Stats Season Code
    private string _statsSeasonCode = "";
    public string StatsSeasonCode
    {
      get { return _statsSeasonCode; }
      set { _statsSeasonCode = value; }
    }

    // PPR League
    public bool? PPRLeague { get; set; }

    // Action Draft
    public bool? ActionDraft { get; set; }

    // Created
    private DateTime _created = DateTime.Now;
    public DateTime Created
    {
      get { return _created; }
      set { _created = value; }
    }

    // Last Updated
    private DateTime _lastUpdated = DateTime.Now;
    public DateTime LastUpdated
    {
      get { return _lastUpdated; }
      set { _lastUpdated = value; }
    }
  }
}