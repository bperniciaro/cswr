using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom  {

  public static class Helpers  {
    /// <summary>
    /// Returns an array with the names of all local themes
    /// </summary>
    /// 

    // A collection of all teams in the NFL
    private static string[] _teams = new string[] { 
     "Arizona Cardinals", "Atlanta Falcons", "Baltimore Ravens", "Buffalo Bills", "Carolina Panthers", "Chicago Bears", "Cincinatti Bengals", 
     "Cleveland Browns", "Dallas Cowboys", "Denver Broncos", "Detroit Lions", "Houston Texans",
     "Green Bay Packers", "Indianapolis Colts", "Jacksonville Jaguars", "Kansas City Chiefs", "Miami Dolphins",
     "Minnesota Vikings", "New England Patriots", "New Orleans Saints", "New York Giants", "New York Jets", 
     "Las Vegas Raiders", "Philidelphia Eagles", "Pittsburg Steelers", "San Diego Charger", "San Francisco 49ers",
     "Seattle Seahawks", "St. Louis Rams", "Tampa Bay Buccaneers", "Tennessee Titans", "Washington Redskins"};

    // A public method which returns a collecion of string which represent NFL teams
    public static StringCollection GetTeams()
    {
      StringCollection teams = new StringCollection();
      teams.AddRange(_teams);
      return teams;
    }

    // This static method returns a collection of the current themes available, based on the files
    // contained in the APP_Themes folder
    public static string[] GetThemes()  {
      if (HttpContext.Current.Cache["SiteThemes"] != null)  {
        return (string[])HttpContext.Current.Cache["SiteThemes"];
      }
      else  {
        string themesDirPath = HttpContext.Current.Server.MapPath("~/App_Themes");
        // get the array of themes folders under /app_themes
        string[] themes = Directory.GetDirectories(themesDirPath);
        for (int i = 0; i <= themes.Length - 1; i++)  {
          themes[i] = Path.GetFileName(themes[i]);
        }
        // cache the array with a dependency to the folder
        CacheDependency dep = new CacheDependency(themesDirPath);
        HttpContext.Current.Cache.Insert("SiteThemes", themes, dep);
        return themes;
      }
    }

    /// <summary>
    /// Converts the input plain-text to HTML version, replacing carriage returns
    /// and spaces with <br /> and &nbsp;
    /// </summary>
    public static string ConvertToHtml(string content)
    {
      content = HttpUtility.HtmlEncode(content);
      content = content.Replace("  ", "&nbsp;&nbsp;").Replace(
         "\t", "&nbsp;&nbsp;&nbsp;").Replace("\n", "<br>");
      return content;
    }

    public static List<Stat> GetDefaultStatCodes(string positionCode)
    {
      List<Stat> defaultStatCodes = new List<Stat>();
      List<string> defaultStats = new List<string>();

      defaultStats = Globals.GetDefaultStatCodes(positionCode);
      for (int i = 0; i < defaultStats.Count; i++)
      {
        defaultStatCodes.Add(new Stat(defaultStats[i], String.Empty, String.Empty, String.Empty));
      }

      return defaultStatCodes;
    }

    public static void LogoutUser()
    {
      MembershipUser user = Membership.GetUser(false);

      FormsAuthentication.SignOut();

      // in order to make the logged-in user appear logged-out after calling SignOut, we have to subtract the UserIsOnlineTimeWindow from
      // the LastActivityDate, otherwise the user who just logged out will appear as logged-in for the 'UserIsOnlineTimeWindow'.
      if (user != null)
      {
        user.LastActivityDate = DateTime.Now.AddMinutes(-(Membership.UserIsOnlineTimeWindow + 1));
        Membership.UpdateUser(user);
      }
    }

    public static string GetRelativePositionalRankingsPage(string positionCode)
    {
      string relativePageUrl = String.Empty;

      switch (positionCode)
      {
        case "QB":
          relativePageUrl = "~/fantasy-football/nfl/free/rankings/offense/quarterbacks.aspx";
          break;
        case "RB":
          relativePageUrl = "~/fantasy-football/nfl/free/rankings/offense/running-backs.aspx";
          break;
        case "WR":
          relativePageUrl = "~/fantasy-football/nfl/free/rankings/offense/wide-receivers.aspx";
          break;
        case "TE":
          relativePageUrl = "~/fantasy-football/nfl/free/rankings/offense/tight-ends.aspx";
          break;
        case "K":
          relativePageUrl = "~/fantasy-football/nfl/free/rankings/offense/kickers.aspx";
          break;
        case "DF":
          relativePageUrl = "~/fantasy-football/nfl/free/rankings/offense/defenses.aspx";
          break;
      }
      return relativePageUrl;
    }



    /// <summary>
    /// We must programmatically decide which stylesheets to load, the uncompressed ones or the production-ready, compressed file.  We 
    /// could do this declaratively, but then the dynamic loading of MetaDescriptions fails because there are <% %> tags.  So
    /// we have to load these resources in the code-behind
    /// </summary>
    public static void AddStyleSheetReferences(Page targetPage, bool responsive = false)
    {

      if(!responsive)
      {
        if ((Globals.CSWRSettings.ApplicationState == ApplicationState.Local.ToString().ToLower()) && (Globals.CSWRSettings.ForceMinified == false))
        {
          // CDNs 
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" href=\"https://fonts.googleapis.com/css?family=Arvo:regular,italic,bold,bolditalic\" />"));

          // 3rd Party Styles
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/font-awesome-4.2.0/css/font-awesome.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/jquery.qtip.min.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/jquery-ui-1.11.4.custom.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/jquery.dataTables.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/flat-icon/flaticon.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/bootstrap/css/bootstrap.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/bootstrap/css/bootstrap-theme.min.css") + "\" />"));

          //targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/fb-traffic-pop.css") + "\" />"));

          // CSWR Styles
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/cheatsheets.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/nflteamstyles.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/usercontrols.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/pages.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/main.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/newpages.css") + "\" />"));
        }
        else
        {
          // We use CDNs whenever possible in PROD
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" href=\"https://fonts.googleapis.com/css?family=Arvo:regular,italic,bold,bolditalic\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.2.0/css/font-awesome.min.css\" />"));

          // Compressed CSSs 
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/prod/" + Globals.CSWRSettings.Version + ".min.css") + "\" />"));


        }
      }
      else
      {
        if (((Globals.CSWRSettings.ApplicationState == ApplicationState.Local.ToString().ToLower()) && (Globals.CSWRSettings.ForceMinified == false)) || Globals.CSWRSettings.ForceUnMinified)
        {
          // CDNs 
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" href=\"https://fonts.googleapis.com/css?family=Arvo:regular,italic,bold,bolditalic\" />"));

          // 3rd Party Styles
          //targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/font-awesome-4.2.0/css/font-awesome.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/responsive/font-awesome.min.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/jquery.qtip.min.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/jquery-ui-1.11.4.custom.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/jquery.dataTables.css") + "\" />"));
          //targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/flat-icon/flaticon.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/responsive/bootstrap.min.css") + "\" />"));
          //targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/bootstrap/css/bootstrap-theme.min.css") + "\" />"));

          //targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/fb-traffic-pop.css") + "\" />"));

          // CSWR Styles
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/responsive/style.css") + "\" />"));

          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/newpages.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/cheatsheets.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/nflteamstyles.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/pages.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/usercontrols.css") + "\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/local/main.css") + "\" />"));
        }
        else
        {
          // We use CDNs whenever possible in PROD
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" href=\"https://fonts.googleapis.com/css?family=Arvo:regular,italic,bold,bolditalic\" />"));
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.2.0/css/font-awesome.min.css\" />"));

          // Compressed CSSs 
          targetPage.Header.Controls.Add(new LiteralControl("<link rel=\"stylesheet\" media=\"screen\" href=\"" + targetPage.ResolveUrl("~/styles/prod/" + Globals.CSWRSettings.Version + ".min.css") + "\" />"));


        }

      }
    }

    public static void AddScriptReferences(Page targetPage, bool responsive = false)
    {
      if(!responsive)
      {
        if ((Globals.CSWRSettings.ApplicationState == ApplicationState.Local.ToString().ToLower()) && (Globals.CSWRSettings.ForceMinified == false))
        {
          // Local versions of CDN-available scripts, in case I lose a connection while debugging locally
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "JQuery", targetPage.ResolveUrl("~/scripts/local/jquery-1.11.0.min.js"));
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "JQueryUI", targetPage.ResolveUrl("~/scripts/local/jquery-ui-1.11.4.min.js"));
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "JQueryMigrate", targetPage.ResolveUrl("~/scripts/local/jquery-migrate-1.2.1.js"));

          // Local versions of non-CDN-available scripts, these are normally referenced via the PROD-ready minified/compressed file
          // QTip
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "QTip", targetPage.ResolveUrl("~/scripts/local/jquery.qtip-2.0.0.min.js"));
          // TouchPunch
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "TouchPunch", targetPage.ResolveUrl("~/scripts/local/jquery.ui.touch-punch-0.2.3.min.js"));
          // NoteEditor (our custom scripts)
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "NoteEditor", targetPage.ResolveUrl("~/scripts/local/noteeditor.js"));
          // Bootstrap
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "Bootstrap", targetPage.ResolveUrl("~/scripts/local/bootstrap.min.js"));
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "DataTables", targetPage.ResolveUrl("~/scripts/local/jquery.dataTables.1.10.4.min.js"));

          // New Captcha Control
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "Recaptcha", "https://www.google.com/recaptcha/api.js");
        }
        else
        {
          // CDNs 
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "JQuery", "https://code.jquery.com/jquery-1.11.0.min.js");
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "JQueryUI", "https://code.jquery.com/ui/1.10.4/jquery-ui.min.js");
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "JQueryMigrate", "https://code.jquery.com/jquery-migrate-1.2.1.js");  // necessary for qQtip to function properly
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "Bootstrap", "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js");  // necessary for qQtip to function properly
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "DataTables", "https://cdn.datatables.net/1.10.7/js/jquery.dataTables.min.js");

          // All remaining JavaScript files (minified), minus scripts available over CDN
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "Min", targetPage.ResolveUrl("~/scripts/prod/" + Globals.CSWRSettings.Version + ".min.js"));
        }
      }
      else
      {
        if (((Globals.CSWRSettings.ApplicationState == ApplicationState.Local.ToString().ToLower()) && (Globals.CSWRSettings.ForceMinified == false)) || Globals.CSWRSettings.ForceUnMinified)
        {
          // Local versions of CDN-available scripts, in case I lose a connection while debugging locally
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "JQuery", targetPage.ResolveUrl("~/scripts/local/jquery-3.4.1.min.js"));
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "JQueryUI", targetPage.ResolveUrl("~/scripts/local/jquery-ui-1.12.1.min.js"));
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "JQueryMigrate", targetPage.ResolveUrl("~/scripts/local/jquery-migrate-1.2.1.js"));

          // Local versions of non-CDN-available scripts, these are normally referenced via the PROD-ready minified/compressed file
          // QTip
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "QTip", targetPage.ResolveUrl("~/scripts/local/jquery.qtip-2.0.0.min.js"));
          // TouchPunch
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "TouchPunch", targetPage.ResolveUrl("~/scripts/local/jquery.ui.touch-punch-0.2.3.min.js"));


          // NoteEditor (our custom scripts)
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "NoteEditor", targetPage.ResolveUrl("~/scripts/local/noteeditor.js"));
          // Bootstrap
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "Bootstrap", targetPage.ResolveUrl("~/scripts/local/bootstrap.min.js"));
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "DataTables", targetPage.ResolveUrl("~/scripts/local/jquery.dataTables.1.10.4.min.js"));

          // New Captcha Control
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "Recaptcha", "https://www.google.com/recaptcha/api.js");
        }
        else
        {
          // CDNs 
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "JQuery", "https://code.jquery.com/jquery-3.4.1.min.js");
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "JQueryUI", "https://code.jquery.com/ui/1.12.1/jquery-ui.min.js");
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "JQueryMigrate", "https://code.jquery.com/jquery-migrate-1.2.1.js");  // necessary for qQtip to function properly
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "Bootstrap", "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js");  // necessary for qQtip to function properly
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "DataTables", "https://cdn.datatables.net/1.10.7/js/jquery.dataTables.min.js");

          // All remaining JavaScript files (minified), minus scripts available over CDN
          ScriptManager.RegisterClientScriptInclude(targetPage, typeof(Page), "Min", targetPage.ResolveUrl("~/scripts/prod/" + Globals.CSWRSettings.Version + ".min.js"));
        }
      }


      // Ads 
      string scriptstr = "\r\n<script type='text/javascript' async='async' data-noptimize='1' data-cfasync='false' src='//scripts.mediavine.com/tags/cheatsheet-war-room.js'></script>\r\n";
            targetPage.ClientScript.RegisterClientScriptBlock(typeof(Page), "defaultslideshow", scriptstr);
    }

    /// <summary>
    /// Loads a string which identifies those weeks for which weekly stats have been processed.
    /// </summary>
    public static string DetermineProcessedStatWeeks(string statSeasonCode)
    {
      string importedWeeksString = String.Empty;

      for (int i = 1; i <= Globals.NflWeeksInSeason; i++)
      {
        if (SportSeasonPlayerWeeklyStat.GetSportSeasonPlayerWeeklyStatCount("FOO", statSeasonCode, i) > 0)
        {
          if (i > 1)
          {
            importedWeeksString += ", ";
          }
          importedWeeksString += i.ToString();
        }
      }
      if (importedWeeksString == String.Empty)
      {
        importedWeeksString = "None";
      }
      return importedWeeksString;
    }



    /// <summary>
    // Try to figure out which week we need to import based on if any weekly stats already exist for a particular week.
    /// </summary>
    /// <returns></returns>
    public static int GetNextStatWeek()
    {
      int nextWeekToProcess = 0;

      for (int week = 1; week <= Globals.NflWeeksInSeason; week++)
      {
        if (SportSeasonPlayerWeeklyStat.GetSportSeasonPlayerWeeklyStatCount("FOO", SportSeason.GetCurrentSportSeason("FOO").SeasonCode, week) == 0)
        {
          nextWeekToProcess = week;
          break;
        }
      }
      return nextWeekToProcess;
    }


    /// <summary>
    /// Returns the file location of a weekly stat import file based on the 'side of the ball' being considered
    /// and the week selected in the dropdownbox
    /// </summary>
    /// <param name="sportSide"></param>
    /// <returns></returns>
    public static string GetFOOStatFileLocation(SportSide sportSide, StatImportType importType, int targetWeek, string statSeason)
    {
      string endOfFileName = String.Empty;
      string fileName = String.Empty;

      // offense vs defense filesnames are named differently
      if (sportSide == SportSide.Offense)
      {
        if (importType == StatImportType.WEEKLY)
        {
          endOfFileName = "co.csv";
        }
        else
        {
          endOfFileName = "coytd.csv";
        }
      }
      else
      {
        if (importType == StatImportType.WEEKLY)
        {
          endOfFileName = "ct.csv";
        }
        else
        {
          endOfFileName = "ctytd.csv";
        }
      }

      // build the full filename
      if (targetWeek < 10)
      {
        fileName = statSeason.Substring(2, 2) + "wk0" + targetWeek.ToString() + endOfFileName;
      }
      else
      {
        fileName = statSeason.Substring(2, 2) + "wk" + targetWeek.ToString() + endOfFileName;
      }

      // determine the directory path
      string baseDirectory = HttpContext.Current.Server.MapPath("~/TextFiles");
      string dirExtension = "\\Stats\\NFLStats\\" + statSeason + "\\" + fileName;

      // build the entire file location based on source directory and directory extension
      string fileLocation = baseDirectory + dirExtension;

      return fileLocation;
    }

    /// <summary>
    /// Returns a collection of SportSeasons for which some stat has been recorded, limited by
    /// the specified count.
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCount"></param>
    /// <returns></returns>
    //public static List<SportSeason> GetLatestStatSeasons(string sportCode, int seasonCount)
    //{
    //  List<SportSeason> statSeasons = SportSeason.GetSportStatSeasons(sportCode);
    //  if (statSeasons != null)
    //  {
    //    return statSeasons.OrderByDescending(x => x.SeasonCode).Take(seasonCount).ToList();
    //  }
    //  return null;
    //}


    public static bool DoesFOOPositionSupportPPR(string positionCode)
    {
      if ((positionCode == FOOPositionsOffense.RB.ToString()) ||
          (positionCode == FOOPositionsOffense.WR.ToString()) ||
          (positionCode == FOOPositionsOffense.TE.ToString()))
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="positionCode"></param>
    /// <returns></returns>
    public static int GetMaxRankPlayersConsideredBySportPosition(string sportCode, string positionCode)
    {
      int maxItems = 0;

      switch (sportCode)
      {
        case FOO.FOOString:
          if (positionCode == FOOPositionsOffense.QB.ToString())
          {
            maxItems = Globals.CswrMaxRankings_QB;
          }
          else if (positionCode == FOOPositionsOffense.RB.ToString())
          {
            maxItems = Globals.CswrMaxRankings_RB;
          }
          else if (positionCode == FOOPositionsOffense.WR.ToString())
          {
            maxItems = Globals.CswrMaxRankings_WR;
          }
          else if (positionCode == FOOPositionsOffense.TE.ToString())
          {
            maxItems = Globals.CswrMaxRankings_TE;
          }
          else if (positionCode == FOOPositionsOffense.K.ToString())
          {
            maxItems = Globals.CswrMaxRankings_K;
          }
          else if (positionCode == FOOPositionsOffense.DF.ToString())
          {
            maxItems = Globals.CswrMaxRankings_DF;
          }
          break;
        case Globals.RacString:
          if (positionCode == "DR")
          {
            maxItems = Globals.CswrMaxRankings_DR;
          }
          break;
      }
      return maxItems;
    }


    public static FOOPositionsOffense ConvertPositionCodeToConstant(string positionCode)
    {
      switch (positionCode)
      {
        case "QB":
          return FOOPositionsOffense.QB;
          //break;
        case "RB":
          return FOOPositionsOffense.RB;
          //break;
        case "WB":
          return FOOPositionsOffense.WR;
          //break;
        case "TE":
          return FOOPositionsOffense.TE;
          //break;
        case "K":
          return FOOPositionsOffense.K;
          //break;
        case "DF":
          return FOOPositionsOffense.DF;
          //break;
        default:
          return FOOPositionsOffense.QB;
      }
    }


    /// <summary>
    /// Returns the number of items that are graded within a user's cheat sheet
    /// </summary>
    /// <param name="sportCode">The sport on which the cheat sheet is based</param>
    /// <param name="positionCode">The position on which the cheat sheet is based</param>
    /// <returns></returns>
    public static int GetUserSheetGradedItemsBySportPosition(string sportCode, string positionCode)
    {
      int maxItems = 0;

      switch (sportCode)
      {
        case FOO.FOOString:
          if (positionCode == FOOPositionsOffense.QB.ToString())
          {
            maxItems = Globals.SheetGradingSize_QB;
          }
          else if (positionCode == FOOPositionsOffense.RB.ToString())
          {
            maxItems = Globals.SheetGradingSize_RB;
          }
          else if (positionCode == FOOPositionsOffense.WR.ToString())
          {
            maxItems = Globals.SheetGradingSize_WR;
          }
          else if (positionCode == FOOPositionsOffense.TE.ToString())
          {
            maxItems = Globals.SheetGradingSize_TE;
          }
          else if (positionCode == FOOPositionsOffense.K.ToString())
          {
            maxItems = Globals.SheetGradingSize_K;
          }
          else if (positionCode == FOOPositionsOffense.DF.ToString())
          {
            maxItems = Globals.SheetGradingSize_DF;
          }
          break;
        case Globals.RacString:
          if (positionCode == "DR")
          {
            //maxItems = Globals.CswrMaxRankings_DR;
          }
          break;
      }
      return maxItems;
    }

    /// <summary>
    /// This method will determine if we are in the middle of the season specified in the parameter list.  In order for it to be
    /// the "Middle" of a season, the following criteria must be met:  1) this season has to have started (based on the DB column in SportSeasons), 
    /// 2) the season cannot yet be over and 3) some stats have to be have been loaded for the season in question.  This method is mostly
    /// useful during football season
    /// </summary>
    /// <param name="seasonCode"></param>
    /// <returns></returns>
    public static bool IsMiddleOfSeason(string sportCode)
    {
      SportSeason currentSportSeason = SportSeason.GetCurrentSportSeason(FOO.FOOString);

      if ((currentSportSeason.SeasonStarted) && (!currentSportSeason.SeasonEnded) && (currentSportSeason.SomeStatsLoaded))
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Returns true of bye weeks are loaded for the current season
    /// </summary>
    /// <returns></returns>
    public static bool ByeWeeksLoaded()
    {
      return (ByeWeek.GetByeWeeks(FOO.CurrentSeason, FOO.FOOString).Count > 0);
    }

    /// <summary>
    /// Returns true if PPR scoring is relevant to the specified position
    /// </summary>
    /// <returns></returns>
    public static bool IsPPRRelevant(string positionCode)
    {
      if ((positionCode == FOOPositionsOffense.RB.ToString()) ||
        (positionCode == FOOPositionsOffense.WR.ToString()) ||
        (positionCode == FOOPositionsOffense.TE.ToString()))
      {
        return true;
      }
      else
      {
        return false;
      }
    }


  }

}