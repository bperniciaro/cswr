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
  public class FootballPlayerWeeklyStat : BaseSheet
  {
    public FootballPlayerWeeklyStat(string seasonCode, int playerID, int week, string statCode, double statValue)
    {
      this.SeasonCode = seasonCode;
      this.PlayerID = playerID;
      this.Week = week;
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

    // Week Code
    private int _week = 0;
    public int Week
    {
      get { return _week; }
      set { _week = value; }
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
      get { return _statValue; }
      set { _statValue = value; }
    }

    /// <summary>
    /// Returns a collection with all the playerstats
    /// </summary>
    public static int GetFootballPlayerWeeklyStatCount(string seasonCode, int playerID, int week)
    {
      return SiteProvider.Sheets.GetFootballPlayerWeeklyStatCount(seasonCode, playerID, week);
    }



    public static double GetFootballPlayerWeeklyStat(string seasonCode, int week, int playerID, string statCode)
    {
      FootballPlayerWeeklyStatDetails record = new FootballPlayerWeeklyStatDetails(seasonCode, playerID, week, statCode, 0);
      double statValue = SiteProvider.Sheets.GetFootballPlayerWeeklyStat(record);
      return statValue;
    }


    // Instance Insert
    public int Insert()  {
      return FootballPlayerWeeklyStat.InsertFootballPlayerWeeklyStat(this.SeasonCode, this.PlayerID, this.Week, this.StatCode, this.StatValue);
    }

    // Static Insert
    public static int InsertFootballPlayerWeeklyStat(string seasonCode, int playerID, int week, string statCode, double statValue)
    {

      // create an article entity to inser and use the specific provider to do the insert
      FootballPlayerWeeklyStatDetails record = new FootballPlayerWeeklyStatDetails(seasonCode, playerID, week, statCode, statValue);
      int ret = SiteProvider.Sheets.InsertFootballPlayerWeeklyStat(record);
      // since we've added an article we should clear all articles so that the new article is picked up
      //BizObject.PurgeCacheItems("Sheets_SupplementalSheets");
      return ret;
    }



    // Instance Update
    public bool Update()
    {
      return FootballPlayerWeeklyStat.UpdateFootballPlayerWeeklyStat(this.SeasonCode, this.PlayerID, this.Week, this.StatCode, this.StatValue);
    }

    // Static Update
    public static bool UpdateFootballPlayerWeeklyStat(string seasonCode, int  playerID, int week, string statCode, double statValue)
    {

      // build an article entity to update, then use the module specific provider to update it
      FootballPlayerWeeklyStatDetails record = new FootballPlayerWeeklyStatDetails(seasonCode, playerID, week, statCode, statValue);
      bool ret = SiteProvider.Sheets.UpdateFootballPlayerWeeklyStat(record);

      return ret;
    }



    /// <summary>
    /// Returns a PlayerStat object filled with the data taken from the input PlayerStatDetails
    /// </summary>
    private static FootballPlayerWeeklyStat GetFootballPlayerWeeklyStatFromFootballPlayerWeeklyStatDetails(FootballPlayerWeeklyStatDetails playerStat)
    {
      if (playerStat == null)
        return null;
      else
      {
        return new FootballPlayerWeeklyStat(playerStat.SeasonCode, playerStat.PlayerID, playerStat.Week, playerStat.StatCode, playerStat.StatValue);
      }
    }

    /// <summary>
    /// Returns a list of PlayerStat objects filled with the data taken from the input list of PlayerStatDetails
    /// </summary>
    private static List<FootballPlayerWeeklyStat> GetFootballPlayerWeeklyStatListFromFootballPlayerWeeklyStatDetailsList(List<FootballPlayerWeeklyStatDetails> recordset)
    {
      List<FootballPlayerWeeklyStat> FootballPlayerWeeklyStats = new List<FootballPlayerWeeklyStat>();
      foreach (FootballPlayerWeeklyStatDetails record in recordset)
        FootballPlayerWeeklyStats.Add(GetFootballPlayerWeeklyStatFromFootballPlayerWeeklyStatDetails(record));
      return FootballPlayerWeeklyStats;
    }
  }
}