using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.ScheduledTasks
{

  public partial class CalculateFOOADP : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (SportSetting.Football.CalculateADP)
      {
        PerformFOOADPCalculations(SportSetting.Football.TimespanInDays);
      }
    }

    private void PerformFOOADPCalculations(int timespanInDays)
    {
      string currentStatSeason = SportSeason.GetCurrentSportStatSeason(FOO.FOOString).SeasonCode;
      string currentSeason = FOO.CurrentSeason;

      /* we need to be sure that we're not double-logging ADP calculations */
      // Quarterback ADP
      if ((ADPCalculation.GetADPCalculationCount(currentSeason, FOO.FOOString, FOOPositionsOffense.QB.ToString(), DateTime.Now.Date) == 0) || !Globals.LimitADPCalcuations)
      {
        ADPManager.CalculateADP(FOO.FOOString, currentStatSeason, FOOPositionsOffense.QB.ToString(), timespanInDays);
      }

      // Running Back ADP
      if ((ADPCalculation.GetADPCalculationCount(currentSeason, FOO.FOOString, FOOPositionsOffense.RB.ToString(), DateTime.Now.Date) == 0) || !Globals.LimitADPCalcuations)
      {
        ADPManager.CalculateADP(FOO.FOOString, currentStatSeason, FOOPositionsOffense.RB.ToString(), timespanInDays);
      }

      // Wide Receiver ADP
      if ((ADPCalculation.GetADPCalculationCount(currentSeason, FOO.FOOString, FOOPositionsOffense.WR.ToString(), DateTime.Now.Date) == 0) || !Globals.LimitADPCalcuations)
      {
        ADPManager.CalculateADP(FOO.FOOString, currentStatSeason, FOOPositionsOffense.WR.ToString(), timespanInDays);
      }

      // Tight End ADP
      if ((ADPCalculation.GetADPCalculationCount(currentSeason, FOO.FOOString, FOOPositionsOffense.TE.ToString(), DateTime.Now.Date) == 0) || !Globals.LimitADPCalcuations)
      {
        ADPManager.CalculateADP(FOO.FOOString, currentStatSeason, FOOPositionsOffense.TE.ToString(), timespanInDays);
      }

      // Kicker ADP
      if ((ADPCalculation.GetADPCalculationCount(currentSeason, FOO.FOOString, FOOPositionsOffense.K.ToString(), DateTime.Now.Date) == 0) || !Globals.LimitADPCalcuations)
      {
        ADPManager.CalculateADP(FOO.FOOString, currentStatSeason, FOOPositionsOffense.K.ToString(), timespanInDays);
      }

      // Defense ADP
      if ((ADPCalculation.GetADPCalculationCount(currentSeason, FOO.FOOString, FOOPositionsOffense.DF.ToString(), DateTime.Now.Date) == 0) || !Globals.LimitADPCalcuations)
      {
        ADPManager.CalculateADP(FOO.FOOString, currentStatSeason, FOOPositionsOffense.DF.ToString(), timespanInDays);
      }
    }



    //private void DelayBetweenCalculations()
    //{
    //  if (Globals.Settings.ApplicationState == ApplicationState.Prod.ToString().ToLower())
    //  {
    //    Thread.Sleep(5000);
    //  }
    //}


  }
}