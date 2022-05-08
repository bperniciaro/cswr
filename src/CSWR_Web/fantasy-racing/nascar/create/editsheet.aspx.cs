using System;
using System.Collections.Generic;
using System.Text;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class EditSheet : BasePage
  {
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

    public int SheetID
    {
      get
      {
        return (ViewState["SheetID"] == null) ? 0 : (int)ViewState["SheetID"];
      }
      set
      {
        ViewState["SheetID"] = value;
      }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        SessionHandler.CurrentSportCode = "RAC";
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        this.SheetID = GetSheetIDFromQueryString();

        if (this.SheetID > 0)
        {
          scmlnNavigation.CheatSheetID = this.SheetID;
          spmSheetItemManager.SheetID = this.SheetID;

          this.CurrentSheet = CheatSheet.GetCheatSheet(this.SheetID);
          // load controls
          LoadControls();
          // stat season
          LoadStatSeason();
          // initialize sheetitemmanager
          spmSheetItemManager.SportCode = "RAC";
        }
      }
      trSettingsMessageRow.Visible = false;
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
    }


    /// <summary>
    /// This method loads the seasons for which stats are available.  We will limit
    /// this to two because that is the most seasons that coule be relevant for
    /// a particular sport on a particular year.
    /// </summary>
    private void LoadStatSeason()
    {
      SportSeason currentSeason = SportSeason.GetCurrentSportSeason(FOO.FOOString);

      if (currentSeason.SeasonStarted)
      {
        List<SportSeason> relevantSeasons = SportSeason.GetSportStatSeasons("RAC");
        if (relevantSeasons.Count > 2)
        {
          relevantSeasons.RemoveRange(2, relevantSeasons.Count - 2);
        }

        trStatSeasonRow.Visible = true;
        ddlStatsSeason.DataSource = relevantSeasons;
        ddlStatsSeason.DataBind();
      }
      else
      {
        trStatSeasonRow.Visible = false;
      }
    }


    protected void butSave_Click(object sender, EventArgs e)
    {
      // sheet name
      this.CurrentSheet.SheetName = tbSheetName.Text;
      // stat season
      if (trStatSeasonRow.Visible)
      {
        this.CurrentSheet.StatsSeasonCode = ddlStatsSeason.SelectedValue;
      }
      // try to update
      if (this.CurrentSheet.Update() == true)
      {
        trSettingsMessageRow.Visible = true;
        mbSheetSettingsMessage.MessageType = MessageType.SUCCESS;
        StringBuilder successMessage = new StringBuilder("Settings have been saved for sheet: ");
        string sheetPage = Page.ResolveClientUrl("~/fantasy-racing/nascar/create/custom-sheet.aspx?Sheet=") + this.CurrentSheet.CheatSheetID.ToString();
        string sheetLink = " <a title='Click to return to this sheet.' href='" + sheetPage + "'>" + this.CurrentSheet.SheetName + "</a>.";
        successMessage.Append(sheetLink);
        mbSheetSettingsMessage.Message = successMessage;
      }
    }

    protected void ddlStatsSeason_DataBound(object sender, EventArgs e)
    {
      ddlStatsSeason.SelectedValue = this.CurrentSheet.StatsSeasonCode;
    }



  }
}