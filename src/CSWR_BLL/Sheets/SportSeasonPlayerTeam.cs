using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Summary description for PlayerStatDetails
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class SportSeasonPlayerTeam : BaseSheet
  {
    public SportSeasonPlayerTeam(string sportCode, string seasonCode, int playerID, string teamCode)
    {
      this.SportCode = sportCode;
      this.SeasonCode = seasonCode;
      this.PlayerID = playerID;
      this.TeamCode = teamCode;
    }

    // Sport Code
    public string SportCode {get;set;}
    // Season Code
    public string SeasonCode {get;set;}
    // Player ID
    public int PlayerID {get;set;}
    // Stat Code
    public string TeamCode { get; set; }



    /// <summary>
    /// Returns the team for a particular player
    /// </summary>
    public static SportSeasonPlayerTeam GetSportSeasonPlayerTeam(string sportCode, string seasonCode, int playerID)
    {
      SportSeasonPlayerTeam sportSeasonPlayerTeam = null;
      string key = "Sheets_SportSeasonPlayerTeamBySportSeasonPlayer_" + sportCode + "_" + seasonCode + "_" + playerID.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        sportSeasonPlayerTeam = (SportSeasonPlayerTeam)BizObject.Cache[key];
      }
      else
      {
        SportSeasonPlayerTeamDetails recordset = SiteProvider.Sheets.GetSportSeasonPlayerTeam(sportCode, seasonCode, playerID);
        sportSeasonPlayerTeam = GetSportSeasonPlayerTeamFromSportSeasonPlayerTeamDetails(recordset);
        BaseSheet.CacheData(key, sportSeasonPlayerTeam);
      }
      return sportSeasonPlayerTeam;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode"></param>
    /// <param name="playerId"></param>
    /// <param name="teamCode"></param>
    /// <returns></returns>
    public static int InsertSportSeasonPlayerTeam(string sportCode, string seasonCode, int playerId, string teamCode)
    {
      // create an article entity to inser and use the specific provider to do the insert
      SportSeasonPlayerTeamDetails record = new SportSeasonPlayerTeamDetails(sportCode, seasonCode, playerId, teamCode);
      return SiteProvider.Sheets.InsertSportSeasonPlayerTeam(record);
    }








    /// <summary>
    /// Returns a PlayerStat object filled with the data taken from the input PlayerStatDetails
    /// </summary>
    private static SportSeasonPlayerTeam GetSportSeasonPlayerTeamFromSportSeasonPlayerTeamDetails(SportSeasonPlayerTeamDetails sportSeasonPlayerTeam)
    {
      if (sportSeasonPlayerTeam == null)
        return null;
      else
      {
        return new SportSeasonPlayerTeam(sportSeasonPlayerTeam.SportCode, sportSeasonPlayerTeam.SeasonCode, sportSeasonPlayerTeam.PlayerID, sportSeasonPlayerTeam.TeamCode);
      }
    }

    /// <summary>
    /// Returns a list of PlayerStat objects filled with the data taken from the input list of PlayerStatDetails
    /// </summary>
    //private static List<SportSeasonPlayerTeam> GetSportSeasonPlayerTeamListFromSportSeasonPlayerSeasonStatDetailsList(List<SportSeasonPlayerSeasonStatDetails> recordset)
    //{
    //  List<SportSeasonPlayerSeasonStat> sportSeasonPlayerSeasonStats = new List<SportSeasonPlayerSeasonStat>();
    //  foreach (SportSeasonPlayerSeasonStatDetails record in recordset)
    //    sportSeasonPlayerSeasonStats.Add(GetSportSeasonPlayerSeasonStatFromSportSeasonPlayerSeasonStatDetails(record));
    //  return sportSeasonPlayerSeasonStats;
    //}

  }
}