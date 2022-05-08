using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class NascarDriverRankings : BasePage
  {

    private SportSeason _currentSportSeason;

    protected void Page_Init(object sender, EventArgs e)
    {
      _currentSportSeason = SportSeason.GetCurrentSportSeason(Globals.RacString);
      cswrRanking.RankType = CSWRRankingType.CSWRRank;
      cswrRanking.SeasonCode = _currentSportSeason.SeasonCode;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
      string currentSeason = _currentSportSeason.SeasonCode;
      this.Page.Title = "Driver Rankings for the " + currentSeason + " Fantasy NASCAR Sprint Cup Season";
      litPageTitle.Text = currentSeason + " NASCAR Driver Rankings";
      this.Page.MetaDescription = "The top NASCAR driver rankings for the " + currentSeason + " NASCAR fantasy racing Sprint Cup season";
    }

  }
}