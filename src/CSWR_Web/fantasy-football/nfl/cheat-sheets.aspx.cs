using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class FantasyFootballCheatSheets : BasePage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        LoadSocialTags();

        if (this.User.Identity.IsAuthenticated)
        {
          panCreateSheetContainer.Visible = false;
        }
        Page.MetaDescription = "Prepare for your " + SportSeason.GetCurrentSportSeason("FOO").SeasonCode + " NFL fantasy football draft by creating customized fantasy football cheat sheets, for free.";

        hlPlayerRankings.Text = FOO.CurrentSeason + " NFL player rankings";
      }
    }

    private void LoadSocialTags()  
    {
      SportMaster myMaster = (SportMaster)this.Page.Master;
      myMaster.OpenGraphImage = "http://www.cheatsheetwarroom.com/images/socialsharing/fantasy-football-cheat-sheets.jpg";
      myMaster.SchemaOrgImage = "http://www.cheatsheetwarroom.com/images/socialsharing/fantasy-football-cheat-sheets.jpg";
      myMaster.TwitterImage = "http://www.cheatsheetwarroom.com/images/socialsharing/fantasy-football-cheat-sheets.jpg";
    }
  }
}