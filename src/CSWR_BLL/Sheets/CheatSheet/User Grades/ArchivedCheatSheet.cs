using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// This is the class for a cheat sheet, which descends from the base class BaseSheet
/// </summary>
/// 

namespace BP.CheatSheetWarRoom.BLL.Sheets
{

  [Serializable()]
  public class ArchivedCheatSheet : BaseSheet
  {

    public ArchivedCheatSheet(int archivedCheatSheetID, string seasonCode, string sportCode, string positionCode, string sheetName, 
                              string username, DateTime created, DateTime lastUpdated, bool? pprLeague)
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

    public List<ArchivedCheatSheetItem> Items
    {
      get
      {
        return ArchivedCheatSheetItem.GetArchivedCheatSheetItems(this.ArchivedCheatSheetID);
      }
    }

    /// <summary>
    /// Returns a collection of archived cheat sheets
    /// </summary>
    public static List<ArchivedCheatSheet> GetArchivedCheatSheets(string sportCode, string seasonCode, string positionCode)
    {
      List<ArchivedCheatSheet> archivedCheatSheets = null;
      string key = "Sheets_ArchivedCheatSheetsBySportSeasonPosition_" + sportCode.ToCharArray() + "_" + seasonCode.ToString() + "_" + positionCode.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        archivedCheatSheets = (List<ArchivedCheatSheet>)BizObject.Cache[key];
      }
      else
      {
        List<ArchivedCheatSheetDetails> recordset = SiteProvider.Sheets.GetArchivedCheatSheets(sportCode, seasonCode, positionCode);
        archivedCheatSheets = GetArchivedCheatSheetListFromArchivedCheatSheetDetailsList(recordset);
        BaseSheet.CacheData(key, archivedCheatSheets);
      }
      return archivedCheatSheets.GetRange(0, archivedCheatSheets.Count);
    }


    public static ArchivedCheatSheet GetArchivedCheatSheet(int archivedCheatSheetId)
    {
      return GetArchivedCheatSheetFromArchivedCheatSheetDetails(SiteProvider.Sheets.GetArchivedCheatSheet(archivedCheatSheetId));
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="archivedCheatSheetId"></param>
    /// <param name="playerId"></param>
    /// <returns></returns>
    public static bool RemoveArchivedCheatSheetItem(int archivedCheatSheetId, int playerId)
    {
      ArchivedCheatSheetItemDetails record = new ArchivedCheatSheetItemDetails(archivedCheatSheetId, playerId, 0, false, false, false, string.Empty);
      bool ret = SiteProvider.Sheets.RemoveArchivedCheatSheetItem(record);
      BizObject.PurgeCacheItems("Sheets_ArchivedCheatSheetItems_" + archivedCheatSheetId);

     return ret;
    }




    /// <summary>
    /// Converts a entity into a business-level domain object.
    /// </summary>
    /// <param name="cheatSheet">A cheat sheet entity object.</param>
    /// <returns>A cheat sheet domain object.</returns>
    private static ArchivedCheatSheet GetArchivedCheatSheetFromArchivedCheatSheetDetails(ArchivedCheatSheetDetails archivedCheatSheetDetails)
    {
      if (archivedCheatSheetDetails == null)
        return null;
      else
      {
        return new ArchivedCheatSheet(archivedCheatSheetDetails.ArchivedCheatSheetID, archivedCheatSheetDetails.SeasonCode, archivedCheatSheetDetails.SportCode,
                                      archivedCheatSheetDetails.PositionCode, archivedCheatSheetDetails.SheetName, archivedCheatSheetDetails.Username, 
                                      archivedCheatSheetDetails.Created, archivedCheatSheetDetails.LastUpdated, archivedCheatSheetDetails.PPRLeague);
      }
    }


    /// <summary>
    /// Converts a collection of sheet entities into a collection of business-level domain objects.
    /// </summary>
    /// <param name="cheatSheet">A collection of cheat sheet entity objects.</param>
    /// <returns>A collection cheat sheet domain objects.</returns>
    private static List<ArchivedCheatSheet> GetArchivedCheatSheetListFromArchivedCheatSheetDetailsList(List<ArchivedCheatSheetDetails> recordset)
    {
      List<ArchivedCheatSheet> archivedCheatSheets = new List<ArchivedCheatSheet>();
      foreach (ArchivedCheatSheetDetails record in recordset)
        archivedCheatSheets.Add(GetArchivedCheatSheetFromArchivedCheatSheetDetails(record));
      return archivedCheatSheets;
    }

  }
}