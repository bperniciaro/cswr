using System;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class CheatSheetCreation : BasePage
  {
    protected void Page_Init(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        SessionHandler.CurrentSportCode = "RAC";
      }
    }

  }
}