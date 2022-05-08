using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Summary description for PlayerStat
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class SportSeasonPlayerWeeklyStat : BaseSheet
  {
    public SportSeasonPlayerWeeklyStat(string sportCode, string seasonCode, int week, int playerID, string statCode, double statValue)
    {
      this.SportCode = sportCode;
      this.SeasonCode = seasonCode;
      this.Week = week;
      this.PlayerID = playerID;
      this.StatCode = statCode;
      this.StatValue = statValue;

    }

    /// <summary>
    /// The sport for which the stat is relevant.
    /// </summary>
    public string SportCode {get;set;}

    /// <summary>
    /// The season for which the stat is relevant
    /// </summary>
    public string SeasonCode {get;set;}

    /// <summary>
    /// The week for which the stat is relevant
    /// </summary>
    public int Week { get; set; }

    /// <summary>
    /// The unique id of the player who recorded the stat
    /// </summary>
    public int PlayerID {get;set;}

    /// <summary>
    /// The unique id of the statistic
    /// </summary>
    public string StatCode {get;set;}

    /// <summary>
    /// The value associated with the statistic
    /// </summary>
    public double StatValue {get;set;}


    /// <summary>
    /// Returns the total number of cheat sheets belonging to a particular user.
    /// </summary>
    public static int GetSportSeasonPlayerWeeklyStatCount(string sportCode, string seasonCode, int week)
    {
      return SiteProvider.Sheets.GetSportSeasonPlayerWeeklyStatCount(sportCode, seasonCode, week);
    }

    /// <summary>
    /// Inserts a new weekly statistic for a player
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode"></param>
    /// <param name="playerID"></param>
    /// <param name="statCode"></param>
    /// <param name="statValue"></param>
    /// <returns></returns>
    public static int InsertSportSeasonPlayerWeeklyStat(string sportCode, string seasonCode, int week, int playerID, string statCode, double statValue)
    {

      // create an article entity to inser and use the specific provider to do the insert
      SportSeasonPlayerWeeklyStatDetails record = new SportSeasonPlayerWeeklyStatDetails(sportCode, seasonCode, week, playerID, statCode, statValue);
      int ret = SiteProvider.Sheets.InsertSportSeasonPlayerWeeklyStat(record);

      BizObject.PurgeCacheItems("Sheets_SportSeasonPlayerWeeklyStatsBySportSeasonWeekPlayerID_" + sportCode + "_" + seasonCode + "_" + week.ToString() + "_" + playerID.ToString());

      return ret;
    }


    /// <summary>
    /// Deletes all weekly stats for a particular player and particular week.  This is usually done when deleting a Player or  updating 
    /// player stats.
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode"></param>
    /// <param name="playerID"></param>
    /// <returns></returns>
    public static bool DeleteSportSeasonPlayerWeeklyStats(string sportCode, string seasonCode, int week, int playerID)
    {
      bool ret = SiteProvider.Sheets.DeleteSportSeasonPlayerWeeklyStats(sportCode, seasonCode, week, playerID);
      BizObject.PurgeCacheItems("Sheets_SportSeasonPlayerWeeklyStats");
      return ret;
    }

    /// <summary>
    /// Deletes all weekly stats for a particular week.  This is usually done while importing weekly stats 
    /// player stats.
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode"></param>
    /// <param name="playerID"></param>
    /// <returns></returns>
    public static bool DeleteSportSeasonPlayerWeeklyStats(string sportCode, string seasonCode, int week, string positionCode)
    {
      bool ret = SiteProvider.Sheets.DeleteSportSeasonPlayerWeeklyStats(sportCode, seasonCode, week, positionCode);
      BizObject.PurgeCacheItems("Sheets_SportSeasonPlayerWeeklyStats");
      return ret;
    }


    /// <summary>
    /// Returns a PlayerStat object filled with the data taken from the input PlayerStatDetails
    /// </summary>
    private static SportSeasonPlayerWeeklyStat GetSportSeasonPlayerWeeklyStatFromSportSeasonPlayerWeeklyStatDetails(SportSeasonPlayerWeeklyStatDetails playerStat)
    {
      if (playerStat == null)
        return null;
      else
      {
        return new SportSeasonPlayerWeeklyStat(playerStat.SportCode, playerStat.SeasonCode, playerStat.Week, 
                    playerStat.PlayerID, playerStat.StatCode.Trim(), playerStat.StatValue);
      }
    }

    /// <summary>
    /// Returns a list of PlayerStat objects filled with the data taken from the input list of PlayerStatDetails
    /// </summary>
    private static List<SportSeasonPlayerWeeklyStat> GetSportSeasonPlayerWeeklyStatListFromSportSeasonPlayerWeeklyStatDetailsList(List<SportSeasonPlayerWeeklyStatDetails> recordset)
    {
      List<SportSeasonPlayerWeeklyStat> sportSeasonPlayerWeeklyStats = new List<SportSeasonPlayerWeeklyStat>();
      foreach (SportSeasonPlayerWeeklyStatDetails record in recordset)
        sportSeasonPlayerWeeklyStats.Add(GetSportSeasonPlayerWeeklyStatFromSportSeasonPlayerWeeklyStatDetails(record));
      return sportSeasonPlayerWeeklyStats;
    }
  }
}