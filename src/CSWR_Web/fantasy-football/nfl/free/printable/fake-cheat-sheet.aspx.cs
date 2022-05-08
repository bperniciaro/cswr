using System;
using BP.CheatSheetWarRoom;
using BP.CheatSheetWarRoom.UI;

public partial class fantasy_football_nfl_free_printable_fake_cheat_sheet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        hlByeWeeks.Text = FOO.CurrentSeason + " NFL bye weeks";
        LoadSocialTags();
      }
    }


    protected void butGenerateRandomSheet_Click(object sender, EventArgs e)
    {
      string randomizeByes = cbRandomizeByes.Checked.ToString();

      Response.Redirect("~/fantasy-football/nfl/free/printable/offense/fcheat-sheet.aspx"
                            + "?groupSize=" + hfGroupSize.Value.ToString()
                            + "&lockCount=" + ddlIgnoreCount.SelectedValue
                            + "&randomizeByes=" + randomizeByes);
    }

    private void LoadSocialTags()
    {
      ResponsiveTwoCol myMaster = (ResponsiveTwoCol)this.Page.Master;
      //myMaster.OpenGraphImage = "https://www.cheatsheetwarroom.com/images/socialsharing/printable/fake-fantasy-football-cheat-sheet.jpg";
      //myMaster.SchemaOrgImage = "https://www.cheatsheetwarroom.com/images/socialsharing/printable/fake-fantasy-football-cheat-sheet.jpg";
      //myMaster.TwitterImage = "https://www.cheatsheetwarroom.com/images/socialsharing/printable/fake-fantasy-football-cheat-sheet.jpg";
    }

}