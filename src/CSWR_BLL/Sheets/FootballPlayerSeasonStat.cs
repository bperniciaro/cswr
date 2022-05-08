using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.CheatSheetWarRoom.DAL;
using System.Collections.Generic;


/// <summary>
/// Summary description for PlayerStat
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class FootballPlayerSeasonStat : BaseSheet
  {
    public FootballPlayerSeasonStat(string seasonCode, int playerID, string statCode, double statValue)
    {
      this.SeasonCode = seasonCode;
      this.PlayerID = playerID;
      this.StatCode = statCode;
      this.StatValue = statValue;

    }

    // Season Code
    private string _seasonCode = "";
    public string SeasonCode
    {
      get { return _seasonCode; }
      set { _seasonCode = value; }
    }

    // Player ID
    public int _playerID = 0;
    public int PlayerID
    {
      get { return _playerID; }
      set { _playerID = value; }
    }

    // Stat Code
    private string _statCode = "";
    public string StatCode
    {
      get { return _statCode; }
      set { _statCode = value; }
    }

    // Stat Value
    private double _statValue = 0;
    public double StatValue
    {
      get { return Math.Round(_statValue, 1); }
      set { _statValue = value; }
    }


    /// <summary>
    /// Returns a collection with all the playerstats
    /// </summary>
    //public static List<FootballPlayerSeasonStat> GetFootballPlayerSeasonStats(string seasonCode, int playerID)
    //{
    //  List<FootballPlayerSeasonStat> footballPlayerSeasonStats = null;
    //  string key = "Sheets_StatsBySeasonCodePlayerID_" + seasonCode + "_" + playerID.ToString();

    //  if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
    //  {
    //    footballPlayerSeasonStats = (List<FootballPlayerSeasonStat>)BizObject.Cache[key];
    //  }
    //  else
    //  {
    //    List<FootballPlayerSeasonStatDetails> recordset = SiteProvider.Sheets.GetFootballPlayerSeasonStats(seasonCode, playerID);
    //    footballPlayerSeasonStats = GetFootballPlayerSeasonStatListFromFootballPlayerSeasonStatDetailsList(recordset);
    //    BaseSheet.CacheData(key, footballPlayerSeasonStats);
    //  }
    //  return footballPlayerSeasonStats.GetRange(0, footballPlayerSeasonStats.Count);
    //}


    /// <summary>
    /// Returns a collection with all the playerstats
    /// </summary>
    //public static List<FootballPlayerSeasonStat> GetFootballPlayerSeasonStats(string seasonCode)
    //{
    //  List<FootballPlayerSeasonStat> footballPlayerSeasonStats = null;
    //  string key = "Sheets_StatsBySeasonCode_" + seasonCode;

    //  if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
    //  {
    //    footballPlayerSeasonStats = (List<FootballPlayerSeasonStat>)BizObject.Cache[key];
    //  }
    //  else
    //  {
    //    List<FootballPlayerSeasonStatDetails> recordset = SiteProvider.Sheets.GetFootballPlayerSeasonStats(seasonCode);
    //    footballPlayerSeasonStats = GetFootballPlayerSeasonStatListFromFootballPlayerSeasonStatDetailsList(recordset);
    //    BaseSheet.CacheData(key, footballPlayerSeasonStats);
    //  }
    //  return footballPlayerSeasonStats.GetRange(0, footballPlayerSeasonStats.Count);
    //}


    /// <summary>
    /// Get a particular stat based on the season, player, and stat code
    /// </summary>
    /// <param name="seasonCode"></param>
    /// <param name="playerID"></param>
    /// <param name="statCode"></param>
    /// <returns></returns>
    //public static FootballPlayerSeasonStat GetFootballPlayerSeasonStatBySeasonPlayerStatCodes(string seasonCode, int playerID, string statCode)
    //{
    //  FootballPlayerSeasonStat footballPlayerSeasonStat;

    //  string key = "Sheets_FootballPlayerSeasonStatBySeasonPlayerStat_" + seasonCode.ToString() + "_" + playerID.ToString() + "_" + statCode.ToString();

    //  if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
    //  {
    //    footballPlayerSeasonStat = (FootballPlayerSeasonStat)BizObject.Cache[key];
    //  }
    //  else
    //  {
    //    footballPlayerSeasonStat = GetFootballPlayerSeasonStatFromFootballPlayerSeasonStatDetails(SiteProvider.Sheets.GetFootballPlayerSeasonStatBySeasonPlayerStatCodes(seasonCode, playerID, statCode));
    //    BaseSheet.CacheData(key, footballPlayerSeasonStat);
    //  }
    //  return footballPlayerSeasonStat;
    //}


    //public static bool GenerateSeasonStats(string seasonCode, string statCode, int playerID)
    //{
    //  return SiteProvider.Sheets.GenerateSeasonStats(seasonCode, statCode, playerID);
    //}


    /// <summary>
    /// This method determines how many games a player took part in during the season specified.  Specificall, it counts 
    /// the number of games where the player recorded any stat.
    /// </summary>
    /// <param name="seasonCode"></param>
    /// <param name="playerID"></param>
    /// <returns></returns>
    //public static bool GenerateSeasonGamesPlayed(string seasonCode, int playerID)
    //{
    //  return SiteProvider.Sheets.GenerateSeasonGamesPlayed(seasonCode, playerID);
    //}


    // Static Insert
    //public static int InsertFootballPlayerSeasonStat(string seasonCode, int playerID, string statCode, double statValue)
    //{

    //  // create an article entity to inser and use the specific provider to do the insert
    //  FootballPlayerSeasonStatDetails record = new FootballPlayerSeasonStatDetails(seasonCode, playerID, statCode, statValue);
    //  int ret = SiteProvider.Sheets.InsertFootballPlayerSeasonStat(record);
    //  // since we've added an article we should clear all articles so that the new article is picked up
    //  //BizObject.PurgeCacheItems("Sheets_SupplementalSheets");
    //  return ret;
    //}



    // Static Update
    //public static bool UpdateFootballPlayerSeasonStat(string seasonCode, int playerID, string statCode, double statValue)
    //{

    //  // build an article entity to update, then use the module specific provider to update it
    //  FootballPlayerSeasonStatDetails record = new FootballPlayerSeasonStatDetails(seasonCode, playerID, statCode, statValue);
    //  bool ret = SiteProvider.Sheets.UpdateFootballPlayerSeasonStat(record);

    //  return ret;
    //}

    //public static double GetStatValueFromFootballPlayerSeasonStatCollection(List<FootballPlayerSeasonStat> footballPlayerSeasonStats, string statCode)
    //{
    //  int i;
    //  for (i = 0; i < footballPlayerSeasonStats.Count; i++)
    //  {
    //    if (footballPlayerSeasonStats[i].StatCode.Trim() == statCode)
    //      break; 
    //  }
    //  return footballPlayerSeasonStats[i].StatValue;
    //}



    /// <summary>
    /// Returns a PlayerStat object filled with the data taken from the input PlayerStatDetails
    /// </summary>
    private static FootballPlayerSeasonStat GetFootballPlayerSeasonStatFromFootballPlayerSeasonStatDetails(FootballPlayerSeasonStatDetails playerStat)
    {
      if (playerStat == null)
        return null;
      else
      {
        return new FootballPlayerSeasonStat(playerStat.SeasonCode, playerStat.PlayerID, playerStat.StatCode.Trim(), playerStat.StatValue);
      }
    }

    /// <summary>
    /// Returns a list of PlayerStat objects filled with the data taken from the input list of PlayerStatDetails
    /// </summary>
    private static List<FootballPlayerSeasonStat> GetFootballPlayerSeasonStatListFromFootballPlayerSeasonStatDetailsList(List<FootballPlayerSeasonStatDetails> recordset)
    {
      List<FootballPlayerSeasonStat> footballPlayerSeasonStats = new List<FootballPlayerSeasonStat>();
      foreach (FootballPlayerSeasonStatDetails record in recordset)
        footballPlayerSeasonStats.Add(GetFootballPlayerSeasonStatFromFootballPlayerSeasonStatDetails(record));
      return footballPlayerSeasonStats;
    }
  }
}