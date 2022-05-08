using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class SheetCreationManageLevelNavigation : System.Web.UI.UserControl
  {

    /// <summary>
    /// These represent the various stages a user can be in when creating and managing cheat sheets
    /// </summary>
    public enum CreationStage { CUSTOMSHEET, EDITSHEET, MANAGESHEETS, NEWSHEET, PRINTSHEET }

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
        BuildControls();
      }
    }

    /// <summary>
    /// This property keeps track of the cheat sheet creation stage the user is currently in
    /// </summary>
    public CreationStage CurrentStage
    {
      get
      {
        return (ViewState["CreationStage"]==null) ? CreationStage.CUSTOMSHEET : (CreationStage)ViewState["CreationStage"];
      }
      set
      {
        ViewState["CreationStage"] = value;
      }
    }


    /// <summary>
    /// We only need to build the controls once per page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        BuildControls();
      }
    }

    /// <summary>
    /// Building the controls involved their configuration, updating the links
    /// </summary>
    public void BuildControls()
    {
      BuildButtonLinks();
      ShowHideButtons();
    }



    private void BuildButtonLinks()
    {
      // configure 'back to sheet' link
      hlBack.NavigateUrl = "~/fantasy-football/nfl/create/custom-sheet.aspx?SheetID=" + this.CheatSheetID;
      //lbBack.ToolTip = "Click to go back to the last fantasy football cheat sheet you were editing.";
    }



    private void ShowHideButtons()
    {
      // show or hide the labels/hyperlinks as appropriate
      switch (this.CurrentStage)
      {
        case CreationStage.CUSTOMSHEET:
          // configure back
          hlBack.Visible = false;
          //lbBack.Attributes.Add("class", "disabled");
          // configure manage
          //lbManageSheets.Visible = true;
          // configure new sheet
          //lbNewSheet.Visible = true;
          // configure print
          //lbOnePagePrint.Visible = true;
          //configure help
          hlHelp.NavigateUrl = "~/fantasy-football/nfl/create/custom-sheet.aspx#sheetCreation"; ;
          //lbHelp.Target = "_self";
          break;
        case CreationStage.EDITSHEET:
          // configure back
          //lbBack.Visible = false;
          // configure manage
          //lbNewSheet.Attributes.Add("class", "disabled");
          //lbManageSheets.Attributes.Add("class", "disabled");
          //..lbManageSheets.Visible = false;
          // configure new sheet
          //lbNewSheet.Visible = true;
          // configure print
          //lbOnePagePrint.Visible = true;
          // configure help
          hlHelp.NavigateUrl = "~/fantasy-football/nfl/create/custom-sheet.aspx#editSheet";
          break;
        case CreationStage.MANAGESHEETS:
          // configure back
          //lbBack.Visible = false;
          //lbManageSheets.Attributes.Add("class", "disabled");
          //lbManageSheets.Attributes.Add("class", "disabled");
          // configure manage
          //lbManageSheets.Visible = false;
          // configure new sheet
          //lbNewSheet.Visible = false;
          hlHelp.NavigateUrl = "~/fantasy-football/nfl/create/custom-sheet.aspx#manageSheets";
          //hlSheetHelp.NavigateUrl += "#manageSheets";
          // configure print
          //lbOnePagePrint.Visible = false;
          // configure help
          //hlSheetHelp.Target = "_blank";
          break;
        case CreationStage.NEWSHEET:
          // configure manage
          //lbManageSheets.Visible = false;
          // configure new sheet
          //lbNewSheet.Attributes.Add("class", "disabled");
          //spNewSheet.Visible = true;
          hlHelp.NavigateUrl = "~/fantasy-football/nfl/create/custom-sheet.aspx#sheetCreation";
          //hlSheetHelp.Target = "_blank";
          // configure print
          //lbOnePagePrint.Visible = false;
          break;
        case CreationStage.PRINTSHEET:
          // configure manage
          //lbManageSheets.Visible = false;
          // configure new sheet
          //lbNewSheet.Visible = false;
          // configure print
          //lbOnePagePrint.Visible = false;
          //lbOnePagePrint.Attributes.Add("class", "disabled");
          //configure help
          hlHelp.NavigateUrl = "~/fantasy-football/nfl/create/custom-sheet.aspx#printableSheets";
          //hlSheetHelp.Target = "_blank";
          //lbOnePagePrint.Visible = true;
          break;
      }

    }

 
    //protected void lbImage_Click(object sender, EventArgs e)
    //{
    //  OnEvent(new Globals.ExportEventArgs());
    //}
  }
}