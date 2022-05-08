/// <summary>
/// Summary description for SportDetails
/// </summary>
/// 
namespace BP.CheatSheetWarRoom.DAL
{
  public class SportSeasonDetails
  {
    public SportSeasonDetails(string sportCode, string seasonCode, bool currentSeason, bool seasonStarted, bool seasonEnded, bool someStatsLoaded)
    {
      this.SportCode = sportCode;
      this.SeasonCode = seasonCode;
      this.CurrentSeason = currentSeason;
      this.SeasonStarted = seasonStarted;
      this.SeasonEnded = seasonEnded;
      this.SomeStatsLoaded = someStatsLoaded;
    }

    // Sport Code
    public string SportCode { get; set; }

    // Season Code
    public string SeasonCode { get; set; }

    // Current Season
    public bool CurrentSeason { get; set; }

    // Season Started
    public bool SeasonStarted { get; set; }

    // Season Ended
    public bool SeasonEnded { get; set; }

    // StatsLoaded
    public bool SomeStatsLoaded { get; set; }
  }
}