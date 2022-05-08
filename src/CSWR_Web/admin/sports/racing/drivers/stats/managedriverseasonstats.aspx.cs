using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.Admin.Sheets
{
  public partial class ManageDriverSeasonStats : BasePage
  {


    private List<PlayerAndStats> _driversWithStats = new List<PlayerAndStats>();
    public List<PlayerAndStats> DriversWithStats
    {
      get
      {
        return _driversWithStats;
      }
      set
      {
        _driversWithStats = value;
      }
    }


    /// <summary>
    /// A private variable indicating which command is currently active
    /// </summary>
    private string _activeCommand = String.Empty;

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
        BindSports();
        BindDrivers();
      }
    }

    private void BindSports()
    {
      ddlSeasons.DataSource = Season.GetSeasons();
      ddlSeasons.DataBind();
    }

    private void BindDrivers()
    {
      List<PlayerAndStats> playersWithStats = new List<PlayerAndStats>();

      List<Player> gridPlayers = Player.GetPlayersBySportSeasonPositionCodes("RAC", ddlSeasons.SelectedValue, "DR", false, true);
      foreach (Player currentPlayer in gridPlayers)
      {
        List<SportSeasonPlayerSeasonStat> playerSeasonStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("RAC", ddlSeasons.SelectedValue, currentPlayer.PlayerID);
        if (playerSeasonStats.Count > 0)
        {
          playersWithStats.Add(new PlayerAndStats(currentPlayer.PlayerID, currentPlayer.FullNameLastFirst, playerSeasonStats));
        }
      }
      DriversWithStats.Clear();
      PlayerAndStats.SortByStats(ref playersWithStats);
      DriversWithStats = playersWithStats;

      if ((_activeCommand == "Insert") && (!((BasePage)this.Page).IsRefresh))
      {
        playersWithStats.Insert(0, new PlayerAndStats(0, String.Empty, null));
        //playersWithStats.Add(new PlayerAndStats(0, String.Empty, null));
      }


      gvDrivers.DataSource = playersWithStats;
      gvDrivers.DataBind();
    }


    /// <summary>
    /// We need to manipulate element in each row after data is bound to it
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDrivers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      // if we're binding to a data row, manipulate the controls within
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        // get references to the appropriate controls
        PlayerAndStats boundPlayerAndStats = (PlayerAndStats)e.Row.DataItem;

        ImageButton ibDelete = (ImageButton)e.Row.FindControl("ibDelete");
        ImageButton ibUpdate = (ImageButton)e.Row.FindControl("ibUpdate");
        ImageButton ibCancel = (ImageButton)e.Row.FindControl("ibCancel");
        ImageButton ibAdd = (ImageButton)e.Row.FindControl("ibAdd");
        ImageButton ibEdit = (ImageButton)e.Row.FindControl("ibEdit");

        // if the row which was bound isn't being edited, load it with the relevant data
        if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
        {
          // get references to the appropriate controls
          Label labName = (Label)e.Row.FindControl("labName");
          Label labRank = (Label)e.Row.FindControl("labRank");
          Label labPoints = (Label)e.Row.FindControl("labPoints");
          Label labBehind = (Label)e.Row.FindControl("labBehind");
          Label labStarts = (Label)e.Row.FindControl("labStarts");
          Label labPoles = (Label)e.Row.FindControl("labPoles");
          Label labWins = (Label)e.Row.FindControl("labWins");
          Label labWinnings = (Label)e.Row.FindControl("labWinnings");
          Label labAverageFinishPosition = (Label)e.Row.FindControl("labAverageFinishPosition");
          Label labTop5Finishes = (Label)e.Row.FindControl("labTop5Finishes");
          Label labTop10Finishes = (Label)e.Row.FindControl("labTop10Finishes");
          Label labADP = (Label)e.Row.FindControl("labADP");

          // hide the buttons associated with adding a drivers
          ibUpdate.Visible = false;
          ibCancel.Visible = false;
          ibAdd.Visible = false;

          // load the labels
          if (boundPlayerAndStats.PlayerID != 0)
          {
            labName.Text = boundPlayerAndStats.Name.ToString();
            labRank.Text = boundPlayerAndStats.GetStat("RANK").ToString();
            labPoints.Text = boundPlayerAndStats.GetStat("PNTS").ToString();
            labBehind.Text = boundPlayerAndStats.GetStat("BHND").ToString();
            labStarts.Text = boundPlayerAndStats.GetStat("STRT").ToString();
            labPoles.Text = boundPlayerAndStats.GetStat("POLE").ToString();
            labWins.Text = boundPlayerAndStats.GetStat("WINS").ToString();
            labWinnings.Text = boundPlayerAndStats.GetStat("WNGS").ToString();
            labAverageFinishPosition.Text = boundPlayerAndStats.GetStat("AFP").ToString();
            labTop5Finishes.Text = boundPlayerAndStats.GetStat("TP5").ToString();
            labTop10Finishes.Text = boundPlayerAndStats.GetStat("TP10").ToString();
            labADP.Text = boundPlayerAndStats.GetStat("ADP").ToString();
          }

        }
        // if the row which was bound is being edited, load the appropriate controls for editing
        else if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
          // get references to the appropriate controls
          DropDownList ddlDriverName = (DropDownList)e.Row.FindControl("ddlDriverName");
          TextBox tbRank = (TextBox)e.Row.FindControl("tbRank");
          TextBox tbPoints = (TextBox)e.Row.FindControl("tbPoints");
          TextBox tbBehind = (TextBox)e.Row.FindControl("tbBehind");
          TextBox tbStarts = (TextBox)e.Row.FindControl("tbStarts");
          TextBox tbPoles = (TextBox)e.Row.FindControl("tbPoles");
          TextBox tbWins = (TextBox)e.Row.FindControl("tbWins");
          TextBox tbWinnings = (TextBox)e.Row.FindControl("tbWinnings");
          TextBox tbAverageFinishPosition = (TextBox)e.Row.FindControl("tbAverageFinishPosition");
          TextBox tbTop5Finishes = (TextBox)e.Row.FindControl("tbTop5Finishes");
          TextBox tbTop10Finishes = (TextBox)e.Row.FindControl("tbTop10Finishes");
          TextBox tbADP = (TextBox)e.Row.FindControl("tbADP");

          List<Player> relevantDrivers = Player.GetPlayersBySportSeasonPositionCodes("RAC", ddlSeasons.SelectedValue, "DR", true, true);

          // if we're inserting (as opposed to editing), hide the appropriate buttons and turn on
          // the phone type validator
          if (_activeCommand == "Insert")
          {
            ibDelete.Visible = false;
            ibUpdate.Visible = false;
            ibEdit.Visible = false;
            ibAdd.Visible = true;

            // if we're inserting a driver, remove the drivers who already have stats from the list so that you can't select them twice
            ddlDriverName.Items.Insert(0, new ListItem("Select Driver", "0"));
            foreach (PlayerAndStats currentDriver in DriversWithStats)
            {
              Player foundPlayer = relevantDrivers.Find((delegate(Player targetPlayer) { return (targetPlayer.PlayerID == currentDriver.PlayerID); }));
              if (foundPlayer != null)
              {
                relevantDrivers.Remove(foundPlayer);
              }
            }
            ddlDriverName.DataSource = relevantDrivers;
            ddlDriverName.DataBind();
          }
          else
          {
            ddlDriverName.SelectedValue = boundPlayerAndStats.PlayerID.ToString();
            ddlDriverName.Enabled = false;
            tbRank.Text = boundPlayerAndStats.GetStat("RANK").ToString();
            tbPoints.Text = boundPlayerAndStats.GetStat("PNTS").ToString();
            tbBehind.Text = boundPlayerAndStats.GetStat("BHND").ToString();
            tbStarts.Text = boundPlayerAndStats.GetStat("STRT").ToString();
            tbPoles.Text = boundPlayerAndStats.GetStat("POLE").ToString();
            tbWins.Text = boundPlayerAndStats.GetStat("WINS").ToString();
            tbWinnings.Text = boundPlayerAndStats.GetStat("WNGS").ToString();
            tbAverageFinishPosition.Text = boundPlayerAndStats.GetStat("AFP").ToString();
            tbTop5Finishes.Text = boundPlayerAndStats.GetStat("TP5").ToString();
            tbTop10Finishes.Text = boundPlayerAndStats.GetStat("TP10").ToString();
            tbADP.Text = boundPlayerAndStats.GetStat("ADP").ToString();

            ibAdd.Visible = false;
            ddlDriverName.DataSource = relevantDrivers;
            ddlDriverName.DataBind();
          }
        }

        // if we're binding to an 'Add' row, put the gridview in Edit mode for that row.
        if ((e.Row.DataItem != null) && (!((BasePage)this.Page).IsRefresh))
        {
          if (((PlayerAndStats)e.Row.DataItem).PlayerID == 0)
          {
            gvDrivers.EditIndex = e.Row.RowIndex;
          }
        }
      }
    }

    /// <summary>
    /// Before we update an email, load important values that aren't set through the gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDrivers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      int index = gvDrivers.EditIndex;
      GridViewRow editRow = gvDrivers.Rows[index];

      DropDownList ddlDriverName = (DropDownList)editRow.FindControl("ddlDriverName");

      TextBox tbRank = (TextBox)editRow.FindControl("tbRank");
      TextBox tbPoints = (TextBox)editRow.FindControl("tbPoints");
      TextBox tbBehind = (TextBox)editRow.FindControl("tbBehind");
      TextBox tbStarts = (TextBox)editRow.FindControl("tbStarts");
      TextBox tbPoles = (TextBox)editRow.FindControl("tbPoles");
      TextBox tbWins = (TextBox)editRow.FindControl("tbWins");
      TextBox tbWinnings = (TextBox)editRow.FindControl("tbWinnings");
      TextBox tbAverageFinishPosition = (TextBox)editRow.FindControl("tbAverageFinishPosition");
      TextBox tbTop5Finishes = (TextBox)editRow.FindControl("tbTop5Finishes");
      TextBox tbTop10Finishes = (TextBox)editRow.FindControl("tbTop10Finishes");
      TextBox tbADP = (TextBox)editRow.FindControl("tbADP");

      // update all stats
      int driverID = int.Parse(ddlDriverName.SelectedValue);

      // Rank
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "RANK", double.Parse(tbRank.Text));
      // Points
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "PNTS", double.Parse(tbPoints.Text));
      // Behind
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "BHND", double.Parse(tbBehind.Text));
      // Starts
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "STRT", double.Parse(tbStarts.Text));
      // Poles
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "POLE", double.Parse(tbPoles.Text));
      // Wins
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "WINS", double.Parse(tbWins.Text));
      // Winnings
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "WNGS", double.Parse(tbWinnings.Text));
      // AFP
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "AFP", double.Parse(tbAverageFinishPosition.Text));
      // TP5
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "TP5", double.Parse(tbTop5Finishes.Text));
      // TP10
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "TP10", double.Parse(tbTop10Finishes.Text));
      // ADP
      SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "ADP", double.Parse(tbADP.Text));

      _activeCommand = String.Empty;
      gvDrivers.EditIndex = -1;
      BindDrivers();
    }

    /// <summary>
    /// After the gridview is bound, we need to hide those 'Arrow' controls that aren't relevant
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDrivers_DataBound(object sender, EventArgs e)
    {
      if (gvDrivers.Rows.Count == 0)
      {
        labNoStats.Visible = true;
      }
      else
      {
        labNoStats.Visible = false;
      }

      if ((_activeCommand == "Insert") && (!((BasePage)this.Page).IsRefresh) && (!Inserted))
      {
        Inserted = true;
        BindDrivers();
      }

    }


    /// <summary>
    /// After a row is created in the gridview, and if it is in edit mode,
    /// we need to hide the edit and delete buttons (because the update and cancel buttons
    /// will be shown
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDrivers_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void gvDrivers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      _activeCommand = e.CommandName;
    }

    /// <summary>
    /// Since the gridview doesn't natively support the insert command, we need to launch it directly
    /// through the event associated with the add button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibAdd_Command(object sender, EventArgs e)
    {
      DropDownList ddlDriverName = (DropDownList)gvDrivers.Rows[gvDrivers.EditIndex].FindControl("ddlDriverName");
      TextBox tbRank = (TextBox)gvDrivers.Rows[gvDrivers.EditIndex].FindControl("tbRank");
      TextBox tbPoints = (TextBox)gvDrivers.Rows[gvDrivers.EditIndex].FindControl("tbPoints");
      TextBox tbBehind = (TextBox)gvDrivers.Rows[gvDrivers.EditIndex].FindControl("tbBehind");
      TextBox tbStarts = (TextBox)gvDrivers.Rows[gvDrivers.EditIndex].FindControl("tbStarts");
      TextBox tbPoles = (TextBox)gvDrivers.Rows[gvDrivers.EditIndex].FindControl("tbPoles");
      TextBox tbWins = (TextBox)gvDrivers.Rows[gvDrivers.EditIndex].FindControl("tbWins");
      TextBox tbWinnings = (TextBox)gvDrivers.Rows[gvDrivers.EditIndex].FindControl("tbWinnings");
      TextBox tbAverageFinishPosition = (TextBox)gvDrivers.Rows[gvDrivers.EditIndex].FindControl("tbAverageFinishPosition");
      TextBox tbTop5Finishes = (TextBox)gvDrivers.Rows[gvDrivers.EditIndex].FindControl("tbTop5Finishes");
      TextBox tbTop10Finishes = (TextBox)gvDrivers.Rows[gvDrivers.EditIndex].FindControl("tbTop10Finishes");
      TextBox tbADP = (TextBox)gvDrivers.Rows[gvDrivers.EditIndex].FindControl("tbADP");

      int driverID = int.Parse(ddlDriverName.SelectedValue);

      // Rank
      SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "RANK", double.Parse(tbRank.Text));
      // Points
      SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "PNTS", double.Parse(tbPoints.Text));
      // Behind
      SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "BHND", double.Parse(tbBehind.Text));
      // Starts
      SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "STRT", double.Parse(tbStarts.Text));
      // Poles
      SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "POLE", double.Parse(tbPoles.Text));
      // Wins
      SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "WINS", double.Parse(tbWins.Text));
      // Winnings
      SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "WNGS", double.Parse(tbWinnings.Text));
      // Average Finish Position
      SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "AFP", double.Parse(tbAverageFinishPosition.Text));
      // Top 5 Finishes
      SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "TP5", double.Parse(tbTop5Finishes.Text));
      // Top 10 Finishes
      SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "TP10", double.Parse(tbTop10Finishes.Text));
      // ADP
      if (tbADP.Text != String.Empty)
      {
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "ADP", double.Parse(tbADP.Text));
      }
      else
      {
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("RAC", ddlSeasons.SelectedValue, driverID, "ADP", 0);
      }

      _activeCommand = String.Empty;
      gvDrivers.EditIndex = -1;
      BindDrivers();
    }

    /// <summary>
    /// Since there is no 'CommandName' associated with the add button, we need to load
    /// the _activeCommand variable directly if it is clicked, then rebind the gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbAddStat_Click(object sender, EventArgs e)
    {
      _activeCommand = "Insert";
      BindDrivers();
    }



    protected void ddlSeasons_DataBound(object sender, EventArgs e)
    {
      ddlSeasons.SelectedValue = SportSeason.GetCurrentSportSeason("RAC").SeasonCode;
    }


    public class PlayerAndStats
    {
      public PlayerAndStats(int playerID, string name, List<SportSeasonPlayerSeasonStat> seasonStats)
      {
        this.PlayerID = playerID;
        this.Name = name;
        this.SeasonStats = seasonStats;
      }

      public PlayerAndStats() { }

      public int PlayerID { get; set; }
      public string Name { get; set; }
      public List<SportSeasonPlayerSeasonStat> SeasonStats { get; set; }

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

      public static void SortByStats(ref List<PlayerAndStats> playersToSort)
      {

        //List<PlayerAndStats> 
        for (int i = 0; i < playersToSort.Count; i++)
        {
          for (int j = 0; j < playersToSort.Count; j++)
          {
            if (playersToSort[i].GetStat("RANK") < playersToSort[j].GetStat("RANK"))
            {
              PlayerAndStats tempPlayerAndStats = new PlayerAndStats();
              tempPlayerAndStats = playersToSort[i];
              playersToSort[i] = playersToSort[j];
              playersToSort[j] = tempPlayerAndStats;
            }
          }
        }
      }
    }



    protected void gvDrivers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
      _activeCommand = String.Empty;
      gvDrivers.EditIndex = -1;
      BindDrivers();
    }


    protected void gvDrivers_RowEditing(object sender, GridViewEditEventArgs e)
    {
      gvDrivers.EditIndex = e.NewEditIndex;
      BindDrivers();
    }

    protected void gvDrivers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      SportSeasonPlayerSeasonStat.DeleteSportSeasonPlayerSeasonStats("RAC", ddlSeasons.SelectedValue, int.Parse(gvDrivers.DataKeys[e.RowIndex].Value.ToString()));
      gvDrivers.EditIndex = -1;
      BindDrivers();
    }


    protected void ddlSeasons_SelectedIndexChanged(object sender, EventArgs e)
    {
      BindDrivers();
    }
}
}