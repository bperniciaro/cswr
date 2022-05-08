/// <summary>
/// Summary description for SportDetails
/// </summary>
/// 
namespace BP.CheatSheetWarRoom.DAL
{
  public class SportDetails
  {
    public SportDetails(string sportCode, string sportName, string leagueName, string leagueAbbreviation)
    {
      this.SportCode = sportCode;
      this.SportName = sportName;
      this.LeagueName = leagueName;
      this.LeagueAbbreviation = leagueAbbreviation;
    }

    // Sport Code
    private string _sportCode = "";
    public string SportCode  {
      get { return _sportCode; }
      set { _sportCode = value; }
    }

    // Sport Name
    private string _sportName = "";
    public string SportName
    {
      get { return _sportName; }
      set { _sportName = value; }
    }

    // League Name
    private string _leagueName = "";
    public string LeagueName
    {
      get { return _leagueName; }
      set { _leagueName = value; }
    }

    // League Abbreviation
    private string _leagueAbbreviation = "";
    public string LeagueAbbreviation
    {
      get { return _leagueAbbreviation; }
      set { _leagueAbbreviation = value; }
    }

  }
}