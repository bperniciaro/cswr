using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class KickerADPRankings : BasePage
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
        this.Page.Title = "Kicker ADP - Average Draft Positions for " + _currentSportSeason.SeasonCode;
        litSeasonCode.Text = _currentSportSeason.SeasonCode;
        litPageTitle.Text = _currentSportSeason.SeasonCode + " Kicker ADP";
        this.Page.MetaDescription = "The average draft position (ADP) of kickers for the " + _currentSportSeason.SeasonCode + " NFL fantasy football season.";
      }
    }

    private void LoadSocialTags()
    {
      ResponsiveTwoCol myMaster = (ResponsiveTwoCol)this.Page.Master;
      //myMaster.OpenGraphImage = "https://www.cheatsheetwarroom.com/images/socialsharing/rankings/adp/kicker-adp-rankings.jpg";
      //myMaster.SchemaOrgImage = "https://www.cheatsheetwarroom.com/images/socialsharing/rankings/adp/kicker-adp-rankings.jpg";
      //myMaster.TwitterImage = "https://www.cheatsheetwarroom.com/images/socialsharing/rankings/adp/kicker-adp-rankings.jpg";
    }

  }
}