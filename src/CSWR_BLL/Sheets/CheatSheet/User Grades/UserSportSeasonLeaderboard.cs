using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// This is the class for a cheat sheet, which descends from the base class BaseSheet
/// </summary>
/// 

namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class UserSportSeasonLeaderboard : BaseSheet
  {
    public UserSportSeasonLeaderboard(int userSheetLeaderboardID, string sportCode, string seasonCode, string username,
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



    /// <summary>
    /// Returns the distinct years which have been populated with user grades
    /// </summary>
    public static List<string> GetLeaderboardYears(string sportCode)
    {
      List<string> leaderboardYears;
      var key = "Sheets_LeaderboardYears_" + sportCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        leaderboardYears = (List<string>)BizObject.Cache[key];
      }
      else
      {
        leaderboardYears = SiteProvider.Sheets.GetLeaderboardYears(sportCode);
        BaseSheet.CacheData(key, leaderboardYears);
      }

      return leaderboardYears.GetRange(0, leaderboardYears.Count);

    }



    /// <summary>
    /// Returns a collection with all the cheatsheetitems.  We don't cache here because we don't want this cached collection to
    /// interfere with a RAC or FOO collection since there are fewer properties here and the keys would be the same
    /// </summary>
    public static List<UserSportSeasonLeaderboard> GetUserSportSeasonLeaderboards(string sportCode, string seasonCode)
    {
      List<UserSportSeasonLeaderboard> userSportSeasonLeaderboards = null;
      string key = "Sheets_UserSportSeasonLeaderboardsBySportSeasonCodes_" + sportCode + "_" + seasonCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        userSportSeasonLeaderboards = (List<UserSportSeasonLeaderboard>)BizObject.Cache[key];
      }
      else
      {
        List<UserSportSeasonLeaderboardDetails> recordset = SiteProvider.Sheets.GetUserSportSeasonLeaderboards(sportCode, seasonCode);
        userSportSeasonLeaderboards = GetUserSportSeasonLeaderboardListFromGetUserSportSeasonLeaderboardList(recordset);
        BaseSheet.CacheData(key, userSportSeasonLeaderboards);
      }

      return userSportSeasonLeaderboards.GetRange(0, userSportSeasonLeaderboards.Count);

    }


    /// <summary>
    /// Inserts a user sheet positional grade
    /// </summary>
    /// <param name="CheatSheetItem"></param>
    /// <returns></returns>
    public static int InsertUserSportSeasonLeaderboard(string sportCode, string seasoncode, string username, int QBScore,
                                                    int RBScore, int WRScore, int TEScore, int KScore, 
                                                    int DFScore, int overallScore, int rank)
    {

      UserSportSeasonLeaderboardDetails record = new UserSportSeasonLeaderboardDetails(0, sportCode, seasoncode, username,
                                                       QBScore, RBScore, WRScore, TEScore, KScore,
                                                       DFScore, overallScore, rank);

      return SiteProvider.Sheets.InsertUserSportSeasonLeaderboard(record);
    }


    /// <summary>
    /// Converts a entity into a business-level domain object.
    /// </summary>
    /// <param name="cheatSheet">A cheat sheet entity object.</param>
    /// <returns>A cheat sheet domain object.</returns>
    private static UserSportSeasonLeaderboard GetUserSportSeasonLeaderboardFromUserSportSeasonLeaderboardDetails(UserSportSeasonLeaderboardDetails userSportSeasonLeaderboard)
    {
      if (userSportSeasonLeaderboard == null)
        return null;
      else
      {
        return new UserSportSeasonLeaderboard(userSportSeasonLeaderboard.UserSheetLeaderboardID, userSportSeasonLeaderboard.SportCode,
                                            userSportSeasonLeaderboard.SeasonCode, userSportSeasonLeaderboard.Username, 
                                            userSportSeasonLeaderboard.QBScore, userSportSeasonLeaderboard.RBScore,
                                            userSportSeasonLeaderboard.WRScore, userSportSeasonLeaderboard.TEScore,
                                            userSportSeasonLeaderboard.KScore, userSportSeasonLeaderboard.DFScore,
                                            userSportSeasonLeaderboard.OverallScore, userSportSeasonLeaderboard.Rank);
      }
    }


    /// <summary>
    /// Converts a collection of sheet entities into a collection of business-level domain objects.
    /// </summary>
    /// <param name="cheatSheet">A collection of cheat sheet entity objects.</param>
    /// <returns>A collection cheat sheet domain objects.</returns>
    private static List<UserSportSeasonLeaderboard> GetUserSportSeasonLeaderboardListFromGetUserSportSeasonLeaderboardList(List<UserSportSeasonLeaderboardDetails> recordset)
    {
      List<UserSportSeasonLeaderboard> UserSheetPositionGrade = new List<UserSportSeasonLeaderboard>();
      foreach (UserSportSeasonLeaderboardDetails record in recordset)
        UserSheetPositionGrade.Add(GetUserSportSeasonLeaderboardFromUserSportSeasonLeaderboardDetails(record));
      return UserSheetPositionGrade;
    }

  
  
  }
}
