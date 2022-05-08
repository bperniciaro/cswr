using System;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.Admin.Sheets
{
  public partial class PowerUsers : BasePage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      gvPowerUsers.DataSource = UserSession.GetUserSessions().OrderByDescending(x => x.SessionCount).Take(40);
      gvPowerUsers.DataBind();
    }



    protected void gvPowerUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        UserSession boundUserSession = (UserSession)e.Row.DataItem;
        Label labIsModerator = (Label)e.Row.FindControl("labIsModerator");
        HyperLink hlEmail = (HyperLink)e.Row.FindControl("hlEmail");

         //NavigateUrl='<%# Bind("EmailAddress", "mailto:{0}") %>' Text='<%# Bind("EmailAddress") %>' 
        
        // indicate if the user is a moderator or not
        labIsModerator.Text = (Roles.IsUserInRole(boundUserSession.UserName, "Moderator")) ? "Yes" : "No";
        if (labIsModerator.Text == "Yes")
        {
          e.Row.BackColor = System.Drawing.Color.FromName("#39ec39");
        }


        // build the link to pre-populate an email asking to be a moderator
        hlEmail.Text = "Contact";

        string emailName = Profile.GetProfile(boundUserSession.UserName).FirstName;
        if (emailName == String.Empty)
        {
          emailName = boundUserSession.UserName;
        }

        string emailBody = "My name is Brad Perniciaro and I'm the creator of Cheat Sheet War Room.  I'm writing you because I have a proposition that I " +
          "believe could benefit both of us this fantasy season. %0D%0A%0D%0A" + 
          "As Cheat Sheet War Room has grown, it has become difficult to keep track of all the changes that " +
          "occur during the preseason.  While accurate player information is a necessity, it is simply impossible for one person " + 
          "to ensure player information remains accurate at all times. %0D%0A" +

          "So, I'm reaching-out to a few of my power users and asking for help in keeping player information current.  The idea is pretty simple: I'll give you " +
          "Moderator access to a page where you can update player information at any time.  If you're using " +
          "the app and notice a player's information is incorrect, you can simply go to your Moderator page and make the fix. It's that easy. %0D%0A" +

          "As a 'Thank You' I'll happily upgrade your account when I implement some advanced functionality this season.  I will be charging a small upgrade fee for " +
          "things like tiered rankings and multiple positions per sheet, but active Moderators will get this upgrade for free for as long as they're willing to help.%0D%0A" +

          "Either way, I honestly appreciate your time and thank you for using my application.  If you like it now, I can promise you that I've only just begun. %0D%0A" +
          "Looking forward to hearing from you, %0D%0A %0D%0ABrad";

        hlEmail.NavigateUrl = "mailto:" + boundUserSession.EmailAddress + "?subject=Partnership Proposition" + "&body=Hey " + emailName + ",%0D%0A %0D%0A" + emailBody.ToString();

      }
    }
}
}