using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class RandomCheatSheet : System.Web.UI.Page
  {

    private int _quarterbackCounter = 0;
    private int _runningBackCounter = 0;
    private int _wideReceiverCounter = 0;
    private int _tightEndCounter = 0;
    private int _kickerCounter = 0;
    private int _defenseCounter = 0;

    private int _groupSize = 0;
    private int _lockCount = 0;
    private bool _randomizeByes = false;
    private CSWRRankingType _rankType;
    private Dictionary<string, int> _teamSpecificRandomizedByes;

    //Function to get random number
    private static readonly Random Random = new Random();
    private static readonly object SyncLock = new object();

    protected void Page_Load(object sender, EventArgs e)
    {
      Helpers.AddStyleSheetReferences(this);
      if (!IsPostBack)
      {
        // Determine group randomization size
        if (ProcessQueryString())
        {
          // Build SEO data
          BuildSeoInfo();
          // Bind sheet based on point in season
          BindCSWRItems();
        }
      }
    }


    private bool ProcessQueryString()
    {
      bool groupSizeFound = false;

      bool randomizeByesFound = false;

      bool lockCountFound = false;

      // Determine Groupsize
      if (Request.QueryString["groupSize"] != null)
      {
        var groupSize = 0;
        if (int.TryParse(Request.QueryString["groupSize"], out groupSize))
        {
          _groupSize = groupSize;
          groupSizeFound = true;
        }
      }
      // Determine whether byes are randomized
      if (Request.QueryString["randomizeByes"] != null)
      {
        var randomizeByes = false;
        if (Boolean.TryParse(Request.QueryString["randomizeByes"], out randomizeByes))
        {
          _randomizeByes = randomizeByes;
          _teamSpecificRandomizedByes = Team.GetTeams(FOO.FOOString).ToDictionary(currentTeam => currentTeam.TeamCode, currentTeam => GetRandomNumber(4, 11));
          randomizeByesFound = true;
        }
      }
      // Determine Lock Count
      if (Request.QueryString["lockCount"] != null)
      {
        var lockCount = 0;
        if (int.TryParse(Request.QueryString["lockCount"], out lockCount))
        {
          _lockCount = lockCount;
          lockCountFound = true;
        }
      }

      return groupSizeFound && randomizeByesFound && lockCountFound;
    }


    private void BindCSWRItems()
    {
      int supplementalSourceID = SupplementalSource.GetSupplementalSource("CSWR").SupplementalSourceID;

      // Quarterbacks
      SupplementalSheet targetQBSuppSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, supplementalSourceID, "FOO", "QB");
      repQuarterbacks.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetQBSuppSheet.SupplementalSheetID).Take(Globals.CswrMaxPrintableWithoutRoster_QB)
                                                      .ToList().GroupRandomize(_groupSize, _lockCount);
      repQuarterbacks.DataBind();

      // Running Backs
      SupplementalSheet targetRBSuppSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, supplementalSourceID, "FOO", "RB");
      repRunningBacks.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetRBSuppSheet.SupplementalSheetID).Take(Globals.CswrMaxPrintableWithoutRoster_RB)
                                                      .ToList().GroupRandomize(_groupSize, _lockCount);
      repRunningBacks.DataBind();

      // Wide Receivers
      SupplementalSheet targetWRSuppSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, supplementalSourceID, "FOO", "WR");
      repWideReceivers.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetWRSuppSheet.SupplementalSheetID).Take(Globals.CswrMaxPrintableWithoutRoster_WR)
                                                      .ToList().GroupRandomize(_groupSize, _lockCount);
      repWideReceivers.DataBind();
      
      // TightEnds
      SupplementalSheet targetTESuppSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, supplementalSourceID, "FOO", "TE");
      repTightEnds.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetTESuppSheet.SupplementalSheetID).Take(Globals.CswrMaxPrintableWithoutRoster_TE)
                                                      .ToList().GroupRandomize(_groupSize, _lockCount);
      repTightEnds.DataBind();

      
      // Kickers
      SupplementalSheet targetKSuppSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, supplementalSourceID, "FOO", "K");
      repKickers.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetKSuppSheet.SupplementalSheetID).Take(Globals.CswrMaxPrintableWithoutRoster_K)
                                                      .ToList().GroupRandomize(_groupSize, _lockCount);
      repKickers.DataBind();

      
      // Defenses
      SupplementalSheet targetDFSuppSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, supplementalSourceID, "FOO", "DF");
      repDefenses.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetDFSuppSheet.SupplementalSheetID).Take(Globals.CswrMaxPrintableWithoutRoster_DF)
                                                      .ToList().GroupRandomize(_groupSize, _lockCount);
      repDefenses.DataBind();
    }


    private void BuildSeoInfo()
    {
      string currentSeason = SportSeason.GetCurrentSportSeason("FOO").SeasonCode;
      Page.Title = "Free, Printable " + currentSeason + " Fantasy Football Cheat Sheet*";
      Page.MetaDescription = "This free, printable " + currentSeason + " fantasy football cheat sheet includes all offensive positions for leagues with more than ten teams.";
    }


    protected void repPosition_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        Label rank = (Label)(e.Item.FindControl("labRank"));
        Label teamAbbreviation = (Label)(e.Item.FindControl("labTeamAbbreviation"));
        Literal playerName = (Literal)(e.Item.FindControl("litPlayerName"));
        Label byeWeek = (Label)(e.Item.FindControl("labByeWeek"));
        Image sleeperTag = (Image)(e.Item.FindControl("imaSleeperTag"));
        Image bustTag = (Image)(e.Item.FindControl("imaBustTag"));
        Image injuredTag = (Image)(e.Item.FindControl("imaInjuredTag"));

        Player boundPlayer = new Player();


        switch (_rankType)
        {
          case CSWRRankingType.CSWRRank:
            SupplementalSheetItem boundSheetItem = (SupplementalSheetItem)e.Item.DataItem;
            boundPlayer = boundSheetItem.Player;
            // Sleeper
            if ((bool)boundSheetItem.MappedProperties[SSIProperty.Sleeper.ToString()] == true)
            {
              sleeperTag.Visible = true;
            }
            // Bust
            if ((bool)boundSheetItem.MappedProperties[SSIProperty.Bust.ToString()] == true)
            {
              bustTag.Visible = true;
            }
            break;
          case CSWRRankingType.PlayerStat:
            SportSeasonPlayerSeasonStat boundSeasonStat = (SportSeasonPlayerSeasonStat)e.Item.DataItem;
            boundPlayer = boundSeasonStat.Player;
            break;
        }

        Team playerTeam = Team.GetTeam(boundPlayer.TeamCode);

        // Name
        playerName.Text = boundPlayer.FullName;

        // Rank
        int currentRank = 0;
        switch (boundPlayer.PositionCode)
        {
          case "QB":
            currentRank = ++_quarterbackCounter;
            break;
          case "RB":
            currentRank = ++_runningBackCounter;
            break;
          case "WR":
            currentRank = ++_wideReceiverCounter;
            break;
          case "TE":
            currentRank = ++_tightEndCounter;
            break;
          case "K":
            currentRank = ++_kickerCounter;
            break;
          case "DF":
            currentRank = ++_defenseCounter;
            break;
        }
        rank.Text = currentRank.ToString();

        // Team Abbreviation
        if (boundPlayer.PositionCode != "DF")
        {
          teamAbbreviation.Text = "(" + playerTeam.Abbreviation + ")";
        }

        if (_randomizeByes)
        {
          //System.Random randNum = new System.Random();
          //int randomByeWeek = randNum.Next(4, 12);
          byeWeek.Text = "[" + _teamSpecificRandomizedByes[boundPlayer.TeamCode] + "]";
        }
        else
        {
          // Bye Week
          ByeWeek playerBye = ByeWeek.GetByeWeek(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, "FOO", boundPlayer.TeamCode);
          if (playerBye != null)
          {
            byeWeek.Text = "[" + ByeWeek.GetByeWeek(FOO.CurrentSeason, FOO.FOOString, boundPlayer.TeamCode).Bye + "]";
          }
        }
      
      }
    }



    /// <summary>
    /// Geneate a random number
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    private static int GetRandomNumber(int min, int max)
    {
      lock (SyncLock)
      { // synchronize
        return Random.Next(min, max);
      }
    }


  }
}