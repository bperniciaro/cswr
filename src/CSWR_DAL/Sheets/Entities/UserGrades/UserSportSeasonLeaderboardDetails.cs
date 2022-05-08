/// <summary>
/// Summary description for CheatSheet
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class UserSportSeasonLeaderboardDetails
  {
    public UserSportSeasonLeaderboardDetails(int userSheetLeaderboardID, string sportCode, string seasonCode, string username,
                                           int QBScore, int RBScore, int WRScore, int TEScore, int KScore,
                                           int DFScore, int overallScore, int rank)
    {
      this.UserSheetLeaderboardID = userSheetLeaderboardID;
      this.SportCode = sportCode;
      this.SeasonCode = seasonCode;
      this.Username = username;
      this.QBScore = QBScore;
      this.RBScore = RBScore;
      this.WRScore = WRScore;
      this.TEScore = TEScore;
      this.KScore = KScore;
      this.DFScore = DFScore;
      this.OverallScore = overallScore;
      this.Rank = rank;

    }

    public int UserSheetLeaderboardID {get;set;}
    public string SportCode { get; set; }
    public string SeasonCode { get; set; }
    public string Username { get; set; }
    public int QBScore { get; set; }
    public int RBScore { get; set; }
    public int WRScore { get; set; }
    public int TEScore { get; set; }
    public int KScore { get; set; }
    public int DFScore { get; set; }
    public int OverallScore { get; set; }
    public int Rank { get; set; }
  
  }
}