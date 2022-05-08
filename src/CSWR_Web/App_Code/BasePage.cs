using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for BasePage
/// </summary>
/// 
namespace BP.CheatSheetWarRoom.UI  {
  
  public class BasePage : Page    {


    /// <summary>
    /// Allows us to set the canonical URL tag from the page directive, rather than passing it up through several master pages or putting the
    /// code to insert it in the code-behind of every page
    /// </summary>
    public string CanonicalUrl { get; set; }


    /// <summary>
    /// Allows us to set the meta robots tag from page directive, rather than passing it up through several master pages or putting the code
    /// to insert it in the code-behind of every relevant page
    /// </summary>
    public string MetaRobotsText { get; set; }


    /// <summary>
    /// Allows us to set the meta robots tag from page directive, rather than passing it up through several master pages or putting the code
    /// to insert it in the code-behind of every relevant page
    /// </summary>
    private string _useAuthorship = "false";
    public string UseAuthorship
    {
      get
      {
        return _useAuthorship;
      }
      set
      {
        _useAuthorship = value;
      }
    }




    public BasePage()
    {
      Init += new EventHandler(BasePage_Init);
    }

    void BasePage_Init(object sender, EventArgs e)
    {

      // if we want the application to be online, but we want to simulate that it is down to all users except me,
      // we need to redirect them to the maintence page
      if (Globals.CSWRSettings.SimulateDowntime)
      {
        bool administratorFound = false;
        foreach(string role in Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name))
        {
          if(role == "Administrator")  
          {
            administratorFound = true;
          }
        }
        if (!administratorFound)
        {
          Response.Redirect("~/access/app_offline.htm");
        }
      }


      // puts the description on its own line to it's easier to read in the HTML file
      LiteralControl newln0 = new LiteralControl(Environment.NewLine);
      LiteralControl newln1 = new LiteralControl(Environment.NewLine);
      if (!String.IsNullOrEmpty(MetaDescription))
      {
        HtmlMeta tag = new HtmlMeta();
        tag.Name = "description";
        tag.Content = MetaDescription;
        Header.Controls.AddAt(1, newln0);
        Header.Controls.AddAt(2, tag);
        Header.Controls.AddAt(3, newln1);
      }

      // the a canonical URL is listed as a page directive, add it to the page, otherwise don't add the tag
      if (!String.IsNullOrEmpty(this.CanonicalUrl))  
      {
        HtmlLink canonicalTag = new HtmlLink();
        canonicalTag.Href = this.CanonicalUrl;
        canonicalTag.Attributes["rel"] = "canonical";
        Header.Controls.AddAt(4, canonicalTag);
      }

      // the a canonical URL is listed as a page directive, add it to the page, otherwise don't add the tag
      if (!String.IsNullOrEmpty(this.MetaRobotsText))
      {
        HtmlMeta metaRobots = new HtmlMeta();
        metaRobots.Name = "ROBOTS";
        metaRobots.Content = this.MetaRobotsText;
        Header.Controls.AddAt(5, metaRobots);
      }

      // the a canonical URL is listed as a page directive, add it to the page, otherwise don't add the tag
      if (!String.IsNullOrEmpty(this.UseAuthorship))
      {
        bool useAuthorship = false;
        bool.TryParse(this.UseAuthorship, out useAuthorship);
        if (useAuthorship)
        {
          HtmlHead pageHeader = (HtmlHead)Page.Header;
          HtmlLink authorshipLink = new HtmlLink();
          authorshipLink.Attributes.Add("href", "https://plus.google.com/113268984320725042818");
          authorshipLink.Attributes.Add("rel", "author");
          pageHeader.Controls.Add(authorshipLink);
        }
      }

      
      //if (this.UseAuthorship)
      //{
      //  HtmlMeta metaRobots = new HtmlMeta();
      //  metaRobots.Name = "ROBOTS";
      //  metaRobots.Content = this.MetaRobotsText;
      //  Header.Controls.AddAt(5, metaRobots);
      //}

    }


    public string BaseUrl
    {
      get
      {
        string url = this.Request.ApplicationPath;
        if (url.EndsWith("/"))
          return url;
        else
          return url + "/";
      }
    }

    public string FullBaseUrl
    {
      get
      {
        return this.Request.Url.AbsoluteUri.Replace(
           this.Request.Url.PathAndQuery, "") + this.BaseUrl;
      }
    }

    private bool _refreshState;
    private bool _isRefresh;

    public bool IsRefresh
    {
      get
      {
        return _isRefresh;
      }
    }

    protected override void LoadViewState(object savedState)
    {
      object[] allStates = (object[])savedState;
      base.LoadViewState(allStates[0]);
      _refreshState = (bool)allStates[1];
      _isRefresh = _refreshState == SessionHandler.IsRefresh;
    }

    protected override object SaveViewState()
    {
      SessionHandler.IsRefresh = _refreshState;
      object[] allStates = new object[2];
      allStates[0] = base.SaveViewState();
      allStates[1] = !_refreshState;
      return allStates;
    }




  }
}

