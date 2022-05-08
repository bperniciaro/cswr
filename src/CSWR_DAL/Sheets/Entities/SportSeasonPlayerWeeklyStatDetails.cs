/// <summary>
/// Summary description for PlayerStatDetails
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class SportSeasonPlayerWeeklyStatDetails
  {
    public SportSeasonPlayerWeeklyStatDetails(string sportCode, string seasonCode, int week, int playerID, string statCode, double statValue)
    {
      this.SportCode = sportCode;
      this.SeasonCode = seasonCode;
      this.Week = week;
      this.PlayerID = playerID;
      this.StatCode = statCode;
      this.StatValue = statValue;

    }

    // Sport Code
    public string SportCode { get; set; }

    // Season Code
    public string SeasonCode { get; set; }

    // Week;
    public int Week { get; set; }

    // Player ID
    public int PlayerID { get; set; }

    // Stat Code
    public string StatCode { get; set; }

    // Stat Value
    public double StatValue { get; set; }
  }
}