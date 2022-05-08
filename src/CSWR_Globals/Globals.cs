using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for Globals
/// </summary>
/// 
namespace BP.CheatSheetWarRoom
{
  // indicates if the application resides on my local machine or in production
  public enum ApplicationState { Local, Prod }
  // indicates the type of ranking used on a page: CSWR rankings, ADP, or stat-based
  public enum CSWRRankingType { CSWRRank, ADP, PlayerStat }
  // the two ranking sources that I can use to create busts or sleepers
  public enum BustSleeperComparisonSource { CBS, ADP }
  // helps to tell a template of it is displaying busts or sleepers
  public enum DifferentialPlayerType { Sleeper, Bust }
  // these are different properties available for a cheat sheet item
  public enum CSIProperty { Sleeper, Bust, Injured }
  // these are different properties available for a cheat sheet
  public enum CSProperty { PPRLeague, AuctionDraft }
  // these are different properties available for a cheat sheet item
  public enum SSIProperty { Sleeper, Bust }
  // the 2 main types of sheets, 'Cheat Sheets', and 'Supplemental Sheets'
  public enum SheetType { CheatSheet, SuppSheet };
  // most sports have offensive and defensive players
  public enum SportSide { Offense, Defense };
  // these are the 4 stared types of messages given to the user
  public enum MessageType { ERROR, SUCCESS, WARNING, NONE, INSTRUCTIONS }
  // these are the sports that are currently supported
  public enum SupportedSport { FOO, RAC }
  // these are the offensive positions supported in fantasy football
  public enum FOOPositionsOffense { QB, RB, WR, TE, K, DF }
  // these are the general ways to can sort data, either ascending, or descending
  public enum SortDir { ASC, DESC }
  // these are the different types of users
  public enum UserCategory { MEMBER, VISITOR }
  // these are two types of stat files
  public enum StatImportType { WEEKLY, SEASONAL }
  // these are the player status enumerations
  public enum PlayerStatusCodes { RETIRD, THREWR, SWTEAM }



  public static class Globals
  {

    public const int TransactionErrorSentinel = 999;
    public const int TransactionRetryCount = 5;
    public const string FooString = "FOO";
    public const string RacString = "RAC";
    public const int DefaultSleeperBustDifferential = 10;
    public const int MinSleeperBustListingCount = 3;
    public const int FirstGradedSeason = 2013;

    // Declare a public delegate which defines the signature for handling a custom event
    public delegate void MessageEventHandler(object sender, MessageBoxEventArgs e);
    public class MessageBoxEventArgs : EventArgs
    {
      public MessageBoxEventArgs(string text, MessageType messageType)
      {
        this.text = text;
        this._messageType = messageType;
      }

      private string text = String.Empty;
      public string Text
      {
        get { return text; }
      }

      private MessageType _messageType;
      public MessageType MessageType
      {
        get { return _messageType; }
      }
    }


    // Declare a public delegate which defines the signature for handling a custom event
    public delegate void ExportEventHandler(object sender, ExportEventArgs e);
    public class ExportEventArgs : EventArgs
    {
      public ExportEventArgs()
      {
      }
    }


    // default size for supplemental rankings
    public const int SupplementRankingPageSize = 20;
    public const int CswrMaxPrintableSingleSheetRows = 40;

    /// <summary>
    /// The number of top ranked quarterbacks (by TFP) which will be considered when grading a user's cheat sheet
    /// </summary>
    public const int CswrMaxRankings_QB = 40;

    /// <summary>
    /// The number of top ranked running backs (by TFP) which will be considered when grading a user's cheat sheet
    /// </summary>
    public const int CswrMaxRankings_RB = 80;

    /// <summary>
    // The number of top ranked wide receivers (by TFP) which will be considered when grading a user's cheat sheet
    /// </summary>
    public const int CswrMaxRankings_WR = 80;

    /// <summary>
    // The number of top ranked tight ends (by TFP) which will be considered when grading a user's cheat sheet
    /// </summary>
    public const int CswrMaxRankings_TE = 40;

    /// <summary>
    // The number of top ranked kickers (by TFP) which will be considered when grading a user's cheat sheet
    /// </summary>
    public const int CswrMaxRankings_K = 40;

    /// <summary>
    // The number of top ranked defenses (by TFP) which will be considered when grading a user's cheat sheet
    /// </summary>
    public const int CswrMaxRankings_DF = 32;
    
    public const int CswrMaxPrintableWithRoster_QB = 42;
    public const int CswrMaxPrintableWithoutRoster_QB = 35;

    public const int CswrMaxPrintableWithRoster_RB = 50;
    public const int CswrMaxPrintableWithoutRoster_RB = 74;

    public const int CswrMaxPrintableWithRoster_WR = 50;
    public const int CswrMaxPrintableWithoutRoster_WR = 74;

    public const int CswrMaxPrintableWithRoster_TE = 42;
    public const int CswrMaxPrintableWithoutRoster_TE = 35;

    public const int CswrMaxPrintableWithRoster_K = 23;
    public const int CswrMaxPrintableWithoutRoster_K = 23;

    public const int CswrMaxPrintableWithRoster_DF = 23;
    public const int CswrMaxPrintableWithoutRoster_DF = 23;

    /// <summary>
    // The number of user cheat sheet items, ranked by a user, considered when grading their quarterbacks cheat sheet
    /// </summary>
    public const int SheetGradingSize_QB = 30;
    
    /// <summary>
    // The number of user cheat sheet items, ranked by a user, considered when grading their running backs cheat sheet
    /// </summary>
    public const int SheetGradingSize_RB = 50;
    
