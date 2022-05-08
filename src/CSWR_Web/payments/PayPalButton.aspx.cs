using System;
using System.Web.Security;

public partial class test_PayPalButton : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    bool userFound = false;

    MembershipUser currentUser = Membership.GetUser(this.User.Identity.Name);
    if (currentUser != null)
    {
      if (currentUser.ProviderUserKey != null)
      {
        myHiddenField.Value = currentUser.ProviderUserKey.ToString();
        myHiddenField.ID = "custom";
        userFound = true;
      }
    }
    else
    {
      // error!
    }
  }
}