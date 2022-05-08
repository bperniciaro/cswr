using System;
using BP.CheatSheetWarRoom;
using BP.CheatSheetWarRoom.BLL.Sheets;

public partial class fantasy_football_nfl_free_printable_offense_quarterback_cheat_sheet : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!IsPostBack)
    {
      Helpers.AddStyleSheetReferences(this);
      BuildSEOInfo();
    }
  }

  private void BuildSEOInfo()
  {
    string currentSeason = SportSeason.GetCurrentSportSeason("FOO").SeasonCode;
    Page.Title = "Quarterbacks Cheat Sheet in Printable Format for " + currentSeason;
    Page.MetaDescription = "This free, printable quarterbacks cheat sheet includes NFL QB rankings for your " + currentSeason + " fantasy football draft.";
  }


}
