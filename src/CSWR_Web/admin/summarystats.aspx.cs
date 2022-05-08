using System;
using System.Collections.Generic;
using System.Linq;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.Admin
{

  public partial class SummaryStats : BasePage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      LoadStatistics();
    }

    private void LoadStatistics()
    {
      string currentSeasonCode = FOO.CurrentSeason;

      List<CheatSheet> allFOOSheets = CheatSheet.GetCheatSheets("FOO").Where(x => x.Username != String.Empty).Where(x => x.SeasonCode == currentSeasonCode).OrderBy(x => x.CheatSheetID).ToList();
      int sleeperSheetCount = CheatSheet.GetUserCheatSheetSleeperUsageCount(FOO.FOOString, currentSeasonCode);
      int bustSheetCount = CheatSheet.GetUserCheatSheetBustUsageCount(FOO.FOOString, currentSeasonCode);
      int noteSheetCount = CheatSheet.GetUserCheatSheetNoteUsageCount(FOO.FOOString, currentSeasonCode);


      decimal sleeperPercentage = (sleeperSheetCount * 100) / allFOOSheets.Count;
      decimal bustPercentage = (bustSheetCount * 100) / allFOOSheets.Count;
      decimal notePercentage = (noteSheetCount * 100) / allFOOSheets.Count;
      labSleeperUsagePercentage.Text = sleeperPercentage.ToString() + "%";
      labBustUsagePercentage.Text = bustPercentage.ToString() + "%";
      labNoteUsagePercentage.Text = notePercentage.ToString() + "%";
    }

  }
}