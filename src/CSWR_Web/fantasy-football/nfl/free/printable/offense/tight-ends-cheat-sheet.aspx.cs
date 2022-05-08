using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class FantasyFootballTightEndsCheatSheet : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      Helpers.AddStyleSheetReferences(this);
      BuildSeoInfo();
    }


    public void BuildSeoInfo()
    {
      string currentSeason = SportSeason.GetCurrentSportSeason("FOO").SeasonCode;
      Page.Title = "Tight Ends Cheat Sheet in Printable Format for " + currentSeason;
      Page.MetaDescription = "This free, printable tight ends cheat sheet includes NFL TE rankings for your " + currentSeason + " fantasy football draft.";

    }
  }
}