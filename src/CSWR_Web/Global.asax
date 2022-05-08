<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Web.Http" %>
<%@ Import Namespace="BP.CheatSheetWarRoom" %>
<%@ Import Namespace="BP.CheatSheetWarRoom.BLL.Sheets" %>

<script runat="server">

  private static Dictionary<string, string> globalRedirect = new Dictionary<string, string>();

  /// <summary>
  /// Code that runs on application startups 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  void Application_Start(object sender, EventArgs e)
  {
    // look for the text file which contains all of our redirects.  we read this on start-up so that it
    // is only read once
    if (System.IO.File.Exists(Server.MapPath("~/TextFiles/global-redirects.txt")))
    {
      globalRedirect.Clear();
      System.IO.StreamReader input = new System.IO.StreamReader(Server.MapPath("~/TextFiles/global-redirects.txt"));
      string inputLine = "";
      string[] values = null;
      string oldUrl = "";
      string newUrl = "";
      while ((inputLine = input.ReadLine()) != null)
      {
        values = inputLine.Split(',');
        if (values.Length >= 2)
        {
          oldUrl = values[0].Trim('"').ToLower();
          newUrl = values[1].Trim('"').ToLower();
          if (!string.IsNullOrEmpty(oldUrl) && !string.IsNullOrEmpty(newUrl) && !globalRedirect.ContainsKey(oldUrl))
          {
            globalRedirect.Add(oldUrl, newUrl);
          }
        }
      }
      input.Close();


    }

    //WebApiConfig.Register(GlobalConfiguration.Configuration);

    //RouteTable.Routes.MapHttpRoute(
    //         name: "MyAPI",
    //         routeTemplate: "api/{controller}/{id}",
    //         defaults: new
    //         {
    //           id = System.Web.Http.RouteParameter.Optional
    //         }
    //     );

    // set up routes
    RegisterRoutes(RouteTable.Routes);

  }


  void RegisterRoutes(RouteCollection routes)
  {
    //Register a route for Products /{ ProductName}
    //routes.MapPageRoute(
    //   "User Graded Sheets",           // Route name
    //   "fantasy-football/nfl/accuracy/users/user/{Username}/{SeasonCode}/{PositionCode}", // Route URL
    //   "~/fantasy-football/nfl/accuracy/users/user/positional-sheet.aspx"      // Web page to handle route
    //);

    //Register a route for Products /{ ProductName}
    routes.MapPageRoute(
       "CheatSheets",           // Route name
       "api/cheatsheet/{id}", // Route URL
       "~/widget/htmlgenerator.aspx"      // Web page to handle route
    );
  }


  void Application_End(object sender, EventArgs e)
  {
    //  Code that runs on application shutdown

  }

  void Application_Error(object sender, EventArgs e)
  {
    // Code that runs when an unhandled error occurs

  }

  void Session_Start(object sender, EventArgs e)
  {
    if (this.User.Identity.IsAuthenticated)
    {
      MembershipUser loggedUser = Membership.GetUser(this.User.Identity.Name);
      if (loggedUser != null)
      {
        UserSession.LogUserSession(new Guid(loggedUser.ProviderUserKey.ToString()));
      }
    }
  }

  void Session_End(object sender, EventArgs e)
  {
    // Code that runs when a session ends. 
    // Note: The Session_End event is raised only when the sessionstate mode
    // is set to InProc in the Web.config file. If session mode is set to StateServer 
    // or SQLServer, the event is not raised.

  }

  void Application_BeginRequest(object sender, EventArgs e)
  {
    if (Globals.CSWRSettings.ApplicationState == ApplicationState.Prod.ToString().ToLower())
    {

      // set up the www as the preferred site to avoid duplicate content
      //if (!Request.Url.Host.ToLower().ToString().StartsWith("www."))
      //{
      //  Response.Clear();
      //  Response.Status = "301 Moved Permanently";
      //  Response.AddHeader("Location", "https://www.cheatsheetwarroom.com" + Request.Url.PathAndQuery);
      //  Response.End();
      //}

      // peel any querystring parameteres off the absoluteui and make everything lowercase
      string absoluteUriWithoutQueryStringParameters = String.Empty;
      if (Request.Url.AbsoluteUri.IndexOf("?") != -1)
      {
        absoluteUriWithoutQueryStringParameters = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf("?")).ToLower();
      }
      else
      {
        absoluteUriWithoutQueryStringParameters = Request.Url.AbsoluteUri;
      }

      // starting a fantasy football league
      if (absoluteUriWithoutQueryStringParameters == "https://www.cheatsheetwarroom.com/fantasy-football/articles/complete-guide-to-starting-a-league/10/summary.aspx")
      {
        Response.Clear();
        Response.Status = "301 Moved Permanently";
        Response.AddHeader("Location", "https://www.cheatsheetwarroom.com/blog/post/starting-a-fantasy-football-league.aspx");
        Response.End();
      }

      // league configuration
      if (absoluteUriWithoutQueryStringParameters == "https://www.cheatsheetwarroom.com/fantasy-football/articles/complete-guide-to-starting-a-league/7/league-configuration.aspx")
      {
        Response.Clear();
        Response.Status = "301 Moved Permanently";
        Response.AddHeader("Location", "https://www.cheatsheetwarroom.com/blog/fantasy-football/leagues/best-settings");
        Response.End();
      }

      // prize ideas
      if (absoluteUriWithoutQueryStringParameters == "https://www.cheatsheetwarroom.com/fantasy-football/articles/complete-guide-to-starting-a-league/9/establish-prizes.aspx")
      {
        Response.Clear();
        Response.Status = "301 Moved Permanently";
        Response.AddHeader("Location", "https://www.cheatsheetwarroom.com/blog/fantasy-football/prizes");
        Response.End();
      }

      // cheat sheet creation
      if (absoluteUriWithoutQueryStringParameters == "https://www.cheatsheetwarroom.com/fantasy-football/cheat-sheet-creation.aspx")
      {
        Response.Clear();
        Response.Status = "301 Moved Permanently";
        Response.AddHeader("Location", "https://www.cheatsheetwarroom.com/fantasy-football/nfl/create/custom-sheet.aspx");
        Response.End();
      }

      // standard scoring system
      if (absoluteUriWithoutQueryStringParameters == "https://www.cheatsheetwarroom.com/fantasy-football/standard-scoring-system.aspx")
      {
        Response.Clear();
        Response.Status = "301 Moved Permanently";
        Response.AddHeader("Location", "https://www.cheatsheetwarroom.com/blog/fantasy-football/leagues/scoring-systems");
        Response.End();
      }

      // standard scoring system
      if (absoluteUriWithoutQueryStringParameters == "https://www.cheatsheetwarroom.com/fantasy-football/standard-scoring-system.aspx")
      {
        Response.Clear();
        Response.Status = "301 Moved Permanently";
        Response.AddHeader("Location", "https://www.cheatsheetwarroom.com/blog/fantasy-football/leagues/scoring-systems");
        Response.End();
      }

      // player rankings
      if (absoluteUriWithoutQueryStringParameters == "https://www.cheatsheetwarroom.com/fantasy-football/nfl-2009/wide-receiver-rankings.aspx")
      {
        Response.Clear();
        Response.Status = "301 Moved Permanently";
        Response.AddHeader("Location", "https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/rankings/offense/wide-receivers.aspx");
        Response.End();
      }

      // login
      //if (absoluteUriWithoutQueryStringParameters == "~/access/login.aspx")
      //{
      //    Response.Clear();
      //    Response.Status = "301 Moved Permanently";
      //    Response.AddHeader("Location", "~/access/login.aspx");
      //    Response.End();
      //}
      // login
      //if (absoluteUriWithoutQueryStringParameters == "~/access/register.aspx")
      //{
      //    Response.Clear();
      //    Response.Status = "301 Moved Permanently";
      //    Response.AddHeader("Location", "~/access/register.aspx");
      //    Response.End();
      //}



    }


    // use the redirects in our global-redirect.txt file to do 301, only urls within the same virtual application will work here
    //string url = Request.RawUrl.ToLower();
    string url = Request.Url.ToString().ToLower();
    //if (!url.Contains("http"))
    //{
    //    url = Request.Url.ToString();
    //}

    if (!globalRedirect.ContainsKey(url) && url.IndexOf("?") > 0)
    {
      url = url.Substring(0, url.IndexOf("?")).ToLower();
    }
    if (globalRedirect.ContainsKey(url))
    {
      string newUrl = String.Empty;
      //try
      //{
      //    newUrl = VirtualPathUtility.ToAbsolute(globalRedirect[url]);
      //}
      //catch
      //{
      //    newUrl = globalRedirect[url];
      //}
      newUrl = globalRedirect[url];

      try
      {
        // Create the 301 Redirect  
        Response.Clear();
        Response.Status = "301 Moved Permanently";
        Response.AddHeader("Location", newUrl);
        Response.End();
        return;
      }
      catch (Exception ex)
      {
        string err = ex.Message;
      }
    }
  }
</script>
