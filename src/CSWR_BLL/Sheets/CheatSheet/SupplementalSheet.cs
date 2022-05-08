using System;
using System.Collections.Generic;
using System.Linq;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Summary description for SupplementalSheet
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  [Serializable()]
  public class SupplementalSheet : BaseSheet
  {

    public SupplementalSheet() { }

    public SupplementalSheet(int supplementalSheetID, string seasonCode, int supplementalSourceID, string sportCode, string positionCode, DateTime lastUpdated, string url)
    {
      this.SupplementalSheetID = supplementalSheetID;
      this.SeasonCode = seasonCode;
      this.SupplementalSourceID = supplementalSourceID;
      this.SportCode = sportCode;
      this.PositionCode = positionCode;
      this.LastUpdated = lastUpdated;
      this.Url = url;
    }

    /// <summary>
    /// The unique ID of the supplemental sheet
    /// </summary>
    public int SupplementalSheetID {get;set;}

    /// <summary>
    /// The season in which the supplemental sheet is relevant.
    /// </summary>
    public string SeasonCode {get;set;}

    /// <summary>
    /// The ID of the supplemental source from which the sheet was created
    /// </summary>
    public int SupplementalSourceID {get;set;}

    /// <summary>
    /// The sport on which the supplemental sheet is based
    /// </summary>
    public string SportCode {get;set;}

    /// <summary>
    /// The position on which the supplemental sheet is based
    /// </summary>
    /// 
    private string _positionCode = String.Empty;
    public string PositionCode
    {
      get
      {
        return _positionCode.Trim();
      }
      set
      {
        _positionCode = value;
      }
    }

    /// <summary>
    /// The date and time that the supplemental sheet was last updated
    /// </summary>
    public DateTime LastUpdated {get;set;}

    /// <summary>
    /// The url where the supplemental sheet can be found
    /// </summary>
    public string Url {get;set;}


    /// <summary>
    /// This calculated property returns the year of the statistics referenced in the cheat sheet.  For supplemental sheets this is always
    /// the season before the supplemental sheet seasoncode
    /// </summary>
    private string _statsSeasonCode = "";
    public string StatsSeasonCode
    {
      get 
      {
        if (_statsSeasonCode == String.Empty)
        {
          int statsSeason = int.Parse(this.SeasonCode) - 1;
          _statsSeasonCode = statsSeason.ToString();
        }
        return _statsSeasonCode; 
      }
    }



    /// <summary>
    /// A read only property which provides access to the items that make up a supplemental sheet 
    /// </summary>
    private List<SupplementalSheetItem> _items = null;
    public List<SupplementalSheetItem> Items
    {
      get
      {
        if (_items == null)
        {
          _items = SupplementalSheetItem.GetSupplementalSheetItems(this.SupplementalSheetID);
        }
        return _items;
      }
    }



    /// <summary>
    /// Returns a supplemental sheet based on the requested supplementalSheetID.  This is used when editing a particular sheet.
    /// </summary>
    public static SupplementalSheet GetSupplementalSheet(int supplementalSheetID)
    {
      SupplementalSheet supplementalSheet = null;
      string key = "Sheets_SupplementalSheet_" + supplementalSheetID.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        supplementalSheet = (SupplementalSheet)BizObject.Cache[key];
      }
      else
      {
        supplementalSheet = GetSupplementalSheetFromSupplementalSheetDetails(SiteProvider.Sheets.GetSupplementalSheet(supplementalSheetID));
        BaseSheet.CacheData(key, supplementalSheet);
      }
      return supplementalSheet;
    }


    /// <summary>
    /// Returns a supplementalsheet object based on season, source, sport, and position.  This method is used in calculting ADP when we don't know
    /// the ID of the supplemental sheet that we need to reference.
    /// </summary>
    public static SupplementalSheet GetSupplementalSheet(string seasonCode, int supplementalSourceID, string sportCode, string positionCode)
    {
      SupplementalSheet supplementalSheet = null;
      string key = "Sheets_SupplementalSheet_" + seasonCode + "_" + supplementalSourceID.ToString() + "_" + sportCode + "_" + positionCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        supplementalSheet = (SupplementalSheet)BizObject.Cache[key];
      }
      else
      {
        supplementalSheet = GetSupplementalSheetFromSupplementalSheetDetails(SiteProvider.Sheets.GetSupplementalSheet(seasonCode, supplementalSourceID, sportCode, positionCode));
        BaseSheet.CacheData(key, supplementalSheet);
      }
      return supplementalSheet;
    }


    /// <summary>
    /// Returns a sorted collection of all supplemental sheets for a given season and sport.  This code is
    /// used to sort supplementals in the administration area.
    /// </summary>
    public static List<SupplementalSheet> GetSupplementalSheets(string seasonCode, string sportCode, string orderBy)
    {
      List<SupplementalSheet> supplementalSheets = null;
      string key = "Sheets_SupplementalSheetsBySeasonSportCodes_" + seasonCode + "_" + sportCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        supplementalSheets = (List<SupplementalSheet>)BizObject.Cache[key];
      }
      else
      {
        List<SupplementalSheetDetails> recordset = SiteProvider.Sheets.GetSupplementalSheets(seasonCode, sportCode);
        supplementalSheets = GetSupplementalSheetListFromSupplementalSheetDetailsList(recordset);
        BaseSheet.CacheData(key, supplementalSheets);
      }

      // now sort the sheets accordingly

      if (String.IsNullOrEmpty(orderBy))
        return supplementalSheets;

      // sort list
      switch (orderBy)
      {
        case "SeasonCode":
          supplementalSheets.OrderBy(x => x.SeasonCode);
          break;
        /**  This is probably a bug, need to determine if it is used **/
        case "SuppSource":
          supplementalSheets.OrderBy(x => x.SupplementalSourceID);
          break;
        case "SportCode":
          supplementalSheets.OrderBy(x => x.SportCode);
          break;
        case "PositionCode":
          supplementalSheets.OrderBy(x => x.PositionCode);
          break;
      }

      return supplementalSheets.GetRange(0, supplementalSheets.Count);
    }


    /// <summary>
    /// Returns a collection with the supplemental sheets for a particular sport.  This is useful if we want to 
    /// remove all player from all supplemental sheets of a particular sport.
    /// </summary>
    public static List<SupplementalSheet> GetSupplementalSheets(string sportCode)
    {
      List<SupplementalSheet> supplementalSheets = null;
      string key = "Sheets_SupplementalSheetsBySportCode_" + sportCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        supplementalSheets = (List<SupplementalSheet>)BizObject.Cache[key];
      }
      else
      {
        List<SupplementalSheetDetails> recordset = SiteProvider.Sheets.GetSupplementalSheets(sportCode);
        supplementalSheets = GetSupplementalSheetListFromSupplementalSheetDetailsList(recordset);
        BaseSheet.CacheData(key, supplementalSheets);
      }
      return supplementalSheets.GetRange(0, supplementalSheets.Count);
    }



    /// <summary>
    /// This instance update can be called when you already have a Supplemental Sheet instantiated
    /// </summary>
    /// <returns></returns>
    public bool Update()
    {
      return SupplementalSheet.UpdateSupplementalSheet(this.SupplementalSheetID, this.SeasonCode, this.SupplementalSourceID, this.SportCode, this.PositionCode, this.LastUpdated, this.Url);
    }


    /// <summary>
    /// This update is called when a supplemental sheet is updated
    /// </summary>
    public static bool UpdateSupplementalSheet(int supplementalSheetID, string seasonCode, int supplementalSourceID, string sportCode, string positionCode, DateTime lastUpdated, string url)
    {

      // convert any nulls to empty strings
      sportCode = BizObject.ConvertNullToEmptyString(sportCode);
      positionCode = BizObject.ConvertNullToEmptyString(positionCode);
      url = BizObject.ConvertNullToEmptyString(url);

      // not sure what this does
      if (lastUpdated == DateTime.MinValue)
        lastUpdated = DateTime.Now;

      // build an article entity to update, then use the module specific provider to update it
      SupplementalSheetDetails record = new SupplementalSheetDetails(supplementalSheetID, seasonCode, supplementalSourceID, sportCode, positionCode, lastUpdated, url);
      bool ret = SiteProvider.Sheets.UpdateSupplementalSheet(record);

      BizObject.PurgeCacheItems("Sheets_SupplementalSheets");
      BizObject.PurgeCacheItems("Sheets_SupplementalSheet_" + supplementalSheetID.ToString());
      BizObject.PurgeCacheItems("Sheets_SupplementalSheet_" + seasonCode + "_" + supplementalSourceID.ToString() + "_" + sportCode + "_" + positionCode);
      
      return ret;
    }

    /// <summary>
    /// You create a supplemental sheet through the 'sheet management' interface in administaration.  Supplemental sheet creation
    /// involves several steps so they are executed as a transaction in the DAL.  Supplemental sheets are always created using
    /// total fantasy points from the previous season.
    /// </summary>
    public static bool CreateSupplementalSheet(int supplementalSourceID, string seasonCode, string sportCode, string positionCode, DateTime lastUpdated, string url)
    {
      SupplementalSheetDetails record = new SupplementalSheetDetails(0, seasonCode, supplementalSourceID, sportCode, positionCode, lastUpdated, url);

      bool result = SiteProvider.Sheets.CreateSupplementalSheet(record);
      BizObject.PurgeCacheItems("Sheets_SupplementalSheets");

      return true;
    }



    /// <summary>
    /// This method is used to delete a supplemental sheet when managing sheets
    /// </summary>
    /// <param name="supplementalSheetID"></param>
    /// <returns></returns>
    public static bool DeleteSupplementalSheet(int supplementalSheetID)
    {
      // we've got to get a reference to the sheet to delete in order to properly clear all cache
      SupplementalSheet suppSheetToDelete = SupplementalSheet.GetSupplementalSheet(supplementalSheetID);
      bool ret = SiteProvider.Sheets.DeleteSupplementalSheet(supplementalSheetID);

      BizObject.PurgeCacheItems("Sheets_SupplementalSheets");
      BizObject.PurgeCacheItems("Sheets_SupplementalSheet_" + supplementalSheetID.ToString());
      BizObject.PurgeCacheItems("Sheets_SupplementalSheet_" + suppSheetToDelete.SeasonCode + "_" + suppSheetToDelete.SupplementalSheetID.ToString() + "_" + suppSheetToDelete.SportCode + "_" + suppSheetToDelete.PositionCode);
      return ret;
    }


    /// <summary>
    /// Returns a collection of players that aren't currently in the specified cheat sheet.  This method is called from the SheetItemManager
    /// user control.  This pulls all players who recorded a stat the previous season (except those who are retired) and also all
    /// rookies.
    /// </summary>
    public static List<Player> GetSupplementalSheetAvailablePlayers(int supplementalSheetID, string sortParam, string sortDir)
    {
      SupplementalSheet currentSuppSheet = SupplementalSheet.GetSupplementalSheet(supplementalSheetID);
      List<SupplementalSheetItem> supplementalSheetItems = new List<SupplementalSheetItem>();

      supplementalSheetItems = SupplementalSheetItem.GetSupplementalSheetItems(supplementalSheetID);

      /***************************************************************************/
      /* Create a collection of all possible players for the current season code */
      /***************************************************************************/
      List<Player> availablePlayers = Player.GetPlayersBySportSeasonPositionCodes(currentSuppSheet.SportCode, currentSuppSheet.StatsSeasonCode, currentSuppSheet.PositionCode, false, true);

      // build a collection of players who are not on the current sheet
      List<Player> notOnSheetPlayers = new List<Player>();

      // isolate those players who are available, but who aren't on the current sheet
      foreach (Player currentPlayer in availablePlayers)
      {
        // if we can't find the current player among the players already in the cheat sheet
        if (supplementalSheetItems.Find((delegate(SupplementalSheetItem targetItem) { return (targetItem.PlayerID == currentPlayer.PlayerID); })) == null)
        {
          notOnSheetPlayers.Add(currentPlayer);
        }
      }

      /************************************************************************************/
      /* Sort the resulting collection of players based on the sorting parameter specified*/
      /************************************************************************************/

      // if we're sorting by name, just call a local sort method to reorder the players
      if (sortParam.ToLower() == "name")
      {
        notOnSheetPlayers.OrderBy(x => x.FullNameLastFirst);
        return notOnSheetPlayers;
      }
      // if we're sorting by statistic, get a sorted list of available players, then add rookies to the end since their stats will
      // be the lowest by definition
      else
      {
        // get all available players sorted by the specified statistic (sortParam) to use as a comparison point to sort
        // the notOnSheetPlayers we already identified
        List<Player> sortedPlayers = new List<Player>();
        List<Player> statSortedPlayers = Player.GetPlayers(currentSuppSheet.SportCode, currentSuppSheet.StatsSeasonCode, currentSuppSheet.PositionCode, false, sortParam, sortDir);
        
        // determine players who were not retired, but who did not record a stat in the specified year.  these players
        // will not show-up when querying for things like TFP, but we still need to add them to the player pool
        List<Player> noStatsPlayers = new List<Player>();
        foreach(Player currentPlayer in availablePlayers)  
        {
          Player availPlayer = statSortedPlayers.Find((delegate(Player targetPlayer) { return (targetPlayer.PlayerID == currentPlayer.PlayerID); }));
          if (availPlayer == null)
          {
            noStatsPlayers.Add(currentPlayer);
          }
        }
        
        // using the stats players we can sort the players that we know are not on the current sheet
        foreach (Player currentPlayer in statSortedPlayers)
        {
          Player statSortedPlayer = notOnSheetPlayers.Find((delegate(Player targetPlayer) { return (targetPlayer.PlayerID == currentPlayer.PlayerID); } ));
          if (statSortedPlayer != null)
          {
            sortedPlayers.Add(statSortedPlayer);
          }
        }
        
        // add the players who did not record a stat (this includes rookies by default)
        sortedPlayers.AddRange(noStatsPlayers);
        // return a copy of the collection of sorted players
        return sortedPlayers.GetRange(0, sortedPlayers.Count);
      }
    }


    

    /// <summary>
    /// This method is called when supplemental sheet items are reorderd
    /// </summary>
    /// <param name="supplementalSheetID"></param>
    /// <param name="oldIndex"></param>
    /// <param name="newIndex"></param>
    /// <returns></returns>
    public static bool ReorderSupplementalSheetItems(int supplementalSheetID, int oldIndex, int newIndex)
    {
      BizObject.PurgeCacheItems("Sheets_SupplementalSheetItems");
      BizObject.PurgeCacheItems("Sheets_SupplementalSheetItem_" + supplementalSheetID.ToString());
      return SiteProvider.Sheets.ReorderSupplementalSheetItems(supplementalSheetID, oldIndex, newIndex);
    }
    

    /// <summary>
    /// This method clears all items from a supplemental sheet, and is generally used when scraping and replacing
    /// items from another supplemental source
    /// </summary>
    /// <param name="supplementalSheetID"></param>
    /// <returns></returns>
    public static bool RemoveAllSupplementalSheetItems(int supplementalSheetID)
    {
      bool ret = SiteProvider.Sheets.RemoveAllSupplementalSheetItems(supplementalSheetID);
      BizObject.PurgeCacheItems("Sheets_SupplementalSheets");
      BizObject.PurgeCacheItems("Sheets_SupplementalSheet_" + supplementalSheetID.ToString());
      BizObject.PurgeCacheItems("Sheets_SupplementalSheetItem_" + supplementalSheetID.ToString());
      return ret;
    }


    /// <summary>
    /// This method is called to remove a player from a supplemental sheet.  It can also be used when deleting a player
    /// </summary>
    public static bool RemoveSupplementalSheetItem(int supplementalSheetID, int playerID)
    {
      SupplementalSheetItemDetails record = new SupplementalSheetItemDetails(supplementalSheetID, playerID, 0, false, false, String.Empty);
      bool ret = SiteProvider.Sheets.RemoveSupplementalSheetItem(record);
      BizObject.PurgeCacheItems("Sheets_SupplementalSheetItems");
      BizObject.PurgeCacheItems("Sheets_SupplementalSheet_" + supplementalSheetID.ToString());
      BizObject.PurgeCacheItems("Sheets_SupplementalSheetItem_" + supplementalSheetID.ToString());
      return ret;
    }

    /// <summary>
    /// This method is called to add a player to a supplemental sheet
    /// </summary>
    public static bool AddSupplementalSheetItem(int supplementalSheetID, int playerID)
    {
      SupplementalSheetItemDetails record = new SupplementalSheetItemDetails(supplementalSheetID, playerID, 0, false, false, String.Empty);
      bool ret = SiteProvider.Sheets.AddSupplementalSheetItem(record);
      BizObject.PurgeCacheItems("Sheets_SupplementalSheetItems");
      BizObject.PurgeCacheItems("Sheets_SupplementalSheet_" + supplementalSheetID.ToString());
      return ret;
    }


      /// <summary>
    /// Returns a SupplementalSheet object filled with the data taken from the input SupplementalSheetDetails
    /// </summary>
    private static SupplementalSheet GetSupplementalSheetFromSupplementalSheetDetails(SupplementalSheetDetails supplementalSheet)
    {
      if (supplementalSheet == null)
        return null;
      else
      {
        return new SupplementalSheet(supplementalSheet.SupplementalSheetID, supplementalSheet.SeasonCode, supplementalSheet.SupplementalSourceID, supplementalSheet.SportCode,
            supplementalSheet.PositionCode, supplementalSheet.LastUpdated, supplementalSheet.Url);
      }
    }

    /// <summary>
    /// Returns a list of Sport objects filled with the data taken from the input list of SportDetails
    /// </summary>
    private static List<SupplementalSheet> GetSupplementalSheetListFromSupplementalSheetDetailsList(List<SupplementalSheetDetails> recordset)
    {
      List<SupplementalSheet> supplementalSheets = new List<SupplementalSheet>();
      foreach (SupplementalSheetDetails record in recordset)
        supplementalSheets.Add(GetSupplementalSheetFromSupplementalSheetDetails(record));
      return supplementalSheets;
    }


  }
}