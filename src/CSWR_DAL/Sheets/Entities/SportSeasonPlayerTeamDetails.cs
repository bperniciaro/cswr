/// <summary>
/// Summary description for PlayerStatDetails
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class SportSeasonPlayerTeamDetails
  {
    public SportSeasonPlayerTeamDetails(string sportCode, string seasonCode, int playerID, string teamCode)
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

  }
}