using System;
using System.Collections.Generic;
using System.Linq;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
///  Logs the previous ADP calculations for a particular player.  This can be used to track trending.
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class ADPPlayerLog : BaseSheet
  {
    public ADPPlayerLog(int adpPlayerLogID, int adpCalculationID, string sportCode, string seasonCode, int playerID, double adp, DateTime calcTimestamp)
    {
      this.ADPPlayerLogID = adpPlayerLogID;
      this.ADPCalculationID = adpCalculationID;
      this.SportCode = sportCode;
      this.SeasonCode = seasonCode;
      this.PlayerID = playerID;
      this.ADP = adp;
      this.CalcTimestamp = calcTimestamp;
    }

    /// <summary>
    /// This is the unique ID for the log entry.
    /// </summary>
    public int ADPPlayerLogID {get;set;}

    /// <summary>
    /// The ID from the ADPCalculation table.  It helps us determine which calculation this ADP is associated with
    /// </summary>
    public int ADPCalculationID { get; set; }

    /// <summary>
    /// The sport of the player whose ADP was calculated
    /// </summary>
    public string SportCode {get;set;}



    /// <summary>
    /// The season for which the ADP was relevant
    /// </summary>
    public string SeasonCode {get;set;}

    /// <summary>
    /// The unique ID for the player whose ADP was calculated
    /// </summary>
    public int PlayerID {get;set;}

    /// <summary>
    /// The actual ADP which was calculated for the player
    /// </summary>
    public double ADP {get;set;}

    /// <summary>
    /// The date and time which the ADP was calculated
    /// </summary>
    public DateTime CalcTimestamp {get;set;}

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
    /// Inserts a new ADP player log each time ADP is calculated
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode"></param>
    /// <param name="playerID"></param>
    /// <param name="adp"></param>
    /// <param name="calcTimestamp"></param>
    /// <returns></returns>
    public static int InsertADPPlayerLog(int adpCalculationID, string sportCode, string seasonCode, int playerID, double adp, DateTime calcTimestamp)
    {

      // create an article entity to inser and use the specific provider to do the insert
      ADPPlayerLogDetails record = new ADPPlayerLogDetails(0, adpCalculationID, sportCode, seasonCode, playerID, adp, calcTimestamp);
      return SiteProvider.Sheets.InsertADPPlayerLog(record);
    }


    public static List<ADPPlayerLog> GetADPPlayerLogs(string sportCode, string seasonCode, string positionCode)
    {
      List<ADPPlayerLog> adpPlayerLogs = null;
      string key = "Sheets_LatestADPPlayerLogsBySportSeasonPosition_" + sportCode + "_" + seasonCode + "_" + positionCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        adpPlayerLogs = (List<ADPPlayerLog>)BizObject.Cache[key];
      }
      else
      {
        List<ADPPlayerLogDetails> recordset = SiteProvider.Sheets.GetADPPlayerLogs(sportCode, seasonCode, positionCode);
        adpPlayerLogs = GetADPPlayerLogListFromADPPlayerLogDetailsList(recordset);
        BaseSheet.CacheData(key, adpPlayerLogs);
      }
      adpPlayerLogs.OrderBy(x => x.ADP);
      return adpPlayerLogs.GetRange(0, adpPlayerLogs.Count);
    }


    public static List<ADPPlayerLog> GetADPPlayerLogs(string sportCode, string seasonCode, string positionCode, DateTime calcTimestamp)
    {
      List<ADPPlayerLog> adpPlayerLogs = null;
      string key = "Sheets_LatestADPPlayerLogsBySportSeasonPositionDate_" + sportCode + "_" + seasonCode + "_" + positionCode + "_" + calcTimestamp.ToShortDateString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        adpPlayerLogs = (List<ADPPlayerLog>)BizObject.Cache[key];
      }
      else
      {
        List<ADPPlayerLogDetails> recordset = SiteProvider.Sheets.GetADPPlayerLogs(sportCode, seasonCode, positionCode, calcTimestamp);
        adpPlayerLogs = GetADPPlayerLogListFromADPPlayerLogDetailsList(recordset);
        BaseSheet.CacheData(key, adpPlayerLogs);
      }
      adpPlayerLogs.OrderBy(x => x.ADP);
      return adpPlayerLogs.GetRange(0, adpPlayerLogs.Count);
    }


    /// <summary>
    /// Converts a adpPlayerLog entity into an adpPlayerLog business-level domain object.
    /// </summary>
    /// <param name="cheatSheet">A cheat sheet entity object.</param>
    /// <returns>A cheat sheet domain object.</returns>
    private static ADPPlayerLog GetADPPlayerLogFromADPPlayerLogDetails(ADPPlayerLogDetails adpPlayerLog)
    {
      if (adpPlayerLog == null)
        return null;
      else
        return new ADPPlayerLog(adpPlayerLog.ADPPlayerLogID, adpPlayerLog.ADPCalculationID, adpPlayerLog.SportCode, adpPlayerLog.SeasonCode, adpPlayerLog.PlayerID, adpPlayerLog.ADP, adpPlayerLog.CalcTimestamp);
    }

    /// <summary>
    /// Converts a collection of adpPlayerLog entities into a collection of adpPlayerLog business-level domain objects.
    /// </summary>
    /// <param name="cheatSheet">A collection of adpPlayerLog entity objects.</param>
    /// <returns>A collection cheat sheet domain objects.</returns>
    private static List<ADPPlayerLog> GetADPPlayerLogListFromADPPlayerLogDetailsList(List<ADPPlayerLogDetails> recordset)
    {
      List<ADPPlayerLog> adpPlayerLogs = new List<ADPPlayerLog>();
      foreach (ADPPlayerLogDetails record in recordset)
        adpPlayerLogs.Add(GetADPPlayerLogFromADPPlayerLogDetails(record));
      return adpPlayerLogs;
    }
  }
}