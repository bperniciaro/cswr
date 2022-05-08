/// <summary>
/// Summary description for PlayerStatDetails
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class SportSeasonPlayerSeasonStatDetails
  {
    public SportSeasonPlayerSeasonStatDetails(string sportCode, string seasonCode, int playerID, string statCode, double statValue)
    {
      this.SportCode = sportCode;
      this.SeasonCode = seasonCode;
      this.PlayerID = playerID;
      this.StatCode = statCode;
      this.StatValue = statValue;

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

    // Player ID
    public int _playerID = 0;
    public int PlayerID
    {
      get { return _playerID; }
      set { _playerID = value; }
    }

    // Stat Code
    private string _statCode = "";
    public string StatCode
    {
      get { return _statCode; }
      set { _statCode = value; }
    }

    // Stat Value
    private double _statValue = 0;
    public double StatValue
    {
      get { return _statValue; }
      set { _statValue = value; }
    }

  }
}