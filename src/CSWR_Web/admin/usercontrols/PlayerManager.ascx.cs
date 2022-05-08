using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class PlayerManager : System.Web.UI.UserControl
  {

    public string SportCode
    {
      get
      {
        return (ViewState["SportCode"] == null) ? "FOO" : ViewState["SportCode"].ToString();
      }
      set
      {
        ViewState["SportCode"] = value;
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        BindControls();
        BindGrid();
      }
      // configure search-related fields
      labNoPlayersFound.Visible = false;
    }

    private void BindControls()
    {
      // Bind Sport Seasons
      ddlSportSeasons.DataSource = SportSeason.GetSportSeasons(this.SportCode);
      ddlSportSeasons.DataBind();
      // Bind Teams
      ddlTeams.DataSource = Team.GetTeams(this.SportCode);
      ddlTeams.DataBind();
      switch (this.SportCode)
      {
        case "FOO":
          trTeamRow.Visible = true;
          break;
        case "RAC":
          trTeamRow.Visible = false;
          break;
      }
    }


    private void BindGrid()
    {
      switch (this.SportCode)
      {
        case "FOO":
          gvPlayers.DataSource = Player.GetPlayers(this.SportCode, ddlSportSeasons.SelectedValue, ddlTeams.SelectedValue, cbRetired.Checked, true).OrderBy(x => x.FullNameLastFirst).ToList();
          break;
        case "RAC":
          gvPlayers.DataSource = Player.GetPlayersBySportSeasonPositionCodes(this.SportCode, ddlSportSeasons.SelectedValue, "DR", cbRetired.Checked, false);
          break;
      }
      gvPlayers.DataBind();
      gvPlayers.SelectedIndex = -1;
      // put the details view in edit mode
    }









    /// <summary>
    /// If click the 'edit' button for a particular user, put the details view in edit mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPlayers_SelectedIndexChanged(object sender, EventArgs e)
    {
      dvPlayerDetails.ChangeMode(DetailsViewMode.Edit);
      Player boundPlayer = Player.GetPlayer(int.Parse(gvPlayers.SelectedValue.ToString()));
      List<Player> playerCollection = new List<Player>();
      playerCollection.Add(boundPlayer);
      dvPlayerDetails.DataSource = playerCollection;
      dvPlayerDetails.DataBind();
    }

    /// <summary>
    /// When a row is created, we add an onclick even to the delete button to generate a confirm dialog
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPlayers_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        ImageButton deleteButton = e.Row.Cells[11].Controls[0] as ImageButton;
        deleteButton.OnClientClick = "if (confirm('Are you sure you want to delete this player?')==false)  return false; ";
      }
    }

    /// <summary>
    /// If we delete a row, rebind the players grid and put the details view back in insert mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPlayers_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
      // de-select players and re-bind
      gvPlayers.SelectedIndex = -1;
      gvPlayers.DataBind();
      // put the details view in edit mode
      dvPlayerDetails.ChangeMode(DetailsViewMode.Insert);
    }



    /// <summary>
    /// As we build each row in the grid, dynamically build things like the team name and years experience
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPlayers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        Player boundPlayer = (Player)e.Row.DataItem;

        Label labExperience = (Label)e.Row.FindControl("labExperience");
        HyperLink hlPlayerProfileLink = (HyperLink)e.Row.FindControl("hlPlayerProfileLink");
        
        // convert the FirstYear field to 'Experience'
        int experience = DateTime.Now.Year - boundPlayer.FirstYear.Year;
        labExperience.Text = experience.ToString() + " years";
        
        // show the team abbreviation instead of the team code
        switch (this.SportCode)
        {
          case "FOO":
            e.Row.Cells[3].Text = Team.GetTeam(boundPlayer.TeamCode).Abbreviation;


            string searchString = "https://www.google.com/search?q=";
            searchString += "site:nfl.com+\"" + boundPlayer.FirstName + "+" + boundPlayer.LastName + "\"+profile";
            searchString += "&btnI";
            hlPlayerProfileLink.NavigateUrl = searchString;
            break;
          case "RAC":
            e.Row.Cells[3].Text = Team.GetTeam(boundPlayer.TeamCode).FullTeamName;
            break;
        }
      }
    }



    /// <summary>
    /// If we insert a new item into the details view we need to deselect the currrent item in the grid view and rebind it
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dvPlayerDetails_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
      // de-select players and re-bind
      gvPlayers.SelectedIndex = -1;
      gvPlayers.DataBind();
    }


    /// <summary>
    /// If we update an item in the details view we need to deselect the current item in the grid view and rebind it
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dvPlayerDetails_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
    {
      // de-select players and re-bind
      gvPlayers.SelectedIndex = -1;
      gvPlayers.DataBind();
    }

    /// <summary>
    /// If we cancel the details view edit we need to deselect the current item in the gridview and rebind it
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dvPlayerDetails_ItemCommand(object sender, DetailsViewCommandEventArgs e)
    {
      if (e.CommandName == "Cancel")
      {
        // de-select players and re-bind
        gvPlayers.SelectedIndex = -1;
        gvPlayers.DataBind();
      }

    }



    /// <summary>
    /// When we build the details view we need to dynamically populate each template-based field
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dvPlayerDetails_DataBound(object sender, EventArgs e)
    {
      // get references to the appropriate controls
      Label detailHeader = (Label)dvPlayerDetails.FindControl("labDetailsHeader");

      TextBox tbFirstName = (TextBox)dvPlayerDetails.FindControl("tbFirstName");
      TextBox tbLastName = (TextBox)dvPlayerDetails.FindControl("tbLastName");
      TextBox tbMiddleName = (TextBox)dvPlayerDetails.FindControl("tbMiddleName");
      TextBox tbNumber = (TextBox)dvPlayerDetails.FindControl("tbNumber");
      TextBox tbStatMapID = (TextBox)dvPlayerDetails.FindControl("tbStatMapID");

      DropDownList ddlTeams = (DropDownList)dvPlayerDetails.FindControl("ddlTeams");
      DropDownList ddlPositions = (DropDownList)dvPlayerDetails.FindControl("ddlPositions");
      TextBox tbExperience = (TextBox)dvPlayerDetails.FindControl("tbExperience");
 
      CheckBox cbRetired = (CheckBox)dvPlayerDetails.FindControl("cbRetired");

      Player boundPlayer = (Player)dvPlayerDetails.DataItem;

      // sport teams
      List<Team> sportTeams = Team.GetTeams(this.SportCode);
      ddlTeams.DataSource = sportTeams;
      ddlTeams.DataBind();
      // sport positions
      List<Position> sportPositions = Position.GetPositions(this.SportCode);
      ddlPositions.DataSource = sportPositions;
      ddlPositions.DataBind();

      // load different controls based on the mode of the details view
      switch (dvPlayerDetails.CurrentMode)
      {
        case DetailsViewMode.Edit:
          // header
          detailHeader.Text = "Edit Player";
          // first name
          tbFirstName.Text = boundPlayer.FirstName;
          // last name
          tbLastName.Text = boundPlayer.LastName;
          // middle name
          tbMiddleName.Text = boundPlayer.MiddleName;
          // team
          ddlTeams.SelectedValue = boundPlayer.TeamCode;
          // position
          ddlPositions.SelectedValue = boundPlayer.PositionCode;
          // number
          tbNumber.Text = boundPlayer.Number.ToString();
          // retired
          cbRetired.Checked = boundPlayer.Retired;
          // statmapid
          tbStatMapID.Text = boundPlayer.StatMapID.ToString();
          // years experience
          int experience = DateTime.Now.Year - boundPlayer.FirstYear.Year;
          tbExperience.Text = experience.ToString();
          break;

        case DetailsViewMode.Insert:
          // header
          detailHeader.Text = "Insert Player";
          // sport teams
          ddlTeams.Items.Insert(0, new ListItem("Select Team", "0"));
          // sport positions
          ddlPositions.Items.Insert(0, new ListItem("Select Position", "0"));
          break;
      }
    }


    /// <summary>
    /// When inserting a new player, we need to calculate when a player's first year
    /// was based on how many years of expereience were configured
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dvPlayerDetails_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
      TextBox tbFirstName = (TextBox)dvPlayerDetails.FindControl("tbFirstName");
      TextBox tbLastName = (TextBox)dvPlayerDetails.FindControl("tbLastName");
      TextBox tbMiddleName = (TextBox)dvPlayerDetails.FindControl("tbMiddleName");
      TextBox tbNumber = (TextBox)dvPlayerDetails.FindControl("tbNumber");
      TextBox tbStatMapID = (TextBox)dvPlayerDetails.FindControl("tbStatMapID");

      DropDownList ddlTeamsinDetails = (DropDownList)dvPlayerDetails.FindControl("ddlTeams");
      DropDownList ddlPositions = (DropDownList)dvPlayerDetails.FindControl("ddlPositions");
      TextBox tbExperience = (TextBox)dvPlayerDetails.FindControl("tbExperience");

      CheckBox cbRetired = (CheckBox)dvPlayerDetails.FindControl("cbRetired");

      // experience
      int firstYear = DateTime.Now.Year - int.Parse(tbExperience.Text);
      DateTime fY = new DateTime(firstYear, 1, 1);

      int statMapID = 0;
      if (tbStatMapID.Text != String.Empty)
      {
        statMapID = int.Parse(tbStatMapID.Text);
      }

      Player.InsertPlayer(0, this.SportCode, ddlPositions.SelectedValue, tbFirstName.Text, tbLastName.Text, tbMiddleName.Text,
        ddlTeamsinDetails.SelectedValue, int.Parse(tbNumber.Text), fY, DateTime.MinValue, String.Empty, statMapID, cbRetired.Checked);

      // preserve the player's new team before we clear it out, so we re-bind the grid using this team
      string insertTeam = ddlTeamsinDetails.SelectedValue;

      // clear the insert form
      dvPlayerDetails.ChangeMode(DetailsViewMode.Insert);
      // clear controls
      tbFirstName.Text = String.Empty;
      tbMiddleName.Text = String.Empty;
      tbLastName.Text = String.Empty;
      ddlTeamsinDetails.SelectedIndex = 0;
      ddlPositions.SelectedIndex = 0;
      tbNumber.Text = String.Empty;
      tbExperience.Text = String.Empty;
      TextBox tbStatMapId = (TextBox)dvPlayerDetails.FindControl("tbStatMapId");
      tbStatMapId.Text = String.Empty;

      ddlTeams.SelectedValue = insertTeam;
      
      BindGrid();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dvPlayerDetails_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
      TextBox tbFirstName = (TextBox)dvPlayerDetails.FindControl("tbFirstName");
      TextBox tbLastName = (TextBox)dvPlayerDetails.FindControl("tbLastName");
      TextBox tbMiddleName = (TextBox)dvPlayerDetails.FindControl("tbMiddleName");
      TextBox tbNumber = (TextBox)dvPlayerDetails.FindControl("tbNumber");
      TextBox tbStatMapID = (TextBox)dvPlayerDetails.FindControl("tbStatMapID");
      DropDownList ddlTeams = (DropDownList)dvPlayerDetails.FindControl("ddlTeams");
      DropDownList ddlPositions = (DropDownList)dvPlayerDetails.FindControl("ddlPositions");
      TextBox tbExperience = (TextBox)dvPlayerDetails.FindControl("tbExperience");

      CheckBox cbRetired = (CheckBox)dvPlayerDetails.FindControl("cbRetired");

      // experience
      int firstYear = DateTime.Now.Year - int.Parse(tbExperience.Text);
      DateTime fY = new DateTime(firstYear, 1, 1);

      Player.UpdatePlayer(int.Parse(dvPlayerDetails.DataKey["PlayerID"].ToString()), this.SportCode, ddlPositions.SelectedValue, tbFirstName.Text, tbMiddleName.Text, tbLastName.Text,
        ddlTeams.SelectedValue, int.Parse(tbNumber.Text), fY, DateTime.MinValue, String.Empty, int.Parse(tbStatMapID.Text), cbRetired.Checked);

      BindGrid();
    
    }


    /// <summary>
    /// If we change teams, change the details view back to 'insert' mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTeams_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList ddlTeamsInDetails = (DropDownList)dvPlayerDetails.FindControl("ddlTeams");
      ddlTeamsInDetails.SelectedValue = ddlTeams.SelectedValue;
      tbLastName.Text = String.Empty;

      dvPlayerDetails.ChangeMode(DetailsViewMode.Insert);
      BindGrid();
    }

    protected void ddlSportSeasons_SelectedIndexChanged(object sender, EventArgs e)
    {
      dvPlayerDetails.ChangeMode(DetailsViewMode.Insert);
      BindGrid();
    }

     protected void ddlSportSeasons_DataBound(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        ddlSportSeasons.SelectedValue = SportSeason.GetCurrentSportSeason(SessionHandler.CurrentSportCode).SeasonCode;
      }
    }

     protected void dvPlayerDetails_ModeChanging(object sender, DetailsViewModeEventArgs e)
     {
       dvPlayerDetails.ChangeMode(e.NewMode);
       BindGrid();
     }

     protected void gvPlayers_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
       Player.DeletePlayer(int.Parse(gvPlayers.DataKeys[e.RowIndex].Value.ToString()));
       BindGrid();
     }

     protected void butSearch_Click(object sender, EventArgs e)
     {
       List<Player> searchResults = Player.GetPlayers(this.SportCode, tbLastName.Text);
       gvPlayers.DataSource = searchResults;
       gvPlayers.DataBind();
       gvPlayers.SelectedIndex = -1;

       if (searchResults.Count == 0)
       {
         labNoPlayersFound.Visible = true;
       }
     }


     protected void cbRetired_CheckedChanged(object sender, EventArgs e)
     {
       BindGrid();
     }


     protected void gvPlayers_DataBound(object sender, EventArgs e)
     {
       switch (this.SportCode)
       {
         case "FOO":
           trTeamRow.Visible = true;
           break;
         case "RAC":
           gvPlayers.Columns[9].Visible = false;
           break;
       }

     }
}
}