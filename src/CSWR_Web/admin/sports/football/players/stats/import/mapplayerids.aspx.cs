using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;
using BP.CheatSheetWarRoom.CSV;

namespace BP.CheatSheetWarRoom.UI.Admin.Sheets  
{
  public partial class MapPlayerIDs : System.Web.UI.Page
  {

    /// <summary>
    /// A private variable indicating which command is currently active
    /// </summary>
    private string ActiveCommand {get;set;}

    /// <summary>
    /// Counter indicating how many players were processed.
    /// </summary>
    private int PlayerCounter {get;set;}

    /// <summary>
    /// This property holds the filename of the spreadsheet where the StatMapIDs are stored
    /// </summary>
    private string FileLocation
    {
      get
      {
        string fileName = String.Empty;
        int currentWeek = 0;
        int.TryParse(ddlWeek.SelectedValue, out currentWeek);

        if(int.Parse(ddlWeek.SelectedValue) < 10)  
        {
          fileName = ddlSeasons.SelectedValue.Substring(2, 2) + "wk0" + currentWeek.ToString() + "co.csv";
        }
        else  
        {
          fileName = ddlSeasons.SelectedValue.Substring(2, 2) + "wk" + currentWeek.ToString() + "co.csv";
        }

        string baseDirectory = HttpContext.Current.Server.MapPath("~/TextFiles");
        string dirExtension = "\\Stats\\NFLStats\\" + ddlSeasons.SelectedValue + "\\" + fileName;

        // build the entire file location based on source directory and directory extension
        string fileLocation = baseDirectory + dirExtension; 
        
        return (ViewState["FileLocation"] == null) ? fileLocation : ViewState["FileLocation"].ToString();
      }
    }

    private MenuChoices CurrentMenuChoice
    {
      get
      {
        return (ViewState["CurrentMenuChoice"] == null) ? MenuChoices.ALLPLAYERS : (MenuChoices)ViewState["CurrentMenuChoice"];
      }
      set
      {
        ViewState["CurrentMenuChoice"] = value;
      }
    }



    /// <summary>
    /// 3 menu choices for viewing mapped or unmapped players
    /// </summary>
    private enum MenuChoices { ALLPLAYERS, NOSTATMAPID, NOPLAYER }



    /// <summary>
    /// Load the sport seasons
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
      labErrorMessage.Visible = false;

      if (!IsPostBack)
      {
        // load the default control values
        LoadControls();
        // bind all players
        LoadAllPlayers();
      }
    }


    /// <summary>
    /// Load the control values of dropdownlists and such
    /// </summary>
    private void LoadControls()
    {
      // load all sport seasons so we can decide how to map the StatMapIDs
      List<SportSeason> allStatSportSeasons = SportSeason.GetSportSeasons("FOO");
      ddlSeasons.DataSource = allStatSportSeasons;
      ddlSeasons.DataBind();
      // load the NFL season weeks
      for (int i = 1; i <= Globals.NflWeeksInSeason; i++)
      {
        ddlWeek.Items.Add(new ListItem(i.ToString(), i.ToString()));
      }
      ddlWeek.DataBind();
    }


    /// <summary>
    /// This method loads the 'All Players' collection, which lists players who played in the specified season (the season for which stats were loaded)
    /// </summary>
    private void LoadAllPlayers()
    {
      // load all players from the specified season
      List<Player> relevantPlayers = Player.GetPlayers("FOO", ddlSeasons.SelectedValue, false);
      BuildPlayerGrid(ref relevantPlayers);
      
      // show/hide controls
      BuildMenu(MenuChoices.ALLPLAYERS);
      // load totals
      labTotalPlayers.Text = relevantPlayers.Count.ToString();
    }

    /// <summary>
    /// This method loads the 'Players w/o StatMapID' collection, which lists players who played in the specified season
    /// but for whom no StatMapID could be found in the .csv vile
    /// </summary>
    private void LoadPlayersWithoutStatMapIDs()
    {
      List<Player> playersWithoutStatMapIDs = new List<Player>(); 
      List<Player> allSeasonPlayers =  Player.GetPlayers("FOO", ddlSeasons.SelectedValue, false);
      foreach (Player currentPlayer in allSeasonPlayers)
      {
        if (currentPlayer.StatMapID == 0)
        {
          playersWithoutStatMapIDs.Add(currentPlayer);
        }
      }
      BuildPlayerGrid(ref playersWithoutStatMapIDs);

      // show/hide controls
      BuildMenu(MenuChoices.NOSTATMAPID);
      // load totals
      labTotalPlayers.Text = playersWithoutStatMapIDs.Count.ToString();
    }

