using System;

/// <summary>
/// Summary description for SeasonDetails
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class ADPCalculationDetails
  {
    public ADPCalculationDetails(int adpCalculationID, string sportCode, string seasonCode, string positionCode, DateTime calcTimestamp,
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

    public int ADPCalculationID { get; set; }
    public string SeasonCode { get; set; }
    public string SportCode { get; set; }
    public string PositionCode { get; set; }
    public int TimespanInDays { get; set; }
    public int TotalSheetsConsidered { get; set; }
    public int Last24Sheets { get; set; }
    public int Last48Sheets { get; set; }
    public int Last72Sheets { get; set; }
    public DateTime CalcTimestamp { get; set; }
  }
}