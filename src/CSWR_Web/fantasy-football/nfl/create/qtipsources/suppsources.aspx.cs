using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.FOO
{
  public partial class SuppSources : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

      if ((Request["adp"] != null) && (Request["cswr"] != null) && (Request["cbs"] != null) && (Request["positionCode"] != null) )
      {
        LoadRankings();
        LoadLinkData();
      }
    }


    private void LoadLinkData()
    {
      Position currentPosition = Position.GetPosition(Request["positionCode"]);
      SportSeason currentSeason = SportSeason.GetCurrentSportSeason("FOO");

      // Source 1
      var source1 = SupplementalSource.GetSupplementalSource("CSWR");
      var suppSheet1 = SupplementalSheet.GetSupplementalSheet(currentSeason.SeasonCode, source1.SupplementalSourceID, "FOO", currentPosition.PositionCode);

      // Source 2
      var source2 = SupplementalSource.GetSupplementalSource("CBS");
      var suppSheet2 = SupplementalSheet.GetSupplementalSheet(currentSeason.SeasonCode, source2.SupplementalSourceID, "FOO", currentPosition.PositionCode);

      if (suppSheet1 != null)
      {
        hlCSWRRank.NavigateUrl = "https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/rankings/offense/" +
                                 currentPosition.Name.Replace(' ', '-').ToLower() + "s.aspx";
        hlCSWRRank.ToolTip = "Click to view all " + currentPosition.Name.ToLower() +
                             " rankings from Cheat Sheet War Room.";
      }
      else
      {
        hlCSWRRank.Visible = false;
      }

      if (suppSheet2 != null)
      {
        hlCBSRank.NavigateUrl = suppSheet2.Url;
        hlCBSRank.ToolTip = "Click to view all " + currentPosition.Name.ToLower() + " rankings from CBSSports.";
      }
      else
      {
        hlCBSRank.Visible = false;
      }

    }

    private void LoadRankings()
    {
      //ADP
      //if (Request["adp"] != null)
      //{
      //  labADP.Text = Request["adp"].ToString();
      //}

      //CSWR
      if (Request["cswr"] != null)
      {
        labCSWR.Text = Request["cswr"].ToString();
      }

      //CBSSports
      if (Request["cbs"] != null)
      {
        labCBS.Text = Request["cbs"].ToString();
      }
    }
  }
}