    /// <summary>
    /// This method loads the 'StatMapIDs without Players' collection, which are players who appear in the
    /// .csv file, but for whom no players could be found to match
    /// </summary>
    private void LoadStatMapIDsWithoutPlayers()
    {
      List<MappedPlayer> mappedPlayers = new List<MappedPlayer>();
      string fileLocation = this.FileLocation;

      // build the file location based on the season and week under consideration
      if (!File.Exists(fileLocation))
      {
        labErrorMessage.Visible = true;
        return;
      }
      else
      {
        labErrorMessage.Visible = false;
      }

      // using the CSVReader object we spin through each line and process the data
      using (CSVReader csv = new CSVReader(@fileLocation))
      {
        string[] fields;
        while ((fields = csv.GetCSVLine()) != null)
        {
          mappedPlayers.Add(new MappedPlayer(fields[(int)StatLayout.FULLNAMELASTFIRST], fields[(int)StatLayout.POSITION],
                              fields[(int)StatLayout.TEAM], int.Parse(fields[(int)StatLayout.PLAYERSTATMAPID])));
        }
      }

      List<Player> allPlayers = Player.GetPlayers("FOO");
      List<MappedPlayer> unfoundPlayers = new List<MappedPlayer>();
      foreach (MappedPlayer currentMappedPlayer in mappedPlayers)
      {
        if (currentMappedPlayer.Position.Trim() != "P")
        {
          Player foundPlayer = allPlayers.Find((delegate(Player targetPlayer) { return (targetPlayer.StatMapID == currentMappedPlayer.StatMapID); }));
          if (foundPlayer == null)
          {
            unfoundPlayers.Add(currentMappedPlayer);
          }
        }
      }


      repStatMapIdsWithoutPlayers.DataSource = unfoundPlayers;
      repStatMapIdsWithoutPlayers.DataBind();

      BuildMenu(MenuChoices.NOPLAYER);
      labTotalPlayers.Text = unfoundPlayers.Count.ToString();
    }

    /// <summary>
    /// This method binds players to the grid, but removes defensive players
    /// </summary>
    /// <param name="playersToBind"></param>
    private void BuildPlayerGrid(ref List<Player> playersToBind)
    {
      List<Team> footballTeams = Team.GetTeams("FOO");
      // remove all defensive players
      foreach (Team currentTeam in footballTeams)
      {
        Player foundPlayer = playersToBind.Find((delegate(Player targetPlayer) { return (targetPlayer.LastName == currentTeam.TeamCode); }));
        if (foundPlayer != null)
        {
          playersToBind.Remove(foundPlayer);
        }
      }
      gvPlayers.DataSource = playersToBind;
      gvPlayers.DataBind();


    }


    /// <summary>
    /// This event spins through the spreadsheet and processes each player to map the StatMapID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void butLoadStatMapIDs_Click(object sender, EventArgs e)
    {
      int mappedPlayerCount = 0;

      // build the file location based on the season and week under consideration
      string fileLocation = this.FileLocation;
      if (!File.Exists(fileLocation))
      {
        labErrorMessage.Visible = true;
        return;
      }
      else
      {
        labErrorMessage.Visible = false;
      }

      // using the CSVReader object we spin through each line and process the data
      using (CSVReader csv = new CSVReader(@fileLocation))
      {
        string[] fields;
        while ((fields = csv.GetCSVLine()) != null)
        {
          if (ProcessPlayer(fields))
          {
            mappedPlayerCount++;
          }
        }
      }

      labMappedPlayers.Text = "Mapped Players: " + mappedPlayerCount.ToString();
      LoadAllPlayers();


    }

