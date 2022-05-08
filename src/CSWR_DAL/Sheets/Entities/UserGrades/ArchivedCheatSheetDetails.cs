using System;

/// <summary>
/// Summary description for CheatSheet
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class ArchivedCheatSheetDetails
  {
    public ArchivedCheatSheetDetails(int archivedCheatSheetID, string seasonCode, string sportCode, string positionCode,
                              string sheetName, string username, DateTime created, DateTime lastUpdated, bool? pprLeague)
    {
      this.ArchivedCheatSheetID = archivedCheatSheetID;
      this.SeasonCode = seasonCode;
      this.SportCode = sportCode;
      this.PositionCode = positionCode;
      this.SheetName = sheetName;
      this.Username = username;
      this.Created = created;
      this.LastUpdated = lastUpdated;
      this.PPRLeague = pprLeague;
    }

    public int ArchivedCheatSheetID {get;set;}
    public string Username {get;set;}
    public string SeasonCode { get; set; }
    public string SportCode { get; set; }
    public string PositionCode { get; set; }
    public string SheetName { get; set; }
    public DateTime Created {get;set;}
    public DateTime LastUpdated {get;set;}
    public bool? PPRLeague { get; set; }

  }
}