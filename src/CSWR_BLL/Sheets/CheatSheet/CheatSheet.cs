using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// This is the class for a cheat sheet, which descends from the base class BaseSheet
/// </summary>
/// 

namespace BP.CheatSheetWarRoom.BLL.Sheets
{

  [Serializable()]
  public class CheatSheet : BaseSheet
  {

    /// <summary>
    /// The default constructor for the CheatSheet class.
    /// </summary>
    public CheatSheet() { }

    /// <summary>
    /// A constructor used to create a cheat sheet object
    /// </summary>
    /// <param name="cheatSheetID">The unique id of the cheat sheet.</param>
    /// <param name="username">The username of the user that created the cheat sheet.</param>
    /// <param name="seasonCode">The season on which the cheat sheet is based.</param>
    /// <param name="sportCode">The sport on which the cheat sheet is based.</param>
    /// <param name="sheetName">The name given to the cheat sheet by the user.</param>
    /// <param name="lastUpdated">The date and time that the cheat sheet was last updated by the user.</param>
    public CheatSheet(int cheatSheetID, string username, string seasonCode, string sportCode, string sheetName, string statsSeasonCode, 
                        DateTime created, DateTime lastUpdated, Dictionary<string, object> mappedProperties)
    {
      this.CheatSheetID = cheatSheetID;
      this.Username = username;
      this.SeasonCode = seasonCode;
      this.SportCode = sportCode;
      this.SheetName = sheetName;
      this.StatsSeasonCode = statsSeasonCode;
      this.Created = created;
      this.LastUpdated = lastUpdated;
      this.MappedProperties = mappedProperties;
    }

    /// <summary>
    /// The unique id which is assigned to the cheat sheet.
    /// </summary>
    public int CheatSheetID {get;set;}

    /// <summary>
    /// The user which created the cheat sheet
    /// </summary>
    /// 
    private string _userName { get; set; }
    public string Username
    {
      get
      {
        return _userName.Trim();
      }
      set
      {
        _userName = value;
      }
    }

    /// <summary>
    /// This is the year that the sheet is RELEVANT.  If the sheet is for year 2013, then this field will be 2013.
    /// </summary>
    public string SeasonCode {get;set;}


    /// <summary>
    /// The sport on which the cheat sheet is based.
    /// </summary>
    public string SportCode {get;set;}

    
    /// <summary>
    /// The name given to the cheat sheet by the user.  This name helps users differentiate between sheets for the same position that may be in 
    /// different leagues and based on different scoring rules.
    /// </summary>
    public string SheetName {get;set;}

    /// <summary>
    /// This is the year on which the displayed statistics should be based.  This feature would allow people to base cheat sheets
    /// both on the previous year and the current year (if those stats are available).
    /// </summary>
    public string StatsSeasonCode {get;set;}

    /// <summary>
    /// The date and time that the cheat sheet was created. 
    /// </summary>
    public DateTime Created {get;set;}

    /// <summary>
    /// The date and time that the cheat sheet was last updated.  This will be updated each time a sheet it changed (items are reordered).
    /// </summary>
    public DateTime LastUpdated {get;set;}


    /// <summary>
    /// Dictionary of properties which vary by sport
    /// </summary>
    public Dictionary<string, object> MappedProperties { get; set; }
    
    private List<CheatSheetPosition> _cheatSheetPositions= null;
    public List<CheatSheetPosition> CheatSheetPositions
    {
      get
      {
        if (_cheatSheetPositions == null)
        {
          _cheatSheetPositions = CheatSheetPosition.GetCheatSheetPositions(this.CheatSheetID);
        }
        return _cheatSheetPositions;
      }
    }

    private List<Position> _positions = null;
    public List<Position> Positions
    {
      get
      {
        if (_positions == null)
        {
          _positions = new List<Position>();
          foreach (CheatSheetPosition currentCheatSheetPosition in this.CheatSheetPositions)
          {
            _positions.Add(Position.GetPosition(currentCheatSheetPosition.PositionCode));
          }
        }
        return _positions;
      }
    }


    /// <summary>
    /// A read only property which provides access to the items that make up a cheat sheet 
    /// </summary>
    private List<CheatSheetItem> _items = null;
    public List<CheatSheetItem> Items
    {
      get
      {
        if (_items == null)
        {
          _items = CheatSheetItem.GetCheatSheetItems(this.CheatSheetID);
        }
        return _items;
      }
    }


