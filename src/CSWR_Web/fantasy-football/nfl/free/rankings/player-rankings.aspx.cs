using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;
using BP.CheatSheetWarRoom.UI.UserControls;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class NFLPlayerRankings : BasePage
  {

    private SportSeason _currentSeason;
    private CSWRRankingType _rankingType;
    private int _playerRankCounter = 0;
    private const int _maxPlayersToDisplay = 10;
    

    public void Page_Init(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        LoadSocialTags();
        this.Title = FOO.CurrentSeason + " NFL Player Rankings";
        _currentSeason = SportSeason.GetCurrentSportSeason(FOO.FOOString);
        litCurrentSeason1.Text = _currentSeason.SeasonCode;
        litCurrentSeason2.Text = _currentSeason.SeasonCode;
      }
    }

    public void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        litMainHeader.Text = _currentSeason.SeasonCode + " Fantasy Football Player Rankings";
        Page.MetaDescription = "NFL player rankings of the top players for the " + _currentSeason.SeasonCode + " fantasy football season.";
        BindRelevantPlayers();
      }
    }

    public void BindRelevantPlayers()
    {
      if (Helpers.IsMiddleOfSeason(FOO.FOOString))
      {
        _rankingType = CSWRRankingType.PlayerStat;
        BindPlayerRankingItems();
      }
      else
      {
        _rankingType = CSWRRankingType.CSWRRank;
        BindCSWRItems();
      }
    }

    /// <summary>
    /// Binds the players based on CSWR supplemental rankings
    /// </summary>
    private void BindCSWRItems()
    {
      SupplementalSource targetSource = SupplementalSource.GetSupplementalSource("CSWR");

      // Quarterbacks
      SupplementalSheet targetQBSheet = SupplementalSheet.GetSupplementalSheet(_currentSeason.SeasonCode, targetSource.SupplementalSourceID, 
                                                                                FOO.FOOString, FOOPositionsOffense.QB.ToString());
      List<SupplementalSheetItem> cswrQBRankings = SupplementalSheetItem.GetSupplementalSheetItems(targetQBSheet.SupplementalSheetID).Take(_maxPlayersToDisplay).ToList();
      repQBRankings.DataSource = cswrQBRankings;
      repQBRankings.DataBind();

      // Running Backs
      SupplementalSheet targetRBSheet = SupplementalSheet.GetSupplementalSheet(_currentSeason.SeasonCode, targetSource.SupplementalSourceID,
                                                                                FOO.FOOString, FOOPositionsOffense.RB.ToString());
      List<SupplementalSheetItem> cswrRBRankings = SupplementalSheetItem.GetSupplementalSheetItems(targetRBSheet.SupplementalSheetID).Take(_maxPlayersToDisplay).ToList();
      repRBRankings.DataSource = cswrRBRankings;
      repRBRankings.DataBind();

      // Wide Receivers
      SupplementalSheet targetWRSheet = SupplementalSheet.GetSupplementalSheet(_currentSeason.SeasonCode, targetSource.SupplementalSourceID,
                                                                                FOO.FOOString, FOOPositionsOffense.WR.ToString());
      List<SupplementalSheetItem> cswrWRRankings = SupplementalSheetItem.GetSupplementalSheetItems(targetWRSheet.SupplementalSheetID).Take(_maxPlayersToDisplay).ToList();
      repWRRankings.DataSource = cswrWRRankings;
      repWRRankings.DataBind();

      // Tight Ends
      SupplementalSheet targetTESheet = SupplementalSheet.GetSupplementalSheet(_currentSeason.SeasonCode, targetSource.SupplementalSourceID,
                                                                                FOO.FOOString, FOOPositionsOffense.TE.ToString());
      List<SupplementalSheetItem> cswrTERankings = SupplementalSheetItem.GetSupplementalSheetItems(targetTESheet.SupplementalSheetID).Take(_maxPlayersToDisplay).ToList();
      repTERankings.DataSource = cswrTERankings;
      repTERankings.DataBind();

      // Kickers
      SupplementalSheet targetKSheet = SupplementalSheet.GetSupplementalSheet(_currentSeason.SeasonCode, targetSource.SupplementalSourceID,
                                                                                FOO.FOOString, FOOPositionsOffense.K.ToString());
      List<SupplementalSheetItem> cswrKRankings = SupplementalSheetItem.GetSupplementalSheetItems(targetKSheet.SupplementalSheetID).Take(_maxPlayersToDisplay).ToList();
      repKRankings.DataSource = cswrKRankings;
      repKRankings.DataBind();

      // Defenses
      SupplementalSheet targetDFSheet = SupplementalSheet.GetSupplementalSheet(_currentSeason.SeasonCode, targetSource.SupplementalSourceID,
                                                                                FOO.FOOString, FOOPositionsOffense.DF.ToString());
      List<SupplementalSheetItem> cswrDFRankings = SupplementalSheetItem.GetSupplementalSheetItems(targetDFSheet.SupplementalSheetID).Take(_maxPlayersToDisplay).ToList();
      repDFRankings.DataSource = cswrDFRankings;
      repDFRankings.DataBind();
    }


    /// <summary>
    /// Binds the players based on CSWR supplemental rankings
    /// </summary>
    private void BindPlayerRankingItems()
    {
      // Quarterbacks
      List<SportSeasonPlayerSeasonStat> quarterbackYTDRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, _currentSeason.SeasonCode,
                                                                                 FOOPositionsOffense.QB.ToString(), "TFP").Take(_maxPlayersToDisplay).ToList();
      repQBRankings.DataSource = quarterbackYTDRankings;
      repQBRankings.DataBind();

      // Running Backs
      List<SportSeasonPlayerSeasonStat> runningBackYTDRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, _currentSeason.SeasonCode,
                                                                                 FOOPositionsOffense.RB.ToString(), "TFP").Take(_maxPlayersToDisplay).ToList();
      repRBRankings.DataSource = runningBackYTDRankings;
      repRBRankings.DataBind();

      // Wide Receivers
      List<SportSeasonPlayerSeasonStat> wideReceiverYTDRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, _currentSeason.SeasonCode,
                                                                                 FOOPositionsOffense.WR.ToString(), "TFP").Take(_maxPlayersToDisplay).ToList();
      repWRRankings.DataSource = wideReceiverYTDRankings;
      repWRRankings.DataBind();

      // Tight Ends
      List<SportSeasonPlayerSeasonStat> tightEndYTDRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, _currentSeason.SeasonCode,
                                                                                 FOOPositionsOffense.TE.ToString(), "TFP").Take(_maxPlayersToDisplay).ToList();
      repTERankings.DataSource = tightEndYTDRankings;
      repTERankings.DataBind();

      // Kickers
      List<SportSeasonPlayerSeasonStat> kickerYTDRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, _currentSeason.SeasonCode,
                                                                                 FOOPositionsOffense.K.ToString(), "TFP").Take(_maxPlayersToDisplay).ToList();
      repKRankings.DataSource = kickerYTDRankings;
      repKRankings.DataBind();

      // Defenses
      List<SportSeasonPlayerSeasonStat> defenseYTDRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, _currentSeason.SeasonCode,
                                                                                 FOOPositionsOffense.DF.ToString(), "TFP").Take(_maxPlayersToDisplay).ToList();
      repDFRankings.DataSource = defenseYTDRankings;
      repDFRankings.DataBind();
    }


    protected void cswrRanking_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        CSWRFOORankingItemTemplate fooRankingItemTemplate = (CSWRFOORankingItemTemplate)e.Item.FindControl("fooRankingItemTemplate");
        
        switch (_rankingType)
        {
          case CSWRRankingType.CSWRRank:
            SupplementalSheetItem boundItem = (SupplementalSheetItem)e.Item.DataItem;
            fooRankingItemTemplate.PlayerID = boundItem.PlayerID;
            fooRankingItemTemplate.RankLabel = boundItem.Seqno.ToString();
            fooRankingItemTemplate.LoadTemplate();
            break;
          case CSWRRankingType.PlayerStat:
            SportSeasonPlayerSeasonStat boundPlayerRanking = (SportSeasonPlayerSeasonStat)e.Item.DataItem;
            fooRankingItemTemplate.PlayerID = boundPlayerRanking.PlayerID;
            _playerRankCounter++;
            fooRankingItemTemplate.RankLabel = _playerRankCounter.ToString();
            fooRankingItemTemplate.LoadTemplate();
            break;
        }

      }
    }

    private void LoadSocialTags()
    {
      //SportMaster myMaster = (SportMaster)this.Page.Master;
      //myMaster.OpenGraphImage = "https://www.cheatsheetwarroom.com/images/socialsharing/nfl-player-rankings.jpg";
      //myMaster.SchemaOrgImage = "https://www.cheatsheetwarroom.com/images/socialsharing/nfl-player-rankings.jpg";
      //myMaster.TwitterImage = "https://www.cheatsheetwarroom.com/images/socialsharing/nfl-player-rankings.jpg";
    }
  }
}