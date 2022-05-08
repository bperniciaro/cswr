using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class PrintableCheatSheetWithRoster : System.Web.UI.Page
  {
    private int _quarterbackCounter = 0;
    private int _runningBackCounter = 0;
    private int _wideReceiverCounter = 0;
    private int _tightEndCounter = 0;
    private int _kickerCounter = 0;
    private int _defenseCounter = 0;
    private CSWRRankingType _rankType;

    protected void Page_Load(object sender, EventArgs e)
    {
      Helpers.AddStyleSheetReferences(this);

      if (!IsPostBack)
      {
        // Build SEO data
        BuildSeoInfo();
        // Bind sheet based on point in season
        BindRelevantPlayers();
      }
    }

    public void BindRelevantPlayers()
    {
      if (Helpers.IsMiddleOfSeason(FOO.FOOString))
      {
        _rankType = CSWRRankingType.PlayerStat;
        BindPlayerRankingItems();
      }
      else
      {
        _rankType = CSWRRankingType.CSWRRank;
        BindCSWRItems();
      }
    }


    /// <summary>
    /// Binds the players based on CSWR supplemental rankings
    /// </summary>
    private void BindPlayerRankingItems()
    {
      string seasonCode = FOO.CurrentSeason;

      // Quarterbacks
      List<SportSeasonPlayerSeasonStat> qbRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, seasonCode,
                                                             FOOPositionsOffense.QB.ToString(), "TFP").Take(Globals.CswrMaxPrintableWithRoster_QB).ToList();
      repQuarterbacks.DataSource = qbRankings;
      repQuarterbacks.DataBind();

      // Running Backs
      List<SportSeasonPlayerSeasonStat> rbRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, seasonCode,
                                                             FOOPositionsOffense.RB.ToString(), "TFP").Take(Globals.CswrMaxPrintableWithRoster_RB).ToList();
      repRunningBacks.DataSource = rbRankings;
      repRunningBacks.DataBind();

      // Wide Receivers
      List<SportSeasonPlayerSeasonStat> wrRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, seasonCode,
                                                             FOOPositionsOffense.WR.ToString(), "TFP").Take(Globals.CswrMaxPrintableWithRoster_WR).ToList();
      repWideReceivers.DataSource = wrRankings;
      repWideReceivers.DataBind();

      // Tight Ends
      List<SportSeasonPlayerSeasonStat> teRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, seasonCode,
                                                             FOOPositionsOffense.TE.ToString(), "TFP").Take(Globals.CswrMaxPrintableWithRoster_TE).ToList();
      repTightEnds.DataSource = teRankings;
      repTightEnds.DataBind();

      // Kickers
      List<SportSeasonPlayerSeasonStat> kRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, seasonCode,
                                                             FOOPositionsOffense.K.ToString(), "TFP").Take(Globals.CswrMaxPrintableWithRoster_K).ToList();
      repKickers.DataSource = kRankings;
      repKickers.DataBind();

      // Defenses
      List<SportSeasonPlayerSeasonStat> dfRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, seasonCode,
                                                             FOOPositionsOffense.DF.ToString(), "TFP").Take(Globals.CswrMaxPrintableWithRoster_DF).ToList();
      repDefenses.DataSource = dfRankings;
      repDefenses.DataBind();
    }


    private void BindCSWRItems()
    {
      int supplementalSourceID = SupplementalSource.GetSupplementalSource("CSWR").SupplementalSourceID;
      // Running Backs
      SupplementalSheet targetRBSuppSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, supplementalSourceID, "FOO", "RB");
      repRunningBacks.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetRBSuppSheet.SupplementalSheetID).Take(Globals.CswrMaxPrintableWithRoster_RB);
      repRunningBacks.DataBind();
      // Wide Receivers
      SupplementalSheet targetWRSuppSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, supplementalSourceID, "FOO", "WR");
      repWideReceivers.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetWRSuppSheet.SupplementalSheetID).Take(Globals.CswrMaxPrintableWithRoster_WR);
      repWideReceivers.DataBind();
      // Quarterbacks
      SupplementalSheet targetQBSuppSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, supplementalSourceID, "FOO", "QB");
      repQuarterbacks.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetQBSuppSheet.SupplementalSheetID).Take(Globals.CswrMaxPrintableWithRoster_QB);
      repQuarterbacks.DataBind();
      // TightEnds
      SupplementalSheet targetTESuppSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, supplementalSourceID, "FOO", "TE");
      repTightEnds.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetTESuppSheet.SupplementalSheetID).Take(Globals.CswrMaxPrintableWithRoster_TE);
      repTightEnds.DataBind();
      // Kickers
      SupplementalSheet targetKSuppSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, supplementalSourceID, "FOO", "K");
      repKickers.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetKSuppSheet.SupplementalSheetID).Take(Globals.CswrMaxPrintableWithRoster_K);
      repKickers.DataBind();
      // Defenses
      SupplementalSheet targetDFSuppSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, supplementalSourceID, "FOO", "DF");
      repDefenses.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetDFSuppSheet.SupplementalSheetID).Take(Globals.CswrMaxPrintableWithRoster_DF);
      repDefenses.DataBind();
    }


    protected void repPositions_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
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
          teamAbbreviation.Text = "(" + Team.GetTeam(boundPlayer.TeamCode).Abbreviation + ")";
        }

        // Bye Week
        ByeWeek playerBye = ByeWeek.GetByeWeek(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, "FOO", boundPlayer.TeamCode);
        if (playerBye != null)
        {
          byeWeek.Text = "[" + playerBye.Bye.ToString() + "]";
        }
      }
    }


    private void BuildSeoInfo()
    {
      string currentSeason = SportSeason.GetCurrentSportSeason("FOO").SeasonCode;
      Page.Title = "Free, Printable " + currentSeason + " Fantasy Football Cheat Sheet /w Roster Area";
      Page.MetaDescription = "This free, printable " + currentSeason + " fantasy football cheat sheet includes all offensive positions and a roster area for leagues with ten or fewer teams.";


      litDraftYear1.Text = SportSeason.GetCurrentSportSeason("FOO").SeasonCode;
      litDraftYear2.Text = SportSeason.GetCurrentSportSeason("FOO").SeasonCode;
    }


  }
}