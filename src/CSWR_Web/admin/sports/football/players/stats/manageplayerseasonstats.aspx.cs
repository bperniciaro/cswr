using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.Admin.Sheets
{
  public partial class ManagePlayerSeasonStats : BasePage
  {

    /// <summary>
    /// A private variable indicating which command is currently active
    /// </summary>
    private string _activeCommand = String.Empty;

    public string CurrentPositionCode  
    {
      get
      {
        return (ViewState["CurrentPositionCode"] == null) ? String.Empty : ViewState["CurrentPositionCode"].ToString();
      }
      set
      {
        ViewState["CurrentPositionCode"] = value;
      }
    }


    private void Page_Load()
    {
      if (!IsPostBack)
      {
        this.CurrentPositionCode = "QB";
        LoadSeasonDropdown();
        LoadTeamsDropdown();
        LoadPositionsRepeater();
        ShowHideGrids();
        GetPlayerStats();
      }
      //StyleLinks();
    }

    private void ShowHideGrids()
    {
      gvQuarterbacks.Visible = false;
      gvRBWRTE.Visible = false;
      gvDefenses.Visible = false;
      gvKickers.Visible = false;
      
      switch (this.CurrentPositionCode)
      {
        case "QB":
          gvQuarterbacks.Visible = true;
          break;
        case "RB":
          gvRBWRTE.Visible = true;
          break;
        case "WR":
          gvRBWRTE.Visible = true;
          break;
        case "TE":
          gvRBWRTE.Visible = true;
          break;
        case "K":
          gvKickers.Visible = true;
          break;
        case "DF":
          gvDefenses.Visible = true;
          break;
      }
    }

    private void GetPlayerStats()
    {
      BindPlayers();
    }


    private void BindPlayers()
    {
      List<PlayerAndStats> playersWithStats = new List<PlayerAndStats>();
      
      List<Player> gridPlayers = Player.GetPlayers("FOO", ddlSeasons.SelectedValue, ddlTeams.SelectedValue, this.CurrentPositionCode, false);
      foreach (Player currentPlayer in gridPlayers)
      {
        List<SportSeasonPlayerSeasonStat> playerSeasonStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", ddlSeasons.SelectedValue, currentPlayer.PlayerID);
        //List<FootballPlayerSeasonStat> playerSeasonStats = FootballPlayerSeasonStat.GetFootballPlayerSeasonStats(ddlSeasons.SelectedValue, currentPlayer.PlayerID);
        playersWithStats.Add(new PlayerAndStats(currentPlayer.PlayerID, currentPlayer.FullNameLastFirst, currentPlayer.PositionCode, playerSeasonStats));
      }

      switch (this.CurrentPositionCode)
      {
        case "QB":
          gvQuarterbacks.DataSource = playersWithStats;
          gvQuarterbacks.DataBind();
          break;
        case "RB":
          gvRBWRTE.DataSource = playersWithStats;
          gvRBWRTE.DataBind();
          break;
        case "WR":
          gvRBWRTE.DataSource = playersWithStats;
          gvRBWRTE.DataBind();
          break;
        case "TE":
          gvRBWRTE.DataSource = playersWithStats;
          gvRBWRTE.DataBind();
          break;
        case "K":
          gvKickers.DataSource = playersWithStats;
          gvKickers.DataBind();
          break;
        case "DF":
          gvDefenses.DataSource = playersWithStats;
          gvDefenses.DataBind();
          break;
      }
    }

   

    private void LoadSeasonDropdown()
    {
      List<SportSeason> footballSeasons = SportSeason.GetSportStatSeasons("FOO");
      //footballSeasons.Reverse();
      ddlSeasons.DataSource = footballSeasons;
      ddlSeasons.DataBind();

      foreach (SportSeason currentSeason in footballSeasons)
      {
        if (currentSeason.SeasonStarted)
        {
          ddlSeasons.SelectedValue = currentSeason.SeasonCode;
          break;
        }
      }
    }

    private void LoadTeamsDropdown()
    {
      ddlTeams.DataSource = Team.GetTeams("FOO");
      ddlTeams.DataBind();
    }

    private void LoadPositionsRepeater()
    {
      repPositions.DataSource = Position.GetPositions("FOO");
      repPositions.DataBind();
    }

    private class PlayerAndStats
    {
      public PlayerAndStats(int playerID, string name, string positionCode, List<SportSeasonPlayerSeasonStat> seasonStats)
      {
        this.PlayerID = playerID;
        this.Name = name;
        this.SeasonStats = seasonStats;

        LoadCalculatedRankings(positionCode);

      }

      private const string CurrentSeasonCode = "2013";
      public int PlayerID { get; set; }
      public string Name { get; set; }
      public List<SportSeasonPlayerSeasonStat> SeasonStats { get; set; }

      private void LoadCalculatedRankings(string positionCode)
      {
        // Total Fantasy Points Rank
        SportSeasonPlayerSeasonStat totalFantasyPointsRank = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", CurrentSeasonCode, this.PlayerID, "TFPR");
        if (totalFantasyPointsRank != null)
        {
          this.SeasonStats.Add(totalFantasyPointsRank);
        }

        // Fantasy Points Per Game Rank
        SportSeasonPlayerSeasonStat fantasyPointsPerGameRank = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", CurrentSeasonCode, this.PlayerID, "FPGR");
        if (fantasyPointsPerGameRank != null)
        {
          this.SeasonStats.Add(fantasyPointsPerGameRank);
        }

        if( (positionCode=="RB") || (positionCode=="WR") || (positionCode=="TE") )  
        {

          // Total Fantasy Points PPR 
          SportSeasonPlayerSeasonStat totalFantasyPointsPPR = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", CurrentSeasonCode, this.PlayerID, "TFPP");
          if (totalFantasyPointsPPR != null)
          {
            this.SeasonStats.Add(totalFantasyPointsPPR);
          }

          // Total Fantasy Points PPR Rank
          SportSeasonPlayerSeasonStat totalFantasyPointsPPRRank = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", CurrentSeasonCode, this.PlayerID, "TPPR");
          if (totalFantasyPointsPPRRank != null)
          {
            this.SeasonStats.Add(totalFantasyPointsPPRRank);
          }

          // Fantasy Points Per Game PPR
          SportSeasonPlayerSeasonStat fantasyPointsPerGamePPR = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", CurrentSeasonCode, this.PlayerID, "FPGP");
          if (fantasyPointsPerGamePPR != null)
          {
            this.SeasonStats.Add(fantasyPointsPerGamePPR);
          }

          // Fantasy Points Per Game PPR Rank
          SportSeasonPlayerSeasonStat fantasyPointsPerGamePPRRank = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", CurrentSeasonCode, this.PlayerID, "FPPR");
          if (fantasyPointsPerGamePPRRank != null)
          {
            this.SeasonStats.Add(fantasyPointsPerGamePPRRank);
          }
          
        }
      }

      public double GetStat(string statCode)
      {
        foreach (SportSeasonPlayerSeasonStat currentStat in this.SeasonStats)
        {
          if (currentStat.StatCode == statCode)
          {
            return currentStat.StatValue;
          }
        }
        return 0;
      }
    }

    protected void gvQuarterbacks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      // if we're binding to a data row, manipulate the controls within
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        // get references to the appropriate controls
        PlayerAndStats boundPlayer = (PlayerAndStats)e.Row.DataItem;

        Label labGAM = (Label)e.Row.FindControl("labGAM");
        Label labPAYD = (Label)e.Row.FindControl("labPAYD");
        Label labPATD = (Label)e.Row.FindControl("labPATD");
        Label labRUYD = (Label)e.Row.FindControl("labRUYD");
        Label labRUTD = (Label)e.Row.FindControl("labRUTD");
        Label labINT = (Label)e.Row.FindControl("labINT");
        Label labFUM = (Label)e.Row.FindControl("labFUM");
        Label labTFP = (Label)e.Row.FindControl("labTFP");
        Label labTFPR = (Label)e.Row.FindControl("labTFPR");
        Label labFPPG = (Label)e.Row.FindControl("labFPPG");
        Label labFPGR = (Label)e.Row.FindControl("labFPGR");


        ImageButton ibUpdate = (ImageButton)e.Row.FindControl("ibUpdate");
        ImageButton ibCancel = (ImageButton)e.Row.FindControl("ibCancel");
        ImageButton ibEdit = (ImageButton)e.Row.FindControl("ibEdit");


        // if the row which was bound isn't being edited, load it with the relevant data
        if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
        {

          labGAM.Text = boundPlayer.GetStat("GAM").ToString();
          labPAYD.Text = boundPlayer.GetStat("PAYD").ToString();
          labPATD.Text = boundPlayer.GetStat("PATD").ToString();
          labRUYD.Text = boundPlayer.GetStat("RUYD").ToString();
          labRUTD.Text = boundPlayer.GetStat("RUTD").ToString();
          labINT.Text = boundPlayer.GetStat("INT").ToString();
          labFUM.Text = boundPlayer.GetStat("FUM").ToString();
          labTFP.Text = boundPlayer.GetStat("TFP").ToString();
          labTFPR.Text = boundPlayer.GetStat("TFPR").ToString();
          labFPPG.Text = boundPlayer.GetStat("FPPG").ToString();
          labFPGR.Text = boundPlayer.GetStat("FPGR").ToString();

          // hide the buttons associated with adding a phone
          ibUpdate.Visible = false;
          ibCancel.Visible = false;
        }
        // if the row which was bound is being edited, load the appropriate controls for editing
        else if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
          TextBox tbGAM = (TextBox)e.Row.FindControl("tbGAM");
          TextBox tbPAYD = (TextBox)e.Row.FindControl("tbPAYD");
          TextBox tbPATD = (TextBox)e.Row.FindControl("tbPATD");
          TextBox tbRUYD = (TextBox)e.Row.FindControl("tbRUYD");
          TextBox tbRUTD = (TextBox)e.Row.FindControl("tbRUTD");
          TextBox tbINT = (TextBox)e.Row.FindControl("tbINT");
          TextBox tbFUM = (TextBox)e.Row.FindControl("tbFUM");
          TextBox tbTFP = (TextBox)e.Row.FindControl("tbTFP");
          TextBox tbTFPR = (TextBox)e.Row.FindControl("tbTFPR");
          TextBox tbFPPG = (TextBox)e.Row.FindControl("tbFPPG");
          TextBox tbFPGR = (TextBox)e.Row.FindControl("tbFPGR");

          tbGAM.Text = boundPlayer.GetStat("GAM").ToString();
          tbPAYD.Text = boundPlayer.GetStat("PAYD").ToString();
          tbPATD.Text = boundPlayer.GetStat("PATD").ToString();
          tbRUYD.Text = boundPlayer.GetStat("RUYD").ToString();
          tbRUTD.Text = boundPlayer.GetStat("RUTD").ToString();
          tbINT.Text = boundPlayer.GetStat("INT").ToString();
          tbFUM.Text = boundPlayer.GetStat("FUM").ToString();
          tbTFP.Text = boundPlayer.GetStat("TFP").ToString();
          tbTFPR.Text = boundPlayer.GetStat("TFPR").ToString();
          tbFPPG.Text = boundPlayer.GetStat("FPPG").ToString();
          tbFPGR.Text = boundPlayer.GetStat("FPGR").ToString();

          // if we're inserting (as opposed to editing), hide the appropriate buttons and turn on
          // the phone type validator
          if (_activeCommand == "Insert")
          {
            ibUpdate.Visible = false;
            ibEdit.Visible = false;
          }
        }

      }
    
    }


    /// <summary>
    /// After a row is created in the gridview, and if it is in edit mode,
    /// we need to hide the edit and delete buttons (because the update and cancel buttons
    /// will be shown
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPosition_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
      {
        if (!String.IsNullOrEmpty(_activeCommand))
        {
          ImageButton ibEdit = (ImageButton)e.Row.FindControl("ibEdit");
          if (_activeCommand == "Edit")
          {
            ibEdit.Visible = false;
          }
        }
      }
    }



    protected void gvQuarterbacks_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      int playerID = Int32.Parse(gvQuarterbacks.DataKeys[e.RowIndex].Value.ToString());

      TextBox tbGAM = (TextBox)gvQuarterbacks.Rows[e.RowIndex].FindControl("tbGAM");
      TextBox tbPAYD = (TextBox)gvQuarterbacks.Rows[e.RowIndex].FindControl("tbPAYD");
      TextBox tbPATD = (TextBox)gvQuarterbacks.Rows[e.RowIndex].FindControl("tbPATD");
      TextBox tbRUYD = (TextBox)gvQuarterbacks.Rows[e.RowIndex].FindControl("tbRUYD");
      TextBox tbRUTD = (TextBox)gvQuarterbacks.Rows[e.RowIndex].FindControl("tbRUTD");
      TextBox tbINT = (TextBox)gvQuarterbacks.Rows[e.RowIndex].FindControl("tbINT");
      TextBox tbFUM = (TextBox)gvQuarterbacks.Rows[e.RowIndex].FindControl("tbFUM");
      TextBox tbTFP = (TextBox)gvQuarterbacks.Rows[e.RowIndex].FindControl("tbTFP");
      TextBox tbTFPR = (TextBox)gvQuarterbacks.Rows[e.RowIndex].FindControl("tbTFPR");
      TextBox tbFPPG = (TextBox)gvQuarterbacks.Rows[e.RowIndex].FindControl("tbFPPG");
      TextBox tbFPGR = (TextBox)gvQuarterbacks.Rows[e.RowIndex].FindControl("tbFPGR");

      // Games
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "GAM", Double.Parse(tbPAYD.Text));
      // Passing Yards
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "PAYD", Double.Parse(tbPAYD.Text));
      // Passing Touchdowns
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "PATD", Double.Parse(tbPATD.Text));
      // Rushing Yards
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "RUYD", Double.Parse(tbRUYD.Text));
      // Rushing Touchdowns
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "RUTD", Double.Parse(tbRUTD.Text));
      // Interceptions
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "INT", Double.Parse(tbINT.Text));
      // Fumbles
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "FUM", Double.Parse(tbFUM.Text));
      // TFP
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "TFP", Double.Parse(tbTFP.Text));
      // TFP
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "TFPR", Double.Parse(tbTFPR.Text));
      // FPPG
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "FPPG", Double.Parse(tbFPPG.Text));
      // FPGR
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "FPGR", Double.Parse(tbFPGR.Text));


      gvQuarterbacks.EditIndex = -1;
      BindPlayers();
    }


    /// <summary>
    /// When a command is fired from a command row, save the command so we can reference
    /// it directly later
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvQuarterbacks_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      _activeCommand = e.CommandName;
    }

    protected void gvQuarterbacks_RowEditing(object sender, GridViewEditEventArgs e)
    {
      gvQuarterbacks.EditIndex = e.NewEditIndex;
      BindPlayers();
    }

    protected void gvQuarterbacks_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
      gvQuarterbacks.EditIndex = -1;
      BindPlayers();
    }







    protected void gvRBWRTE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      // if we're binding to a data row, manipulate the controls within
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        // get references to the appropriate controls
        PlayerAndStats boundPlayer = (PlayerAndStats)e.Row.DataItem;

        Label labGAM = (Label)e.Row.FindControl("labGAM");
        Label labRUYD = (Label)e.Row.FindControl("labRUYD");
        Label labRUTD = (Label)e.Row.FindControl("labRUTD");
        Label labREYD = (Label)e.Row.FindControl("labREYD");
        Label labRETD = (Label)e.Row.FindControl("labRETD");
        Label labFUM = (Label)e.Row.FindControl("labFUM");
        Label labTFP = (Label)e.Row.FindControl("labTFP");
        Label labTFPR = (Label)e.Row.FindControl("labTFPR");
        Label labFPPG = (Label)e.Row.FindControl("labFPPG");
        Label labFPGR = (Label)e.Row.FindControl("labFPGR");
        Label labTFPP = (Label)e.Row.FindControl("labTFPP");
        Label labTPPR = (Label)e.Row.FindControl("labTPPR");
        Label labFPGP = (Label)e.Row.FindControl("labFPGP");
        Label labFPPR = (Label)e.Row.FindControl("labFPPR");

        ImageButton ibUpdate = (ImageButton)e.Row.FindControl("ibUpdate");
        ImageButton ibCancel = (ImageButton)e.Row.FindControl("ibCancel");
        ImageButton ibEdit = (ImageButton)e.Row.FindControl("ibEdit");

        // if the row which was bound isn't being edited, load it with the relevant data
        if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
        {

          labGAM.Text = boundPlayer.GetStat("GAM").ToString();
          labRUYD.Text = boundPlayer.GetStat("RUYD").ToString();
          labRUTD.Text = boundPlayer.GetStat("RUTD").ToString();
          labREYD.Text = boundPlayer.GetStat("REYD").ToString();
          labRETD.Text = boundPlayer.GetStat("RETD").ToString();
          labFUM.Text = boundPlayer.GetStat("FUM").ToString();
          labTFP.Text = boundPlayer.GetStat("TFP").ToString();
          labTFPR.Text = boundPlayer.GetStat("TFPR").ToString();
          labFPPG.Text = boundPlayer.GetStat("FPPG").ToString();
          labFPGR.Text = boundPlayer.GetStat("FPGR").ToString();
          labTFPP.Text = boundPlayer.GetStat("TFPP").ToString();
          labTPPR.Text = boundPlayer.GetStat("TPPR").ToString();
          labFPGP.Text = boundPlayer.GetStat("FPGP").ToString();
          labFPPR.Text = boundPlayer.GetStat("FPPR").ToString();

          // hide the buttons associated with adding a phone
          ibUpdate.Visible = false;
          ibCancel.Visible = false;
        }
        // if the row which was bound is being edited, load the appropriate controls for editing
        else if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
          TextBox tbGAM = (TextBox)e.Row.FindControl("tbGAM");
          TextBox tbRUYD = (TextBox)e.Row.FindControl("tbRUYD");
          TextBox tbRUTD = (TextBox)e.Row.FindControl("tbRUTD");
          TextBox tbREYD = (TextBox)e.Row.FindControl("tbREYD");
          TextBox tbRETD = (TextBox)e.Row.FindControl("tbRETD");
          TextBox tbFUM = (TextBox)e.Row.FindControl("tbFUM");
          TextBox tbTFP = (TextBox)e.Row.FindControl("tbTFP");
          TextBox tbTFPR = (TextBox)e.Row.FindControl("tbTFPR");
          TextBox tbFPPG = (TextBox)e.Row.FindControl("tbFPPG");
          TextBox tbFPGR = (TextBox)e.Row.FindControl("tbFPGR");
          TextBox tbTFPP = (TextBox)e.Row.FindControl("tbTFPP");
          TextBox tbTPPR = (TextBox)e.Row.FindControl("tbTPPR");
          TextBox tbFPGP = (TextBox)e.Row.FindControl("tbFPGP");
          TextBox tbFPPR = (TextBox)e.Row.FindControl("tbFPPR");

          tbGAM.Text = boundPlayer.GetStat("GAM").ToString();
          tbRUYD.Text = boundPlayer.GetStat("RUYD").ToString();
          tbRUTD.Text = boundPlayer.GetStat("RUTD").ToString();
          tbREYD.Text = boundPlayer.GetStat("REYD").ToString();
          tbRETD.Text = boundPlayer.GetStat("RETD").ToString();
          tbFUM.Text = boundPlayer.GetStat("FUM").ToString();
          tbTFP.Text = boundPlayer.GetStat("TFP").ToString();
          tbTFPR.Text = boundPlayer.GetStat("TFPR").ToString();
          tbFPPG.Text = boundPlayer.GetStat("FPPG").ToString();
          tbFPGR.Text = boundPlayer.GetStat("FPGR").ToString();
          tbTFPP.Text = boundPlayer.GetStat("TFPP").ToString();
          tbTPPR.Text = boundPlayer.GetStat("TPPR").ToString();
          tbFPGP.Text = boundPlayer.GetStat("FPGP").ToString();
          tbFPPR.Text = boundPlayer.GetStat("FPPR").ToString();

          // if we're inserting (as opposed to editing), hide the appropriate buttons and turn on
          // the phone type validator
          if (_activeCommand == "Insert")
          {
            ibUpdate.Visible = false;
            ibEdit.Visible = false;
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
    protected void gvRBWRTE_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      _activeCommand = e.CommandName;
    }

    protected void gvRBWRTE_RowEditing(object sender, GridViewEditEventArgs e)
    {
      gvRBWRTE.EditIndex = e.NewEditIndex;
      BindPlayers();
    }

    protected void gvRBWRTE_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
      gvRBWRTE.EditIndex = -1;
      BindPlayers();
    }

    protected void gvRBWRTE_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      int playerID = Int32.Parse(gvRBWRTE.DataKeys[e.RowIndex].Value.ToString());

      TextBox tbGAM = (TextBox)gvRBWRTE.Rows[e.RowIndex].FindControl("tbGAM");
      TextBox tbRUYD = (TextBox)gvRBWRTE.Rows[e.RowIndex].FindControl("tbRUYD");
      TextBox tbRUTD = (TextBox)gvRBWRTE.Rows[e.RowIndex].FindControl("tbRUTD");
      TextBox tbREYD = (TextBox)gvRBWRTE.Rows[e.RowIndex].FindControl("tbREYD");
      TextBox tbRETD = (TextBox)gvRBWRTE.Rows[e.RowIndex].FindControl("tbRETD");
      TextBox tbFUM = (TextBox)gvRBWRTE.Rows[e.RowIndex].FindControl("tbFUM");
      TextBox tbTFP = (TextBox)gvRBWRTE.Rows[e.RowIndex].FindControl("tbTFP");
      TextBox tbTFPR = (TextBox)gvRBWRTE.Rows[e.RowIndex].FindControl("tbTFPR");
      TextBox tbFPPG = (TextBox)gvRBWRTE.Rows[e.RowIndex].FindControl("tbFPPG");
      TextBox tbFPGR = (TextBox)gvRBWRTE.Rows[e.RowIndex].FindControl("tbFPGR");
      TextBox tbTFPP = (TextBox)gvRBWRTE.Rows[e.RowIndex].FindControl("tbTFPP");
      TextBox tbTPPR = (TextBox)gvRBWRTE.Rows[e.RowIndex].FindControl("tbTPPR");
      TextBox tbFPGP = (TextBox)gvRBWRTE.Rows[e.RowIndex].FindControl("tbFPGP");
      TextBox tbFPPR = (TextBox)gvRBWRTE.Rows[e.RowIndex].FindControl("tbFPPR");

      //List<SportSeasonPlayerSeasonStat> allPlayerStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", ddlSeasons.SelectedValue, playerID);
      //if (allPlayerStats > 0)
      //{

      //}

      // Games
      if (!SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "GAM", Double.Parse(tbGAM.Text)))
      {
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "GAM", Double.Parse(tbGAM.Text));
      }
      // Rushing Yards
      if (!SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "RUYD", Double.Parse(tbRUYD.Text)))
      {
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "RUYD", Double.Parse(tbRUYD.Text));
      }
      // Rushing Touchdowns
      if (!SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "RUTD", Double.Parse(tbRUTD.Text)))
      {
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "RUTD", Double.Parse(tbRUTD.Text));
      }
      // Receiving Yards
      if (!SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "REYD", Double.Parse(tbREYD.Text)))
      {
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "REYD", Double.Parse(tbREYD.Text));
      }
      // Receiving Touchdowns
      if (!SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "RETD", Double.Parse(tbRETD.Text)))
      {
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "RETD", Double.Parse(tbRETD.Text));
      }
      // Fumbles
      if (!SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "FUM", Double.Parse(tbFUM.Text)))
      {
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "FUM", Double.Parse(tbFUM.Text));
      }
      // TFP
      if (!SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "TFP", Double.Parse(tbTFP.Text)))
      {
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "TFP", Double.Parse(tbTFP.Text));
      }
      // TFPR
      if (!SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "TFPR", Double.Parse(tbTFPR.Text)))
      {
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "TFPR", Double.Parse(tbTFPR.Text));
      }
      // FPPG
      if (!SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "FPPG", Double.Parse(tbFPPG.Text)))
      {
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "FPPG", Double.Parse(tbFPPG.Text));
      }
      // FPGR
      if (!SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "FPGR", Double.Parse(tbFPGR.Text)))
      {
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "FPGR", Double.Parse(tbFPGR.Text));
      }
      // TFPP
      if (!SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "TFPP", Double.Parse(tbTFPP.Text)))
      {
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "TFPP", Double.Parse(tbTFPP.Text));
      }
      // TPPR
      if (!SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "TPPR", Double.Parse(tbTPPR.Text)))
      {
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "TPPR", Double.Parse(tbTPPR.Text));
      }
      // FPGP
      if (!SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "FPGP", Double.Parse(tbFPGP.Text)))
      {
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "FPGP", Double.Parse(tbFPGP.Text));
      }
      // FPPR
      if (!SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "FPPR", Double.Parse(tbFPPR.Text)))
      {
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "FPPR", Double.Parse(tbFPPR.Text));
      }


      gvRBWRTE.EditIndex = -1;
      BindPlayers();
    }


    protected void gvKickers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      // if we're binding to a data row, manipulate the controls within
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        // get references to the appropriate controls
        PlayerAndStats boundPlayer = (PlayerAndStats)e.Row.DataItem;

        Label labGAM = (Label)e.Row.FindControl("labGAM");
        Label labMAFG = (Label)e.Row.FindControl("labMAFG");
        Label labMIFG = (Label)e.Row.FindControl("labMIFG");
        Label labMAXP = (Label)e.Row.FindControl("labMAXP");
        Label labMIXP = (Label)e.Row.FindControl("labMIXP");
        Label labTFP = (Label)e.Row.FindControl("labTFP");
        Label labTFPR = (Label)e.Row.FindControl("labTFPR");
        Label labFPPG = (Label)e.Row.FindControl("labFPPG");
        Label labFPGR = (Label)e.Row.FindControl("labFPGR");

        ImageButton ibUpdate = (ImageButton)e.Row.FindControl("ibUpdate");
        ImageButton ibCancel = (ImageButton)e.Row.FindControl("ibCancel");
        ImageButton ibEdit = (ImageButton)e.Row.FindControl("ibEdit");


        // if the row which was bound isn't being edited, load it with the relevant data
        if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
        {

          labGAM.Text = boundPlayer.GetStat("GAM").ToString();
          labMAFG.Text = boundPlayer.GetStat("MAFG").ToString();
          labMIFG.Text = boundPlayer.GetStat("MIFG").ToString();
          labMAXP.Text = boundPlayer.GetStat("MAXP").ToString();
          labMIXP.Text = boundPlayer.GetStat("MIXP").ToString();
          labTFP.Text = boundPlayer.GetStat("TFP").ToString();
          labTFPR.Text = boundPlayer.GetStat("TFPR").ToString();
          labFPPG.Text = boundPlayer.GetStat("FPPG").ToString();
          labFPGR.Text = boundPlayer.GetStat("FPGR").ToString();

          // hide the buttons associated with adding a phone
          ibUpdate.Visible = false;
          ibCancel.Visible = false;
        }
        // if the row which was bound is being edited, load the appropriate controls for editing
        else if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
          TextBox tbGAM = (TextBox)e.Row.FindControl("tbGAM");
          TextBox tbMAFG = (TextBox)e.Row.FindControl("tbMAFG");
          TextBox tbMIFG = (TextBox)e.Row.FindControl("tbMIFG");
          TextBox tbMAXP = (TextBox)e.Row.FindControl("tbMAXP");
          TextBox tbMIXP = (TextBox)e.Row.FindControl("tbMIXP");
          TextBox tbTFP = (TextBox)e.Row.FindControl("tbTFP");
          TextBox tbTFPR = (TextBox)e.Row.FindControl("tbTFPR");
          TextBox tbFPPG = (TextBox)e.Row.FindControl("tbFPPG");
          TextBox tbFPGR = (TextBox)e.Row.FindControl("tbFPGR");

          tbGAM.Text = boundPlayer.GetStat("GAM").ToString();
          tbMAFG.Text = boundPlayer.GetStat("MAFG").ToString();
          tbMIFG.Text = boundPlayer.GetStat("MIFG").ToString();
          tbMAXP.Text = boundPlayer.GetStat("MAXP").ToString();
          tbMIXP.Text = boundPlayer.GetStat("MIXP").ToString();
          tbTFP.Text = boundPlayer.GetStat("TFP").ToString();
          tbTFPR.Text = boundPlayer.GetStat("TFPR").ToString();
          tbFPPG.Text = boundPlayer.GetStat("FPPG").ToString();
          tbFPGR.Text = boundPlayer.GetStat("FPGR").ToString();

          // if we're inserting (as opposed to editing), hide the appropriate buttons and turn on
          // the phone type validator
          if (_activeCommand == "Insert")
          {
            ibUpdate.Visible = false;
            ibEdit.Visible = false;
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
    protected void gvKickers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      _activeCommand = e.CommandName;
    }

    protected void gvKickers_RowEditing(object sender, GridViewEditEventArgs e)
    {
      gvKickers.EditIndex = e.NewEditIndex;
      BindPlayers();
    }

    protected void gvKickers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
      gvKickers.EditIndex = -1;
      BindPlayers();
    }


    protected void gvKickers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      int playerID = Int32.Parse(gvKickers.DataKeys[e.RowIndex].Value.ToString());

      TextBox tbGAM = (TextBox)gvKickers.Rows[e.RowIndex].FindControl("tbGAM");
      TextBox tbMAFG = (TextBox)gvKickers.Rows[e.RowIndex].FindControl("tbMAFG");
      TextBox tbMIFG = (TextBox)gvKickers.Rows[e.RowIndex].FindControl("tbMIFG");
      TextBox tbMAXP = (TextBox)gvKickers.Rows[e.RowIndex].FindControl("tbMAXP");
      TextBox tbMIXP = (TextBox)gvKickers.Rows[e.RowIndex].FindControl("tbMIXP");
      TextBox tbTFP = (TextBox)gvKickers.Rows[e.RowIndex].FindControl("tbTFP");
      TextBox tbTFPR = (TextBox)gvKickers.Rows[e.RowIndex].FindControl("tbTFPR");
      TextBox tbFPPG = (TextBox)gvKickers.Rows[e.RowIndex].FindControl("tbFPPG");
      TextBox tbFPGR = (TextBox)gvKickers.Rows[e.RowIndex].FindControl("tbFPGR");

      // Games
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "GAM", Double.Parse(tbGAM.Text));
      // Made Field Goals
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "MAFG", Double.Parse(tbMAFG.Text));
      // Missed Field Goals
      bool test = SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "MIFG", Double.Parse(tbMIFG.Text));
      // Made Extra Points
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "MAXP", Double.Parse(tbMAXP.Text));
      // Missed Extra Points
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "MIXP", Double.Parse(tbMIXP.Text));
      // TFP
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "TFP", Double.Parse(tbTFP.Text));
      // TFPR
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "TFPR", Double.Parse(tbTFPR.Text));
      // FPPG
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "FPPG", Double.Parse(tbFPPG.Text));
      // FPGR
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "FPGR", Double.Parse(tbFPGR.Text));

      gvKickers.EditIndex = -1;
      BindPlayers();
    }




    protected void gvDefenses_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      // if we're binding to a data row, manipulate the controls within
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        // get references to the appropriate controls
        PlayerAndStats boundPlayer = (PlayerAndStats)e.Row.DataItem;

        Label labGAM = (Label)e.Row.FindControl("labGAM");

        Label labFREC = (Label)e.Row.FindControl("labFREC");
        Label labINT = (Label)e.Row.FindControl("labINT");
        Label labSACK = (Label)e.Row.FindControl("labSACK");
        Label labDTD = (Label)e.Row.FindControl("labDTD");
        Label labPA = (Label)e.Row.FindControl("labPA");
        Label labTFP = (Label)e.Row.FindControl("labTFP");
        Label labTFPR = (Label)e.Row.FindControl("labTFPR");
        Label labFPPG = (Label)e.Row.FindControl("labFPPG");
        Label labFPGR = (Label)e.Row.FindControl("labFPGR");


        ImageButton ibUpdate = (ImageButton)e.Row.FindControl("ibUpdate");
        ImageButton ibCancel = (ImageButton)e.Row.FindControl("ibCancel");
        ImageButton ibEdit = (ImageButton)e.Row.FindControl("ibEdit");


        // if the row which was bound isn't being edited, load it with the relevant data
        if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
        {

          labGAM.Text = boundPlayer.GetStat("GAM").ToString();
          labFREC.Text = boundPlayer.GetStat("FREC").ToString();
          labINT.Text = boundPlayer.GetStat("INT").ToString();
          labSACK.Text = boundPlayer.GetStat("SACK").ToString();
          labDTD.Text = boundPlayer.GetStat("DTD").ToString();
          labPA.Text = boundPlayer.GetStat("PA").ToString();
          labTFP.Text = boundPlayer.GetStat("TFP").ToString();
          labTFPR.Text = boundPlayer.GetStat("TFPR").ToString();
          labFPPG.Text = boundPlayer.GetStat("FPPG").ToString();
          labFPGR.Text = boundPlayer.GetStat("FPGR").ToString();

          // hide the buttons associated with adding a phone
          ibUpdate.Visible = false;
          ibCancel.Visible = false;
        }
        // if the row which was bound is being edited, load the appropriate controls for editing
        else if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
          TextBox tbGAM = (TextBox)e.Row.FindControl("tbGAM");
          TextBox tbFREC = (TextBox)e.Row.FindControl("tbFREC");
          TextBox tbINT = (TextBox)e.Row.FindControl("tbINT");
          TextBox tbSACK = (TextBox)e.Row.FindControl("tbSACK");
          TextBox tbDTD = (TextBox)e.Row.FindControl("tbDTD");
          TextBox tbPA = (TextBox)e.Row.FindControl("tbPA");
          TextBox tbTFP = (TextBox)e.Row.FindControl("tbTFP");
          TextBox tbTFPR = (TextBox)e.Row.FindControl("tbTFPR");
          TextBox tbFPPG = (TextBox)e.Row.FindControl("tbFPPG");
          TextBox tbFPGR = (TextBox)e.Row.FindControl("tbFPGR");

          tbGAM.Text = boundPlayer.GetStat("GAM").ToString();
          tbFREC.Text = boundPlayer.GetStat("FREC").ToString();
          tbINT.Text = boundPlayer.GetStat("INT").ToString();
          tbSACK.Text = boundPlayer.GetStat("SACK").ToString();
          tbDTD.Text = boundPlayer.GetStat("DTD").ToString();
          tbPA.Text = boundPlayer.GetStat("PA").ToString();
          tbTFP.Text = boundPlayer.GetStat("TFP").ToString();
          tbTFPR.Text = boundPlayer.GetStat("TFPR").ToString();
          tbFPPG.Text = boundPlayer.GetStat("FPPG").ToString();
          tbFPGR.Text = boundPlayer.GetStat("FPGR").ToString();

          // if we're inserting (as opposed to editing), hide the appropriate buttons and turn on
          // the phone type validator
          if (_activeCommand == "Insert")
          {
            ibUpdate.Visible = false;
            ibEdit.Visible = false;
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
    protected void gvDefenses_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      _activeCommand = e.CommandName;
    }

    protected void gvDefenses_RowEditing(object sender, GridViewEditEventArgs e)
    {
      gvDefenses.EditIndex = e.NewEditIndex;
      BindPlayers();
    }

    protected void gvDefenses_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
      gvDefenses.EditIndex = -1;
      BindPlayers();
    }


    protected void gvDefenses_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      int playerID = Int32.Parse(gvDefenses.DataKeys[e.RowIndex].Value.ToString());

      TextBox tbGAM = (TextBox)gvDefenses.Rows[e.RowIndex].FindControl("tbGAM");
      TextBox tbFREC = (TextBox)gvDefenses.Rows[e.RowIndex].FindControl("tbFREC");
      TextBox tbINT = (TextBox)gvDefenses.Rows[e.RowIndex].FindControl("tbINT");
      TextBox tbSACK = (TextBox)gvDefenses.Rows[e.RowIndex].FindControl("tbSACK");
      TextBox tbDTD = (TextBox)gvDefenses.Rows[e.RowIndex].FindControl("tbDTD");
      TextBox tbPA = (TextBox)gvDefenses.Rows[e.RowIndex].FindControl("tbPA");
      TextBox tbTFP = (TextBox)gvDefenses.Rows[e.RowIndex].FindControl("tbTFP");
      TextBox tbTFPR = (TextBox)gvDefenses.Rows[e.RowIndex].FindControl("tbTFR");
      TextBox tbFPPG = (TextBox)gvDefenses.Rows[e.RowIndex].FindControl("tbFPPG");
      TextBox tbFPGR = (TextBox)gvDefenses.Rows[e.RowIndex].FindControl("tbFPGR");

      // Games
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "GAM", Double.Parse(tbGAM.Text));
      // Fumble Recoveries
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "FREC", Double.Parse(tbFREC.Text));
      // Interceptions
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "INT", Double.Parse(tbINT.Text));
      // Sack
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "SACK", Double.Parse(tbSACK.Text));
      // Defensive Touchdowns
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "DTD", Double.Parse(tbDTD.Text));
      // Points Allowed
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "PA", Double.Parse(tbPA.Text));
      // TFP
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "TFP", Double.Parse(tbTFP.Text));
      // TFPR
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "TFPR", Double.Parse(tbTFPR.Text));
      // FPPG
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "FPPG", Double.Parse(tbFPPG.Text));
      // FPGR
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("FOO", ddlSeasons.SelectedValue, playerID, "FPGR", Double.Parse(tbFPGR.Text));

      gvDefenses.EditIndex = -1;
      BindPlayers();
    }





    protected void ddlSeasons_SelectedIndexChanged(object sender, EventArgs e)
    {
      ShowHideGrids();
      BindPlayers();
    }

    protected void ddlTeams_SelectedIndexChanged(object sender, EventArgs e)
    {
      ShowHideGrids();
      BindPlayers();
    }

    protected void ddlPositions_SelectedIndexChanged(object sender, EventArgs e)
    {
      ShowHideGrids();
      BindPlayers();
    }


    protected void repPositions_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        Position boundPosition = (Position)e.Item.DataItem;

        LinkButton lbPosition = (LinkButton)e.Item.FindControl("lbPosition");

        lbPosition.Text = boundPosition.Name;
        lbPosition.CommandName = "PositionCode";
        lbPosition.CommandArgument = boundPosition.PositionCode;

        if (boundPosition.PositionCode == CurrentPositionCode)
        {
          lbPosition.CssClass = "active";
        }
      }
    }

    protected void repPositions_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
      this.CurrentPositionCode = e.CommandArgument.ToString();
      LoadPositionsRepeater();
      ShowHideGrids();
      BindPlayers();

    }
}
}