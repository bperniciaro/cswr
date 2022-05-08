using System;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class FantasyRacingCheatSheets : BasePage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.User.Identity.IsAuthenticated)
      {
        panCreateSheetContainer.Visible = false;
      }
    }
  }
}