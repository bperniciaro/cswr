using System;
using System.Web;
using System.Web.Security;
using BP.CheatSheetWarRoom.BLL.Forum;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class NewBanner : System.Web.UI.UserControl
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        InitializeControls();
        ConfigureHomePageLink();
        //if (!Globals.Settings.EnableSocialMedia)
        //{
        //  panAddThis.Visible = false;
        //}
      }
    }


    private void ConfigureHomePageLink()
    {
      if (Globals.CSWRSettings.ApplicationState == ApplicationState.Local.ToString().ToLower())
      {
        hlLogoLink.NavigateUrl = "~/Default.aspx";
      }
      else
      {
        hlLogoLink.NavigateUrl = "https://www.cheatsheetwarroom.com";
      }
    }

    private void InitializeControls()
    {
      liModItem.Visible = false;
      
      // if the user isn't authenticated, wire-up the login control to recognize the 'ENTER' Key if it's controls are active
      if (HttpContext.Current.User.Identity.IsAuthenticated)
      {
        // configure the banner links
        liLogoutItem.Attributes.Add("class", "last");
        //liUserControlPanel.Visible = true;
        liJoinUs.Visible = false;
        liLogin.Visible = false;
        liPublicProfile.Visible = true;


        // configure Administrative and Moderator link
        if (Roles.IsUserInRole("Administrator"))
        {
          liAdminItem.Visible = true;
        }
        if (Roles.IsUserInRole("Moderator"))
        {
          liModItem.Visible = true;
        }
  
        // configure public profile link
        hlPublicProfile.Visible = true;
        litProfileLink.Text = HttpContext.Current.User.Identity.Name;

        //int asppMemberID = ForumMember.GetMemberID(HttpContext.Current.User.Identity.Name);

        // configure links
        if (Globals.CSWRSettings.ApplicationState == ApplicationState.Local.ToString().ToLower())
        {
          // public profile link
          //hlPublicProfile.NavigateUrl = "http://localhost/community/profile/" + asppMemberID.ToString();
          // user control panel
          //hlUserControlPanel.NavigateUrl = "http://localhost/community/editprofile.aspx";
          // registration link
          //hlJoinUs.NavigateUrl = "http://localhost/community/register.aspx";
        }
        else
        {
          // public profile link
          //hlPublicProfile.NavigateUrl = "~/access/profile/" + asppMemberID.ToString();
          // user control panel
          //hlUserControlPanel.NavigateUrl = "~/access/editprofile.aspx";
          // registration link
          //hlJoinUs.NavigateUrl = "~/access/register.aspx";
        }
      }
      // user is not authenticated
      else
      {
        // configure the banner links
        liLogoutItem.Visible = false;
        liAdminItem.Visible = false;
        //liUserControlPanel.Visible = false;
        liJoinUs.Visible = true;
        liLogin.Visible = true;
        liPublicProfile.Visible = false;

        // configure registration (join us) link
        if (Globals.CSWRSettings.ApplicationState == ApplicationState.Local.ToString().ToLower())
        {
          hlJoinUs.NavigateUrl = "~/access/register.aspx";
          hlLogin.NavigateUrl = "~/access/login.aspx" + "?ReturnURL=" + HttpContext.Current.Request.Url.AbsoluteUri;
        }
        else
        {
          hlJoinUs.NavigateUrl = "~/access/register.aspx";
          hlLogin.NavigateUrl = "~/access/login.aspx" + "?ReturnURL=" + HttpContext.Current.Request.Url.AbsoluteUri;
        }
      }
    }

 
    protected void lbBannerLogout_Click(object sender, EventArgs e)
    {
      Helpers.LogoutUser();
      //HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
      //InitializeControls();
      Response.Redirect(Request.Url.AbsoluteUri);
    }

  }
}