using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class FantasyFootballDefensesCheatSheet : System.Web.UI.Page
  {
    protected void Page_Init(object sender, EventArgs e)
    {
      Helpers.AddStyleSheetReferences(this);
      BuildSEOInfo();
    }


    private void BuildSEOInfo()
    {
      string currentSeason = SportSeason.GetCurrentSportSeason("FOO").SeasonCode;
      Page.Title = "Defenses Cheat Sheet in Printable Format for " + currentSeason;
      Page.MetaDescription = "This free, printable defenses cheat sheet includes DEF rankings for your " + currentSeason + " fantasy football draft.";
    }
  }
}