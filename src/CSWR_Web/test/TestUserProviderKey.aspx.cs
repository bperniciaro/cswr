using System;
using System.Web.Security;

public partial class test_TestUserProviderKey : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

      MembershipUser currentUser = Membership.GetUser(this.User.Identity.Name);

      int i = 0;


    }
}