    /// <summary>
    /// This method takes in a player record from the spreadsheet and searches the database for a player
    /// who matches the first and last name.  If one player matches the first and last name, then that player's
    /// StatMapID is updated with the StatMapID from the spreadsheet.
    /// </summary>
    /// <param name="fields"></param>
    private bool ProcessPlayer(string[] fields)
    {
      //QBRecord qbRecord = new QBRecord();
      //RBRecord rbRecord = new RBRecord();
      //WRRecord wrRecord = new WRRecord();
      //TERecord teRecord = new TERecord();
      //KRecord kRecord = new KRecord();

      // create some generic variables for general, non-position-specific use
      //string position = fields[(int)StatLayout.POSITION];
      //string team = fields[(int)StatLayout.TEAM];
      //string name = fields[(int)StatLayout.FULLNAMELASTFIRST];

      //qbRecord.Position = fields[(int)StatLayout.POSITION];
      //qbRecord.PlayerStatMapID = int.Parse(fields[(int)StatLayout.PLAYERSTATMAPID]);

      // since we can only be sure about the first and last name, forget about everything else     
      string[] fullName = fields[(int)StatLayout.FULLNAMELASTFIRST].Split(new Char[] { ' ' });
      string lastName = fullName[0].Remove(fullName[0].Length-1, 1);
      string firstName = fullName[1];

      List<Player> existingPlayers = Player.GetPlayers("FOO", firstName, lastName);
      if(existingPlayers != null)  
      {
        if (existingPlayers.Count == 1)
        {
          Player playerToUpdate = existingPlayers[0];
          if (playerToUpdate.StatMapID == 0)
          {
            playerToUpdate.StatMapID = int.Parse(fields[(int)StatLayout.PLAYERSTATMAPID]);
            playerToUpdate.Update();
            return true;
          }
        }
      }

      return false;
    }




    /// <summary>
    /// The event loads all players in the default menu item.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbSeasonPlayers_Click(object sender, EventArgs e)
    {
      this.CurrentMenuChoice = MenuChoices.ALLPLAYERS;
      LoadAllPlayers();
    }

    /// <summary>
    /// This event loads all players in the database that have no StatMapID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbNoStatMapID_Click(object sender, EventArgs e)
    {
      this.CurrentMenuChoice = MenuChoices.NOSTATMAPID;
      LoadPlayersWithoutStatMapIDs();
    }

    /// <summary>
    /// This method loads players in the spreadsheet for which we can't find an associated
    /// player in the database
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbStatMapNoPlayer_Click(object sender, EventArgs e)
    {
      this.CurrentMenuChoice = MenuChoices.NOPLAYER;
      LoadStatMapIDsWithoutPlayers();
    }


    /// <summary>
    /// If we change the season we need to rebind all of the players in the first menu item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlSeasons_SelectedIndexChanged(object sender, EventArgs e)
    {
      LoadAllPlayers();
    }

    /// <summary>
    /// If we change the week we need to rebind all of the players in the first menu item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
    {
      ClearActiveLinks();
      LoadAllPlayers();
    }

    /// <summary>
    /// This method defaults the link states back to their inactive states
    /// </summary>
    private void ClearActiveLinks()
    {
      lbSeasonPlayers.Visible = true;
      labSeasonPlayers.Visible = false;

      lbNoStatMapID.Visible = true;
      labNoStatMapID.Visible = false;

      lbStatMapNoPlayer.Visible = true;
      labStatMapNoPlayer.Visible = false;
    }

