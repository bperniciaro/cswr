using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Records the last time that ADP calculations were made for a sport/season/position triad.  We use this, along with an 
/// interval setting, to determine when to perform another ADP calculation.
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class ADPCalculation : BaseSheet
  {
    public ADPCalculation(int adpCalculationID, string sportCode, string seasonCode, string positionCode, DateTime calcTimestamp, 
                          int totalSheetsConsidered, int last24Sheets, int last48Sheets, int last72Sheets, int timespanInDays)
    {
      this.ADPCalculationID = adpCalculationID;
      this.SportCode = sportCode;
      this.SeasonCode = seasonCode;
      this.PositionCode = positionCode;
      this.TimespanInDays = timespanInDays;
      this.CalcTimestamp = calcTimestamp;
      this.TotalSheetsConsidered = totalSheetsConsidered;
      this.Last24Sheets = last24Sheets;
      this.Last48Sheets = last48Sheets;
      this.Last72Sheets = last72Sheets;
    }

    /// <summary>
    /// The unique ID for the calculation
    /// </summary>
    public int ADPCalculationID { get; set; }

    /// <summary>
    /// The sport for which the ADP calculation was made.
    /// </summary>
    public string SportCode {get;set;}

    /// <summary>
    /// The season for which the ADP calculation was made
    /// </summary>
    public string SeasonCode {get;set;}

    /// <summary>
    /// The position for which the ADP calculation was made
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
        _positionCode = value.Trim();
      }
    }

    /// <summary>
    /// The past number of days considered in the ADP calculation
    /// </summary>
    public int TimespanInDays { get; set; }

    /// <summary>
    /// The date and time in which the ADP calculation was made
    /// </summary>
    public DateTime CalcTimestamp {get;set;}

    /// <summary>
    /// The total number of sheets considered when calculating ADP
    /// </summary>
    public int TotalSheetsConsidered { get; set; }

    /// <summary>
    /// The total number of sheets considered when calculating ADP which were created/modified in the last 24 hours
    /// </summary>
    public int Last24Sheets { get; set; }

    /// <summary>
    /// The total number of sheets considered when calculating ADP which were created/modified in the last 48 hours
    /// </summary>
    public int Last48Sheets { get; set; }

    /// <summary>
    /// The total number of sheets considered when calculating ADP which were created/modified in the last 72 hours
    /// </summary>
    public int Last72Sheets { get; set; }

    /// <summary>
    /// Returns the count of ADP calculations 
    /// </summary>
    public static int GetADPCalculationCount(string seasonCode, string sportCode, string positionCode, DateTime calcTimestamp)
    {
      return SiteProvider.Sheets.GetADPCalculationCount(seasonCode, sportCode, positionCode, calcTimestamp);
    }

    /// <summary>
    /// Updates an ADP timestamp with a new timestamp.
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode"></param>
    /// <param name="positionCode"></param>
    /// <param name="calcTimestamp"></param>
    /// <returns></returns>
    //public static bool UpdateADPCalculation(int adpCalculationID, string sportCode, string seasonCode, string positionCode, DateTime calcTimestamp)
    //{
    //  ADPCalculationDetails ADPCalculation = new ADPCalculationDetails(adpCalculationID, sportCode, seasonCode, positionCode, calcTimestamp);
    //  return SiteProvider.Sheets.UpdateADPCalculation(ADPCalculation);
    //}


    /// <summary>
    /// Inserts a new ADP timestamp.  This is usually done the first time ADP is calculated for a sport/season/position triad.
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode"></param>
    /// <param name="positionCode"></param>
    /// <param name="calcTimestamp"></param>
    public static int InsertADPCalculation(string sportCode, string seasonCode, string positionCode, DateTime calcTimestamp, 
                                           int totalSheetsConsidered, int last24Sheets, int last48Sheets, int last72Sheets, int timespanInDays)
    {
      ADPCalculationDetails record = new ADPCalculationDetails(0, sportCode, seasonCode, positionCode, calcTimestamp, totalSheetsConsidered, 
                                                               last24Sheets, last48Sheets, last72Sheets, timespanInDays);
      return SiteProvider.Sheets.InsertADPCalculation(record);
    }


    public static List<ADPCalculation> GetADPCalculations(string sportCode, string seasonCode, string positionCode)
    {
      List<ADPCalculation> adpPlayerLogs = null;
      string key = "Sheets_ADPCalculationsBySportSeasonPosition_" + sportCode + "_" + seasonCode + "_" + positionCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        adpPlayerLogs = (List<ADPCalculation>)BizObject.Cache[key];
      }
      else
      {
        List<ADPCalculationDetails> recordset = SiteProvider.Sheets.GetADPCalculations(sportCode, seasonCode, positionCode);
        adpPlayerLogs = GetADPCalculationFromADPCalculationDetailsList(recordset);
        BaseSheet.CacheData(key, adpPlayerLogs);
      }
      return adpPlayerLogs.GetRange(0, adpPlayerLogs.Count);
    }

    public static List<ADPCalculation> GetADPCalculations(string sportCode, string seasonCode)
    {
      List<ADPCalculation> adpPlayerLogs = null;
      string key = "Sheets_ADPCalculationsBySportSeason_" + sportCode + "_" + seasonCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        adpPlayerLogs = (List<ADPCalculation>)BizObject.Cache[key];
      }
      else
      {
        List<ADPCalculationDetails> recordset = SiteProvider.Sheets.GetADPCalculations(sportCode, seasonCode);
        adpPlayerLogs = GetADPCalculationFromADPCalculationDetailsList(recordset);
        BaseSheet.CacheData(key, adpPlayerLogs);
      }
      return adpPlayerLogs.GetRange(0, adpPlayerLogs.Count);
    }


    /// <summary>
    /// This method is used to get all of the ADP calculations made on a specific day so that we can remove
    /// any duplicates if necessary
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode"></param>
    /// <param name="calcTimestamp"></param>
    /// <returns></returns>
    public static List<ADPCalculation> GetADPCalculations(string sportCode, string seasonCode, string positionCode, DateTime calcTimestamp)
    {
      List<ADPCalculationDetails> recordset = SiteProvider.Sheets.GetADPCalculations(sportCode, seasonCode, positionCode, calcTimestamp);
      return GetADPCalculationFromADPCalculationDetailsList(recordset);
    }


    /// <summary>
    /// Used to delete any extra ADP calculations on each day
    /// </summary>
    public static bool DeleteADPCalculation(int adpCalculationID)
    {
      return SiteProvider.Sheets.DeleteADPCalculation(adpCalculationID);
    }


    /// <summary>
    /// Converts an ADPCalculationDetails entity into an ADPCalculation business-level domain object.
    /// </summary>
    /// <param name="cheatSheet">A cheat sheet entity object.</param>
    /// <returns>A cheat sheet domain object.</returns>
    private static ADPCalculation GetADPCalculationFromADPCalculationDetails(ADPCalculationDetails adpCalculation)
    {
      if (adpCalculation == null)
        return null;
      else
        return new ADPCalculation(adpCalculation.ADPCalculationID, adpCalculation.SeasonCode, adpCalculation.SportCode, adpCalculation.PositionCode,
                                  adpCalculation.CalcTimestamp, adpCalculation.TotalSheetsConsidered, adpCalculation.Last24Sheets,
                                  adpCalculation.Last48Sheets, adpCalculation.Last72Sheets, adpCalculation.TimespanInDays);
    }

    /// <summary>
    /// Converts a collection of ADPCalculationDetails entities into a collection of ADPCalculationDetails business-level domain objects.
    /// </summary>
    /// <param name="cheatSheet">A collection of adpPlayerLog entity objects.</param>
    /// <returns>A collection cheat sheet domain objects.</returns>
    private static List<ADPCalculation> GetADPCalculationFromADPCalculationDetailsList(List<ADPCalculationDetails> recordset)
    {
      List<ADPCalculation> ADPCalculations = new List<ADPCalculation>();
      foreach (ADPCalculationDetails record in recordset)
        ADPCalculations.Add(GetADPCalculationFromADPCalculationDetails(record));
      return ADPCalculations;
    }
 
  }
}