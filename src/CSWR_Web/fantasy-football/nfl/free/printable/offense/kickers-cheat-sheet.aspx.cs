using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class FantasyFootballKickersCheatSheet : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      Helpers.AddStyleSheetReferences(this);
      BuildSEOInfo();
    }

    private void BuildSEOInfo()
    {
      string currentSeason = SportSeason.GetCurrentSportSeason("FOO").SeasonCode;
      Page.Title = "Kickers Cheat Sheet in Printable Format for " + currentSeason;
      Page.MetaDescription = "This free, printable kickers cheat sheet includes NFL K rankings for your " + currentSeason + " fantasy football draft.";
    }
  }
}