    /// <summary>
    /// This method will check to see if a cheat sheet sequence is correct.  If it is not, it will automatically correct the sequence.
    ///     /// </summary>
    public bool Validate()
    {
      return CheatSheet.ValidateCheatSheet(this);
    }

    /// <summary>
    /// This static method takes in a target CheatSheet and validates it's sequence.  If it is correct the method returns true, otherwise it returns false.
    /// </summary>
    /// <param name="targetSheet"></param>
    /// <returns></returns>
    public static bool ValidateCheatSheet(CheatSheet targetSheet)
    {
      int i = 0;
      bool correctionNeeded = false;

      // spin through all items to be sure seqno is incremented once for each item
      foreach (CheatSheetItem currentItem in targetSheet.Items)
      {
        if (currentItem.Seqno == i + 1)
        {
          i++;
        }
        else
        {
          // if we get here, the sequence went wrong somewhere.
          correctionNeeded = true;
          break;
        }
      }

      // if a correction is needed, we re-sequence everything
      if (correctionNeeded)
      {
        i = 0;
        foreach (CheatSheetItem currentItem in targetSheet.Items)
        {
          currentItem.Seqno = i + 1;
          currentItem.Update();
          i++;
        }
        // since we updated the cheat sheet, update the modified timestamp
        targetSheet.LastUpdated = DateTime.Now;
        targetSheet.Update();

        // purge call cache for this cheat sheet the may have been affected by the re-sequencing
        BizObject.PurgeCacheItems("Sheets_CheatSheetItems_" + targetSheet.CheatSheetID.ToString());
        // purge cheat sheet cache
      }

      // the return value should indicate if the sequence was or wasn't corrected
      return correctionNeeded;
    }


    /// <summary>
    /// Checks to see if the user name of the currently logged in user matches the username
    /// tied to the cheat sheet they're editing.  We have to cast them both to lower because users 
    /// can login with a username whose case does not match the username in the database
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public bool ConfirmOwner(string userName)
    {
      string test1 = GetCheatSheet(this.CheatSheetID).Username.ToLower();
      string test2 = userName.ToLower();
      return (GetCheatSheet(this.CheatSheetID).Username.ToLower() == userName.ToLower());
    }


    /// <summary>
    /// Instance update method for a cheat sheet
    /// </summary>
    /// <returns>TRUE if the update was successful, FALSE otherwise</returns>
    public bool Update()
    {
      return CheatSheet.UpdateCheatSheet(this.CheatSheetID, this.Username, this.SeasonCode, this.SportCode, this.SheetName, this.StatsSeasonCode, 
                                            this.Created, this.LastUpdated, this.MappedProperties);
    }


    /// <summary>
    /// Updates a cheat sheet to the values specified in the parameter list.
    /// </summary>
    public static bool UpdateCheatSheet(int cheatSheetID, string userName, string seasonCode, string sportCode, string sheetName, 
                                          string statsSeasonCode, DateTime created, DateTime lastUpdated, Dictionary<string, object> mappedProperties)
    {
      /* Purge Cheat Sheet */
      BizObject.PurgeCacheItems("Sheets_CheatSheetByID_" + cheatSheetID.ToString());

      // PPR League
      bool? pprLeague = null;
      bool? auctionDraft = null;
      if(mappedProperties.ContainsKey(CSProperty.PPRLeague.ToString()))  
      {
        pprLeague = (bool)mappedProperties[CSProperty.PPRLeague.ToString()];
      }
      // Auction Draft
      if (mappedProperties.ContainsKey(CSProperty.AuctionDraft.ToString()))
      {
        auctionDraft = (bool)mappedProperties[CSProperty.AuctionDraft.ToString()];
      }

      // build a cheat sheet entity to update, then use the module specific provider to update it
      CheatSheetDetails cheatSheet = new CheatSheetDetails(cheatSheetID, userName, seasonCode, sportCode, sheetName, statsSeasonCode, 
                                                            created, lastUpdated, pprLeague, auctionDraft);
      return SiteProvider.Sheets.UpdateCheatSheet(cheatSheet);
    }

    
    /// <summary>
    /// Get a cheatSheet based on the specified cheatSheetID
    /// </summary>
    /// <param name="cheatSheetID"></param>
    /// <returns></returns>
    public static CheatSheet GetCheatSheet(int cheatSheetID)
    {
      CheatSheet cheatSheet = null;
      string key = "Sheets_CheatSheetByID_" + cheatSheetID.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        cheatSheet = (CheatSheet)BizObject.Cache[key];
      }
      else
      {
        cheatSheet = GetCheatSheetFromCheatSheetDetails(SiteProvider.Sheets.GetCheatSheet(cheatSheetID));
        BaseSheet.CacheData(key, cheatSheet);
      }
      return cheatSheet;

    }