    /// <summary>
    // The number of user cheat sheet items, ranked by a user, considered when grading their wide receivers cheat sheet
    /// </summary>
    public const int SheetGradingSize_WR = 50;

    /// <summary>
    // The number of user cheat sheet items, ranked by a user, considered when grading their tight ends cheat sheet
    /// </summary>
    public const int SheetGradingSize_TE = 25;
    
    /// <summary>
    // The number of user cheat sheet items, ranked by a user, considered when grading their kickers cheat sheet
    /// </summary>
    public const int SheetGradingSize_K = 15;

    /// <summary>
    // The number of user cheat sheet items, ranked by a user, considered when grading their defenses cheat sheet
    /// </summary>
    public const int SheetGradingSize_DF = 15;

    /// <summary>
    /// The maximum items to show when displaying CSWR driver rankings.  For simplicity we make this
    /// the same as the number of drivers that will fit on a single, printable sheet
    /// </summary>
    public const int CswrMaxRankings_DR = 50;


    // number of weeks in an NFL season
    public const int NflWeeksInSeason = 17;

    // number of teams in the NFL
    public const int NflNumTeams = 32;

    // number of players CSWR ranks at each position
    public const int CSWRPositionalRankingSize = 100;

    public static string ThemeSelectorID = "";

    public static string BaseProdUrl = "https://www.cheatsheetwarroom.com";

    public readonly static CheatSheetWarRoomSection CSWRSettings = (CheatSheetWarRoomSection)WebConfigurationManager.GetSection("cheatSheetWarRoom");
    public readonly static ASPPlaygroundSection ForumSettings = (ASPPlaygroundSection)WebConfigurationManager.GetSection("aspPlayground");
    public readonly static BlogEngineSection BlogSettings = (BlogEngineSection)WebConfigurationManager.GetSection("blogEngine");

    // limit the number of calculations to one per day (turn off temporarily to debug ADP calcuations)
    public const bool LimitADPCalcuations = true;

    /// <summary>
    /// This method returns the number of players we load onto a sheet by default, based on the position
    /// provided.  This method acts as a bridge between the web.config and the code which needs the player count
    /// at runtime.  Since there are cases where we don't know the position being processed, we can use thie method to 
    /// return the player count during runtime.
    /// </summary>
    /// <param name="positionCode"></param>
    /// <returns></returns>
    public static int GetDefaultPlayersPerSheet(List<string> positionCodes)
    {
      if (positionCodes.Count > 1)
      {
        int playersPerSheet = 100 + ((positionCodes.Count - 1) * 20);
        return playersPerSheet;
      }
      else if (positionCodes.Count == 1)
      {
        int playersPerSheet = 0;

        switch (positionCodes[0])
        {
          case "QB":
            playersPerSheet = Globals.CSWRSettings.Sheets.DefaultQBsPerSheet;
            break;
          case "RB":
            playersPerSheet = Globals.CSWRSettings.Sheets.DefaultRBsPerSheet;
            break;
          case "TE":
            playersPerSheet = Globals.CSWRSettings.Sheets.DefaultTEsPerSheet;
            break;
          case "K":
            playersPerSheet = Globals.CSWRSettings.Sheets.DefaultKsPerSheet;
            break;
          case "WR":
            playersPerSheet = Globals.CSWRSettings.Sheets.DefaultWRsPerSheet;
            break;
          case "DR":
            playersPerSheet = Globals.CSWRSettings.Sheets.DefaultDRsPerSheet;
            break;
          default:
            playersPerSheet = Globals.CSWRSettings.Sheets.DefaultDEFsPerSheet;
            break;
        }
        return playersPerSheet;
      }
      else
      {
        return 0;
      }
    }


    public static List<string> GetDefaultStatCodes(string positionCode)
    {
      List<string> returnStats = new List<string>();
      string[] statStrings;

      switch (positionCode)
      {
        case "QB":
          statStrings = Globals.CSWRSettings.Sheets.DefaultQBStatCodes.Split(',');
          foreach (string stat in statStrings)
          {
            returnStats.Add(stat);
          }
          break;
        case "RB":
          statStrings = Globals.CSWRSettings.Sheets.DefaultRBStatCodes.Split(',');
          foreach (string stat in statStrings)
          {
            returnStats.Add(stat);
          }
          break;
        case "WR":
          statStrings = Globals.CSWRSettings.Sheets.DefaultWRStatCodes.Split(',');
          foreach (string stat in statStrings)
          {
            returnStats.Add(stat);
          }
          break;
        case "TE":
          statStrings = Globals.CSWRSettings.Sheets.DefaultTEStatCodes.Split(',');
          foreach (string stat in statStrings)
          {
            returnStats.Add(stat);
          }
          break;
        case "K":
          statStrings = Globals.CSWRSettings.Sheets.DefaultKStatCodes.Split(',');
          foreach (string stat in statStrings)
          {
            returnStats.Add(stat);
          }
          break;
        case "DF":
          statStrings = Globals.CSWRSettings.Sheets.DefaultDFStatCodes.Split(',');
          foreach (string stat in statStrings)
          {
            returnStats.Add(stat);
          }
          break;
        case "DR":
          statStrings = Globals.CSWRSettings.Sheets.DefaultDRStatCodes.Split(',');
          foreach (string stat in statStrings)
          {
            returnStats.Add(stat);
          }
          break;
      }


      return returnStats;
    }


    public static bool isPageInSitemapTree(string urlSubstringMatch)
    {
      Regex footballReg = new Regex(urlSubstringMatch);
      if (footballReg.Match(HttpContext.Current.Request.Url.AbsolutePath.ToLower()).Success)
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
