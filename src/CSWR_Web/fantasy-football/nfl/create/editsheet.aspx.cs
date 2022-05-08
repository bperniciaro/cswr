using System;
using System.Text;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class EditSheet : BasePage
  {

    /// <summary>
    /// Maintains a reference to the cheat sheet currently being edited
    /// </summary>
    private CheatSheet CurrentSheet
    {
      get
      {
        return (ViewState["CurrentSheet"] == null) ? null : (CheatSheet)ViewState["CurrentSheet"];
      }
      set
      {
        ViewState["CurrentSheet"] = value;
      }
    }

    /// <summary>
    /// Initialize the current sport so that the Master Page knows which tertiary navigation to display
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        SessionHandler.CurrentSportCode = "FOO";
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      int cheatSheetID = 0;

      if (!IsPostBack)
      {
        cheatSheetID = GetSheetIDFromQueryString();
        if (cheatSheetID > 0)
        {
          scmlnNavigation.CheatSheetID = cheatSheetID;
          spmSheetItemManager.SheetID = cheatSheetID;
          if (ConfirmSheetOwner(cheatSheetID))
          {
            this.CurrentSheet = CheatSheet.GetCheatSheet(cheatSheetID);
            // load controls
            LoadControls();
            // stat season
            //LoadStatSeasons();
          }
          //panSheetForm.Visible = true;
          //panSheetPlayers.Visible = true;
        }
        else
        {
          mbStatus.MessageType = MessageType.ERROR;
          mbStatus.Message = new StringBuilder("No Sheet Specified");
          //panSheetForm.Visible = false;
          //panSheetPlayers.Visible = false;  
        }
      }
      //trSettingsMessageRow.Visible = false;
    }


    private bool ConfirmSheetOwner(int sheetID)
    {
      // as an administrator I want to be able to view any sheet for for debugging purposes
      if (!this.User.IsInRole("Administrator"))
      {
        if (!CheatSheet.GetCheatSheet(sheetID).ConfirmOwner(User.Identity.Name))
        {
          mbStatus.MessageType = MessageType.ERROR;
          mbStatus.Message = new StringBuilder("This is not your sheet!");
          //panSheetForm.Visible = false;
          return false;
        }
        else
        {
          return true;
        }
      }
      else
      {
        return true;
      }
    }


    private int GetSheetIDFromQueryString()
    {
      int sheetIntID = 0;
      string sheetStringID = String.Empty;

      if (Request.QueryString["SheetID"] != null)
      {
        sheetStringID = Request.QueryString["SheetID"];
        if (int.TryParse(sheetStringID, out sheetIntID))
        {
          sheetIntID = int.Parse(sheetStringID);
        }

      }
      return sheetIntID;
    }


    private void LoadControls()
    {
      // sheet name
      tbSheetName.Text = this.CurrentSheet.SheetName;

      // positions
      if (this.CurrentSheet.Positions.Count == 1)
      {
        labPositions.Text = this.CurrentSheet.Positions[0].PositionCode;
      }
      else
      {
        int positionCounter = 0;
        foreach (CheatSheetPosition currentPosition in this.CurrentSheet.CheatSheetPositions)
        {
          if (positionCounter < this.CurrentSheet.Positions.Count - 1)
          {
            labPositions.Text += currentPosition.PositionCode + ", ";
          }
          else
          {
            labPositions.Text += currentPosition.PositionCode;
          }
          positionCounter++;
        }
      }

      // for defenses, hide sheet item manager
      if (this.CurrentSheet.Positions[0].PositionCode == "DF")
      {
        //panSheetPlayers.Visible = false;
      }

      // scoring configuration
      bool pprLeague = (bool)this.CurrentSheet.MappedProperties[CSProperty.PPRLeague.ToString()];
      labScoringConfiguration.Text = (pprLeague == true) ? "PPR" : "Standard";
      
      // stats season
      labStatsSeason.Text = this.CurrentSheet.StatsSeasonCode;

      bool auctionDraft = (bool)this.CurrentSheet.MappedProperties[CSProperty.AuctionDraft.ToString()];
      rbAuction.Checked = auctionDraft;
      rbSerpentine.Checked = !auctionDraft;
    }


    /// <summary>
    /// This method loads the seasons for which stats are available.  We will limit
    /// this to two because that is the most seasons that could be relevant for
    /// a particular sport on a particular year.  If the current season hasn't started,
    /// we hard-code the stat season from the previous season
    /// </summary>
    //private void LoadStatSeasons()
    //{
    //  ddlStatsSeasons.DataSource = Helpers.GetRelevantStatSeasons(FOO.FOOString);
    //  ddlStatsSeasons.DataBind();
    //}

    protected void butSave_Click(object sender, EventArgs e)
    {
      // change sheet name
      this.CurrentSheet.SheetName = tbSheetName.Text;

      // change scoring configuration
      //this.CurrentSheet.MappedProperties[CSProperty.PPRLeague.ToString()] = rbPPR.Checked;

      // change draft type
      this.CurrentSheet.MappedProperties[CSProperty.AuctionDraft.ToString()] = rbAuction.Checked;

      // change stat season, if relevant
      //if (trStatSeasonRow.Visible)
      //{
      //  this.CurrentSheet.StatsSeasonCode = ddlStatsSeasons.SelectedValue;
      //}

      // try to update
      if (this.CurrentSheet.Update() == true)
      {
        //trSettingsMessageRow.Visible = true;
        mbSheetSettingsMessage.MessageType = MessageType.SUCCESS;
        StringBuilder successMessage = new StringBuilder("Settings have been saved for sheet: ");
        string sheetPage = Page.ResolveClientUrl("~/fantasy-football/nfl/create/custom-sheet.aspx?SheetID=") + this.CurrentSheet.CheatSheetID.ToString();
        string sheetLink = " <a title='Click to return to this sheet.' href='" + sheetPage + "'>" + this.CurrentSheet.SheetName + "</a>.";
        successMessage.Append(sheetLink);
        mbSheetSettingsMessage.Message = successMessage;
      }
    }

    //protected void ddlStatsSeasons_DataBound(object sender, EventArgs e)
    //{
    //  ddlStatsSeasons.SelectedValue = this.CurrentSheet.StatsSeasonCode;
    //}

 
  }
}