    /// <summary>
    /// Returns a collection of Player objects which are not present in the cheat sheet specified in the parameters list.  The players considered
    /// are players from the previous season and present-year rookies.
    /// </summary>
    public static List<Player> GetCheatSheetAvailablePlayers(int cheatSheetID, string sortParam, string sortDir)
    {
      CheatSheet currentSheet = CheatSheet.GetCheatSheet(cheatSheetID);

       /***************************************************************************/
      /* Create a collection of all possible players for the current season code */
      /***************************************************************************/
      // determine all possible players from  the previous year (need to move this to a dedicated SP after refactoring)
      List<Player> allAvailablePlayers = new List<Player>();

      foreach (CheatSheetPosition targetPosition in currentSheet.CheatSheetPositions)
      {
        allAvailablePlayers.AddRange(Player.GetPlayersBySportSeasonPositionCodes(currentSheet.SportCode, currentSheet.StatsSeasonCode,
                                            targetPosition.PositionCode, false, true));
      }

      // build a collection of valid players who are not on the current sheet
      List<Player> notOnSheetPlayers = new List<Player>();

      foreach (Player currentPlayer in allAvailablePlayers)
      {
        // if we can't find the current player among the players already in the cheat sheet.....
        if (currentSheet.Items.Find((delegate(CheatSheetItem targetItem) { return (targetItem.PlayerID == currentPlayer.PlayerID); })) == null)
        {
          notOnSheetPlayers.Add(currentPlayer);
        }
      }

      /************************************************************************************/
      /* Sort the resulting collection of players based on the sorting parameter specified*/
      /************************************************************************************/

      // if we're sorting by name, just call a local sort method to reorder the players
      if (sortParam.ToLower() == "name")
      {
        return notOnSheetPlayers.OrderBy(x => x.FullNameLastFirst).ToList();
      }
      // if we're sorting by statistic, get a sorted list of players, then add rookies to the end since their stats will 
      // be the lowest by definition
      else
      {
        // get all available (non-rookie) players sorted by the specified statistic (sortParam) to use as a comparison point to sort
        // the notOnSheetPlayers we already identified
        List<Player> sortedPlayers = new List<Player>();
        
        //List<Player> allStatSortedPlayers = Player.GetPlayers(currentSheet.SportCode, currentSheet.StatsSeasonCode, currentSheet.Positions[0].PositionCode, false, sortParam, sortDir);

        List<Player> allStatSortedPlayers = Player.GetPlayers(currentSheet.SportCode, currentSheet.StatsSeasonCode, currentSheet.Positions, false, sortParam);
        

        // determine players (including rookies) who are not retired, but who did not record a stat in the specified year.  these players
        // will not show-up when querying for things like TFP, but we still need to add them to the available player pool
        List<Player> noStatsPlayers = new List<Player>();
        foreach (Player currentPlayer in allAvailablePlayers)
        {
          Player availPlayer = allStatSortedPlayers.Find((delegate(Player targetPlayer) { return (targetPlayer.PlayerID == currentPlayer.PlayerID); }));
          if (availPlayer == null)
          {
            noStatsPlayers.Add(currentPlayer);
          }
        }

        // spin through the sorted players and add them to the final player collection
        foreach (Player currentPlayer in allStatSortedPlayers)
        {
          // do we find the sorted player in the list of availablePlayers?
          Player statSortedPlayer = notOnSheetPlayers.Find((delegate(Player targetPlayer) { return (targetPlayer.PlayerID == currentPlayer.PlayerID); } ));
          if (statSortedPlayer != null)
          {
            sortedPlayers.Add(statSortedPlayer);
          }
        }

        // add the players who did not record a stat (this includes rookies by default)
        sortedPlayers.AddRange(noStatsPlayers);

        return sortedPlayers.GetRange(0, sortedPlayers.Count);
      }
    }



    
    /// <summary>
    /// Creates a cheat sheet based on statistics from the previous year.  This sheet also assumes that we want to create a cheat sheet for
    /// the current season as specified in the SeasonCodes table (the season with 'CurrentYear=true').
    /// </summary>
    public static int CreateCheatSheet(string sportCode, string sheetName, string statsSeasonCode, List<Position> cheatSheetPositions, 
                                        List<Stat> cheatSheetStats, string sortStat, string sortStatDir, Dictionary<string, object> mappedProperties)
    {

      // determine how many players we'll include based on a globally-defined variable
      int totalSheetPlayers = Globals.GetDefaultPlayersPerSheet(cheatSheetPositions.Select(x => x.PositionCode).ToList());

      // determine which players to add, we don't need to consider rookies because they'll be at the bottom of huge lists and won't show up in default cheat sheets
      List<Player> sheetPlayers = Player.GetPlayers(sportCode, statsSeasonCode, cheatSheetPositions, false, sortStat);

      // determine how many players to include on the sheet
      if (sheetPlayers.Count < totalSheetPlayers)
      {
        totalSheetPlayers = sheetPlayers.Count;
      }

      List<Player> cheatSheetPlayers = sheetPlayers.GetRange(0, totalSheetPlayers);


      /* determine if any mapped properties are set, if so use them */
      // PPR League
      bool? pprLeague = null;
      if (mappedProperties != null)
      {
        if (mappedProperties.ContainsKey(CSProperty.PPRLeague.ToString()))
        {
          pprLeague = (bool?)mappedProperties[CSProperty.PPRLeague.ToString()];
        }
      }
      // Auction Draft
      bool? auctionDraft = null;
      if (mappedProperties != null)
      {
        if (mappedProperties.ContainsKey(CSProperty.AuctionDraft.ToString()))
        {
          auctionDraft = (bool?)mappedProperties[CSProperty.AuctionDraft.ToString()];
        }
      }

      // create a cheat sheet entity object to pass to the DAL
      CheatSheetDetails cheatSheetRecord = new CheatSheetDetails(0, CurrentUserName, SportSeason.GetCurrentSportSeason(sportCode).SeasonCode, sportCode, 
                                                                      sheetName, statsSeasonCode, DateTime.Now, DateTime.Now, pprLeague, auctionDraft);
      
      // create a collection of position code entity objects to pass to the DAL
      List<PositionDetails> positionDetails = new List<PositionDetails>();
      for (int i = 0; i < cheatSheetPositions.Count; i++)
      {
        PositionDetails positionRecord = new PositionDetails(cheatSheetPositions[i].PositionCode, String.Empty, String.Empty);
        positionDetails.Add(positionRecord);
      }

      // create a collection of stat code entity objects to pass to the DAL
      List<StatDetails> statDetails = new List<StatDetails>();
      for (int i = 0; i < cheatSheetStats.Count; i++)
      {
        StatDetails statRecord = new StatDetails(cheatSheetStats[i].StatCode, String.Empty, String.Empty, String.Empty);
        statDetails.Add(statRecord);
      }
      
      // create a collection of player entity objects to pass to the DAL
      List<PlayerDetails> playerDetails = new List<PlayerDetails>();
      for (int i = 0; i < totalSheetPlayers; i++)
      {
        PlayerDetails playerRecord = new PlayerDetails(cheatSheetPlayers[i].PlayerID, sportCode, cheatSheetPlayers[i].PositionCode, cheatSheetPlayers[i].FirstName,
            cheatSheetPlayers[i].LastName, cheatSheetPlayers[i].MiddleName, cheatSheetPlayers[i].TeamCode, cheatSheetPlayers[i].Number, 
            cheatSheetPlayers[i].FirstYear, cheatSheetPlayers[i].BirthDate, cheatSheetPlayers[i].TwitterUsername, cheatSheetPlayers[i].StatMapID, cheatSheetPlayers[i].Retired);
        playerDetails.Add(playerRecord);
      }

      // call a DAL method to create the cheat sheet
      int newSheetID = 0;
      for (int i = 1; i <= Globals.TransactionRetryCount; i++)
      {
        newSheetID = SiteProvider.Sheets.CreateCheatSheet(cheatSheetRecord, CurrentUserName, positionDetails, statDetails, playerDetails, i);
        if (newSheetID != Globals.TransactionErrorSentinel)
        {
          break;
        }
        else
        {
          Thread.Sleep(2000);
        }
      }

      return newSheetID;
    }




