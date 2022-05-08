using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class TightEndRankings : BasePage
  {

    private SportSeason _currentSportSeason;

    protected void Page_Init(object sender, EventArgs e)
    {
      _currentSportSeason = SportSeason.GetCurrentSportSeason(FOO.FOOString);
      if (Helpers.IsMiddleOfSeason(FOO.FOOString))
      {
        cswrRanking.RankType = CSWRRankingType.PlayerStat;
      }
      else
      {
        cswrRanking.RankType = CSWRRankingType.CSWRRank;
      }
      cswrRanking.SeasonCode = _currentSportSeason.SeasonCode;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        LoadSocialTags();
        string currentSeason = _currentSportSeason.SeasonCode;
        this.Page.Title = "Tight End Rankings for the " + currentSeason + " Fantasy Football Season";
        litPageTitle.Text = currentSeason + " Tight End Rankings";
        this.Page.MetaDescription = "The page lists the top tight end rankings for the " + currentSeason + " NFL fantasy football season";
      }
    }


    private void LoadSocialTags()
    {
      //SportMaster myMaster = (SportMaster)this.Page.Master;
      //myMaster.OpenGraphImage = "https://www.cheatsheetwarroom.com/images/socialsharing/rankings/tight-end-rankings.jpg";
      //myMaster.SchemaOrgImage = "https://www.cheatsheetwarroom.com/images/socialsharing/rankings/tight-end-rankings.jpg";
      //myMaster.TwitterImage = "https://www.cheatsheetwarroom.com/images/socialsharing/rankings/tight-end-rankings.jpg";
    }

  }
}