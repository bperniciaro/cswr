using System;
using System.Collections.Generic;
using System.Linq;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Summary description for Player
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  [Serializable()]
  public class Player : BaseSheet
  {

    public Player(int playerID, string sportCode, string positionCode, string firstName, string lastName, string middleName, string teamCode, int number, 
      DateTime firstYear, DateTime birthDate, string twitterUsername, int statMapID, bool retired)
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
      this.StatMapID = statMapID;
      this.BirthDate = birthDate;
      this.TwitterUsername = twitterUsername;
      this.Retired = retired;
    }

    public Player() { }

    // Player ID
    public int PlayerID {get;set;}

    // Sport Code
    public string SportCode {get;set;}

    // Position Code
    private string _positionCode = String.Empty;
    public string PositionCode 
    {
      get
      {
        return _positionCode.Trim();
      }
      set  
      {
        _positionCode = value;
      }
    }

    // First Name
    public string FirstName {get;set;}

    // Last Name
    public string LastName {get;set;}

    // Middle Name
    private string _middleName = "";
    public string MiddleName
    {
      get 
      {
        if (_middleName == "X")
        {
          return String.Empty;
        }
        else
        {
          return _middleName.Trim();
        }
      }
      set { _middleName = value; }
    }

    // Stat Map ID
    public int StatMapID {get;set;}

    // Retired
    public bool Retired {get;set;}

    // Team Code
    public string TeamCode { get; set; }

    // Number
    public int Number { get; set; }

    // First Year
    public DateTime FirstYear { get; set; }

    public DateTime BirthDate { get; set; }

    public string TwitterUsername { get; set; }

    // Team
    private Team _team = null;
    public Team Team 
    {
      get
      {
        if (_team == null)
        {
          _team = Team.GetTeam(this.TeamCode);
        }
        return _team;
      }
      set
      {
        _team = value;
      }
    }

    // Position
    private Position _position = null;
    public Position Position
    {
      get
      {
        if (_position == null)
        {
          _position = Position.GetPosition(this.PositionCode);
        }
        return _position;
      }
      set
      {
        _position = value;
      }
    }


    /// <summary>
    /// This is the full name of the player in the format "FirstName MiddleName LastName"
    /// </summary>
    private string _fullName = String.Empty;
    public string FullName
    {
      get
      {
        if (_fullName == String.Empty)
        {
          if (!this.IsDefensiveTeamPlayer)
          {
            if ((this.MiddleName != "") && (this.MiddleName != "X"))
            {
              _fullName = this.FirstName + " " + this.MiddleName + " " + this.LastName;
            }
            else
            {
              _fullName = this.FirstName + " " + this.LastName;
            }
          }
          else
          {
            _fullName = this.FirstName;
          }
        }
        return _fullName;
      }
    }

    /// <summary>
    /// This is the full name of the user in the format "LastName, FirstName"
    /// </summary>
    private string _fullNameLastFirst = String.Empty;
    public string FullNameLastFirst
    {
      get
      {
        if (!this.IsDefensiveTeamPlayer)
        {
          if (_fullNameLastFirst == String.Empty)
          {
            _fullNameLastFirst = this.LastName + ", " + this.FirstName;
          }
        }
        else
        {
          _fullNameLastFirst = this.FirstName;
        }

        return _fullNameLastFirst;
      }
    }


    /// <summary>
    /// This is the full name of the user in the format "LastName, FirstName", along 
    /// with the player's position abbreivation
    /// </summary>
    private string _fullNameAndPosition = String.Empty;
    public string FullNameAndPosition
    {
      get
      {
        return this.FullNameLastFirst + " - " + this.Position.Abbreviation;
      }
    }




    /// <summary>
    /// This property calculates the years of experience for a particular player based on their
    /// first year in the league
    /// </summary>
    public int YearsExperience
    {
      get {
        int yearsExperience = (int.Parse(SportSeason.GetCurrentSportSeason(this.SportCode).SeasonCode)) - FirstYear.Year;
        if (yearsExperience < 0)
        {
          return 0;
        }
        else
        {
          return yearsExperience;
        }
      }
    }

    /// <summary>
    /// A flag indicating whether this player represents a defensive team
    /// </summary>
    public bool IsDefensiveTeamPlayer
    {
      get  
      {
        if (this.PositionCode == "DF")
        {
          return true;
        }
        return false;
      }
    }

    public List<PlayerStatusLog> StatusLogs
    {
      get
      {
        SportSeason currentSeason = SportSeason.GetCurrentSportSeason(Globals.FooString);
        return PlayerStatusLog.GetPlayerStatusLogs(this.PlayerID, currentSeason.SeasonCode);
      }
    }



    /// <summary>
    /// This instance method inserts the existing player
    /// </summary>
    /// <returns></returns>
    public int Insert()
    {
      return Player.InsertPlayer(this.PlayerID, this.SportCode, this.PositionCode, this.FirstName, this.LastName, this.MiddleName, this.TeamCode, 
        this.Number, this.FirstYear, this.BirthDate, this.TwitterUsername, this.StatMapID, this.Retired);
    }

    /// <summary>
    /// This method inserts a new player based on the specified parameters.
    /// </summary>
    /// <param name="playerID"></param>
    /// <param name="sportCode"></param>
    /// <param name="positionCode"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="middleName"></param>
    /// <param name="teamCode"></param>
    /// <param name="number"></param>
    /// <param name="firstYear"></param>
    /// <param name="statMapID"></param>
    /// <param name="retired"></param>
    /// <returns></returns>
    public static int InsertPlayer(int playerID, string sportCode, string positionCode, string firstName, string lastName, string middleName, string teamCode, 
      int number, DateTime firstYear, DateTime birthDate, string twitterUsername, int statMapID, bool retired)
    {
      if (middleName == null)
      {
        middleName = String.Empty;
      }

      // create an article entity to inser and use the specific provider to do the insert
      PlayerDetails record = new PlayerDetails(playerID, sportCode, positionCode, firstName, lastName, middleName, teamCode, number,
                                               firstYear, birthDate, twitterUsername, statMapID, retired);
      int ret = SiteProvider.Sheets.InsertPlayer(record);
      // since we've added an article we should clear all articles so that the new article is picked up
      BizObject.PurgeCacheItems("Sheets_Players");
      if (firstYear.Year == DateTime.Now.Year)
      {
        BizObject.PurgeCacheItems("Sheets_PlayerRookies");
      }
      return ret;
    }

    /// <summary>
    /// This method updates an existing Player
    /// </summary>
    /// <returns></returns>
    public bool Update()
    {
      return Player.UpdatePlayer(this.PlayerID, this.SportCode, this.PositionCode, this.FirstName, this.MiddleName, this.LastName, this.TeamCode, 
        this.Number, this.FirstYear, this.BirthDate, this.TwitterUsername, this.StatMapID, this.Retired);
    }


    public static bool UpdatePlayer(int playerID, string sportCode, string positionCode, string firstName, string middleName, string lastName, string teamCode, 
      int number, DateTime firstYear, DateTime birthDate, string twitterUsername, int statMapID, bool retired)
    {

      // convert any nulls to empty strings
      firstName = BizObject.ConvertNullToEmptyString(firstName);
      middleName = BizObject.ConvertNullToEmptyString(middleName);
      lastName = BizObject.ConvertNullToEmptyString(lastName);

      // build an article entity to update, then use the module specific provider to update it
      PlayerDetails player = new PlayerDetails(playerID, sportCode, positionCode, firstName, lastName, middleName, teamCode, number, 
                                               firstYear, birthDate, twitterUsername, statMapID, retired);
      bool ret = SiteProvider.Sheets.UpdatePlayer(player);

      BizObject.PurgeCacheItems("Sheets_Players");
      BizObject.PurgeCacheItems("Sheets_Player_" + playerID.ToString());
      if (firstYear.Year == DateTime.Now.Year)
      {
        BizObject.PurgeCacheItems("Sheets_PlayerRookies");
      }
      return ret;
    }

    /// <summary>
    /// Deletes the existing Player
    /// </summary>
    /// <returns></returns>
    public bool Delete()
    {
      return Player.DeletePlayer(this.PlayerID);
    }


    /// <summary>
    /// Deletes a Player based on the specified paramters
    /// </summary>
    /// <param name="playerID"></param>
    /// <returns></returns>
    public static bool DeletePlayer(int playerID)
    {
      Player playerToDelete = Player.GetPlayer(playerID);
      string playerTeam = playerToDelete.TeamCode;
      string playerSport = Team.GetTeam(playerTeam).SportCode;

      // get a collection of cheat sheets so that we can remove the player 
      List<CheatSheet> cheatSheets = CheatSheet.GetCheatSheets(playerSport);
      List<CheatSheetDetails> cheatSheetDetails = new List<CheatSheetDetails>();
      for (int i = 0; i < cheatSheets.Count; i++)
      {
        bool? pprLeague = null;
        bool? auctionDraft = null;
        
        // determine PPR League
        if (cheatSheets[i].MappedProperties.ContainsKey(CSProperty.PPRLeague.ToString()))
        {
          pprLeague = (bool)cheatSheets[i].MappedProperties[CSProperty.PPRLeague.ToString()];
        }
        if (cheatSheets[i].MappedProperties.ContainsKey(CSProperty.AuctionDraft.ToString()))
        {
          auctionDraft = (bool)cheatSheets[i].MappedProperties[CSProperty.AuctionDraft.ToString()];
        }


        CheatSheetDetails cheatSheetRecord = new CheatSheetDetails(cheatSheets[i].CheatSheetID, cheatSheets[i].Username, cheatSheets[i].SeasonCode, cheatSheets[i].SportCode,
          cheatSheets[i].SheetName, cheatSheets[i].StatsSeasonCode, cheatSheets[i].Created, cheatSheets[i].LastUpdated, pprLeague, auctionDraft);
        cheatSheetDetails.Add(cheatSheetRecord);
      }

      // get a collection of supplemental sheets so that we can remove the player
      List<SupplementalSheet> supplementalSheets = SupplementalSheet.GetSupplementalSheets(playerSport);
      List<SupplementalSheetDetails> supplementalSheetDetails = new List<SupplementalSheetDetails>();
      for (int i = 0; i < supplementalSheets.Count; i++)
      {
        SupplementalSheetDetails supplementalSheetRecord = new SupplementalSheetDetails(supplementalSheets[i].SupplementalSheetID, supplementalSheets[i].SeasonCode,
          supplementalSheets[i].SupplementalSourceID, supplementalSheets[i].SportCode, supplementalSheets[i].PositionCode, supplementalSheets[i].LastUpdated, supplementalSheets[i].Url);
        supplementalSheetDetails.Add(supplementalSheetRecord);
      }

      bool ret = SiteProvider.Sheets.DeletePlayer(playerID, ref cheatSheetDetails, ref supplementalSheetDetails);
      BizObject.PurgeCacheItems("Sheets_Players");
      BizObject.PurgeCacheItems("Sheets_Player_" + playerID.ToString());
      if (playerToDelete.FirstYear.Year == DateTime.Now.Year)
      {
        BizObject.PurgeCacheItems("Sheets_PlayerRookies");
      }
      return ret;
    }


    /// <summary>
    /// Returns a Player object with the specified ID
    /// </summary>
    public static Player GetPlayer(int playerID)
    {
      Player player = null;
      string key = "Sheets_Player_" + playerID.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        player = (Player)BizObject.Cache[key];
      }
      else
      {
        player = GetPlayerFromPlayerDetails(SiteProvider.Sheets.GetPlayer(playerID));
        BaseSheet.CacheData(key, player);
      }
      return player;
    }


    /// <summary>
    /// Returns a Player object with the specified StatMapID
    /// </summary>
    public static Player GetPlayerByStatMapID(int statMapID)
    {
      Player player = null;
      string key = "Sheets_PlayerByStatMapID_" + statMapID.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        player = (Player)BizObject.Cache[key];
      }
      else
      {
        player = GetPlayerFromPlayerDetails(SiteProvider.Sheets.GetPlayerByStatMapID(statMapID));
        BaseSheet.CacheData(key, player);
      }
      return player;
    }



    /// <summary>
    /// Returns a Player object based on the sport and their name parts.
    /// </summary>
    public static Player GetPlayer(string sportCode, string firstName, string middleName, string lastName)
    {
      Player player = null;
      string key = "Sheets_PlayerBySportFirstMiddleLastName_" + sportCode + "_" + firstName + "_" + middleName + "_" + lastName;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        player = (Player)BizObject.Cache[key];
      }
      else
      {
        player = GetPlayerFromPlayerDetails(SiteProvider.Sheets.GetPlayer(sportCode, firstName, middleName, lastName));
        BaseSheet.CacheData(key, player);
      }
      return player;
    }


    /// <summary>
    /// Returns a Player object based on the sport, team, and their name parts.
    /// </summary>
    public static Player GetPlayer(string sportCode, string teamCode, string firstName, string middleName, string lastName)
    {
      Player player = null;
      string key = "Sheets_PlayerBySportTeamFirstMiddleLastName_" + sportCode + "_" + teamCode + "_" +
        firstName + "_" + middleName + "_" + lastName;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        player = (Player)BizObject.Cache[key];
      }
      else
      {
        player = GetPlayerFromPlayerDetails(SiteProvider.Sheets.GetPlayer(sportCode, teamCode, firstName, middleName, lastName));
        BaseSheet.CacheData(key, player);
      }
      return player;
    }


    /// <summary>
    /// Returns a Player object which represents a Defensive Team
    /// </summary>
    public static Player GetDefensiveTeamPlayer(string teamCode)
    {
      Player player = null;
      string key = "Sheets_DefensivePlayer_" + teamCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        player = (Player)BizObject.Cache[key];
      }
      else
      {
        player = GetPlayerFromPlayerDetails(SiteProvider.Sheets.GetDefensivePlayer(teamCode));
        BaseSheet.CacheData(key, player);
      }
      return player;
    }


    /// <summary>
    /// Returns a collection of Player objects which represent all Defensive Teams
    /// </summary>
    public static List<Player> GetDefensiveTeamPlayers()
    {
      List<Player> players = null;
      string key = "Sheets_DefensivePlayers";

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        players = (List<Player>)BizObject.Cache[key];
      }
      else
      {
        List<PlayerDetails> recordset = SiteProvider.Sheets.GetDefensivePlayers();
        players = GetPlayerListFromPlayerDetailsList(recordset);
        BaseSheet.CacheData(key, players);
      }
      return players.GetRange(0, players.Count);
    }


    /// <summary>
    /// Returns a collection of Players based on the specified sport, first name, and last name.
    /// </summary>
    public static List<Player> GetPlayersByPartialName(string sportCode, string partialName)
    {
        return GetPlayerListFromPlayerDetailsList(SiteProvider.Sheets.GetPlayersByPartialName(sportCode, partialName));
    }



    /// <summary>
    /// Returns a collection of Players based on the specified sport, first name, and last name.
    /// </summary>
    public static List<Player> GetPlayers(string sportCode, string firstName, string lastName)
    {
      List<Player> players = null;
      string key = "Sheets_PlayersBySportFirstNameLastName_" + sportCode + "_" + firstName + "_" + lastName;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        players = (List<Player>)BizObject.Cache[key];
      }
      else
      {
        List<PlayerDetails> recordset = SiteProvider.Sheets.GetPlayers(sportCode, firstName, lastName);
        players = GetPlayerListFromPlayerDetailsList(recordset);
        BaseSheet.CacheData(key, players);
      }
      return players.GetRange(0, players.Count);
    }


    /// <summary>
    /// Returns players who played in the specified year (or any year before), regardless of if they
    /// recorded a stat
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode">The season which you want to determine which players played.</param>
    /// <param name="includeRetired"></param>
    /// <returns></returns>
    public static List<Player> GetPlayers(string sportCode, string seasonCode, bool includeRetired)
    {
      List<Player> players = null;
      string key = "Sheets_PlayersBySportSeasonCodesRetired_" + sportCode.ToCharArray() + "_" + seasonCode.ToString() + "_" + includeRetired.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        players = (List<Player>)BizObject.Cache[key];
      }
      else
      {
        List<PlayerDetails> recordset = SiteProvider.Sheets.GetPlayers(sportCode, seasonCode, includeRetired);
        players = GetPlayerListFromPlayerDetailsList(recordset);
        BaseSheet.CacheData(key, players);
      }
      return players.GetRange(0, players.Count);
    }



    /// <summary>
    /// Returns all [offensive] players on the specified team who played during the indicated season (or any preceding
    /// season), regardless of if they recorded a stat
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode">The season which you want to determine which players played.</param>
    /// <param name="teamCode"></param>
    /// <param name="includeRetired"></param>
    /// <returns></returns>
    public static List<Player> GetPlayers(string sportCode, string seasonCode, string teamCode, bool includeRetired)
    {
      List<Player> players = null;
      string key = "Sheets_PlayersBySportSeasonTeamCodesRetired_" + sportCode.ToString() + "_" + seasonCode.ToString() + "_" + teamCode.ToString() + "_" + includeRetired.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        players = (List<Player>)BizObject.Cache[key];
      }
      else
      {
        List<PlayerDetails> recordset = SiteProvider.Sheets.GetPlayers(sportCode, seasonCode, teamCode, includeRetired);
        players = GetPlayerListFromPlayerDetailsList(recordset);
        BaseSheet.CacheData(key, players);
      }
      return players.GetRange(0, players.Count);
    }

    
    /// <summary>
    /// Returns a collection of players who played in a particular year.
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode">The season which you want to determine which players played.</param>
    /// <param name="teamCode"></param>
    /// <param name="includeRetired"></param>
    /// <param name="includeRookies"></param>
    /// <returns></returns>
    public static List<Player> GetPlayers(string sportCode, string seasonCode, string teamCode, bool includeRetired, bool includeRookies)
    {
      List<Player> registeredStatPlayers = Player.GetPlayers(sportCode, seasonCode, teamCode, includeRetired);
      if (includeRookies == true)
      {
        int rookieSeason = int.Parse(seasonCode) + 1;
        List<Player> rookies = Player.GetPlayerRookies(Team.GetTeam(teamCode).SportCode, rookieSeason.ToString(), teamCode);
        for (int i = 0; i < rookies.Count; i++)
        {
          registeredStatPlayers.Add(rookies[i]);
        }
      }
      return registeredStatPlayers.GetRange(0, registeredStatPlayers.Count);
    }


    /// <summary>
    /// Returns a collection of Players based on sport, season, team, position, and whether or not to include rookies
    /// </summary>
    public static List<Player> GetPlayers(string sportCode, string seasonCode, string teamCode, string positionCode, bool includeRetired)
    {
      List<Player> players = null;
      string key = "Sheets_PlayersBySportSeasonTeamPositionCodes_" + sportCode.ToString() + "_" + seasonCode.ToString() + "_" + teamCode.ToString() + "_" +
        positionCode.ToString() + "_" + includeRetired.ToString(); ;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        players = (List<Player>)BizObject.Cache[key];
      }
      else
      {
        List<PlayerDetails> recordset = SiteProvider.Sheets.GetPlayers(sportCode, seasonCode, teamCode, positionCode, includeRetired);
        players = GetPlayerListFromPlayerDetailsList(recordset);
        BaseSheet.CacheData(key, players);
      }
      players.OrderBy(x => x.FullNameLastFirst);
      return players.GetRange(0, players.Count);
    }



    /// <summary>
    /// Returns a collection of Players based on sport, season, position, first name, & last name
    /// </summary>
    public static List<Player> GetPlayers(string sportCode, string seasonCode, string positionCode, string firstName, string lastName, bool includeRetired)
    {
      List<Player> players = null;
      string key = "Sheets_PlayersBySportSeasonPositionFirstNameLastNameRetired_" + seasonCode + "_" + sportCode + "_" + positionCode + "_" + firstName + "_" + lastName + "_" + includeRetired.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        players = (List<Player>)BizObject.Cache[key];
      }
      else
      {
        List<PlayerDetails> recordset = SiteProvider.Sheets.GetPlayers(sportCode, seasonCode, positionCode, firstName, lastName, includeRetired);
        players = GetPlayerListFromPlayerDetailsList(recordset);
        BaseSheet.CacheData(key, players);
      }
      return players.GetRange(0, players.Count);
    }


    /// <summary>
    /// Returns a collection of Players based on sport, season, position, first name, & last name
    /// </summary>
    public static List<Player> GetPlayers(string sportCode, string seasonCode, string positionCode, char firstInitial, string lastName, bool includeRetired)
    {
      List<Player> players = null;
      string key = "Sheets_PlayersBySportSeasonPositionFirstInitialNameLastNameRetired_" + seasonCode + "_" + sportCode + "_" + positionCode + "_" + firstInitial + "_" + lastName + "_" + includeRetired.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        players = (List<Player>)BizObject.Cache[key];
      }
      else
      {
        List<PlayerDetails> recordset = SiteProvider.Sheets.GetPlayers(sportCode, seasonCode, positionCode, firstInitial, lastName, includeRetired);
        players = GetPlayerListFromPlayerDetailsList(recordset);
        BaseSheet.CacheData(key, players);
      }
      return players.GetRange(0, players.Count);
    }





    /// <summary>
    /// Returns a collection of players based on stats from a particular season
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode">The season when the respective stats are accumulated.</param>
    /// <param name="positionCode"></param>
    /// <param name="includeRetired"></param>
    /// <param name="statCode"></param>
    /// <param name="sortDir"></param>
    /// <returns></returns>
    public static List<Player> GetPlayers(string sportCode, string seasonCode, string positionCode, bool includeRetired, string statCode, string sortDir)
    {
      List<Player> players = null;
      string key = "Sheets_PlayersBySeasonSportPositionStatCodesRetiredSortDir_" + seasonCode.ToString() + "_" + sportCode.ToString() + "_" +
          positionCode.ToString() + "_" + statCode.ToString() + "_" + includeRetired.ToString() + "_" + sortDir;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        players = (List<Player>)BizObject.Cache[key];
      }
      else
      {
        List<PlayerDetails> recordset = SiteProvider.Sheets.GetPlayers(sportCode, seasonCode, positionCode, includeRetired, statCode, sortDir);
        players = GetPlayerListFromPlayerDetailsList(recordset);
        BaseSheet.CacheData(key, players);
      }
      return players.GetRange(0, players.Count);
    }


    /// <summary>
    /// Returns a collection of Players based on sport and last name.  This method is used to search for players through the
    /// "Manage Users" interface when I don't know which team they're currently on.
    /// </summary>
    public static List<Player> GetPlayers(string sportCode, string lastName)
    {
      List<Player> players = null;
      string key = "Sheets_PlayersBySportLastName_" + sportCode.ToString() + "_" + lastName;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        players = (List<Player>)BizObject.Cache[key];
      }
      else
      {
        List<PlayerDetails> recordset = SiteProvider.Sheets.GetPlayers(sportCode, lastName);
        players = GetPlayerListFromPlayerDetailsList(recordset);
        BaseSheet.CacheData(key, players);
      }
      return players.GetRange(0, players.Count);
    }


    /// <summary>
    /// Returns a collection of Players based on sport, season, positions, and stat code.  This is used to return the top players for a
    /// particular season which span across multiple positions.  It only includes players from the specified season so it won't include 
    /// rookies for any following season.  
    /// </summary>
    public static List<Player> GetPlayers(string sportCode, string seasonCode, List<Position> positions, bool includeRetired, string statCode)
    {
      // Build a dynamic caching key based on the positions contained in the query
      List<Player> players = null;
      string key = "Sheets_PlayersBySportSeasonPositionsStatCode_" + sportCode.ToString() + "_" + seasonCode + "_";
      foreach (Position currentPosition in positions)
      {
        key += currentPosition.PositionCode + "_";
      }
      key += includeRetired.ToString() + "_";
      key += statCode;


      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        players = (List<Player>)BizObject.Cache[key];
      }
      else
      {
        // create a collection of entities
        List<PositionDetails> positionEntities = new List<PositionDetails>();
        foreach (Position currentPosition in positions)
        {
          positionEntities.Add(new PositionDetails(currentPosition.PositionCode, currentPosition.Name, currentPosition.Abbreviation));
        }

        List<PlayerDetails> recordset = SiteProvider.Sheets.GetPlayers(sportCode, seasonCode, positionEntities, includeRetired, statCode);
        players = GetPlayerListFromPlayerDetailsList(recordset);
        BaseSheet.CacheData(key, players);
      }
      return players.GetRange(0, players.Count);
    }


    /// <summary>
    /// Returns a collection with all the stats
    /// </summary>
    public static List<Player> GetPlayers(string sportCode)
    {
      List<Player> players = null;
      string key = "Sheets_PlayersBySportCode_" + sportCode.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        players = (List<Player>)BizObject.Cache[key];
      }
      else
      {
        List<PlayerDetails> recordset = SiteProvider.Sheets.GetPlayers(sportCode);
        players = GetPlayerListFromPlayerDetailsList(recordset);
        BaseSheet.CacheData(key, players);
      }
      return players.GetRange(0, players.Count);
    }



    /// <summary>
    /// Returns a collection of players who played in a particular year.
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode">The year for which you want to determine the players who played in that year</param>
    /// <param name="positionCode"></param>
    /// <param name="includeRetired"></param>
    /// <param name="includeRookies">Whether you want to include players who were rookies in the season code specified</param>
    /// <returns></returns>
    public static List<Player> GetPlayersBySportSeasonPositionCodes(string sportCode, string seasonCode, string positionCode, bool includeRetired, bool includeRookies)
    {
      List<Player> players = null;
      List<Player> sortedPlayers = null;
      string key = "Sheets_PlayersBySportSeasonPositionCodes_" + sportCode.ToString() + "_" + seasonCode.ToString() + "_" +
        positionCode.ToString() + "_" + includeRetired.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        sortedPlayers = (List<Player>)BizObject.Cache[key];
      }
      else
      {
        List<PlayerDetails> recordset = SiteProvider.Sheets.GetPlayersBySportSeasonPositionCodes(sportCode, seasonCode, positionCode, includeRetired);
        players = GetPlayerListFromPlayerDetailsList(recordset);

        if ((includeRookies == true) && (positionCode != FOOPositionsOffense.DF.ToString()))
        {
          int rookieSeason = int.Parse(seasonCode) + 1;
          List<Player> rookies = Player.GetPlayerRookiesBySportSeasonPositionCodes(sportCode, rookieSeason.ToString(), positionCode);
          for (int i = 0; i < rookies.Count; i++)
          {
            players.Add(rookies[i]);
          }
        }

        sortedPlayers = players.OrderBy(x => x.FullNameLastFirst).ToList();

        BaseSheet.CacheData(key, sortedPlayers);
      }

      return sortedPlayers.GetRange(0, sortedPlayers.Count);
    }



    /// <summary>
    /// Returns a collection of rookie players
    /// </summary>
    /// <param name="sportCode">The season for which you want to determine the rookies.</param>
    /// <param name="seasonCode"></param>
    /// <returns></returns>
    public static List<Player> GetPlayerRookies(string sportCode, string seasonCode)
    {
      List<Player> players = null;
      string key = "Sheets_PlayerRookiesBySportSeasonCodes_" + sportCode.ToString() + "_" + seasonCode.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        players = (List<Player>)BizObject.Cache[key];
      }
      else
      {
        List<PlayerDetails> recordset = SiteProvider.Sheets.GetPlayerRookies(sportCode, seasonCode);
        players = GetPlayerListFromPlayerDetailsList(recordset);
        BaseSheet.CacheData(key, players);
      }
      return players.GetRange(0, players.Count);
    }


    /// <summary>
    /// Returns a collection of rookie players
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode">The season for which you want to determine the rookies.</param>
    /// <param name="teamCode"></param>
    /// <returns></returns>
    public static List<Player> GetPlayerRookies(string sportCode, string seasonCode, string teamCode)
    {
      List<Player> players = null;
      string key = "Sheets_PlayerRookiesBySportSeasonTeamCodes_" + sportCode.ToString() + "_" + seasonCode.ToString() + "_" + teamCode.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        players = (List<Player>)BizObject.Cache[key];
      }
      else
      {
        List<PlayerDetails> recordset = SiteProvider.Sheets.GetPlayerRookies(sportCode, seasonCode, teamCode);
        players = GetPlayerListFromPlayerDetailsList(recordset);
        BaseSheet.CacheData(key, players);
      }
      return players.GetRange(0, players.Count); ;
    }

    /// <summary>
    /// Returns a collection of rookie players
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode">The season for which you want to determine the rookies.</param>
    /// <param name="positionCode"></param>
    /// <returns></returns>
    public static List<Player> GetPlayerRookiesBySportSeasonPositionCodes(string sportCode, string seasonCode, string positionCode)
    {
      List<Player> players = null;
      string key = "Sheets_PlayerRookiesBySportSeasonPositionCodes_" + sportCode + "_" + seasonCode + "_" + positionCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        players = (List<Player>)BizObject.Cache[key];
      }
      else
      {
        List<PlayerDetails> recordset = SiteProvider.Sheets.GetPlayerRookiesBySportSeasonPositionCodes(sportCode, seasonCode, positionCode);
        players = GetPlayerListFromPlayerDetailsList(recordset);
        BaseSheet.CacheData(key, players);
      }
      return players.GetRange(0, players.Count);
    }




    /// <summary>
    /// Returns a Player object filled with the data taken from the input PlayerDetails
    /// </summary>
    private static Player GetPlayerFromPlayerDetails(PlayerDetails player)
    {
      if (player == null)
        return null;
      else
      {
        return new Player(player.PlayerID, player.SportCode, player.PositionCode, player.FirstName, player.LastName, player.MiddleName, player.TeamCode, 
          player.Number, player.FirstYear, player.BirthDate, player.TwitterUsername, player.StatMapID, player.Retired);
      }
    }

    /// <summary>
    /// Returns a list of Player objects filled with the data taken from the input list of PlayerDetails
    /// </summary>
    private static List<Player> GetPlayerListFromPlayerDetailsList(List<PlayerDetails> recordset)
    {
      List<Player> players = new List<Player>();
      foreach (PlayerDetails record in recordset)
        players.Add(GetPlayerFromPlayerDetails(record));
      return players;
    }

  
  
  }
}