    /// <summary>
    /// Creates a cheat sheet based on the rankings of a supplemental source.  This sheet also assumes that we want to create a cheat sheet for
    /// the current season as specified in the SeasonCodes table (the season with 'CurrentYear=true').
    /// </summary>
    public static int CreateCheatSheet(string sportCode, string sheetName, string statsSeasonCode, List<Position> cheatSheetPositions, List<Stat> cheatSheetStats,
                                        int supplementalSourceID, Dictionary<string, object> mappedProperties)
    {

      // determine how many players we'll include based on a globally-defined variable
      int totalSheetPlayers = Globals.GetDefaultPlayersPerSheet(cheatSheetPositions.Select(x => x.PositionCode).ToList());
      int suppPlayerCount = 0;

      /* determine if any mapped properties are set, if so use them */
      // PPR League
      bool? pprLeague = null;
      if (mappedProperties != null)
      {
        if (mappedProperties.ContainsKey(CSProperty.PPRLeague.ToString()))
        {
          pprLeague = (bool?)mappedProperties[CSProperty.PPRLeague.ToString()];
        }
      }

      // Auction Draft
      bool? auctionDraft = null;
      if (mappedProperties != null)
      {
        if (mappedProperties.ContainsKey(CSProperty.AuctionDraft.ToString()))
        {
          auctionDraft = (bool?)mappedProperties[CSProperty.AuctionDraft.ToString()];
        }
      }

      // get a reference to CSWR rankings
      SupplementalSheet targetSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason(sportCode).SeasonCode, supplementalSourceID, sportCode, cheatSheetPositions[0].PositionCode);
      List<SupplementalSheetItem> supplementalSheetItems = new List<SupplementalSheetItem>();
      supplementalSheetItems = SupplementalSheetItem.GetSupplementalSheetItems(targetSheet.SupplementalSheetID);
      suppPlayerCount = supplementalSheetItems.Count;

