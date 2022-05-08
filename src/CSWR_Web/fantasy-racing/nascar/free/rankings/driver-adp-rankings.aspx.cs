using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class NascarDriverADPRankings : BasePage
  {
    private SportSeason _currentSportSeason;

    protected void Page_Init(object sender, EventArgs e)
    {
      _currentSportSeason = SportSeason.GetCurrentSportSeason(Globals.RacString);
      cswrRanking.RankType = CSWRRankingType.ADP;
      cswrRanking.SeasonCode = _currentSportSeason.SeasonCode;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
      InitializeControls();
    }

    private void InitializeControls()
    {
      string currentSeason = _currentSportSeason.SeasonCode;
      this.Page.Title = "Average Draft Position Rankings - Driver ADP for NASCAR Sprint Cup " + currentSeason;
      litPageTitle.Text = currentSeason + " NASCAR Driver ADP Rankings";
      this.Page.MetaDescription = "The average draft position (ADP) of drivers for the Sprint Cup " + currentSeason + " NASCAR fantasy racing season.";

      if (_currentSportSeason.SeasonStarted)
      {
        labADPTimeframe.Text = "final";
        labClosingStatement.Text = " This is a snapshot of each driver's average draft position before the first race of the season.";
      }
      else
      {
        labADPTimeframe.Text = "current";
        labClosingStatement.Text = " This ADP is calculated daily based on current NASCAR cheat sheet rankings.";
      }
      labCurrentSeason.Text = _currentSportSeason.SeasonCode;
    }
  }
}