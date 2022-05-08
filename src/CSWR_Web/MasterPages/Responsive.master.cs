using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace BP.CheatSheetWarRoom.UI
{
  public partial class ResponsiveMaster : System.Web.UI.MasterPage
  {

  
    protected void Page_Init(object sender, EventArgs e)
    {
      Helpers.AddScriptReferences(this.Page, true);
      Helpers.AddStyleSheetReferences(this.Page, true);
    }


    protected void Page_Load(object sender, EventArgs e)
    {

      if (!IsPostBack)
      {
        InitializeControls();
        ConfigureHomePageLink();
        ConfigureEnvironmentLinks();
        if (!Globals.CSWRSettings.EnableSocialMedia)
        {
          panAddThis.Visible = false;
        }

        labSoftwareVersion.Text = "v" + Globals.CSWRSettings.Version;
        litCurrentYear.Text = DateTime.Now.Year.ToString();
        ConfigureNavigation();
      }
    }


    private void InitializeControls()
    {
      ConfigureBannerLinks();
    }

    private void ConfigureBannerLinks()
    {
      // if the user isn't authenticated, wire-up the login control to recognize the 'ENTER' Key if it's controls are active
      if (HttpContext.Current.User.Identity.IsAuthenticated)
      {
        liLogoutItem.Attributes.Add("class", "last");
        // liLogoutItem2.Attributes.Add("class", "last"'); not sure if this is needed

        liJoinUs.Visible = false;
        liJoinUs2.Visible = false;

        liLogin.Visible = false;
        liLogin2.Visible = false;

        liPublicProfile.Visible = true;

        // configure Administrative and Moderator link
        liModItem.Visible = false;
        liModItem2.Visible = false;
        if (Roles.IsUserInRole("Administrator"))
        {
          liAdminItem.Visible = true;
          liAdminItem2.Visible = true;
        }
        if (Roles.IsUserInRole("Moderator"))
        {
          liModItem.Visible = true;
          liModItem2.Visible = true;
        }

        // configure public profile link
        hlPublicProfile.Visible = true;
        hlPublicProfile2.Visible = true;

        litProfileLink.Text = HttpContext.Current.User.Identity.Name;
        litProfileLink2.Text = HttpContext.Current.User.Identity.Name;
      }
      // user is not authenticated
      else
      {
        // configure the banner links
        liLogoutItem.Visible = false;
        liLogoutItem2.Visible = false;
        liAdminItem.Visible = false;
        liAdminItem2.Visible = false;
        liModItem.Visible = false;
        liModItem2.Visible = false;


        liJoinUs.Visible = true;
        liJoinUs2.Visible = true;

        liLogin.Visible = true;
        liLogin2.Visible = true;

        liPublicProfile.Visible = false;
        liPublicProfile2.Visible = false;

        // configure registration (join us) link
        if (Globals.CSWRSettings.ApplicationState == ApplicationState.Local.ToString().ToLower())
        {
          hlJoinUs.NavigateUrl = "~/access/register.aspx";
          hlJoinUs2.NavigateUrl = "~/access/register.aspx";

          hlLogin.NavigateUrl = "~/access/login.aspx" + "?ReturnURL=" + HttpContext.Current.Request.Url.AbsoluteUri;
          hlLogin2.NavigateUrl = "~/access/login.aspx" + "?ReturnURL=" + HttpContext.Current.Request.Url.AbsoluteUri;
        }
        else
        {
          hlJoinUs.NavigateUrl = "~/access/register.aspx";
          hlJoinUs2.NavigateUrl = "~/access/register.aspx";

          hlLogin.NavigateUrl = "~/access/login.aspx" + "?ReturnURL=" + HttpContext.Current.Request.Url.AbsoluteUri;
          hlLogin2.NavigateUrl = "~/access/login.aspx" + "?ReturnURL=" + HttpContext.Current.Request.Url.AbsoluteUri;
        }
      }
    }

    private void ConfigureNavigation()
    {
      // Configure Main Navigation
      if (Globals.isPageInSitemapTree("mock-drafts"))
      {
        //liMainNav_Mocks.Attributes.Add("class", "active");
      }
      else if (Globals.isPageInSitemapTree("/fantasy-football"))
      {
        liMainNav_Sheets.Attributes.Add("class", "active");
      }
    }

    private void ConfigureEnvironmentLinks()
    {

      if (Globals.CSWRSettings.ApplicationState == ApplicationState.Local.ToString().ToLower())
      {
        gaAnalytics.Visible = false;
      }
      else
      {
        gaAnalytics.Visible = true;
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

    protected void lbBannerLogout_Click(object sender, EventArgs e)
    {
      Helpers.LogoutUser();
      //HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
      //InitializeControls();
      Response.Redirect(Request.Url.AbsoluteUri);
    }

  }
}