using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class FooPlayerManager : System.Web.UI.UserControl
  {

    /// <summary>
    /// A private variable indicating which command is currently active
    /// </summary>
    private string _activeCommand = String.Empty;

    /// <summary>
    /// This is set to true for Moderators who are only able to update a few player-specific fields
    /// </summary>
    public bool LimitRights
    {
      get
      {
        return (ViewState["LimitRights"] == null) ? true : (bool)ViewState["LimitRights"];
      }
      set
      {
        ViewState["LimitRights"] = value;
      }
    }
    

    /// <summary>
    /// A flag for indicating if we're searching or not.  This tells us which collection to bind to
    /// </summary>
    public bool SearchActive
    {
      set { ViewState["SearchActive"] = value; }
      get
      {
        return (ViewState["SearchActive"] == null) ? false : (bool)ViewState["SearchActive"];
      }
    }

    /// <summary>
    /// Indicates if we just performed an Insert, and thus need to re-bind the entire list
    /// </summary>
    public bool Inserted
    {
      set { ViewState["Inserted"] = value; }
      get
      {
        return (ViewState["Inserted"] == null) ? false : (bool)ViewState["Inserted"];
      }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
      Inserted = false;
      if (!IsPostBack)
      {
        InitializeControls();
        BindPlayersByControls();
      }
    }


    private void InitializeControls()
    {
      // bind the seasons dropdown
      ddlSportSeasons.DataSource = SportSeason.GetSportSeasons(FOO.FOOString).OrderByDescending(x => x.SeasonCode).Take(2).ToList();
      ddlSportSeasons.DataBind();
      // bind the teams
      ddlTeams.DataSource = Team.GetTeams(FOO.FOOString);
      ddlTeams.DataBind();

      if (!this.LimitRights)
      {
        // allow webmaster to add players
        lbAddPlayer.Visible = true;
        hlRequestPlayerAddition.Visible = false;
      }
      else
      {
        // hide the button to generate player profiles
        butOpenPlayerProfiles.Visible = false;
        // don't let moderators change the player year
        trSeasonRow.Visible = false;
        // don't let moderators add players to avoid accidental duplicates
        lbAddPlayer.Visible = false;
        hlRequestPlayerAddition.Visible = true;
      }

    }

    /// <summary>
    /// Binds a generic list of Players to the gridview, always orderd by lastName, firstName
    /// </summary>
    /// <param name="targetPlayers"></param>
    private void BindPlayers(List<Player> targetPlayers)
    {
      gvPlayers.DataSource = targetPlayers.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
      gvPlayers.DataBind();
    }


    /// <summary>
    /// Builds a bindable list of players based on the controls selected in the header (season, team, retired)
    /// </summary>
    private void BindPlayersByControls()
    {

      List<Player> teamPlayers = Player.GetPlayers(FOO.FOOString, ddlSportSeasons.SelectedValue, ddlTeams.SelectedValue, cbRetired.Checked);


      if (_activeCommand == "Insert")
      {
        // guess the player is roughtly 22 years old
        DateTime birthdayGuess = DateTime.Now.AddYears(-22);
        teamPlayers.Insert(0, new Player(0, FOO.FOOString, FOOPositionsOffense.QB.ToString(), String.Empty, String.Empty, String.Empty,
                                        ddlTeams.SelectedValue, 0, DateTime.Now, birthdayGuess, String.Empty, 0, false));
      }

      BindPlayers(teamPlayers);
    }


    /// <summary>
    /// Iterate over the gridview rows to properly configure the data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPlayers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      // if we're binding to a data row, manipulate the controls within
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        // get references to the appropriate controls
        var boundPlayer = (Player)e.Row.DataItem;

        var ibDelete = (ImageButton)e.Row.FindControl("ibDelete");
        var ibUpdate = (ImageButton)e.Row.FindControl("ibUpdate");
        var ibCancel = (ImageButton)e.Row.FindControl("ibCancel");
        var ibAdd = (ImageButton)e.Row.FindControl("ibAdd");
        var ibEdit = (ImageButton)e.Row.FindControl("ibEdit");

        var labPosition = (Label)e.Row.FindControl("labPosition");
        var labLastName = (Label)e.Row.FindControl("labLastName");
        var labMiddleName = (Label)e.Row.FindControl("labMiddleName");
        var labFirstName = (Label)e.Row.FindControl("labFirstName");

        // if the row which was bound isn't being edited, load it with the relevant data
        if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
        {
          // get references to the appropriate controls
          var labTeamAbbreviation = (Label)e.Row.FindControl("labTeamAbbreviation");
          var labNumber = (Label)e.Row.FindControl("labNumber");
          var labStatMapId = (Label)e.Row.FindControl("labStatMapID");
          var labExperienceInYears = (Label)e.Row.FindControl("labExperienceInYears");
          var labBirthDate = (Label)e.Row.FindControl("labBirthDate");
          var labTwitterUsername = (Label)e.Row.FindControl("labTwitterUsername");
          var labYearsLabel = (Label)e.Row.FindControl("labYearsLabel");

          var labRetired = (Label)e.Row.FindControl("labRetired");
          var hlPlayerNflProfileLink = (HyperLink)e.Row.FindControl("hlPlayerNFLProfileLink");
          var hlPlayerTwitterProfileLink = (HyperLink)e.Row.FindControl("hlPlayerTwitterProfileLink");

          // hide the buttons associated with adding a drivers
          ibUpdate.Visible = false;
          ibCancel.Visible = false;
          ibAdd.Visible = false;
          if (this.LimitRights)
          {
            ibDelete.Visible = false;
          }

          // load the labels
          if (boundPlayer.PlayerID != 0)
          {
            // Last Name
            labLastName.Text = boundPlayer.LastName;
            // Middle Name
            labMiddleName.Text = boundPlayer.MiddleName;
            // First Name
            labFirstName.Text = boundPlayer.FirstName;
            // Abbreviation
            labTeamAbbreviation.Text = boundPlayer.Team.Abbreviation;
            // Position
            labPosition.Text = boundPlayer.Position.Abbreviation;
            // Number
            labNumber.Text = boundPlayer.Number.ToString();
            // StatMapID
            labStatMapId.Text = boundPlayer.StatMapID.ToString();
            // Experience
            if (boundPlayer.YearsExperience == 0)
            {
              labExperienceInYears.Text = "rookie";
            }
            else if (boundPlayer.YearsExperience == 1)
            {
              labExperienceInYears.Text = boundPlayer.YearsExperience.ToString() + " year";
            }
            else
            {
              labExperienceInYears.Text = boundPlayer.YearsExperience.ToString() + " years";
            }
            // Birth Date
            if (boundPlayer.BirthDate != DateTime.MinValue)
            {
              labBirthDate.Text = boundPlayer.BirthDate.Date.ToShortDateString();
            }
            // Twitter Username
            labTwitterUsername.Text = boundPlayer.TwitterUsername;

            labRetired.Text = (boundPlayer.Retired) ? "true" : "false";

            // NFL player profile search
            string nflProfileSearchString = "https://www.google.com/search?q=";
            nflProfileSearchString += "site:nfl.com+\"" + boundPlayer.FirstName + "+" + boundPlayer.LastName + "\"+profile";
            nflProfileSearchString += "&btnI";
            hlPlayerNflProfileLink.NavigateUrl = nflProfileSearchString;

            // if we don't know the player's Twitter handle, then we formulate a guess
            if (boundPlayer.TwitterUsername != String.Empty)
            {
              // Twitter player profile search
              hlPlayerTwitterProfileLink.NavigateUrl = "https://www.twitter.com/" + boundPlayer.TwitterUsername;
            }
            else
            {
              // Twitter player profile search
              string twitterProfileSearchString = "https://www.google.com/search?q=";
              twitterProfileSearchString += "site:twitter.com+\"" + boundPlayer.FirstName + "+" + boundPlayer.LastName + "\"+latest Tweets+player";
              twitterProfileSearchString += "&btnI";
              hlPlayerTwitterProfileLink.NavigateUrl = twitterProfileSearchString;
            }





          }
        }
        // if the row which was bound is being edited, load the appropriate controls for editing
        else if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
          // get references to the appropriate controls
          var tbLastName = (TextBox)e.Row.FindControl("tbLastName");
          var tbFirstName = (TextBox)e.Row.FindControl("tbFirstName");
          var tbMiddleName = (TextBox)e.Row.FindControl("tbMiddleName");
          var ddlTeams = (DropDownList)e.Row.FindControl("ddlTeams");
          var ddlPositions = (DropDownList)e.Row.FindControl("ddlPositions");
          var tbNumber = (TextBox)e.Row.FindControl("tbNumber");
          var tbStatMapID = (TextBox)e.Row.FindControl("tbStatMapID");
          var tbExperienceInYears = (TextBox)e.Row.FindControl("tbExperienceInYears");
          var labYearsLabel = (Label)e.Row.FindControl("labYearsLabel");
          var tbBirthDate = (TextBox)e.Row.FindControl("tbBirthDate");
          var tbTwitterUsername = (TextBox)e.Row.FindControl("tbTwitterUsername");
          var cbRetired = (CheckBox)e.Row.FindControl("cbRetired");

          var rfvFirstName = (RequiredFieldValidator)e.Row.FindControl("rfvFirstName");
          var rfvLastName = (RequiredFieldValidator)e.Row.FindControl("rfvLastName");

          List<Team> allTeams = Team.GetTeams(FOO.FOOString);
          ddlTeams.DataSource = allTeams;
          ddlTeams.DataBind();

          List<Position> allFOOPositions = Position.GetPositions(FOO.FOOString);
          ddlPositions.DataSource = allFOOPositions;
          ddlPositions.DataBind();

          // if we're inserting (as opposed to editing), hide the appropriate buttons 
          if (_activeCommand == "Insert")
          {
            ibDelete.Visible = false;
            ibUpdate.Visible = false;
            ibEdit.Visible = false;
            ibAdd.Visible = true;
            labYearsLabel.Visible = true;

            // get references to the appropriate controls
            tbLastName.Text = boundPlayer.LastName;
            tbLastName.Focus();
            tbFirstName.Text = boundPlayer.FirstName;
            tbMiddleName.Text = boundPlayer.MiddleName;
            ddlTeams.SelectedValue = boundPlayer.TeamCode;
            ddlPositions.SelectedValue = boundPlayer.PositionCode;
            tbNumber.Text = boundPlayer.Number.ToString();
            tbStatMapID.Text = (boundPlayer.StatMapID == 0) ? String.Empty : boundPlayer.StatMapID.ToString();
            tbExperienceInYears.Text = "0";
            tbBirthDate.Text = boundPlayer.BirthDate.Date.ToShortDateString();
            tbTwitterUsername.Text = boundPlayer.TwitterUsername;
            cbRetired.Checked = boundPlayer.Retired;
            labYearsLabel.Visible = true;
          }
          else
          {
            // get references to the appropriate controls
            ddlTeams.SelectedValue = boundPlayer.TeamCode;

            if (this.LimitRights)
            {
              labFirstName.Text = boundPlayer.FirstName;
              labLastName.Text = boundPlayer.LastName;
              labMiddleName.Text = boundPlayer.MiddleName;
              labPosition.Text = boundPlayer.PositionCode;

              ddlPositions.Visible = false;
              tbFirstName.Visible = false;
              tbLastName.Visible = false;
              tbMiddleName.Visible = false;
              rfvFirstName.Enabled = false;
              rfvLastName.Enabled = false;
            }
            else
            {
              tbLastName.Text = boundPlayer.LastName;
              tbFirstName.Text = boundPlayer.FirstName;
              tbMiddleName.Text = boundPlayer.MiddleName;

              labPosition.Visible = false;
              labFirstName.Visible = false;
              labLastName.Visible = false;
              labMiddleName.Visible = false;
              ddlPositions.SelectedValue = boundPlayer.PositionCode;
            }
            
            tbNumber.Text = boundPlayer.Number.ToString();
            tbStatMapID.Text = boundPlayer.StatMapID.ToString();
            tbExperienceInYears.Text = boundPlayer.YearsExperience.ToString();

            if (boundPlayer.BirthDate != DateTime.MinValue)
            {
              tbBirthDate.Text = boundPlayer.BirthDate.Date.ToShortDateString();
            }
            tbTwitterUsername.Text = boundPlayer.TwitterUsername;
            tbTwitterUsername.Focus();
            cbRetired.Checked = boundPlayer.Retired;
            labYearsLabel.Visible = true;

            ibAdd.Visible = false;
          }
        }

        // if we're binding to an 'Add' row, put the gridview in Edit mode for that row.
        if ((e.Row.DataItem != null) && (!((BasePage)this.Page).IsRefresh))
        {
          if (((Player)e.Row.DataItem).PlayerID == 0)
          {
            gvPlayers.EditIndex = e.Row.RowIndex;
          }
        }
      }
    }

    /// <summary>
    /// This method fire when we update a respective row.  We update the Players based on the
    /// values in the row controls
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPlayers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      GridViewRow editRow = gvPlayers.Rows[gvPlayers.EditIndex];

      // get references to the appropriate controls
      var tbLastName = (TextBox)editRow.FindControl("tbLastName");
      var tbFirstName = (TextBox)editRow.FindControl("tbFirstName");
      var tbMiddleName = (TextBox)editRow.FindControl("tbMiddleName");
      var ddlTeams = (DropDownList)editRow.FindControl("ddlTeams");
      var ddlPositions = (DropDownList)editRow.FindControl("ddlPositions");
      var tbBirthDate = (TextBox)editRow.FindControl("tbBirthDate");
      var tbTwitterUsername = (TextBox)editRow.FindControl("tbTwitterUsername");
      var tbNumber = (TextBox)editRow.FindControl("tbNumber");
      var tbStatMapID = (TextBox)editRow.FindControl("tbStatMapID");
      var tbExperienceInYears = (TextBox)editRow.FindControl("tbExperienceInYears");
      var cbRetired = (CheckBox)editRow.FindControl("cbRetired");

      // Player Number
      int playerNumber = 0;
      int.TryParse(tbNumber.Text, out playerNumber);

      // Birth Date
      DateTime playerBirthDate = DateTime.MinValue;
      DateTime.TryParse(tbBirthDate.Text, out playerBirthDate);

      // Years Experience
      int firstYear = DateTime.Now.Year - int.Parse(tbExperienceInYears.Text);
      DateTime fY = new DateTime(firstYear, 1, 1);

      // Twitter Username
      string twitterUsername = tbTwitterUsername.Text;

      // update all stats
      int playerID = (int)gvPlayers.DataKeys[gvPlayers.EditIndex].Value;

      // get a reference to the target player
      Player targetPlayer = Player.GetPlayer(playerID);

      // we don't let mods change Twitter Username or StatMapID, so we have to 
      // initialize these from the database
      int statMapID = 0;
      string positionCode = String.Empty;
      string firstName = String.Empty;
      string lastName = String.Empty;
      string middleName = String.Empty;
      if (this.LimitRights)
      {
        statMapID = targetPlayer.StatMapID;
        positionCode = targetPlayer.PositionCode;
        firstName = targetPlayer.FirstName;
        lastName = targetPlayer.LastName;
        middleName = targetPlayer.MiddleName;
      }
      else
      {
        int.TryParse(tbStatMapID.Text, out statMapID);
        positionCode = ddlPositions.SelectedValue;
        firstName = tbFirstName.Text;
        middleName = tbMiddleName.Text;
        lastName = tbLastName.Text;
      }
      
      // if the player switched positions, we need to remove them from all position-specific sheets for 
      // which they are no longer applicable
      int testSheetCounter = 0;
      if (targetPlayer.PositionCode != positionCode)
      {
        List<CheatSheet> allFOOCheatSheets = CheatSheet.GetCheatSheets(FOO.FOOString);
        foreach (CheatSheet targetSheet in allFOOCheatSheets)
        {
          List<CheatSheetItem> targetCheatSheetItems = CheatSheetItem.GetCheatSheetItems(targetSheet.CheatSheetID);
          CheatSheetItem targetItem = targetCheatSheetItems.SingleOrDefault(x => x.PlayerID == playerID);
          if (targetItem != null)
          {
            CheatSheet.RemoveCheatSheetItem(targetItem.CheatSheetID, playerID);
            testSheetCounter++;
          }
        }
      }

      Player.UpdatePlayer(playerID, FOO.FOOString, positionCode, firstName, middleName, lastName,
                              ddlTeams.SelectedValue, playerNumber, fY, playerBirthDate, twitterUsername, statMapID, cbRetired.Checked);

      _activeCommand = String.Empty;
      gvPlayers.EditIndex = -1;

      if (SearchActive)
      {
        BindPlayersBySearch();
      }
      else
      {
        BindPlayersByControls();
      }

    }


    /// <summary>
    /// After the gridview is bound, we need to hide those 'Arrow' controls that aren't relevant
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPlayers_DataBound(object sender, EventArgs e)
    {
      if ((_activeCommand == "Insert") && (!((BasePage)this.Page).IsRefresh) && (!Inserted))
      {
        Inserted = true;
        BindPlayersByControls();
      }

      if (this.LimitRights)
      {
        // hide NFL shortcut icon to reduce confusion
        //gvPlayers.Columns[gvPlayers.Columns.Count - 2].Visible = false;
        // hide twitter username column
        //gvPlayers.Columns[gvPlayers.Columns.Count - 4].Visible = false;
        // hide StatMapID column
        gvPlayers.Columns[gvPlayers.Columns.Count - 9].Visible = false;
      }
    }


    /// <summary>
    /// After a row is created in the gridview, and if it is in edit mode,
    /// we need to hide the edit and delete buttons (because the update and cancel buttons
    /// will be shown
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPlayers_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
      {
        if (!String.IsNullOrEmpty(_activeCommand))
        {

          ImageButton ibEdit = (ImageButton)e.Row.FindControl("ibEdit");
          ImageButton ibDelete = (ImageButton)e.Row.FindControl("ibDelete");

          if (_activeCommand == "Edit")
          {
            ibEdit.Visible = false;
            ibDelete.Visible = false;
          }
        }
      }
    }

    /// <summary>
    /// When a command is fired from a command row, save the command so we can reference
    /// it directly later
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPlayers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      _activeCommand = e.CommandName;
    }


    /// <summary>
    /// This event will be called when we actually want to insert a player into the database
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void ibUpdate_Command(object sender, EventArgs e)
    //{

    //  int i = 0;
    //}

    /// <summary>
    /// This event will be called when we actually want to insert a player into the database
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibAdd_Command(object sender, EventArgs e)
    {
      TextBox tbLastName = (TextBox)gvPlayers.Rows[gvPlayers.EditIndex].FindControl("tbLastName");
      TextBox tbFirstName = (TextBox)gvPlayers.Rows[gvPlayers.EditIndex].FindControl("tbFirstName");
      TextBox tbMiddleName = (TextBox)gvPlayers.Rows[gvPlayers.EditIndex].FindControl("tbMiddleName");
      DropDownList ddlTeams = (DropDownList)gvPlayers.Rows[gvPlayers.EditIndex].FindControl("ddlTeams");
      DropDownList ddlPositions = (DropDownList)gvPlayers.Rows[gvPlayers.EditIndex].FindControl("ddlPositions");
      TextBox tbBirthDate = (TextBox)gvPlayers.Rows[gvPlayers.EditIndex].FindControl("tbBirthDate");
      TextBox tbTwitterUsername = (TextBox)gvPlayers.Rows[gvPlayers.EditIndex].FindControl("tbTwitterUsername");
      TextBox tbNumber = (TextBox)gvPlayers.Rows[gvPlayers.EditIndex].FindControl("tbNumber");
      TextBox tbStatMapID = (TextBox)gvPlayers.Rows[gvPlayers.EditIndex].FindControl("tbStatMapID");
      TextBox tbExperienceInYears = (TextBox)gvPlayers.Rows[gvPlayers.EditIndex].FindControl("tbExperienceInYears");
      CheckBox cbRetired = (CheckBox)gvPlayers.Rows[gvPlayers.EditIndex].FindControl("cbRetired");

      // Player Number
      int playerNumber = 0;
      int.TryParse(tbNumber.Text, out playerNumber);

      // Stat Map ID
      int statMapID = 0;
      int.TryParse(tbStatMapID.Text, out statMapID);

      // Birth Date
      DateTime playerBirthDate = DateTime.MinValue;
      DateTime.TryParse(tbBirthDate.Text, out playerBirthDate);

      // Years Experience
      int firstYear = DateTime.Now.Year - int.Parse(tbExperienceInYears.Text);
      DateTime fY = new DateTime(firstYear, 1, 1);

      // update all stats
      //int playerID = (int)gvPlayers.DataKeys[gvPlayers.EditIndex].Value;

      Player.InsertPlayer(0, FOO.FOOString, ddlPositions.SelectedValue, tbFirstName.Text, tbLastName.Text, tbMiddleName.Text, ddlTeams.SelectedValue,
                                playerNumber, fY, playerBirthDate, tbTwitterUsername.Text, statMapID, cbRetired.Checked);

      _activeCommand = String.Empty;
      gvPlayers.EditIndex = -1;
      BindPlayersByControls();
    }


    /// <summary>
    /// This event is fired when the user makes a request to add a new player so we know to insert a new player line
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbAddPlayer_Click(object sender, EventArgs e)
    {
      _activeCommand = "Insert";
      gvPlayers.EditIndex = -1;
      BindPlayersByControls();
    }



    protected void ddlSportSeasons_DataBound(object sender, EventArgs e)
    {
      ddlSportSeasons.SelectedValue = FOO.CurrentSeason;
    }


    protected void gvPlayers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
      _activeCommand = String.Empty;
      gvPlayers.EditIndex = -1;
      BindPlayersByControls();
    }


    protected void gvPlayers_RowEditing(object sender, GridViewEditEventArgs e)
    {
      gvPlayers.EditIndex = e.NewEditIndex;

      if (SearchActive)
      {
        BindPlayersBySearch();
      }
      else
      {
        BindPlayersByControls();
      }
    }


    protected void gvPlayers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      int playerID = int.Parse(gvPlayers.DataKeys[e.RowIndex].Value.ToString());

      // delete the player
      Player.DeletePlayer(int.Parse(gvPlayers.DataKeys[e.RowIndex].Value.ToString()));

      // re-bind sheets
      gvPlayers.EditIndex = -1;
      BindPlayersByControls();
    }


    protected void ddlSportSeasons_SelectedIndexChanged(object sender, EventArgs e)
    {
      BindPlayersByControls();
    }


    protected void ddlTeams_SelectedIndexChanged(object sender, EventArgs e)
    {
      tbLastName.Text = String.Empty;
      BindPlayersByControls();
    }

    protected void cbRetired_CheckedChanged(object sender, EventArgs e)
    {
      if (tbLastName.Text != String.Empty)
      {
        BindPlayersBySearch();
      }
      else
      {
        BindPlayersByControls();
      }
    }

    protected void butSearch_Click(object sender, EventArgs e)
    {
      gvPlayers.EditIndex = -1;
      BindPlayersBySearch();
      this.SearchActive = true;
    }


    private void BindPlayersBySearch()
    {
      List<Player> searchResults = Player.GetPlayers(FOO.FOOString, tbLastName.Text);

      if (cbRetired.Checked)
      {
        BindPlayers(searchResults);
      }
      else
      {
        BindPlayers(searchResults.Where(x => x.Retired == false).ToList());
      }
    }



    protected void butClearSearch_Click(object sender, ImageClickEventArgs e)
    {
      tbLastName.Text = String.Empty;
      BindPlayersByControls();

      this.SearchActive = false;
    }

  
  }
}