    /// <summary>
    /// Hide the edit buttton if we're in 'edit' mode on the grid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPlayers_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
      {
        if (!String.IsNullOrEmpty(this.ActiveCommand))
        {
          ImageButton ibEdit = (ImageButton)e.Row.FindControl("ibEdit");
          if (this.ActiveCommand == "Edit")
          {
            ibEdit.Visible = false;
          }
        }
      }
    }

    /// <summary>
    /// Called when we're updating a row in the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPlayers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      int playerID = Int32.Parse(gvPlayers.DataKeys[e.RowIndex].Value.ToString());
      Player targetPlayer = Player.GetPlayer(playerID);
      TextBox tbStatMapID = (TextBox)gvPlayers.Rows[e.RowIndex].FindControl("tbStatMapID");

      targetPlayer.StatMapID = int.Parse(tbStatMapID.Text);
      targetPlayer.Update();

      gvPlayers.EditIndex = -1;
      BindRelevantGrid();
    }


    /// <summary>
    /// Store the active comand in the local property 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPlayers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      this.ActiveCommand = e.CommandName;
    }


    /// <summary>
    /// When in edit mode load the edit index and all players
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPlayers_RowEditing(object sender, GridViewEditEventArgs e)
    {
      gvPlayers.EditIndex = e.NewEditIndex;
      BindRelevantGrid();
    }

    /// <summary>
    /// When cancelling the edit, set the index to -1 and re-bind all players
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPlayers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
      gvPlayers.EditIndex = -1;
      BindRelevantGrid();
    }

    private void BindRelevantGrid()
    {
      switch (this.CurrentMenuChoice)
      {
        case MenuChoices.ALLPLAYERS:
          LoadAllPlayers();
          break;
        case MenuChoices.NOSTATMAPID:
          LoadPlayersWithoutStatMapIDs();
          break;
        case MenuChoices.NOPLAYER:
          LoadStatMapIDsWithoutPlayers();
          break;
      }
    }

    /// <summary>
    /// Called for each row in the grid when players are bound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPlayers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      // if we're binding to a data row, manipulate the controls within
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        // get references to the appropriate controls
        Player boundPlayer = (Player)e.Row.DataItem;
        Label labStatMapID = (Label)e.Row.FindControl("labStatMapID");

        ImageButton ibUpdate = (ImageButton)e.Row.FindControl("ibUpdate");
        ImageButton ibCancel = (ImageButton)e.Row.FindControl("ibCancel");
        ImageButton ibEdit = (ImageButton)e.Row.FindControl("ibEdit");

        // if the row which was bound isn't being edited, load it with the relevant data
        if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
        {
          labStatMapID.Text = boundPlayer.StatMapID.ToString();

          // hide the buttons associated with adding a phone
          ibUpdate.Visible = false;
          ibCancel.Visible = false;
        }
        // if the row which was bound is being edited, load the appropriate controls for editing
        else if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {

          TextBox tbStatMapID = (TextBox)e.Row.FindControl("tbStatMapID");
          tbStatMapID.Text = boundPlayer.StatMapID.ToString();

          if (this.ActiveCommand == "Insert")
          {
            ibUpdate.Visible = false;
            ibEdit.Visible = false;
          }
        }

      }

    }


    /// <summary>
    /// Clear all StatMapIDs.  This is mostly for debugging purposes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void butClearStatMapIDs_Click(object sender, EventArgs e)
    {
      List<Player> allPlayers = Player.GetPlayers("FOO", ddlSeasons.SelectedValue, false);
      foreach (Player currentPlayer in allPlayers)
      {
        currentPlayer.StatMapID = 0;
        currentPlayer.Update();
      }
    }


    private class MappedPlayer
    {
      public MappedPlayer(string fullNameLastFirst, string position, string teamAbbreviation, int statMapID) 
      {
        this.FullNameLastFirst = fullNameLastFirst;
        this.Position = position;
        this.TeamAbbreviation = teamAbbreviation;
        this.StatMapID = statMapID;
      }

      public string FullNameLastFirst { get; set; }
      public string Position { get; set; }
      public string TeamAbbreviation { get; set; }
      public int StatMapID { get; set; }

      public string FirstName
      {
        get
        {
          return this.FullNameLastFirst.Split(',')[1].Trim();
        }
      }

      public string LastName
      {
        get
        {
          return this.FullNameLastFirst.Split(',')[0].Trim();
        }
      }

    }

      protected void repStatMapIdsWithoutPlayers_ItemDataBound(object sender, RepeaterItemEventArgs e)
      {
          if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
          {
              MappedPlayer boundMappedPlayer = (MappedPlayer) e.Item.DataItem;
              this.PlayerCounter++;

              Label labFullNameLastFirst = (Label) e.Item.FindControl("labFullNameLastFirst");
              Label labStatMapID = (Label) e.Item.FindControl("labStatMapID");
              Label labTeamAbbreviation = (Label) e.Item.FindControl("labTeamAbbreviation");
              Label labCounter = (Label) e.Item.FindControl("labCounter");
              Label labPosition = (Label) e.Item.FindControl("labPosition");
              HyperLink hlPlayerProfileLink = (HyperLink) e.Item.FindControl("hlPlayerProfileLink");
              HyperLink hlTwitterProfileLink = (HyperLink) e.Item.FindControl("hlTwitterProfileLink");

              labFullNameLastFirst.Text = boundMappedPlayer.FullNameLastFirst;
              labStatMapID.Text = boundMappedPlayer.StatMapID.ToString();
              labTeamAbbreviation.Text = boundMappedPlayer.TeamAbbreviation;
              labPosition.Text = boundMappedPlayer.Position;
              labCounter.Text = this.PlayerCounter.ToString();

              // since we've found a bunch of players in the stat file for which we cannot match a record in the database, we want to first
              // be doubly sure that the player we're searching for isn't in the database under a different name or something similar

              var nflProfileLink = "https://www.google.com/search?q=";
              nflProfileLink += "nfl.com+" + boundMappedPlayer.FirstName + "+" + boundMappedPlayer.LastName +
                                "+profile";
              nflProfileLink += "&btnI";
              hlPlayerProfileLink.NavigateUrl = nflProfileLink;

              // Twitter player profile search
              var twitterProfileSearchString = "https://www.google.com/search?q=";
              twitterProfileSearchString += "site:twitter.com+\"" + boundMappedPlayer.FirstName + "+" +
                                            boundMappedPlayer.LastName + "\"+latest Tweets+player";
              twitterProfileSearchString += "&btnI";
              hlTwitterProfileLink.NavigateUrl = twitterProfileSearchString;
          }
      }


      private void BuildMenu(MenuChoices choice)
          {
              switch (choice)
              {
                  // Shows all players with or without a StatMapID
                  case MenuChoices.ALLPLAYERS:
                      // show the 'all players' grid
                      gvPlayers.Visible = true;
                      repStatMapIdsWithoutPlayers.Visible = false;
                      // configure labels
                      labSeasonPlayers.Visible = true;
                      labNoStatMapID.Visible = false;
                      labStatMapNoPlayer.Visible = false;
                      // configure menu items
                      lbSeasonPlayers.Visible = false;
                      lbNoStatMapID.Visible = true;
                      lbStatMapNoPlayer.Visible = true;
                      break;

                  // Shos all players with no StatMapID
                  case MenuChoices.NOSTATMAPID:
                      // show the 'all players' grid
                      gvPlayers.Visible = true;
                      repStatMapIdsWithoutPlayers.Visible = false;
                      // configure labels
                      labSeasonPlayers.Visible = false;
                      labNoStatMapID.Visible = true;
                      labStatMapNoPlayer.Visible = false;
                      // configure menu items
                      lbSeasonPlayers.Visible = true;
                      lbNoStatMapID.Visible = false;
                      lbStatMapNoPlayer.Visible = true;
                      break;

                  case MenuChoices.NOPLAYER:
                      // show the repeater, hide the grid
                      gvPlayers.Visible = false;
                      repStatMapIdsWithoutPlayers.Visible = true;
                      // configure labels
                      labSeasonPlayers.Visible = false;
                      labNoStatMapID.Visible = false;
                      labStatMapNoPlayer.Visible = true;
                      // configure links
                      lbSeasonPlayers.Visible = true;
                      lbNoStatMapID.Visible = true;
                      lbStatMapNoPlayer.Visible = false;
                      break;
              }
          }

        /// <summary>
        /// Select the current season by default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSeasons_DataBound(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlSeasons.SelectedValue = SportSeason.GetCurrentSportSeason("FOO").SeasonCode;
            }
        }

        /// <summary>
        /// The layout of offensive statistics
        /// </summary>
        private enum StatLayout
    {
      FULLNAMELASTFIRST = 0,  // first name last first
      PLAYERSTATMAPID = 1,    // player stat map id
      TEAM = 2,               // team abbreviation
      TEAMSTATMAPID = 3,      // team stat map id
      POSITION = 4,           // position

      INJURYSTATUS = 5,       // not used

      GAM = 6,      // games played
      GAMS = 7,     // games started

      INACTIVE = 8,  // not used 

      COMP = 9,     // pass completions
      PATT = 10,     // pass attempts
      PAYD = 11,     // pass yards
      INT = 12,     // pass interceptions
      PATD = 13,    // pass touchdowns
      PA2P = 14,    // pass 2 point conversions
      PSAC = 15,    // pass sacks
      SCYD = 16,    // pass sack yards lost

      RATT = 17,    // rush attempts
      RUYD = 18,    // rush yards
      RUTD = 19,    // rush touchdowns
      RU2P = 20,    // rush 2 point conversions

      RETG = 21,    // receiving targets
      RREC = 22,    // receiving receptions
      REYD = 23,    // receiving yards
      RETD = 24,    // recieving touchdowns
      RE2P = 25,    // receiving 2 point conversions

      XPMA = 26,    // extra points
      XPAT = 27,    // extra points attempts
      XPBL = 28,    // extra points blocked

      FGMA = 29,    // field goals made 
      FGAT = 30,    // field goal attempts 
      FGBL = 31,    // field goals blocked
      FG29 = 32,    // field goals 29 yards and under 
      FG39 = 33,    // field goals between 30 and 39 yards 
      FG49 = 34,    // field goals between 40 and 49 yards 
      FG50 = 35,    // field goals 50 yards and over 

      FUML = 36     // fumbles lost
    }


    private class QBRecord
    {
      public int PlayerStatMapID { get; set; }
      public string Name { get; set; }
      public string Team { get; set; }
      public string Position { get; set; }
      public int GAMP { get; set; }
      public int GAMS { get; set; }
      public int COMP { get; set; }
      public int PAAT { get; set; }
      public int PAYD { get; set; }
      public int PATD { get; set; }
      public int RUYD { get; set; }
      public int RUTD { get; set; }
      public int FUM { get; set; }
      public int INT { get; set; }
      public int SAKD { get; set; }
      public int SKYD { get; set; }
    }


    private class RBRecord
    {
      public int PlayerStatMapID { get; set; }
      public string Name { get; set; }
      public string Team { get; set; }
      public string Position { get; set; }
      public int GAMP { get; set; }
      public int GAMS { get; set; }
      public int RUCA { get; set; }  // rushing carries
      public int RUYD { get; set; }
      public int RYPC { get; set; }  // rushing yards per carry (CALCULATED)
      public int RUTD { get; set; }
      public int RETR { get; set; }  // receiving targets
      public int RECP { get; set; }  // receptions
      public int REYD { get; set; }
      public int RETD { get; set; }
      public int FUM { get; set; }
    }


    private class WRRecord
    {
      public int PlayerStatMapID { get; set; }
      public string Name { get; set; }
      public string Team { get; set; }
      public string Position { get; set; }

      public int GAMP { get; set; }
      public int GAMS { get; set; }

      public int RUYD { get; set; }
      public int RUTD { get; set; }

      public int RETR { get; set; }  // receiving targets
      public int RECP { get; set; }  // receptions
      public int REYD { get; set; }
      public int RETD { get; set; }

      public int FUM { get; set; }
    }


    private class TERecord
    {
      public int PlayerStatMapID { get; set; }
      public string Name { get; set; }
      public string Team { get; set; }
      public string Position { get; set; }
      public int GAMP { get; set; }
      public int GAMS { get; set; }

      public int RUYD { get; set; }
      public int RUTD { get; set; }

      public int RETR { get; set; }  // receiving targets
      public int RECP { get; set; }  // receptions
      public int REYD { get; set; }
      public int RETD { get; set; }

      public int FUM { get; set; }

    }



    private class KRecord
    {
      public int PlayerStatMapID { get; set; }

      public string Name { get; set; }
      public string Team { get; set; }

      public string Position { get; set; }
      public int MAFG { get; set; }
      public int MIFG { get; set; }

      public int MAXP { get; set; }
      public int MIXP { get; set; }

      public int FG_0029 { get; set; }
      public int FG_3039 { get; set; }
      public int FG_4049 { get; set; }
      public int FG_50PL { get; set; }
    }




    private class DEFRecord
    {
      public int PlayerStatMapID { get; set; }
      public string Team { get; set; }
      public int FREC { get; set; }
      public int INT { get; set; }
      public int SACK { get; set; }
      public int DTD { get; set; }
      public int PA { get; set; }
      public int SAF { get; set; }
      public int BFG { get; set; }
    }

}
}
