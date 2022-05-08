/// <summary>
/// Summary description for CheatSheet
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class UserSheetPositionGradeDetails
  {
    public UserSheetPositionGradeDetails(int userSheetPositionGradeID, string seasonCode, string sportCode, string positionCode, 
                                           int archivedCheatSheetID, string userName, int totalRankDifferential, int score, int rank)
    {
      this.UserSheetPositionGradeID = userSheetPositionGradeID;
      this.SeasonCode = seasonCode;
      this.SportCode = sportCode;
      this.PositionCode = positionCode;
      this.ArchivedCheatSheetID = archivedCheatSheetID;
      this.UserName = userName;
      this.TotalRankDifferential = totalRankDifferential;
      this.Score = score;
      this.Rank = rank;
    }

    public int UserSheetPositionGradeID {get;set;}
    public string SeasonCode { get; set; }
    public string SportCode { get; set; }
    public string PositionCode { get; set; }
    public int ArchivedCheatSheetID { get; set; }
    public string UserName { get; set; }
    public int TotalRankDifferential { get; set; }
    public int Score { get; set; }
    public int Rank { get; set; }

  }
}