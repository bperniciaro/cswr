using System;
using System.Web.Security;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class Default2 : BasePage
  {

    private void AddUserToMemberRoleIfNecessary()
    {

      // add user to the 'Member' role if they're not in there already.  Some users are able to register without
      // a role, probably due to an exception during registration
      if (!Roles.IsUserInRole(this.User.Identity.Name, "Member"))
      {
        Roles.AddUserToRole(this.User.Identity.Name, "Member");
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.User.Identity.IsAuthenticated)
      {
        // don't show the banner ad after users are authenticated
        AddUserToMemberRoleIfNecessary();
        //hlCreateFootballCheatSheet.Text = "MY FOOTBALL CHEAT SHEETS";
        //hlCreateRacingCheatSheet.Text = "MY RACING CHEAT SHEETS";

        //hlCreateFootballCheatSheet.NavigateUrl = "~/fantasy-football/nfl/create/custom-sheet.aspx";
        //hlCreateRacingCheatSheet.NavigateUrl = "~/fantasy-racing/nascar/create/custom-sheet.aspx";
      }
      else
      {
        // only show the top banner ad if the switch is explicitly set in the web.config
        //hlCreateFootballCheatSheet.Text = "FANTASY FOOTBALL CHEAT SHEETS";
        //hlCreateRacingCheatSheet.Text = "FANASY RACING CHEAT SHEETS";

        //hlCreateFootballCheatSheet.NavigateUrl = "~/fantasy-football/nfl/create/custom-sheet.aspx"; ;
        //hlCreateRacingCheatSheet.NavigateUrl = "~/fantasy-racing/nascar/create/custom-sheet.aspx";
      }
    }



  }
}