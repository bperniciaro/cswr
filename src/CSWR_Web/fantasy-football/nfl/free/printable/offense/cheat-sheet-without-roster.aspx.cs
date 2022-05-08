using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class PrintableCheatSheetWithoutRoster : System.Web.UI.Page
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


    private void BindCSWRItems()
    {
      int supplementalSourceID = SupplementalSource.GetSupplementalSource("CSWR").SupplementalSourceID;
      // Running Backs
      SupplementalSheet targetRBSuppSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, supplementalSourceID, "FOO", "RB");
      repRunningBacks.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetRBSuppSheet.SupplementalSheetID).Take(Globals.CswrMaxPrintableWithoutRoster_RB);
      repRunningBacks.DataBind();
      // Wide Receivers
      SupplementalSheet targetWRSuppSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, supplementalSourceID, "FOO", "WR");
      repWideReceivers.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetWRSuppSheet.SupplementalSheetID).Take(Globals.CswrMaxPrintableWithoutRoster_WR);
      repWideReceivers.DataBind();
      // Quarterbacks
      SupplementalSheet targetQBSuppSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, supplementalSourceID, "FOO", "QB");
      repQuarterbacks.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetQBSuppSheet.SupplementalSheetID).Take(Globals.CswrMaxPrintableWithoutRoster_QB);
      repQuarterbacks.DataBind();
      // TightEnds
      SupplementalSheet targetTESuppSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, supplementalSourceID, "FOO", "TE");
      repTightEnds.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetTESuppSheet.SupplementalSheetID).Take(Globals.CswrMaxPrintableWithoutRoster_TE);
      repTightEnds.DataBind();
      // Kickers
      SupplementalSheet targetKSuppSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, supplementalSourceID, "FOO", "K");
      repKickers.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetKSuppSheet.SupplementalSheetID).Take(Globals.CswrMaxPrintableWithoutRoster_K);
      repKickers.DataBind();
      // Defenses
      SupplementalSheet targetDFSuppSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, supplementalSourceID, "FOO", "DF");
      repDefenses.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetDFSuppSheet.SupplementalSheetID).Take(Globals.CswrMaxPrintableWithoutRoster_DF);
      repDefenses.DataBind();
    }

    /// <summary>
    /// Binds the players based on CSWR supplemental rankings
    /// </summary>
    private void BindPlayerRankingItems()
    {
      string seasonCode = FOO.CurrentSeason;

      // Quarterbacks
      List<SportSeasonPlayerSeasonStat> qbRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, seasonCode,
                                                             FOOPositionsOffense.QB.ToString(), "TFP").Take(Globals.CswrMaxPrintableWithoutRoster_QB).ToList();
      repQuarterbacks.DataSource = qbRankings;
      repQuarterbacks.DataBind();

      // Running Backs
      List<SportSeasonPlayerSeasonStat> rbRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, seasonCode,
                                                             FOOPositionsOffense.RB.ToString(), "TFP").Take(Globals.CswrMaxPrintableWithoutRoster_RB).ToList();
      repRunningBacks.DataSource = rbRankings;
      repRunningBacks.DataBind();

      // Wide Receivers
      List<SportSeasonPlayerSeasonStat> wrRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, seasonCode,
                                                             FOOPositionsOffense.WR.ToString(), "TFP").Take(Globals.CswrMaxPrintableWithoutRoster_WR).ToList();
      repWideReceivers.DataSource = wrRankings;
      repWideReceivers.DataBind();

      // Tight Ends
      List<SportSeasonPlayerSeasonStat> teRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, seasonCode,
                                                             FOOPositionsOffense.TE.ToString(), "TFP").Take(Globals.CswrMaxPrintableWithoutRoster_TE).ToList();
      repTightEnds.DataSource = teRankings;
      repTightEnds.DataBind();

      // Kickers
      List<SportSeasonPlayerSeasonStat> kRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, seasonCode,
                                                             FOOPositionsOffense.K.ToString(), "TFP").Take(Globals.CswrMaxPrintableWithoutRoster_K).ToList();
      repKickers.DataSource = kRankings;
      repKickers.DataBind();

      // Defenses
      List<SportSeasonPlayerSeasonStat> dfRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, seasonCode,
                                                             FOOPositionsOffense.DF.ToString(), "TFP").Take(Globals.CswrMaxPrintableWithoutRoster_DF).ToList();
      repDefenses.DataSource = dfRankings;
      repDefenses.DataBind();
    }


    private void BuildSeoInfo()
    {
      string currentSeason = SportSeason.GetCurrentSportSeason("FOO").SeasonCode;
      Page.Title = "Free, Printable " + currentSeason + " Fantasy Football Cheat Sheet w/o Roster Area";
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


    void LoadCheatSheetItemData(ref CheatSheetItem currentItem, ref Literal playerName, ref Label rank, ref Image sleeperTag, ref Image bustTag, ref Image injuredTag, ref Label byeWeek, ref Label teamAbbreviation)
    {
      // Name
      if (currentItem.Player.PositionCode != "DF")
      {
        playerName.Text = currentItem.Player.FullName;
      }
      else
      {
        playerName.Text = currentItem.Player.FirstName;
      }
        // Rank
      int currentRank = 0;
      switch (currentItem.Player.PositionCode)
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
      if (currentItem.Player.PositionCode != "DF")
      {
        teamAbbreviation.Text = "(" + Team.GetTeam(currentItem.Player.TeamCode).Abbreviation + ")";
      }
      // Bye Week
      ByeWeek playerBye = ByeWeek.GetByeWeek(SportSeason.GetCurrentSportSeason(SessionHandler.CurrentSportCode).SeasonCode, SessionHandler.CurrentSportCode, currentItem.Player.TeamCode);
      if (playerBye != null)
      {
        byeWeek.Text = "[" + playerBye.Bye.ToString() + "]";
      }
      else
      {
        //byeWeek.Text = "[n/a]";
      }
      // Tags
      if ((bool)currentItem.MappedProperties[CSIProperty.Sleeper.ToString()] == true)
      {
        sleeperTag.Visible = true;
      }
      if ((bool)currentItem.MappedProperties[CSIProperty.Bust.ToString()] == true)
      {
        bustTag.Visible = true;
      }
      if ((bool)currentItem.MappedProperties[CSIProperty.Injured.ToString()] == true)
      {
        injuredTag.Visible = true;
      }

    }



    //void LoadSuppSheetItemData(ref SupplementalSheetItem currentItem, ref Literal playerName, ref Label rank, ref Image sleeperTag, ref Image bustTag, ref Label byeWeek, ref Label teamAbbreviation)
    //{
    //  // Name
    //  if (currentItem.Player.PositionCode != "DF")
    //  {
    //    playerName.Text = currentItem.Player.FullName;
    //  }
    //  else
    //  {
    //    playerName.Text = currentItem.Player.FirstName;
    //  }
    //  // Rank
    //  int currentRank = 0;
    //  switch (currentItem.Player.PositionCode)
    //  {
    //    case "QB":
    //      currentRank = ++_quarterbackCounter;
    //      break;
    //    case "RB":
    //      currentRank = ++_runningBackCounter;
    //      break;
    //    case "WR":
    //      currentRank = ++_wideReceiverCounter;
    //      break;
    //    case "TE":
    //      currentRank = ++_tightEndCounter;
    //      break;
    //    case "K":
    //      currentRank = ++_kickerCounter;
    //      break;
    //    case "DF":
    //      currentRank = ++_defenseCounter;
    //      break;
    //  }
    //  rank.Text = currentRank.ToString();
    //  // Team Abbreviation
    //  if (currentItem.Player.PositionCode != "DF")
    //  {
    //    teamAbbreviation.Text = "(" + Team.GetTeam(currentItem.Player.TeamCode).Abbreviation + ")";
    //  }
    //  // Bye Week
    //  ByeWeek playerBye = ByeWeek.GetByeWeek(SportSeason.GetCurrentSportSeason(SessionHandler.CurrentSportCode).SeasonCode, SessionHandler.CurrentSportCode, currentItem.Player.TeamCode);
    //  if (playerBye != null)
    //  {
    //    byeWeek.Text = "[" + playerBye.Bye.ToString() + "]";
    //  }
    //  else
    //  {
    //    //byeWeek.Text = "[n/a]";
    //  }
    //  // Tags
    //  if ((bool)currentItem.MappedProperties[SSIProperty.Sleeper.ToString()] == true)
    //  {
    //    sleeperTag.Visible = true;
    //  }
    //  if ((bool)currentItem.MappedProperties[SSIProperty.Sleeper.ToString()] == true)
    //  {
    //    bustTag.Visible = true;
    //  }
    //  //if (currentItem.InjuredTag == true)
    //  //{
    //  //  injuredTag.Visible = true;
    //  //}

    //}

  }
}