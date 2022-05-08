using System;

/// <summary>
/// Summary description for SeasonDetails
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class ADPPlayerLogDetails
  {
    public ADPPlayerLogDetails(int adpPlayerLogID, int adpCalculationID, string sportCode, string seasonCode, int playerID, double adp, DateTime calcTimestamp)
    {
      this.ADPCalculationID = adpCalculationID;
      this.ADPPlayerLogID = adpPlayerLogID;
      this.SportCode = sportCode;
      this.SeasonCode = seasonCode;
      this.PlayerID = playerID;
      this.ADP = adp;
      this.CalcTimestamp = calcTimestamp;
    }

    public int ADPPlayerLogID  { get; set; }
    public int ADPCalculationID { get; set; }
    public string SportCode { get; set; }
    public string SeasonCode { get; set; }
    public int PlayerID { get;  set; }
    public double ADP { get; set; }
    public DateTime CalcTimestamp { get; set; }

  }
}