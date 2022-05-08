/// <summary>
/// Summary description for CheatSheet
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class UserSheetPlayerDifferentialDetails
  {
    public UserSheetPlayerDifferentialDetails(int userSheetPlayerDifferentialID, string seasonCode, string sportCode, string positionCode, 
                                           int playerID, decimal averageDifferential)
    {
      this.UserSheetPlayerDifferentialID = userSheetPlayerDifferentialID;
      this.SeasonCode = seasonCode;
      this.SportCode = sportCode;
      this.PositionCode = positionCode;
      this.PlayerID = playerID;
      this.AverageDifferential = averageDifferential;
    }

    public int UserSheetPlayerDifferentialID {get;set;}
    public string SeasonCode { get; set; }
    public string SportCode { get; set; }
    public string PositionCode { get; set; }
    public int PlayerID { get; set; }
    public decimal AverageDifferential { get; set; }
  }
}