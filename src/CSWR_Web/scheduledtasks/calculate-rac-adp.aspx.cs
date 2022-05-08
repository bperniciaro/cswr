using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.ScheduledTasks
{
  public partial class CalculateRACADP : System.Web.UI.Page
  {

    private System.Object lockThis = new System.Object();

    protected void Page_Load(object sender, EventArgs e)
    {
      lock (lockThis)
      {
        PerformRACADPCalculations();
      }
    }


    private void PerformRACADPCalculations()
    {
      string sportCode = SupportedSport.RAC.ToString();
      ADPManager.CalculateADP(sportCode, SportSeason.GetCurrentSportStatSeason(sportCode).SeasonCode, "DR", 21);
    }
  }
}