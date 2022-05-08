/// <summary>
/// Summary description for ByeWeekDetails
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class ByeWeekDetails
  {
    public ByeWeekDetails(string seasonCode, string sportCode, string teamCode, int byeWeek)
    {
      this.SeasonCode = seasonCode;
      this.SportCode = sportCode;
      this.TeamCode = teamCode;
      this.ByeWeek = byeWeek;
    }

    // Sport Code
    private string _sportCode = "";
    public string SportCode
    {
      get { return _sportCode; }
      set { _sportCode = value; }
    }

    // Season Code
    private string _seasonCode = "";
    public string SeasonCode
    {
      get { return _seasonCode; }
      set { _seasonCode = value; }
    }

    // Team Code
    private string _teamCode = "";
    public string TeamCode
    {
      get { return _teamCode; }
      set { _teamCode = value; }
    }

    // Bye Week
    private int _byeWeek = 0;
    public int ByeWeek
    {
      get { return _byeWeek; }
      set { _byeWeek = value; }
    }


  }
}