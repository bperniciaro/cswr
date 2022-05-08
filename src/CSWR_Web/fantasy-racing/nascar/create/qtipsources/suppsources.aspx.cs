using System;

namespace BP.CheatSheetWarRoom.UI.RAC
{
  public partial class SuppSources : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if ((Request["adp"] != null) && (Request["cswr"] != null))
      {
        LoadLinkData();
      }
    }


    private void LoadLinkData()
    {

      // configure rank values
      if (Request["cswr"] != String.Empty)
      {
        labSuppRankingCSWRRank.Text = Request["cswr"];
      }
      else
      {
        labSuppRankingCSWRRank.Text = "n/r";
        labSuppRankingCSWRRank.ToolTip = "not ranked";
      }

      // configure adp values
      if (Request["adp"] != String.Empty)
      {
        labSuppRankingsADP.Text = Request["adp"];
      }
      else
      {
        labSuppRankingsADP.Text = "n/r";
        labSuppRankingsADP.ToolTip = "not ranked";
      }


      if (Globals.CSWRSettings.ApplicationState.ToLower() == ApplicationState.Local.ToString().ToLower())
      {
        hlADPRankings.NavigateUrl = "../free/rankings/driver-adp-rankings.aspx";
        hlCSWRRankings.NavigateUrl = "../free/rankings/drivers.aspx";
      }
      else 
      {
        hlADPRankings.NavigateUrl = "https://www.cheatsheetwarroom.com/fantasy-racing/nascar/free/rankings/driver-adp-rankings.aspx";
        hlCSWRRankings.NavigateUrl = "https://www.cheatsheetwarroom.com/fantasy-racing/nascar/free/rankings/drivers.aspx";
      }


    }

  }
}