using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class UserLogin : BasePage
  {

    /// <summary>
    /// The page seems to redirect twice after login failure, meaning IsPostBack is wrong, so we need to keep track of
    /// whether the page has been loaded already
    /// </summary>
    public bool BeenOnPage
    {
      get
      {
        return (ViewState["BeenOnPage"] == null) ? false : (bool)ViewState["BeenOnPage"];
      }
      set
      {
        ViewState["BeenOnPage"] = value;
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      // if the user isn't authenticated, wire-up the login control to recognize the 'ENTER' Key if it's controls are active
      if (!HttpContext.Current.User.Identity.IsAuthenticated)
      {
        Login loginControl = (Login)lvLoginView.FindControl("logLogin");
        TextBox tbUserName = (TextBox)loginControl.FindControl("UserName");
        TextBox tbPassword = (TextBox)loginControl.FindControl("Password");
        Button loginButton = (Button)loginControl.FindControl("LoginButton");
        Page.Form.DefaultFocus = tbUserName.ClientID;
        Page.Form.DefaultButton = loginButton.UniqueID;
        //panForMoreRegister.Visible = true;
      }

      this.BeenOnPage = true;
    }

    protected void lsLoginStatus_OnLoggedOut(object sender, EventArgs e)
    {
      Response.Redirect("~/");
    }

    protected void logLogin_OnLoggedError(object sender, EventArgs e)
    {
      Login logLogin = (Login)lvLoginView.FindControl("logLogin");
      MembershipUser currentUser = Membership.GetUser(logLogin.UserName);
      StringBuilder sbErrorMessage = new StringBuilder();

      if (currentUser == null)
      {
        mbLoginError.Message = sbErrorMessage.Append("Login Failed");
        logLogin.FailureText = "Invalid Username. Please try again.";
        mbLoginError.WidthPercentage = 35;
      }
      else
      {
        if (currentUser.IsLockedOut)
        {
          string lockedOutMessage = "Your account is currently locked (probably because of too many failed login attempts).  To unlock your account " +
                                    "please contact a system administrator <a href=\"mailto:admin@cheatsheetwarroom.com\">via email</a> " +
                                    "or <a href=\"contact.aspx\">using our contact form</a>.  Provide " +
                                    "your username or email address and we'll unlock your account for you.";
          mbLoginError.Message = sbErrorMessage.Append(lockedOutMessage);
          mbLoginError.WidthPercentage = 75;
        }
        else
        {
          mbLoginError.Message = sbErrorMessage.Append("Invalid Password. Please try again.");
          mbLoginError.WidthPercentage = 35;
        }
      }

      mbLoginError.Visible = true;
      mbLoginError.SetMessage();
    }

    protected void logLogin_OnLoggedIn(object sender, EventArgs e)
    {
      //TextBox tbUserName = (TextBox)lvLoginView.FindControl("logLogin").FindControl("UserName");

      // spin through all football visitor sheets and delete them
      List<Position> possiblePositions = Position.GetPositions("FOO");
      for (int i = 0; i < possiblePositions.Count; i++)
      {
        if (Session[possiblePositions[i].PositionCode] != null)
        {
          int positionSheetID = (int)Session[possiblePositions[i].PositionCode.ToString()];
          CheatSheet.DeleteCheatSheet(positionSheetID);
          Session[possiblePositions[i].PositionCode] = null;
        }
      }
      SessionHandler.CurrentFOOVisitorSheetPosition = null;
      SessionHandler.CurrentRACVisitorSheetPosition = null;

      mbLoginError.Visible = false;



      //if (Globals.CSWRSettings.ApplicationState == ApplicationState.Local.ToString().ToLower())
      //{
      //  Response.Redirect("~/");
      //}
      //else
      //{
      //  Response.Redirect("https://www.cheatsheetwarroom.com");
      //}
      Response.Redirect("~/");

    }
  }
}