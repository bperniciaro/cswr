/// <summary>
/// Summary description for TeamDetails
/// </summary>
/// 
namespace BP.CheatSheetWarRoom.DAL
{
  public class TeamDetails
  {
    public TeamDetails() {}

    public TeamDetails(string teamCode, string sportCode, string city, string mascot, string abbreviation, int statMapID)
    {
      this.TeamCode = teamCode;
      this.SportCode = sportCode;
      this.City = city;
      this.Mascot = mascot;
      this.Abbreviation = abbreviation;
      this.StatMapID = statMapID;
    }

    // Team Code
    private string _teamCode = "";
    public string TeamCode  {
      get { return _teamCode; }
      set { _teamCode = value; }
    }

    // Sport Code
    private string _sportCode = "";
    public string SportCode
    {
      get { return _sportCode; }
      set { _sportCode = value; }
    }

    // City
    private string _city = "";
    public string City
    {
      get { return _city; }
      set { _city = value; }
    }

    // Mascot
    private string _mascot = "";
    public string Mascot
    {
      get { return _mascot; }
      set { _mascot = value; }
    }

    // Abbreviation
    private string _abbreviation = "";
    public string Abbreviation
    {
      get { return _abbreviation; }
      set { _abbreviation = value; }
    }

    public int StatMapID { get; set; }

  }
}