      // if the supplemental sheet has less items that the default sheet size, reduce the target size
      if (suppPlayerCount < totalSheetPlayers)
      {
        totalSheetPlayers = suppPlayerCount;
      }

      List<Player> cheatSheetPlayers = new List<Player>();
      switch (sportCode)
      {
        case "FOO":
          for (int i = 0; i < totalSheetPlayers; i++)
          {
            cheatSheetPlayers.Add(new Player(supplementalSheetItems[i].PlayerID, sportCode, String.Empty, String.Empty, String.Empty,
                                             String.Empty, String.Empty, 0, DateTime.Now, DateTime.MinValue, String.Empty, 0, false));
          }
          break;
        case "RAC":
          for (int i = 0; i < totalSheetPlayers; i++)
          {
            cheatSheetPlayers.Add(new Player(supplementalSheetItems[i].PlayerID, sportCode, String.Empty, String.Empty, String.Empty,
                                             String.Empty, String.Empty, 0, DateTime.Now, DateTime.MinValue, String.Empty, 0, false));
          }
          break;
      }


      

      // create a cheat sheet entity object to pass to the DAL
      CheatSheetDetails cheatSheetRecord = new CheatSheetDetails(0, CurrentUserName, SportSeason.GetCurrentSportSeason(sportCode).SeasonCode, sportCode, sheetName,
                                                statsSeasonCode, DateTime.Now, DateTime.Now, pprLeague, auctionDraft);

      // create a collection of position code entity objects to pass to the DAL
      List<PositionDetails> positionDetails = new List<PositionDetails>();
      for (int i = 0; i < cheatSheetPositions.Count; i++)
      {
        PositionDetails positionRecord = new PositionDetails(cheatSheetPositions[i].PositionCode, String.Empty, String.Empty);
        positionDetails.Add(positionRecord);
      }

      // create a collection of stat code entity objects to pass to the DAL
      List<StatDetails> statDetails = new List<StatDetails>();
      // cheat sheet stats will be null if multiple positions are selected
      if (cheatSheetStats != null)
      {
        if (cheatSheetStats.Count > 0)
        {
          for (int i = 0; i < cheatSheetStats.Count; i++)
          {
            StatDetails statRecord = new StatDetails(cheatSheetStats[i].StatCode, String.Empty, String.Empty, String.Empty);
            statDetails.Add(statRecord);
          }
        }
      }

