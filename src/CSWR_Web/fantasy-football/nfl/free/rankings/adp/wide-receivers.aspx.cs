using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class WideReceiverADPRankings : BasePage
  {
    private SportSeason _currentSportSeason;

    protected void Page_Init(object sender, EventArgs e)
    {
      _currentSportSeason = SportSeason.GetCurrentSportSeason(FOO.FOOString);
      // Configure the Rankings Control
      cswrRanking.RankType = CSWRRankingType.ADP;
      cswrRanking.SeasonCode = _currentSportSeason.SeasonCode;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        ConfigurePageText();
      }
    }

    private void ConfigurePageText()
    {
      if (!IsPostBack)
      {
        LoadSocialTags();
        this.Page.Title = "Wide Receiver ADP - Average Draft Positions for " + _currentSportSeason.SeasonCode;
        litSeasonCode.Text = _currentSportSeason.SeasonCode;
        litPageTitle.Text = _currentSportSeason.SeasonCode + " Wide Receiver ADP";
        this.Page.MetaDescription = "The average draft position (ADP) of wide receivers for the " + _currentSportSeason.SeasonCode + " NFL fantasy football season.";
        if (_currentSportSeason.SeasonStarted)
        {
          labADPTimeframe.Text = "final";
        }
        else
        {
          labADPTimeframe.Text = "current";
        }
      }
    }

    private void LoadSocialTags()
    {
      ResponsiveTwoCol myMaster = (ResponsiveTwoCol)this.Page.Master;
      //myMaster.OpenGraphImage = "https://www.cheatsheetwarroom.com/images/socialsharing/rankings/adp/wide-receiver-adp-rankings.jpg";
      //myMaster.SchemaOrgImage = "https://www.cheatsheetwarroom.com/images/socialsharing/rankings/adp/wide-receiver-adp-rankings.jpg";
      //myMaster.TwitterImage = "https://www.cheatsheetwarroom.com/images/socialsharing/rankings/adp/wide-receiver-adp-rankings.jpg";
    }

  }
}