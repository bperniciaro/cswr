using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class FantasyFootballRunningBacksCheatSheet : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      Helpers.AddStyleSheetReferences(this);
      BuildSEOInfo();
    }

    private void BuildSEOInfo()
    {
      string currentSeason = SportSeason.GetCurrentSportSeason("FOO").SeasonCode;
      Page.Title = "Running Backs Cheat Sheet in Printable Format for " + currentSeason;
      Page.MetaDescription = "This free, printable running backs cheat sheet includes NFL RB rankings for your " + currentSeason + " fantasy football draft.";
    }
  }
}