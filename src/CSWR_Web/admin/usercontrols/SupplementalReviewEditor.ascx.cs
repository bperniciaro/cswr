using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class SupplementalReviewEditor : System.Web.UI.UserControl
  {

    public string SportCode { get; set; }

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
        InitializeDropdowns();
        BindReviews();
      }
    }

    private void InitializeDropdowns()
    {
      // Bind sport seasons
      List<SportSeason> sportSeasons = SportSeason.GetSportSeasons(this.SportCode);
      sportSeasons.Reverse();
      ddlSeason.DataSource = sportSeasons;
      ddlSeason.DataBind();
      // Bind supplemental sources
      ddlSupplementalSource.DataSource = SupplementalSource.GetSupplementalSources();
      ddlSupplementalSource.DataBind();
    }

    private void BindReviews()
    {

      //List<PlayerAndStats> playersWithStats = new List<PlayerAndStats>();

      //List<Player> gridPlayers = Player.GetPlayersBySportSeasonPositionCodes("RAC", ddlSeasons.SelectedValue, "DR", false);
      //foreach (Player currentPlayer in gridPlayers)
      //{
      //  List<SportSeasonPlayerSeasonStat> playerSeasonStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("RAC", ddlSeasons.SelectedValue, currentPlayer.PlayerID);
      //  if (playerSeasonStats.Count > 0)
      //  {
      //    playersWithStats.Add(new PlayerAndStats(currentPlayer.PlayerID, currentPlayer.FullNameLastFirst, playerSeasonStats));
      //  }
      //}
      //DriversWithStats.Clear();
      //PlayerAndStats.SortByStats(ref playersWithStats);
      //DriversWithStats = playersWithStats;

      List<SportSeasonSuppPlayerReview> bindDrivers = SportSeasonSuppPlayerReview.GetSportSeasonSuppPlayerReviews(this.SportCode, ddlSeason.SelectedValue, int.Parse(ddlSupplementalSource.SelectedValue));
      
      if ((_activeCommand == "Insert") && (!((BasePage)this.Page).IsRefresh))
      {
        bindDrivers.Insert(0, new SportSeasonSuppPlayerReview(this.SportCode, ddlSeason.SelectedValue, int.Parse(ddlSupplementalSource.SelectedValue),
                                                                  0, String.Empty));
        //playersWithStats.Add(new PlayerAndStats(0, String.Empty, null));
      }

      gvSupplementalReviews.DataSource = bindDrivers;
      gvSupplementalReviews.DataBind();
    }

    protected void ddlSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
      BindReviews();
    }


    protected void ddlSupplementalSource_SelectedIndexChanged(object sender, EventArgs e)
    {
      BindReviews();
    }

    /// <summary>
    /// Since there is no 'CommandName' associated with the add button, we need to load
    /// the _activeCommand variable directly if it is clicked, then rebind the gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbAddReview_Click(object sender, EventArgs e)
    {
      _activeCommand = "Insert";
      BindReviews();
    }

    protected void gvSupplementalReviews_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      // if we're binding to a data row, manipulate the controls within
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        // get references to the appropriate controls
        SportSeasonSuppPlayerReview boundReview = (SportSeasonSuppPlayerReview)e.Row.DataItem;

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
          Label labURL = (Label)e.Row.FindControl("labURL");

          // hide the buttons associated with adding a drivers
          ibUpdate.Visible = false;
          ibCancel.Visible = false;
          ibAdd.Visible = false;

          // load the labels
          if (boundReview.PlayerID != 0)
          {
            labName.Text = Player.GetPlayer(boundReview.PlayerID).FullNameLastFirst;
            labURL.Text = boundReview.ReviewURL;
          }

        }
        // if the row which was bound is being edited, load the appropriate controls for editing
        else if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
          // get references to the appropriate controls
          DropDownList ddlDrivers = (DropDownList)e.Row.FindControl("ddlDrivers");
          TextBox tbReviewURL = (TextBox)e.Row.FindControl("tbReviewURL");

          List<Player> relevantDrivers = Player.GetPlayersBySportSeasonPositionCodes("RAC", ddlSeason.SelectedValue, "DR", true, false);

          // if we're inserting (as opposed to editing), hide the appropriate buttons and turn on
          // the phone type validator
          if (_activeCommand == "Insert")
          {
            ibDelete.Visible = false;
            ibUpdate.Visible = false;
            ibEdit.Visible = false;
            ibAdd.Visible = true;

            // if we're inserting a driver, remove the drivers who already have reviews from the list so that you can't specify two reviews
            ddlDrivers.Items.Insert(0, new ListItem("Select Driver", "0"));

            List<SportSeasonSuppPlayerReview> driverWithReviews = SportSeasonSuppPlayerReview.GetSportSeasonSuppPlayerReviews("RAC", ddlSeason.SelectedValue, int.Parse(ddlSupplementalSource.SelectedValue));


            foreach (SportSeasonSuppPlayerReview currentDriver in driverWithReviews)
            {
              Player foundPlayer = relevantDrivers.Find((delegate(Player targetPlayer) { return (targetPlayer.PlayerID == currentDriver.PlayerID); }));
              if (foundPlayer != null)
              {
                relevantDrivers.Remove(foundPlayer);
              }
            }
            ddlDrivers.DataSource = relevantDrivers;
            ddlDrivers.DataBind();
          }
          else
          {
            ddlDrivers.SelectedValue = boundReview.PlayerID.ToString();
            tbReviewURL.Text = boundReview.ReviewURL;

            ibAdd.Visible = false;
            ddlDrivers.DataSource = relevantDrivers;
            ddlDrivers.DataBind();
          }
        }

        // if we're binding to an 'Add' row, put the gridview in Edit mode for that row.
        if ((e.Row.DataItem != null) && (!((BasePage)this.Page).IsRefresh))
        {
          if (((SportSeasonSuppPlayerReview)e.Row.DataItem).PlayerID == 0)
          {
            gvSupplementalReviews.EditIndex = e.Row.RowIndex;
          }
        }
      }

    }

    protected void gvSupplementalReviews_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      int index = gvSupplementalReviews.EditIndex;
      GridViewRow editRow = gvSupplementalReviews.Rows[index];

      DropDownList ddlDrivers = (DropDownList)editRow.FindControl("ddlDrivers");
      TextBox tbReviewURL = (TextBox)editRow.FindControl("tbReviewURL");

      // update all stats
      int driverID = int.Parse(ddlDrivers.SelectedValue);

      SportSeasonSuppPlayerReview.UpdateSportSeasonSuppPlayerReview(this.SportCode, ddlSeason.SelectedValue, int.Parse(ddlSupplementalSource.SelectedValue),
                                                                        driverID, tbReviewURL.Text);

      _activeCommand = String.Empty;
      gvSupplementalReviews.EditIndex = -1;
      BindReviews();
    }



    /// <summary>
    /// After the gridview is bound, we need to hide those 'Arrow' controls that aren't relevant
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSupplementalReviews_DataBound(object sender, EventArgs e)
    {
      if ((_activeCommand == "Insert") && (!((BasePage)this.Page).IsRefresh) && (!Inserted))
      {
        Inserted = true;
        BindReviews();
      }
    }





    /// <summary>
    /// After a row is created in the gridview, and if it is in edit mode,
    /// we need to hide the edit and delete buttons (because the update and cancel buttons
    /// will be shown
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSupplementalReviews_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void gvSupplementalReviews_RowCommand(object sender, GridViewCommandEventArgs e)
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
      DropDownList ddlDrivers = (DropDownList)gvSupplementalReviews.Rows[gvSupplementalReviews.EditIndex].FindControl("ddlDrivers");
      TextBox tbReviewURL = (TextBox)gvSupplementalReviews.Rows[gvSupplementalReviews.EditIndex].FindControl("tbReviewURL");

      int driverID = int.Parse(ddlDrivers.SelectedValue);

      SportSeasonSuppPlayerReview.InsertSportSeasonSuppPlayerReview(this.SportCode, ddlSeason.SelectedValue, int.Parse(ddlSupplementalSource.SelectedValue),
                                                                             driverID, tbReviewURL.Text);

      _activeCommand = String.Empty;
      gvSupplementalReviews.EditIndex = -1;
      BindReviews();
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
      BindReviews();
    }



    protected void gvSupplementalReviews_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
      _activeCommand = String.Empty;
      gvSupplementalReviews.EditIndex = -1;
      BindReviews();
    }


    protected void gvSupplementalReviews_RowEditing(object sender, GridViewEditEventArgs e)
    {
      gvSupplementalReviews.EditIndex = e.NewEditIndex;
      BindReviews();
    }


    protected void gvSupplementalReviews_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      bool result = SportSeasonSuppPlayerReview.DeleteSportSeasonSuppPlayerReview(this.SportCode, ddlSeason.SelectedValue, int.Parse(ddlSupplementalSource.SelectedValue),
                                                                  int.Parse(gvSupplementalReviews.DataKeys[e.RowIndex].Value.ToString()));
      gvSupplementalReviews.EditIndex = -1;
      BindReviews();
    }
}
}