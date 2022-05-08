using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class PrintableDriversCheatSheetWithRoster : System.Web.UI.Page
  {
    protected void Page_Init(object sender, EventArgs e)
    {
      this.Page.Title = "Free, Printable " + SportSeason.GetCurrentSportSeason("RAC").SeasonCode + " Fantasy NASCAR Racing Cheat Sheet";
      Helpers.AddStyleSheetReferences(this);
    }

  }
}