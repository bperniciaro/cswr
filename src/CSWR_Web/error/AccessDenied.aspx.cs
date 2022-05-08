using System;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class AccessDenied : BasePage
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      //Master.MainHeader = "Access Denied";

      //The user is authenticated, so he must not have the proper permissions
      labInsufficientPermissions.Visible = this.User.Identity.IsAuthenticated;

      //An anonymous user tries to access a page which requires a login
      labLoginRequired.Visible = (!this.User.Identity.IsAuthenticated && string.IsNullOrEmpty(this.Request.QueryString["loginfailure"]));

      //An anonymous user provided an invalid login
      labInvalidCredentials.Visible = (this.Request.QueryString["loginfailure"] != null && this.Request.QueryString["loginfailure"] == "1");
    }
  }
}