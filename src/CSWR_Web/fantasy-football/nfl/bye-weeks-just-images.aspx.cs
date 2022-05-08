using System;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class ByeWeeks : BasePage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        LoadSocialTags();

        // set update date
        litUpdateDate.Text = DateTime.Now.Subtract(new TimeSpan(2, 0, 0, 0)).ToShortDateString();
      }
    }

    private void LoadSocialTags()
    {
      SportMaster myMaster = (SportMaster)this.Page.Master;
      myMaster.OpenGraphImage = "https://www.cheatsheetwarroom.com/images/sports/football/articles/byeweeks/nfl-bye-weeks-2017.png";
      myMaster.SchemaOrgImage = "https://www.cheatsheetwarroom.com/images/sports/football/articles/byeweeks/nfl-bye-weeks-2017.png";
      myMaster.TwitterImage = "https://www.cheatsheetwarroom.com/images/sports/football/articles/byeweeks/nfl-bye-weeks-2017.png";
    }
  }
}