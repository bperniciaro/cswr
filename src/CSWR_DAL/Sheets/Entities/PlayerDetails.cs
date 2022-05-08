using System;

/// <summary>
/// Summary description for PlayerDetails
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class PlayerDetails
  {
    public PlayerDetails()  {}

    public PlayerDetails(int playerID, string sportCode, string positionCode, string firstName, string lastName, string middleName, string teamCode, 
      int number, DateTime firstYear, DateTime birthDate, string twitterUsername, int statMapID, bool retired) 
    {
      this.PlayerID = playerID;
      this.SportCode = sportCode;
      this.PositionCode = positionCode;
      this.FirstName = firstName;
      this.LastName = lastName;
      this.MiddleName = middleName;
      this.TeamCode = teamCode;
      this.Number = number;
      this.FirstYear = firstYear;
      this.BirthDate = birthDate;
      this.TwitterUsername = twitterUsername;
      this.StatMapID = statMapID;
      this.Retired = retired;
    }

    // Player ID
    private int _playerID = 0;
    public int PlayerID
    {
      get { return _playerID; }
      set { _playerID = value; }
    }

    // Sport Code
    private string _sportCode = "";
    public string SportCode
    {
      get { return _sportCode; }
      set { _sportCode = value; }
    }

    // Position Code
    private string _positionCode = "";
    public string PositionCode
    {
      get { return _positionCode; }
      set { _positionCode = value; }
    }

    // First Name
    private string _firstName = "";
    public string FirstName
    {
      get { return _firstName; }
      set { _firstName = value; }
    }

    // Last Name
    private string _lastName = "";
    public string LastName
    {
      get { return _lastName; }
      set { _lastName = value; }
    }

    // Middle Name
    private string _middleName = "";
    public string MiddleName
    {
      get { return _middleName; }
      set { _middleName = value; }
    }

    // Team Code
    private string _teamCode = "";
    public string TeamCode
    {
      get { return _teamCode; }
      set { _teamCode = value; }
    }

    // Number
    private int _number = 0;
    public int Number
    {
      get { return _number; }
      set { _number = value; }
    }

    // First Year
    private DateTime _firstYear = DateTime.Now;
    public DateTime FirstYear
    {
      get { return _firstYear; }
      set { _firstYear = value; }
    }

    // StatMapID
    private int _statMapID = 0;
    public int StatMapID
    {
      get { return _statMapID; }
      set { _statMapID = value; }
    }

    public DateTime BirthDate { get; set; }
    public string TwitterUsername { get; set; }

    // Retired
    private bool _retired = false;
    public bool Retired
    {
      get { return _retired; }
      set { _retired = value; }
     
    }

  }
}