      // create a collection of player entity objects to pass to the DAL
      List<PlayerDetails> playerDetails = new List<PlayerDetails>();
      for (int i = 0; i < totalSheetPlayers; i++)
      {
        PlayerDetails playerRecord = new PlayerDetails(cheatSheetPlayers[i].PlayerID, sportCode, cheatSheetPlayers[i].PositionCode, cheatSheetPlayers[i].FirstName,
            cheatSheetPlayers[i].LastName, cheatSheetPlayers[i].MiddleName, cheatSheetPlayers[i].TeamCode, cheatSheetPlayers[i].Number,
            cheatSheetPlayers[i].FirstYear, cheatSheetPlayers[i].BirthDate, cheatSheetPlayers[i].TwitterUsername, cheatSheetPlayers[i].StatMapID, cheatSheetPlayers[i].Retired);
        playerDetails.Add(playerRecord);
      }

      // call a DAL method to create the cheat sheet
      int newSheetID = 0;
      for (int i = 1; i <= Globals.TransactionRetryCount; i++)
      {
        newSheetID = SiteProvider.Sheets.CreateCheatSheet(cheatSheetRecord, CurrentUserName, positionDetails, statDetails, playerDetails, i);
        if (newSheetID != Globals.TransactionErrorSentinel)
        {
          break;
        }
        else
        {
          Thread.Sleep(2000);
        }
      }

