using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class SheetCreationSheetLevelNavigation : System.Web.UI.UserControl
  {

    /// <summary>
    /// Define an event handler which will run when the user requests to export a sheet
    /// </summary>
    public event Globals.ExportEventHandler ExportEvent;

    /// <summary>
    /// Define an event to call when we export a sheet.
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnEvent(Globals.ExportEventArgs e)
    {
      if (ExportEvent != null)
        ExportEvent(this, e);
    }

    /// <summary>
    /// These represent the various stages a user can be in when creating and managing cheat sheets
    /// </summary>
    public enum CreationStage { EDITSHEET, PRINTSHEET, EXPORTSHEET }

    /// <summary>
    /// This lets the user control know which sport we're currently viewing, allowing us to point links
    /// to the appropriate directories
    /// </summary>
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

    /// <summary>
    /// The ID of the cheat sheet being modified, allowing us to manipulate URLs as needed
    /// </summary>
    public int CheatSheetID
    {
      get
      {
        return (ViewState["CheatSheetID"] == null) ? 0 : int.Parse(ViewState["CheatSheetID"].ToString());
      }
      set
      {
        ViewState["CheatSheetID"] = value;
        BuildButtonLinks();
      }
    }

    /// <summary>
    /// This property keeps track of the cheat sheet creation stage the user is currently in
    /// </summary>
    public CreationStage CurrentStage
    {
      get
      {
        return (ViewState["CreationStage"]==null) ? CreationStage.EDITSHEET : (CreationStage)ViewState["CreationStage"];
      }
      set
      {
        ViewState["CreationStage"] = value;
      }
    }


    /// <summary>
    /// In some stages we want to hide the edit button since it isn't relevant
    /// </summary>
    public void HideEditButton()
    {
      //hlEditSheetText.Visible = false;
      //hlEditSheetImage.Visible = false;
    }



    /// <summary>
    /// We only need to build the controls once per page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
      if(!IsPostBack && (this.CheatSheetID != 0))
      {
        BuildButtonLinks();
      }
      if (this.Page.User.Identity.IsAuthenticated)
      {
        panMemberButtons.Visible = true;
        panVisitorButtons.Visible = false;
      }
      else
      {
        panVisitorButtons.Visible = true;
        panMemberButtons.Visible = false;
      }
    }

    private void BuildButtonLinks()
    {
      CheatSheet targetSheet = CheatSheet.GetCheatSheet(this.CheatSheetID);

      // get sport information
      string sportName = String.Empty;
      Sport currentSport = Sport.GetSport(this.SportCode);
      sportName = currentSport.SportName.ToLower();

      // get league information
      string leagueAbbreviation = String.Empty;
      leagueAbbreviation = currentSport.LeagueAbbreviation.ToLower();

      // configure the 'edit sheet' link
      if (this.SportCode == "RAC")
      {
        leagueAbbreviation = "nascar";
      }
      hlEditSheet.NavigateUrl = "~/fantasy-" + sportName + "/" + leagueAbbreviation + "/create/editsheet.aspx?SheetID=" + this.CheatSheetID;

      switch (this.SportCode)
      {
        case "RAC":
          hlPrintSheet.NavigateUrl = "~/fantasy-" + sportName + "/" + leagueAbbreviation + "/create/printable/cheatsheetalldrivers.aspx?SheetID=" + this.CheatSheetID.ToString();
          hlEditSheet.ToolTip = "Click to edit your sheet's settings.  Through this interface you can change your sheet name, add/remove drivers from your sheet, or re-sort the drivers in this sheet.";
          hlValidateSheet.Visible = false;
          break;
        case "FOO":
          hlValidateSheet.NavigateUrl = "~/fantasy-" + sportName + "/" + leagueAbbreviation + "/create/validatesheet.aspx?SheetID=" + this.CheatSheetID.ToString();
          hlEditSheet.ToolTip = "Click to edit your sheet's settings.  Through this interface you can change your sheet name, add/remove players from your sheet, or re-sort the players in this sheet.";

          if (targetSheet.Positions.Count > 1)
          {
            hlValidateSheet.Visible = false;
            lbExport.Visible = false;
            hlPrintSheet.NavigateUrl = "~/fantasy-" + sportName + "/" + leagueAbbreviation + "/create/printable/multiple-positions/cheatsheetwithroster.aspx?SheetID=" + this.CheatSheetID.ToString();
          }
          else
          {
            hlPrintSheet.NavigateUrl = "~/fantasy-" + sportName + "/" + leagueAbbreviation + "/create/printable/single-position/cheatsheetbyposition.aspx?SheetID=" + this.CheatSheetID.ToString();
          }

          // PPR leagues don't support validation
          if ((bool)targetSheet.MappedProperties[CSProperty.PPRLeague.ToString()])
          {
            hlValidateSheet.Visible = false;
          }
          break;
      }

      if (!SportSetting.Football.ShowSupplementalRankings)
      {
        hlValidateSheet.Visible = false;
      }

    }



    protected void lbExport_Click(object sender, EventArgs e)
    {
      OnEvent(new Globals.ExportEventArgs());
    }

}
}