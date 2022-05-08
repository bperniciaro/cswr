using System;
using System.Web.Security;

public partial class test_TestEmailSubscriptions : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {

    int subscribedCount = 0;
    int nonSubscribedCount = 0;
    foreach (MembershipUser targetUser in Membership.GetAllUsers())
    {
      if (Profile.GetProfile(targetUser.UserName).EmailNotifications)
      {
        subscribedCount++;
      }
      else
      {
        nonSubscribedCount++;
      }
    }

  }
}