using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class SheetVisitorNavigation : System.Web.UI.UserControl
  {

    public string SportCode
    {
      get
      {
        return (ViewState["SportCode"] == null) ? String.Empty : ViewState["SportCode"].ToString();
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
        BuildControls();
      }
    }

    public void BuildControls()
    {
      ConfigureButtons();
    }

    private void ConfigureButtons()
    {
      Sport currentSport = Sport.GetSport(this.SportCode);
      if (currentSport != null)
      {
        hlSamplePrintableSheet.NavigateUrl = "~/fantasy-" + currentSport.SportName.ToLower() + "/nfl/free/printable/offense/cheat-sheet-with-roster.aspx";
        //hlHelp.NavigateUrl = "~/fantasy-football/nfl/cheat-sheet-help.aspx";
      }
      
    }
  }
}