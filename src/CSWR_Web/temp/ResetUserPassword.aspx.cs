using System;
using System.Web.Security;

public partial class temp_ResetUserPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      MembershipUser mu = Membership.Providers["SqlMembershipProviderOther"].GetUser("mrcneff", false);
      mu.ChangePassword(mu.ResetPassword(), "newPa$$word");
    }
}