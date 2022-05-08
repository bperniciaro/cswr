using System;
using System.Web;
using System.Web.UI;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class Web20Frame : System.Web.UI.MasterPage
  {
    private string _defaultSocialSharingImageUrl = "https://www.cheatsheetwarroom.com/images/socialsharing/cswr-logo.gif";

    // Schema.org for Google+
    public string SchemaOrgName { get; set; }
    public string SchemaOrgDescription {get;set;}
    public string SchemaOrgImage {get;set;}

    // Twitter
    public string TwitterTitle { get; set; }
    public string TwitterDescription { get; set; }
    public string TwitterImage { get; set; }
    public string TwitterUrl { get; set; }


    // Open Graph for Facebook
    public string OpenGraphUrl { get; set; }
    public string OpenGraphImage { get; set; }
    public string OpenGraphTitle { get; set; }
    public string OpenGraphDescription { get; set; }
  
    protected void Page_Init(object sender, EventArgs e)
    {
      Helpers.AddScriptReferences(this.Page);
      Helpers.AddStyleSheetReferences(this.Page);
    }


      protected void Page_Load(object sender, EventArgs e)
      {

          if (!IsPostBack)
          {
              ConfigureEnvironmentLinks();

              DetermineAdDisplay();

              //AddFacebookSlideIn();
              LoadSocialTags();

              if (!Globals.CSWRSettings.EnableSocialMedia)
              {
                  panAddThis.Visible = false;
              }

              labSoftwareVersion.Text = "v" + Globals.CSWRSettings.Version;
              litCurrentYear.Text = DateTime.Now.Year.ToString();
          }

          // only show the top banner ad if the switch is explicitly set in the web.config
          if (!Globals.CSWRSettings.ShowTopBannerAd)
          {
              panTopBannerAd.Visible = false;
          }
          else
          {
              panTopBannerAd.Visible = true;
          }

          if (this.Page.User.Identity.IsAuthenticated)
          {
            panTopBannerAd.Visible = false;
            //hlFantasyFootballEntryPage.NavigateUrl = "~/fantasy-football/nfl/create/custom-sheet.aspx";
            //hlFantasyRacingEntryPage.NavigateUrl = "~/fantasy-racing/nascar/create/custom-sheet.aspx";
            panSkyscraper.CssClass = "skyAuth";
      }
          else
          {

              //hlFantasyFootballEntryPage.NavigateUrl = "~/fantasy-football/nfl/cheat-sheets.aspx";
              //hlFantasyRacingEntryPage.NavigateUrl = "~/fantasy-racing/nascar/cheat-sheets.aspx";
              panSkyscraper.CssClass = "skyUnAuth";
          }

          if (Globals.isPageInSitemapTree("/fantasy-football/"))
          {
              liFootball.Attributes.Remove("class");
              liFootball.Attributes.Add("class", "footballSheets active");
          }
          else if (Globals.isPageInSitemapTree("/fantasy-racing/"))
          {
              //liRacing.Attributes.Remove("class");
              //liRacing.Attributes.Add("class", "racingSheets active");
          }
          else
          {
              liHome.Attributes.Remove("class");
              liHome.Attributes.Add("class", "home active");
          }
        }

      private void AddFacebookSlideIn()
    {

      string facebookSlideInScript = "$(document).ready( \n" +
            "function(){ \n" +
            "$('.cevherlink').CevherLink(  { \n" +

            "language: \"en_US\", \n" +
            "cookie_day: \"7\", \n" +
            "text: \"Like and Support us!\", \n" +
            "border: '1px #bbbbbb solid',\n" +
            "corner: \"5px\",\n" +
            "href: \"https://www.facebook.com/CheatSheetWarRoom\", \n" +
            "text_position: \"top\", \n" +
            "text_font: \"arial\", \n" +
            "text_weight: \"bold\", \n" +
            "text_color: \"#336699\", \n" +
            "size: \"16px\", \n" +
            "background: \"#ffffff\", \n" +
            "icon_src: \"" + ResolveClientUrl("~/Images/addons/cevherlink/close.png") + "\", \n" +
            "minimize: \"" + ResolveClientUrl("~/Images/addons/cevherlink/minimize.png") + "\", \n" +
            "maximize: \"" + ResolveClientUrl("~/Images/addons/cevherlink/maximize.png") + "\", \n" +
            "pause: \"" + ResolveClientUrl("~/Images/addons/cevherlink/pause.png") + "\", \n" +
            "start: \"" + ResolveClientUrl("~/Images/addons/cevherlink/play.png") + "\", \n" +
            "position: \"bottom-right\", \n" +
            "popup_height: \"50px\", \n" +
            "popup_width: \"300px\", \n" +
            "timer: \"20\" \n" +
          //CHANGE END!
          "} \n" +
        " ); \n" +
       "});";


      Page.ClientScript.RegisterStartupScript(Page.GetType(), "emailPopup", facebookSlideInScript, true);
    }

    private void DetermineAdDisplay()
    {
      //if (!Globals.CSWRSettings.EnableAdvertisements)
      //{
      //  panRightColumnAds.Visible = false;
      //}
      //else
      //{
      //  switch (SessionHandler.CurrentSportCode)
      //  {
      //    case "FOO":
      //      if (SportSetting.Football.ShowAffiliateAds)
      //      {
      //        RandomizeAdDisplay();
      //      }
      //      else
      //      {
      //        panGoogleAd.Visible = true;
      //      }
      //      break;
      //    case "RAC":
      //      if (SportSetting.Racing.ShowAffiliateAds)
      //      {
      //        RandomizeAdDisplay();
      //      }
      //      else
      //      {
      //        panGoogleAd.Visible = true;
      //      }
      //      break;
      //  }
      //}
    }

    public void HideAds()
    {
      panRColumn.Visible = false;
    }

    private void RandomizeAdDisplay()
    {
      //System.Random randNum = new System.Random();
      //int myRandomNumber = randNum.Next(2) + 1;

      //panGoogleAd.Visible = false;
      //panTShirtTrophies2Go.Visible = false;

      //switch (myRandomNumber)
      //{
      //  case 1:
      //    panGoogleAd.Visible = true;
      //    break;
      //  case 2:
      //    panTShirtTrophies2Go.Visible = true;
      //    break;
      //}
    }

    //private void HideAdColumn()
    //{
    //  Page.FindControl("rcolumn").
    //}

    private void ConfigureEnvironmentLinks()
    {

      if (Globals.CSWRSettings.ApplicationState == ApplicationState.Local.ToString().ToLower())
      {
        // turn off Google Analytics
        gaAnalytics.Visible = false;

        // forum menu items
        //hlHotTopics.NavigateUrl = "http://localhost/community/";
        //hlFootballWarRoom.NavigateUrl = "http://localhost/community/Football-War-Room-f2.aspx";

        //hlFeatureRequests.NavigateUrl = "http://localhost/community/Feature-Request-f8.aspx";
        //hlReportABug.NavigateUrl = "http://localhost/Report-a-Bug-f7.aspx";

        // blog menu items
        //hlBlogHome.NavigateUrl = "http://localhost/blog/default.aspx";

      }
      else
      {
        // turn off Google Analytics
        gaAnalytics.Visible = true;

        // forum menu items
        //hlHotTopics.NavigateUrl = "~/access/";
        //hlFootballWarRoom.NavigateUrl = "~/access/Football-War-Room-f2.aspx";
        //hlFeatureRequests.NavigateUrl = "~/access/Feature-Request-f8.aspx";
        //hlReportABug.NavigateUrl = "~/access/Report-a-Bug-f7.aspx";

        // blog menu items
        //hlBlogHome.NavigateUrl = "https://www.cheatsheetwarroom.com/blog/default.aspx";
      }
    }


    private void LoadSocialTags()
    {
      // Schema.org Google Plus Tags
      schemaOrgName.Content = (String.IsNullOrEmpty(this.SchemaOrgName)) ? this.Page.Title : this.SchemaOrgName;
      schemaOrgDescription.Content = (String.IsNullOrEmpty(this.SchemaOrgDescription)) ? this.Page.MetaDescription : this.SchemaOrgDescription;
      schemaOrgImage.Content = (String.IsNullOrEmpty(this.SchemaOrgImage)) ? _defaultSocialSharingImageUrl : this.SchemaOrgImage;
      
      // Twitter Tags
      twitterTitle.Content = (String.IsNullOrEmpty(this.TwitterTitle)) ? this.Page.Title : this.TwitterTitle;
      twitterDescription.Content = (String.IsNullOrEmpty(this.TwitterDescription)) ? this.Page.MetaDescription : this.TwitterDescription;
      twitterImage.Content = (String.IsNullOrEmpty(this.TwitterImage)) ? _defaultSocialSharingImageUrl : this.TwitterImage;
      twitterUrl.Content = (String.IsNullOrEmpty(this.TwitterUrl)) ? HttpContext.Current.Request.Url.AbsoluteUri : this.TwitterUrl; // may be fully-dynamic

      // Open Graph - Facebook Tags
      openGraphUrl.Content = (String.IsNullOrEmpty(this.OpenGraphUrl)) ? HttpContext.Current.Request.Url.AbsoluteUri : this.OpenGraphUrl; // may be fully-dynamic
      openGraphTitle.Content = (String.IsNullOrEmpty(this.OpenGraphTitle)) ? this.Page.Title : this.OpenGraphTitle;
      openGraphDescription.Content = (String.IsNullOrEmpty(this.OpenGraphDescription)) ? this.Page.MetaDescription : this.OpenGraphDescription;
      openGraphImage.Content = (String.IsNullOrEmpty(this.OpenGraphImage)) ? _defaultSocialSharingImageUrl : this.OpenGraphImage; 

    
    }


  }

}