      return newSheetID;
    }









    /// <summary>
    /// Gets all cheat sheets for a particular user
    /// </summary>
    public static List<CheatSheet> GetUserCheatSheets(string userName)
    {
      List<CheatSheet> userSheets = GetCheatSheetListFromCheatSheetDetailsList(SiteProvider.Sheets.GetUserCheatSheets(userName));
      userSheets.OrderBy(x => x.SheetName);
      return userSheets;
    }



    /// <summary>
    /// Gets all cheat sheets for a particular user and sport
    /// </summary>
    public static List<CheatSheet> GetUserCheatSheets(string userName, string sportCode)
    {
      List<CheatSheet> userSheets = GetCheatSheetListFromCheatSheetDetailsList(SiteProvider.Sheets.GetUserCheatSheets(userName, sportCode));
      userSheets.OrderBy(x => x.SheetName);
      return userSheets;
    }



    /// <summary>
    /// Gets all cheat sheets for a particular user, sport, and position.
    /// </summary>
    public static List<CheatSheet> GetUserCheatSheets(string userName, string sportCode, string positionCode)
    {
      return GetCheatSheetListFromCheatSheetDetailsList(SiteProvider.Sheets.GetUserCheatSheets(userName, sportCode, positionCode));
    }



    /// <summary>
    /// Gets all cheat sheets for a particular sport
    /// </summary>
    public static List<CheatSheet> GetCheatSheets(string sportCode)
    {
      return GetCheatSheetListFromCheatSheetDetailsList(SiteProvider.Sheets.GetCheatSheets(sportCode));
    }

    /// <summary>
    /// Gets all cheat sheets for a sport, season, and position
    /// </summary>
    public static List<CheatSheet> GetCheatSheets(string sportCode, string seasonCode, string positionCode)
    {
      return GetCheatSheetListFromCheatSheetDetailsList(SiteProvider.Sheets.GetCheatSheets(sportCode, seasonCode, positionCode));
    }


    /// <summary>
    /// Returns the total number of cheat sheets belonging to a particular user.
    /// </summary>
    public static int GetCheatSheetCount(string userName)
    {
      return SiteProvider.Sheets.GetCheatSheetCount(userName);
    }


    /// <summary>
    /// Returns the total number of cheat sheets belonging to a particular user for a particular sport
    /// </summary>
    public static int GetCheatSheetCount(string userName, string sportCode)
    {
      return SiteProvider.Sheets.GetCheatSheetCount(userName, sportCode);
    }


    /// <summary>
    /// Returns the total number of cheat sheets for a particular UserCategory (either all Visitors or
    /// all Members)
    /// </summary>
    /// <param name="includeVisitorSheets"></param>
    /// <returns></returns>
    public static int GetCheatSheetCount(UserCategory targetCategory)
    {
      int cheatSheetCount = 0;

      switch (targetCategory)
      {
        case UserCategory.VISITOR:
          cheatSheetCount = SiteProvider.Sheets.GetVisitorCheatSheetCount();
          break;
        case UserCategory.MEMBER:
          cheatSheetCount = SiteProvider.Sheets.GetMemberCheatSheetCount();
          break;
      }
      return cheatSheetCount;
    }

    /// <summary>
    /// Returns the total number of cheat sheets for a particular UserCategory (either all Visitors or
    /// all Members) and a particular sport
    /// </summary>
    public static int GetCheatSheetCount(UserCategory targetCategory, string sportCode)
    {
      int cheatSheetCount = 0;

      switch (targetCategory)
      {
        case UserCategory.VISITOR:
          cheatSheetCount = SiteProvider.Sheets.GetVisitorCheatSheetCount(sportCode);
          break;
        case UserCategory.MEMBER:
          cheatSheetCount = SiteProvider.Sheets.GetMemberCheatSheetCount(sportCode);
          break;
      }
      return cheatSheetCount;
    }



    /// <summary>
    /// Delete a cheat sheet with the specified ID.
    /// </summary>
    /// <param name="cheatSheetID">The unique ID of the cheat sheet to delete.</param>
    /// <returns>TRUE if the deletion was successful, FALSE otherwise</returns>
    public static bool DeleteCheatSheet(int cheatSheetID)
    {
      return SiteProvider.Sheets.DeleteCheatSheet(cheatSheetID);
    }

    /// <summary>
    /// And instance method for deleteing a cheat sheet
    /// </summary>
    /// <returns></returns>
    public bool Delete()
    {
      return CheatSheet.DeleteCheatSheet(this.CheatSheetID);
    }

    /// <summary>
    /// Deletes visitor cheat sheets that are older than 24 hours
    /// </summary>
    public static void DeleteOldVisitorCheatSheets()
    {
      SiteProvider.Sheets.DeleteOldVisitorCheatSheets();
    }



    /// <summary>
    /// Delete all cheat sheets belonging to a particular user.
    /// </summary>
    /// <param name="userName">The username on which to base the cheat sheet deletion</param>
    /// <returns>TRUE if the deletion was successful, FALSE otherwise</returns>
    public static bool DeleteUserCheatSheets(string userName)
    {
      return SiteProvider.Sheets.DeleteCheatSheets(userName);
    }




    /// <summary>
    /// Adds a particular player to the last position of a cheat sheet 
    /// </summary>
    public static bool AddCheatSheetItem(int cheatSheetID, int playerID, string note)
    {
      CheatSheetItemDetails record = new CheatSheetItemDetails(cheatSheetID, playerID, 0, false, false, false, note);
      bool ret = SiteProvider.Sheets.AddCheatSheetItem(record);
      BizObject.PurgeCacheItems("Sheets_CheatSheetItems_" + cheatSheetID.ToString());

      // update the cheat sheet's last modified date
      CheatSheet sheetToUpdate = CheatSheet.GetCheatSheet(cheatSheetID);
      sheetToUpdate.LastUpdated = DateTime.Now;
      sheetToUpdate.Update();

      return ret;
    }


    /// <summary>
    /// Removes a particular player from a cheat sheet and adjusts other items accordingly
    /// </summary>
    /// <param name="cheatSheetID">The id of the cheat sheet which the player should be removed from.</param>
    /// <param name="playerID">The player id of the item which should be removed from the respective cheat sheet.</param>
    /// <returns>TRUE if the removal was successful, FALSE otherwise</returns>
    public static bool RemoveCheatSheetItem(int cheatSheetID, int playerID)
    {
      CheatSheetItemDetails record = new CheatSheetItemDetails(cheatSheetID, playerID, 0, false, false, false, string.Empty);
      bool ret = SiteProvider.Sheets.RemoveCheatSheetItem(record);
      BizObject.PurgeCacheItems("Sheets_CheatSheetItems_" + cheatSheetID.ToString());

      // update the cheat sheet's last modified date
      CheatSheet sheetToUpdate = CheatSheet.GetCheatSheet(cheatSheetID);
      sheetToUpdate.LastUpdated = DateTime.Now;
      sheetToUpdate.Update();

      return ret;
    }

    /// <summary>
    /// Removes all items from a cheat sheet.  This is done before we re-sort a sheet
    /// </summary>
    /// <param name="cheatSheetID">The id of the cheat sheet which the player should be removed from.</param>
    /// <returns>TRUE if the removal was successful, FALSE otherwise</returns>
    public static bool RemoveAllCheatSheetItems(int cheatSheetID)
    {
      bool ret = SiteProvider.Sheets.RemoveAllCheatSheetItems(cheatSheetID);
      BizObject.PurgeCacheItems("Sheets_CheatSheetItemsByCheatSheetID" + cheatSheetID.ToString());

      // update the cheat sheet's last modified date
      CheatSheet sheetToUpdate = CheatSheet.GetCheatSheet(cheatSheetID);
      sheetToUpdate.LastUpdated = DateTime.Now;
      sheetToUpdate.Update();

      return ret;
    }


    /// <summary>
    /// Reorders items in a cheat sheet based on an item's old and new index, indexed starting with zero
    /// </summary>
    /// <param name="cheatSheetID">The unique id of the cheat sheet to operate.</param>
    /// <param name="oldIndex">The old index of the item to to be reordered</param>
    /// <param name="newIndex">The new index of the item to be reordered.</param>
    /// <returns>TRUE if the reorder was successful, FALSE otherwise</returns>
    public static bool ReorderCheatSheetItems(int cheatSheetID, int oldIndex, int newIndex)
    {
      BizObject.PurgeCacheItems("Sheets_CheatSheetItems_" + cheatSheetID.ToString());

      // update the cheat sheet's last modified date
      CheatSheet sheetToUpdate = CheatSheet.GetCheatSheet(cheatSheetID);
      sheetToUpdate.LastUpdated = DateTime.Now;
      sheetToUpdate.Update();

      return SiteProvider.Sheets.ReorderCheatSheetItems(cheatSheetID, oldIndex, newIndex);
    }





    /// <summary>
    /// Returns the total number of user cheat sheets which utilize the Sleeper feature
    /// </summary>
    public static int GetUserCheatSheetSleeperUsageCount(string sportCode, string seasonCode)
    {
      int cheatSheetCount = SiteProvider.Sheets.GetUserCheatSheetSleeperUsageCount(sportCode, seasonCode);
      return cheatSheetCount;
    }

    /// <summary>
    /// Returns the total number of user cheat sheets which utilize the Bust feature
    /// </summary>
    public static int GetUserCheatSheetBustUsageCount(string sportCode, string seasonCode)
    {
      int cheatSheetCount = SiteProvider.Sheets.GetUserCheatSheetBustUsageCount(sportCode, seasonCode);
      return cheatSheetCount;
    }

    /// <summary>
    /// Returns the total number of user cheat sheets which utilize the Note feature
    /// </summary>
    public static int GetUserCheatSheetNoteUsageCount(string sportCode, string seasonCode)
    {
      int cheatSheetCount = SiteProvider.Sheets.GetUserCheatSheetNoteUsageCount(sportCode, seasonCode);
      return cheatSheetCount;
    }





    /// <summary>
    /// Converts a cheat sheet entity into a cheat sheet business-level domain object.
    /// </summary>
    /// <param name="cheatSheet">A cheat sheet entity object.</param>
    /// <returns>A cheat sheet domain object.</returns>
    private static CheatSheet GetCheatSheetFromCheatSheetDetails(CheatSheetDetails cheatSheetDetails)
    {
      if (cheatSheetDetails == null)
        return null;
      else
      {
        Dictionary<string, object> mappedProperties = CheatSheet.GetMappedProperties(cheatSheetDetails);
        return new CheatSheet(cheatSheetDetails.CheatSheetID, cheatSheetDetails.Username, cheatSheetDetails.SeasonCode, cheatSheetDetails.SportCode, cheatSheetDetails.SheetName,
                                cheatSheetDetails.StatsSeasonCode, cheatSheetDetails.Created, cheatSheetDetails.LastUpdated, mappedProperties);
      }
    }

    /// <summary>
    /// Converts a collection of cheat sheet entities into a collection of cheat sheet business-level domain objects.
    /// </summary>
    /// <param name="cheatSheet">A collection of cheat sheet entity objects.</param>
    /// <returns>A collection cheat sheet domain objects.</returns>
    private static List<CheatSheet> GetCheatSheetListFromCheatSheetDetailsList(List<CheatSheetDetails> recordset)
    {
      List<CheatSheet> cheatSheets = new List<CheatSheet>();
      foreach (CheatSheetDetails record in recordset)
        cheatSheets.Add(GetCheatSheetFromCheatSheetDetails(record));
      return cheatSheets;
    }

    private static Dictionary<string, object> GetMappedProperties(CheatSheetDetails cheatSheetDetails)  
    {
      Dictionary<string, object> mappedProperties = new Dictionary<string, object>();
      if (cheatSheetDetails.PPRLeague != null)
      {
        mappedProperties.Add(CSProperty.PPRLeague.ToString(), cheatSheetDetails.PPRLeague);
      }
      if (cheatSheetDetails.ActionDraft != null)
      {
        mappedProperties.Add(CSProperty.AuctionDraft.ToString(), cheatSheetDetails.ActionDraft);
      }
      return mappedProperties;
    }


  }
}