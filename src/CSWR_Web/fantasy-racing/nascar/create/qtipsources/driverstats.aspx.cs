using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class DriverStats : System.Web.UI.Page
  {

    private int PlayerID { get; set; }
    private string StatSeasonCode { get; set; }
    private Player TargetPlayer { get; set; }



    protected void Page_Load(object sender, EventArgs e)
    {
      if (LoadRequestVariables())
      {
        GetTargetPlayer();
        LoadDriverData();
      }
    }


    private void GetTargetPlayer()
    {
      Player targetPlayer = Player.GetPlayer(this.PlayerID);
      if (targetPlayer != null)
      {
        this.TargetPlayer = targetPlayer;
      }
    }


    private bool LoadRequestVariables()
    {
      // if we receive all of the expected variables...
      if ((Request["playerid"] != null) && (Request["statseasoncode"] != null))
      {
        //PlayerID
        if (Request["playerid"] != null)
        {
          int playerID = 0;
          if (int.TryParse(Request["playerid"], out playerID))
          {
            this.PlayerID = playerID;

          }
          else
          {
            return false;
          }
        }

        //StatSeasonCode
        if (Request["statseasoncode"] != null)
        {
          this.StatSeasonCode = Request["statseasoncode"].ToString();
        }
        return true;
      }
      else
      {
        return false;
      }
    }


    private void LoadDriverData()  
    {
      SportSeasonPlayerSeasonStat winnings = new SportSeasonPlayerSeasonStat();

      List<SportSeasonPlayerSeasonStat> playerStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("RAC", this.StatSeasonCode, this.PlayerID);
      // some drivers have ADP but no stats from the previous season
      if (playerStats != null && playerStats.Count > 0)
      {
        // points
        SportSeasonPlayerSeasonStat pointsStat = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "PNTS"; });
        if (pointsStat != null)
        {
          labStatsPopupPoints.Text = pointsStat.StatValue.ToString(); ;
        }
        else
        {
          labStatsPopupPoints.Text = "0";
        }
        // rank
        SportSeasonPlayerSeasonStat rankStat = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "RANK"; });
        if (rankStat != null)
        {
          labStatsPopupRank.Text = rankStat.StatValue.ToString();
        }
        else
        {
          labStatsPopupRank.Text = "0";
        }
        // wins
        SportSeasonPlayerSeasonStat winsStat = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "WINS"; });
        if (winsStat != null)
        {
          labStatsPopupWins.Text = winsStat.StatValue.ToString();
        }
        else
        {
          labStatsPopupWins.Text = "0";
        }
        // afp
        SportSeasonPlayerSeasonStat afpStat = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "AFP"; });
        if (afpStat != null)
        {
          labStatsPopupAFP.Text = afpStat.StatValue.ToString();
        }
        else
        {
          labStatsPopupAFP.Text = "0";
        }
        // top 10
        SportSeasonPlayerSeasonStat top10Stat = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "TP10"; });
        if (top10Stat != null)
        {
          labStatsPopupTop10.Text = top10Stat.StatValue.ToString();
        }
        else
        {
          labStatsPopupTop10.Text = "0";
        }
        // winnings
        winnings = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "WNGS"; });
        if (winnings != null)
        {
          //labStatsPopupWinnings.Text = "$" + winnings.StatValue.ToString("#,##0");
          labStatsPopupWinnings.Text = winnings.StatValue.ToFormattedMoney();
        }
        else
        {
          labStatsPopupWinnings.Text = "0";
        }

        // behind
        SportSeasonPlayerSeasonStat behind = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "BHND"; });
        if (behind != null)
        {
          labStatsPopupBehind.Text = behind.StatValue.ToString();
        }
        else
        {
          labStatsPopupBehind.Text = "0";
        }
        // starts
        SportSeasonPlayerSeasonStat starts = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "STRT"; });
        if (starts != null)
        {
          labStatsPopupStarts.Text = starts.StatValue.ToString();
        }
        else
        {
          labStatsPopupStarts.Text = "0";
        }
        // poles
        SportSeasonPlayerSeasonStat poles = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "POLE"; });
        if (poles != null)
        {
          labStatsPopupPoles.Text = poles.StatValue.ToString();
        }
        else
        {
          labStatsPopupPoles.Text = "0";
        }
        // top 5
        SportSeasonPlayerSeasonStat top5Stat = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "TP5"; });
        if (top5Stat != null)
        {
          labStatsPopupTop5.Text = top5Stat.StatValue.ToString();
        }
        else
        {
          labStatsPopupTop5.Text = "0";
        }
      }
      else
      {
        // points
        labStatsPopupPoints.Text = "0";
        // rank
        labStatsPopupRank.Text = "0";
        // wins
        labStatsPopupWins.Text = "0";
        // afp
        labStatsPopupAFP.Text = "0";
        // top 10
        labStatsPopupTop10.Text = "0";
        // winnings
        labStatsPopupWinnings.Text = "$" + winnings.StatValue.ToString("#,##0");
        labStatsPopupWinnings.Text = "0";
        // behind
        labStatsPopupBehind.Text = "0";
        // starts
        labStatsPopupStarts.Text = "0";
        // poles
        labStatsPopupPoles.Text = "0";
        // top 5
        labStatsPopupTop5.Text = "0";
      }


    }



  }
}