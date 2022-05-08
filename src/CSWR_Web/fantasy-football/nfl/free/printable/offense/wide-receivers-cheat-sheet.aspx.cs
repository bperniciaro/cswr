using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class FantasyFootballWideReceiversCheatSheet : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      Helpers.AddStyleSheetReferences(this);
      BuildSEOInfo();
    }

    private void BuildSEOInfo()
    {
      string currentSeason = SportSeason.GetCurrentSportSeason("FOO").SeasonCode;
      Page.Title = "Wide Receivers Cheat Sheet in Printable Format for " + currentSeason;
      Page.MetaDescription = "This free, printable wide receivers cheat sheet includes NFL WR rankings for your " + currentSeason + " fantasy football draft.";
    }
  }
}