using System;
using System.Collections.Generic;
using System.Linq;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.ScheduledTasks
{
  public partial class SheetMaintenance : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      DeleteEmptySheets();
      CheckDuplicateADPCalculations();
      DeleteOldVisitorSheets();
      int i = 0;
      i = 5;

    }
   
    private void DeleteOldVisitorSheets()  
    {
      CheatSheet.DeleteOldVisitorCheatSheets();
    }

    private void DeleteEmptySheets()
    {
      // Clear-out any FOO sheets with zero items
      List<CheatSheet> allFOOCheatSheets = CheatSheet.GetCheatSheets(FOO.FOOString);
      foreach (CheatSheet targetSheet in allFOOCheatSheets)
      {
        if (targetSheet.Items.Count == 0)
        {
          targetSheet.Delete();
        }
      }
      // Clear-out any RAC sheets with zero items
      List<CheatSheet> allRACCheatSheets = CheatSheet.GetCheatSheets(Globals.RacString);
      foreach (CheatSheet targetSheet in allRACCheatSheets)
      {
        if (targetSheet.Items.Count == 0)
        {
          targetSheet.Delete();
        }
      }
    }


    private void CheckDuplicateADPCalculations()
    {
      // Process quarterbacks
      List<ADPCalculation> qbCalculations = ADPCalculation.GetADPCalculations(FOO.FOOString, FOO.CurrentSeason, FOOPositionsOffense.QB.ToString(), DateTime.Now).OrderByDescending(x => x.CalcTimestamp).ToList();
      if ((qbCalculations.Count > 1) && (Globals.LimitADPCalcuations))
      {
        DeleteDuplicateCalculations(qbCalculations);
      }
      // Process running backs
      List<ADPCalculation> rbCalculations = ADPCalculation.GetADPCalculations(FOO.FOOString, FOO.CurrentSeason, FOOPositionsOffense.RB.ToString(), DateTime.Now).OrderByDescending(x => x.CalcTimestamp).ToList();
      if ((rbCalculations.Count > 1) && (Globals.LimitADPCalcuations))
      {
        DeleteDuplicateCalculations(rbCalculations);
      }
      // Process wide receivers
      List<ADPCalculation> wrCalculations = ADPCalculation.GetADPCalculations(FOO.FOOString, FOO.CurrentSeason, FOOPositionsOffense.WR.ToString(), DateTime.Now).OrderByDescending(x => x.CalcTimestamp).ToList();
      if ((wrCalculations.Count > 1) && (Globals.LimitADPCalcuations))
      {
        DeleteDuplicateCalculations(wrCalculations);
      }
      // Process tight ends
      List<ADPCalculation> teCalculations = ADPCalculation.GetADPCalculations(FOO.FOOString, FOO.CurrentSeason, FOOPositionsOffense.TE.ToString(), DateTime.Now).OrderByDescending(x => x.CalcTimestamp).ToList();
      if ((teCalculations.Count > 1) && (Globals.LimitADPCalcuations))
      {
        DeleteDuplicateCalculations(teCalculations);
      }
      // Process kickers
      List<ADPCalculation> kCalculations = ADPCalculation.GetADPCalculations(FOO.FOOString, FOO.CurrentSeason, FOOPositionsOffense.K.ToString(), DateTime.Now).OrderByDescending(x => x.CalcTimestamp).ToList();
      if ((kCalculations.Count > 1) && (Globals.LimitADPCalcuations))
      {
        DeleteDuplicateCalculations(kCalculations);
      }
      // Process defenses
      List<ADPCalculation> dfCalculations = ADPCalculation.GetADPCalculations(FOO.FOOString, FOO.CurrentSeason, FOOPositionsOffense.DF.ToString(), DateTime.Now).OrderByDescending(x => x.CalcTimestamp).ToList();
      if ((dfCalculations.Count > 1) && (Globals.LimitADPCalcuations))
      {
        DeleteDuplicateCalculations(dfCalculations);
      }
    }

    private void DeleteDuplicateCalculations(List<ADPCalculation> targetCalculations)
    {
      for (int i = 0; i < targetCalculations.Count - 1; i++)
      {
        ADPCalculation.DeleteADPCalculation(targetCalculations[i].ADPCalculationID);
      }
    }




  }
}