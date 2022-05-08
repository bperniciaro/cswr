using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.Admin.Sheets
{
  public partial class EditSupplementalSheet : BasePage
  {


    protected void Page_Load(object sender, EventArgs e)
    {
      spmSheetItemManager.SheetID = int.Parse(this.Request.QueryString["ID"]);
      hlBackToSheet.NavigateUrl = "RankSupplementalPlayers.aspx?ID=" + spmSheetItemManager.SheetID.ToString();
      if (!IsPostBack)
      {
        SupplementalSheet editSheet = SupplementalSheet.GetSupplementalSheet(spmSheetItemManager.SheetID);
        labSource.Text = SupplementalSource.GetSupplementalSource(editSheet.SupplementalSourceID).Name;
        labPosition.Text = editSheet.PositionCode;
        //Master.MainHeader = "Edit Supplemental Sheet";
      }
    }
  }
}