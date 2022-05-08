using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;
using BP.CheatSheetWarRoom.BLL.Sheets;

/// <summary>
/// Summary description for SupplementalSheet
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class SupplementalSheet : BaseSheet
  {

    public static Comparison<SupplementalSheet> SeasonComparison = delegate(SupplementalSheet s1, SupplementalSheet s2) { return s1.SeasonCode.CompareTo(s2.SeasonCode); };
    public static Comparison<SupplementalSheet> SuppSourceComparison = delegate(SupplementalSheet s1, SupplementalSheet s2) { return s1.SuppSource.CompareTo(s2.SuppSource); };
    public static Comparison<SupplementalSheet> SportComparison = delegate(SupplementalSheet s1, SupplementalSheet s2) { return s1.SportCode.CompareTo(s2.SportCode); };
    public static Comparison<SupplementalSheet> PositionComparison = delegate(SupplementalSheet s1, SupplementalSheet s2) { return s1.PositionCode.CompareTo(s2.PositionCode); };


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

    // Supplemental Sheet ID
    private int _supplementalSheetID = 0;
    public int SupplementalSheetID
    {
      get { return _supplementalSheetID; }
      set { _supplementalSheetID = value; }
    }

    // Season Code
    private string _seasonCode = "";
    public string SeasonCode
    {
      get { return _seasonCode; }
      set { _seasonCode = value; }
    }

    // Supplemental Source ID
    private int _supplementalSourceID = 0;
    public int SupplementalSourceID
    {
      get { return _supplementalSourceID; }
      set { _supplementalSourceID = value; }
    }

    // Supplemental Source (LAZY LOAD)
    private string _supplementalSource = "";
    public string SuppSource
    {
      get
      {
        if (_supplementalSource == "")
          _supplementalSource = SupplementalSource.GetSupplementalSource(this.SupplementalSourceID).Name;
        return _supplementalSource;
      }
    }

    // Sport Code
    private string _sportCode = "";
    public string SportCode
    {
      get { return _sportCode; }
      set { _sportCode = value; }
    }

    // Sport Name (LAZY LOAD)
    private string _sportName = "";
    public string SportName
    {
      get
      {
        if (_sportName == "")
          _sportName = Sport.GetSport(this.SportCode).SportName;
        return _sportName;
      }
    }



    // Position Code
    private string _positionCode = "";
    public string PositionCode
    {
      get { return _positionCode.Trim(); }
      set { _positionCode = value; }
    }

    // Last Updated
    private DateTime _lastUpdated = DateTime.Now;
    public DateTime LastUpdated
    {
      get { return _lastUpdated; }
      set { _lastUpdated = value; }
    }

    // Url
    private string _url = "";
    public string Url
    {
      get { return _url; }
      set { _url = value; }
    }





    /// <summary>
    /// Returns a collection with all the supplemental sources
    /// </summary>
    public static List<SupplementalSheet> GetSupplementalSheets()
    {
      List<SupplementalSheet> supplementalSheets = null;
      string key = "Sheets_SupplementalSheets";

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        supplementalSheets = (List<SupplementalSheet>)BizObject.Cache[key];
      }
      else
      {
        List<SupplementalSheetDetails> recordset = SiteProvider.Sheets.GetSupplementalSheets();
        supplementalSheets = GetSupplementalSheetListFromSupplementalSheetDetailsList(recordset);
        BaseSheet.CacheData(key, supplementalSheets);
      }
      return supplementalSheets.GetRange(0, supplementalSheets.Count);
    }


    /// <summary>
    /// Returns a sorted collection with all the supplemental sources
    /// </summary>
    public static List<SupplementalSheet> GetSupplementalSheetsSorted(string orderBy)
    {
      List<SupplementalSheet> listToSort = GetSupplementalSheets();

      if (String.IsNullOrEmpty(orderBy))
        return listToSort;

      // sort list
      switch (orderBy)
      {
        case "SeasonCode":
          listToSort.Sort(SupplementalSheet.SeasonComparison);
          break;
        case "SuppSource":
          listToSort.Sort(SupplementalSheet.SuppSourceComparison);
          break;
        case "SportCode":
          listToSort.Sort(SupplementalSheet.SportComparison);
          break;
        case "PositionCode":
          listToSort.Sort(SupplementalSheet.PositionComparison);
          break;
      }

      return listToSort;
    }


    /// <summary>
    /// Returns a collection with the supplemental sheets for a particular source
    /// </summary>
    public static List<SupplementalSheet> GetSupplementalSheets(int supplementalSourceID)
    {
      List<SupplementalSheet> supplementalSheets = null;
      string key = "Sheets_SupplementalSheetsBySourceID_" + supplementalSourceID.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        supplementalSheets = (List<SupplementalSheet>)BizObject.Cache[key];
      }
      else
      {
        List<SupplementalSheetDetails> recordset = SiteProvider.Sheets.GetSupplementalSheets(supplementalSourceID);
        supplementalSheets = GetSupplementalSheetListFromSupplementalSheetDetailsList(recordset);
        BaseSheet.CacheData(key, supplementalSheets);
      }
      return supplementalSheets.GetRange(0, supplementalSheets.Count);
    }


    /// <summary>
    /// Returns a collection with the supplemental sheets for a particular source
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
    /// Returns a collection with the supplemental sheets for a particular source
    /// </summary>
    public static List<SupplementalSheet> GetSupplementalSheets(string seasonCode, string sportCode, string positionCode)
    {
      List<SupplementalSheet> supplementalSheets = null;
      string key = "Sheets_SupplementalSheetsBySeasonSportPositionCodes_" + seasonCode + "_" + sportCode + "_" + positionCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        supplementalSheets = (List<SupplementalSheet>)BizObject.Cache[key];
      }
      else
      {
        List<SupplementalSheetDetails> recordset = SiteProvider.Sheets.GetSupplementalSheets(seasonCode, sportCode, positionCode);
        supplementalSheets = GetSupplementalSheetListFromSupplementalSheetDetailsList(recordset);
        BaseSheet.CacheData(key, supplementalSheets);
      }
      return supplementalSheets.GetRange(0, supplementalSheets.Count);
    }



    /// <summary>
    /// Returns a SupplementalSheet object with the specified id
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
    /// Returns a SupplementalSheet object with the specified id
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
    /// Returns a collection of Player objects with the specified id
    /// </summary>
    public static List<Player> GetSupplementalSheetAvailablePlayersBySheetID(int supplementalSheetID, string sortParam)
    {
      List<SupplementalSheetItem> supplementalSheetItems = SupplementalSheetItem.GetSupplementalSheetItems(supplementalSheetID);
      SupplementalSheet supplementalSheet = SupplementalSheet.GetSupplementalSheet(supplementalSheetID);

      int statSeason  = int.Parse(supplementalSheet.SeasonCode) - 1;
      string statSeasonString = statSeason.ToString();

      List<Player> availablePlayers = Player.GetPlayersBySportSeasonPositionCodes(supplementalSheet.SportCode, statSeasonString, supplementalSheet.PositionCode, false);
      // single out all rookies from the current year
      List<Player> rookiePlayers = Player.GetPlayerRookiesBySportSeasonPositionCodes(supplementalSheet.SportCode, supplementalSheet.SeasonCode, supplementalSheet.PositionCode);
      // add rookies to available players
      for (int i = 0; i < rookiePlayers.Count; i++)
      {
        availablePlayers.Add(rookiePlayers[i]);
      }


      List<Player> nonSheetPlayers = new List<Player>();
      bool playerFound = false;
      for (int i = 0; i <= availablePlayers.Count - 1; i++)
      {
        for (int j = 0; j <= supplementalSheetItems.Count - 1; j++)
        {
          if (availablePlayers[i].PlayerID == supplementalSheetItems[j].PlayerID)
          {
            playerFound = true;
            break;
          }
        }
        if (playerFound != true)
        {
          nonSheetPlayers.Add(availablePlayers[i]);
        }
        playerFound = false;
      }

      // if we're sorting by name just sort the players
      if (sortParam == "name")
      {
        Player.SortPlayersByFullNameLastFirst(ref nonSheetPlayers);
        return nonSheetPlayers;
      }
      // if we're sorting by rank (FPPG for the previous year)
      else
      {
        List<Player> sortedPlayers = new List<Player>();
        List<Player> statSortedPlayers = Player.GetPlayers(statSeasonString.ToString(), supplementalSheet.SportCode, supplementalSheet.PositionCode, false, "FPPG");
        for (int i = 0; i < statSortedPlayers.Count; i++)
        {
          for (int j = 0; j < nonSheetPlayers.Count; j++)
          {
            if (statSortedPlayers[i].PlayerID == nonSheetPlayers[j].PlayerID)
            {
              sortedPlayers.Add(nonSheetPlayers[j]);
              break;
            }
          }
        }
        // if there are any rookies left over in the list, add them to the end
        for (int i = 0; i < nonSheetPlayers.Count; i++)
        {
          if (nonSheetPlayers[i].FirstYear.Year == int.Parse(supplementalSheet.SeasonCode))
          {
            sortedPlayers.Add(nonSheetPlayers[i]);
          }
        }
        return sortedPlayers.GetRange(0, sortedPlayers.Count);
      }
    }


    public static bool RemoveSupplementalSheetItem(int supplementalSheetID, int playerID)
    {
      SupplementalSheetItemDetails record = new SupplementalSheetItemDetails(supplementalSheetID, playerID, 0, false, false, String.Empty, String.Empty, String.Empty);
      bool ret = SiteProvider.Sheets.RemoveSupplementalSheetItem(record);
      BizObject.PurgeCacheItems("Sheets_SupplementalSheetItems");
      BizObject.PurgeCacheItems("Sheets_SupplementalSheet_" + supplementalSheetID.ToString());
      return ret;
    }

    public static bool AddSupplementalSheetItem(int supplementalSheetID, int playerID)
    {
      SupplementalSheetItemDetails record = new SupplementalSheetItemDetails(supplementalSheetID, playerID, 0, false, false, String.Empty, String.Empty, String.Empty);
      bool ret = SiteProvider.Sheets.AddSupplementalSheetItem(record);
      BizObject.PurgeCacheItems("Sheets_SupplementalSheetItems");
      BizObject.PurgeCacheItems("Sheets_SupplementalSheet_" + supplementalSheetID.ToString());
      return ret;
    }

    
    
    public static bool UpdateSupplementalSheetTimestamp(int supplementalSheetID)
    {
      bool ret = SiteProvider.Sheets.UpdateSupplementalSheetTimestamp(supplementalSheetID);
      BizObject.PurgeCacheItems("sheets_supplementalsheet_" + supplementalSheetID.ToString());
      return ret;
    }


    // Instance Update
    public bool Update()
    {
      return SupplementalSheet.UpdateSupplementalSheet(this.SupplementalSheetID, this.SeasonCode, this.SupplementalSourceID, this.SportCode, this.PositionCode, this.LastUpdated, this.Url);
    }

    // Static Update
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





    public static bool ReorderSupplementalSheetItems(int supplementalSheetID, int oldIndex, int newIndex)
    {
      BizObject.PurgeCacheItems("Sheets_SupplementalSheetItemsByID_" + supplementalSheetID.ToString());
      return SiteProvider.Sheets.ReorderSupplementalSheetItems(supplementalSheetID, oldIndex, newIndex);
    }


    // Static Insert
    public static int InsertSupplementalSheet(int supplementalSourceID, string seasonCode, string sportCode, string positionCode, DateTime lastUpdated, string url)
    {

      // not sure what this does
      if (lastUpdated == DateTime.MinValue)
        lastUpdated = DateTime.Now;

      // create an article entity to inser and use the specific provider to do the insert
      SupplementalSheetDetails record = new SupplementalSheetDetails(0, seasonCode, supplementalSourceID, sportCode, positionCode, lastUpdated, url);
      int ret = SiteProvider.Sheets.InsertSupplementalSheet(record);

      // since we've added an article we should clear all articles so that the new article is picked up
      BizObject.PurgeCacheItems("Sheets_SupplementalSheets");
      return ret;
    }

    public static bool CreateSupplementalSheet(int supplementalSourceID, string seasonCode, string sportCode, string positionCode, DateTime lastUpdated, string url)
    {
      SupplementalSheetDetails record = new SupplementalSheetDetails(0, seasonCode, supplementalSourceID, sportCode, positionCode, lastUpdated, url);
      
      bool result = SiteProvider.Sheets.CreateSupplementalSheet(record);
      BizObject.PurgeCacheItems("Sheets_SupplementalSheets");

      return true;
    }


    // Static Delete
    public static bool DeleteSupplementalSheet(int supplementalSheetID)
    {
      bool ret = SiteProvider.Sheets.DeleteSupplementalSheet(supplementalSheetID);
      BizObject.PurgeCacheItems("Sheets_SupplementalSheets");
      BizObject.PurgeCacheItems("Sheets_SupplementalSheet_" + supplementalSheetID.ToString());
      return ret;
    }

    public static bool ClearSupplementalSheetItems(int supplementalSheetID)
    {
      bool ret = SiteProvider.Sheets.ClearSupplementalSheetItems(supplementalSheetID);
      BizObject.PurgeCacheItems("Sheets_SupplementalSheets");
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