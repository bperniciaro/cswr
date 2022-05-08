using System.Collections.Generic;
using System.Linq;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Summary description for PlayerStat
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class SportSeasonPlayerSeasonStat : BaseSheet
  {
    public SportSeasonPlayerSeasonStat(string sportCode, string seasonCode, int playerID, string statCode, double statValue)
    {
      this.SportCode = sportCode;
      this.SeasonCode = seasonCode;
      this.PlayerID = playerID;
      this.StatCode = statCode;
      this.StatValue = statValue;
   }

    public SportSeasonPlayerSeasonStat() { }

    /// <summary>
    /// The sport for which the stat is relevant.
    /// </summary>
    public string SportCode {get;set;}

    /// <summary>
    /// The season for which the stat is relevant
    /// </summary>
    public string SeasonCode {get;set;}

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
    /// A lazy-load reference to the Player referenced in the SheetItem
    /// </summary>
    private Player _player = null;
    public Player Player
    {
      get
      {
        if (_player == null)
        {
          _player = Player.GetPlayer(this.PlayerID);
        }
        return _player;
      }
    }


    /// <summary>
    /// Returns all stats for a player for a particular sport/season pair
    /// </summary>
    public static List<SportSeasonPlayerSeasonStat> GetSportSeasonPlayerSeasonStats(string sportCode, string seasonCode, int playerID, bool pprLeague = false)
    {
      List<SportSeasonPlayerSeasonStat> sportSeasonPlayerSeasonStats = null;
      string key = "Sheets_SportSeasonPlayerSeasonStatsBySportSeasonPlayerID_" + sportCode + "_" + seasonCode + "_" + playerID.ToString() + "_" + pprLeague.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        sportSeasonPlayerSeasonStats = (List<SportSeasonPlayerSeasonStat>)BizObject.Cache[key];
      }
      else
      {
        List<SportSeasonPlayerSeasonStatDetails> recordset = SiteProvider.Sheets.GetSportSeasonPlayerSeasonStats(sportCode, seasonCode, playerID);
        sportSeasonPlayerSeasonStats = GetSportSeasonPlayerSeasonStatListFromSportSeasonPlayerSeasonStatDetailsList(recordset);

        // only provide relevant stats for PPR vs Standard Scoring
        if (pprLeague)
        {
          sportSeasonPlayerSeasonStats.Remove(sportSeasonPlayerSeasonStats.SingleOrDefault(x => x.StatCode == "FPPG"));
          sportSeasonPlayerSeasonStats.Remove(sportSeasonPlayerSeasonStats.SingleOrDefault(x => x.StatCode == "TFP"));
        }
        else
        {
          sportSeasonPlayerSeasonStats.Remove(sportSeasonPlayerSeasonStats.SingleOrDefault(x => x.StatCode == "FPGP"));
          sportSeasonPlayerSeasonStats.Remove(sportSeasonPlayerSeasonStats.SingleOrDefault(x => x.StatCode == "TFPP"));
        }

        BaseSheet.CacheData(key, sportSeasonPlayerSeasonStats);
      }
      return sportSeasonPlayerSeasonStats.GetRange(0, sportSeasonPlayerSeasonStats.Count);
    }



    /// <summary>
    /// Returns all stats for all player for a particular sport/season pair
    /// </summary>
    public static List<SportSeasonPlayerSeasonStat> GetSportSeasonPlayerSeasonStats(string sportCode, string seasonCode)
    {
      List<SportSeasonPlayerSeasonStat> sportSeasonPlayerSeasonStats = null;
      string key = "Sheets_SportSeasonPlayerSeasonStatsBySportSeason_" + sportCode + "_" + seasonCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        sportSeasonPlayerSeasonStats = (List<SportSeasonPlayerSeasonStat>)BizObject.Cache[key];
      }
      else
      {
        List<SportSeasonPlayerSeasonStatDetails> recordset = SiteProvider.Sheets.GetSportSeasonPlayerSeasonStats(sportCode, seasonCode);
        sportSeasonPlayerSeasonStats = GetSportSeasonPlayerSeasonStatListFromSportSeasonPlayerSeasonStatDetailsList(recordset);
        BaseSheet.CacheData(key, sportSeasonPlayerSeasonStats);
      }
      return sportSeasonPlayerSeasonStats.GetRange(0, sportSeasonPlayerSeasonStats.Count);
    }


    /// <summary>
    /// Returns all stats for all players for a particular sport/season/position/stat
    /// </summary>
    public static List<SportSeasonPlayerSeasonStat> GetSportSeasonPlayerSeasonStats(string sportCode, string seasonCode, string positionCode, string statCode)
    {
      List<SportSeasonPlayerSeasonStat> sportSeasonPlayerSeasonStats = null;
      string key = "Sheets_SportSeasonPlayerSeasonStatsBySportSeasonPositionStat_" + sportCode + "_" + seasonCode + "_" + positionCode + "_" + statCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        sportSeasonPlayerSeasonStats = (List<SportSeasonPlayerSeasonStat>)BizObject.Cache[key];
      }
      else
      {
        List<SportSeasonPlayerSeasonStatDetails> recordset = SiteProvider.Sheets.GetSportSeasonPlayerSeasonStats(sportCode, seasonCode, positionCode, statCode);
        sportSeasonPlayerSeasonStats = GetSportSeasonPlayerSeasonStatListFromSportSeasonPlayerSeasonStatDetailsList(recordset);
        BaseSheet.CacheData(key, sportSeasonPlayerSeasonStats);
      }
      return sportSeasonPlayerSeasonStats.GetRange(0, sportSeasonPlayerSeasonStats.Count);
    }

    /// <summary>
    /// Returns a particular stat for a particular player based on a sport/season/player triad
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode"></param>
    /// <param name="playerID"></param>
    /// <param name="statCode"></param>
    /// <returns></returns>
    public static SportSeasonPlayerSeasonStat GetSportSeasonPlayerSeasonStat(string sportCode, string seasonCode, int playerID, string statCode)
    {
      SportSeasonPlayerSeasonStat sportSeasonPlayerSeasonStat;

      string key = "Sheets_SportSeasonPlayerSeasonStatBySportSeasonPlayerStat_" + sportCode + "_" + seasonCode + "_" + playerID + "_" + statCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        sportSeasonPlayerSeasonStat = (SportSeasonPlayerSeasonStat)BizObject.Cache[key];
      }
      else
      {
        sportSeasonPlayerSeasonStat = GetSportSeasonPlayerSeasonStatFromSportSeasonPlayerSeasonStatDetails(SiteProvider.Sheets.GetSportSeasonPlayerSeasonStat(sportCode, seasonCode, playerID, statCode));
        BaseSheet.CacheData(key, sportSeasonPlayerSeasonStat);
      }
      return sportSeasonPlayerSeasonStat;
    }


    /// <summary>
    /// Inserts a new season statistic for a player
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode"></param>
    /// <param name="playerID"></param>
    /// <param name="statCode"></param>
    /// <param name="statValue"></param>
    /// <returns></returns>
    public static int InsertSportSeasonPlayerSeasonStat(string sportCode, string seasonCode, int playerID, string statCode, double statValue)
    {

      // create an article entity to inser and use the specific provider to do the insert
      SportSeasonPlayerSeasonStatDetails record = new SportSeasonPlayerSeasonStatDetails(sportCode, seasonCode, playerID, statCode, statValue);
      int ret = SiteProvider.Sheets.InsertSportSeasonPlayerSeasonStat(record);

      BizObject.PurgeCacheItems("Sheets_SportSeasonPlayerSeasonStatsBySportSeasonPlayerID_" + sportCode + "_" + seasonCode + "_" + playerID.ToString());
      
      return ret;
    }


    /// <summary>
    /// Update a particular season stat for a particular player and sport/season pair
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode"></param>
    /// <param name="playerID"></param>
    /// <param name="statCode"></param>
    /// <param name="statValue"></param>
    /// <returns></returns>
    public static bool UpdateSportSeasonPlayerSeasonStat(string sportCode, string seasonCode, int playerID, string statCode, double statValue)
    {

      // build an article entity to update, then use the module specific provider to update it
      SportSeasonPlayerSeasonStatDetails record = new SportSeasonPlayerSeasonStatDetails(sportCode, seasonCode, playerID, statCode, statValue);
      bool ret = SiteProvider.Sheets.UpdateSportSeasonPlayerSeasonStat(record);

      BizObject.PurgeCacheItems("Sheets_SportSeasonPlayerSeasonStatsBySportSeasonPlayerID_" + sportCode + "_" + seasonCode + "_" + playerID.ToString());
      BizObject.PurgeCacheItems("Sheets_SportSeasonPlayerSeasonStatBySportSeasonPlayerStat_" + sportCode + "_" + seasonCode + "_" + playerID + "_" + statCode);
      return ret;
    }

    /// <summary>
    /// Deletes all season stats for a particular player.  This is usually done when deleting a Player
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode"></param>
    /// <param name="playerID"></param>
    /// <returns></returns>
    public static bool DeleteSportSeasonPlayerSeasonStats(string sportCode, string seasonCode, int playerID)
    {
      bool ret = SiteProvider.Sheets.DeleteSportSeasonPlayerSeasonStats(sportCode, seasonCode, playerID);
      BizObject.PurgeCacheItems("Sheets_SportSeasonPlayerSeasonStats");
      return ret;
    }


    /// <summary>
    /// Deletes all season stats for a particular season.  This is usually done when importing seasonal stats
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode"></param>
    /// <param name="playerID"></param>
    /// <returns></returns>
    public static bool DeleteSportSeasonPlayerSeasonStats(string sportCode, string seasonCode, string positionCode)
    {
      bool ret = SiteProvider.Sheets.DeleteSportSeasonPlayerSeasonStats(sportCode, seasonCode, positionCode);
      BizObject.PurgeCacheItems("Sheets_SportSeasonPlayerSeasonStats");
      return ret;
    }



    /// <summary>
    /// Returns a PlayerStat object filled with the data taken from the input PlayerStatDetails
    /// </summary>
    private static SportSeasonPlayerSeasonStat GetSportSeasonPlayerSeasonStatFromSportSeasonPlayerSeasonStatDetails(SportSeasonPlayerSeasonStatDetails playerStat)
    {
      if (playerStat == null)
        return null;
      else
      {
        return new SportSeasonPlayerSeasonStat(playerStat.SportCode, playerStat.SeasonCode, playerStat.PlayerID, playerStat.StatCode.Trim(), playerStat.StatValue);
      }
    }

    /// <summary>
    /// Returns a list of PlayerStat objects filled with the data taken from the input list of PlayerStatDetails
    /// </summary>
    private static List<SportSeasonPlayerSeasonStat> GetSportSeasonPlayerSeasonStatListFromSportSeasonPlayerSeasonStatDetailsList(List<SportSeasonPlayerSeasonStatDetails> recordset)
    {
      List<SportSeasonPlayerSeasonStat> sportSeasonPlayerSeasonStats = new List<SportSeasonPlayerSeasonStat>();
      foreach (SportSeasonPlayerSeasonStatDetails record in recordset)
        sportSeasonPlayerSeasonStats.Add(GetSportSeasonPlayerSeasonStatFromSportSeasonPlayerSeasonStatDetails(record));
      return sportSeasonPlayerSeasonStats;
    }
  }
}