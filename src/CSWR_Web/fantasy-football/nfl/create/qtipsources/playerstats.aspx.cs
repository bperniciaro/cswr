using System;
using System.Collections.Generic;
using System.Linq;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{

  public partial class PlayerStats : System.Web.UI.Page
  {

    private int PlayerID { get; set; }
    private string StatSeasonCode { get; set; }
    private bool PPRLeague { get; set; }
    private Player TargetPlayer { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (LoadRequestVariables())
      {
        GetTargetPlayer();
        LoadPlayerData();
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

        //PPR League
        if (Request["pprLeague"] == Boolean.TrueString)
        {
          this.PPRLeague = true;
        }

        return true;
      }
      else
      {
        return false;
      }
    }


    private void LoadPlayerData()
    {
      if (!this.PPRLeague)
      {
        //TFP Rank
        labTFPRank.Text = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", this.StatSeasonCode, this.TargetPlayer.PlayerID, "TFPR").StatValue.ToString();
        //TFP
        labTFP.Text = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", this.StatSeasonCode, this.TargetPlayer.PlayerID, "TFP").StatValue.ToString();
        //FPPG Rank
        labFPPGRank.Text = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", this.StatSeasonCode, this.TargetPlayer.PlayerID, "FPGR").StatValue.ToString();
        //FPPG
        labFPPG.Text = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", this.StatSeasonCode, this.TargetPlayer.PlayerID, "FPPG").StatValue.ToString();
      }
      else
      {
        //TFP Rank
        labTFPRank.Text = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", this.StatSeasonCode, this.TargetPlayer.PlayerID, "TPPR").StatValue.ToString();
        //TFP
        labTFP.Text = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", this.StatSeasonCode, this.TargetPlayer.PlayerID, "TFPP").StatValue.ToString();
        //FPPG Rank
        labFPPGRank.Text = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", this.StatSeasonCode, this.TargetPlayer.PlayerID, "FPPR").StatValue.ToString();
        //FPPG
        labFPPG.Text = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", this.StatSeasonCode, this.TargetPlayer.PlayerID, "FPGP").StatValue.ToString();
      }


      switch (this.TargetPlayer.PositionCode)
      {
        case "QB":
          BuildQBTable();
          break;
        case "RB":
          BuildRBTable();
          break;
        case "WR":
          BuildWRTable();
          break;
        case "TE":
          BuildTETable();
          break;
        case "K":
          BuildKTable();
          break;
        case "DF":
          BuildDFTable();
          break;
      }

    }


    private void BuildWRTable()
    {
      panWRTable.Visible = true;
      List<SportSeasonPlayerSeasonStat> wrStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", this.StatSeasonCode, this.TargetPlayer.PlayerID);
      // GAM
      labWR_GAM.Text = wrStats.SingleOrDefault(x => x.StatCode == "GAM").StatValue.ToString();
      // RETR
      labWR_RETR.Text = wrStats.SingleOrDefault(x => x.StatCode == "RETR").StatValue.ToString();
      // RECP
      labWR_RECP.Text = wrStats.SingleOrDefault(x => x.StatCode == "RECP").StatValue.ToString();
      // REYD
      labWR_REYD.Text = wrStats.SingleOrDefault(x => x.StatCode == "REYD").StatValue.ToString();
      // RETD
      labWR_RETD.Text = wrStats.SingleOrDefault(x => x.StatCode == "RETD").StatValue.ToString();
      // RUYD
      labWR_RUYD.Text = wrStats.SingleOrDefault(x => x.StatCode == "RUYD").StatValue.ToString();
      // RUTD
      labWR_RUTD.Text = wrStats.SingleOrDefault(x => x.StatCode == "RUTD").StatValue.ToString();
      // FUM
      labWR_FUM.Text = wrStats.SingleOrDefault(x => x.StatCode == "FUM").StatValue.ToString();
    }


    private void BuildTETable()
    {
      panTETable.Visible = true;
      List<SportSeasonPlayerSeasonStat> teStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", this.StatSeasonCode, this.TargetPlayer.PlayerID);
      // GAM
      labTE_GAM.Text = teStats.SingleOrDefault(x => x.StatCode == "GAM").StatValue.ToString();
      // RETR
      labTE_RETR.Text = teStats.SingleOrDefault(x => x.StatCode == "RETR").StatValue.ToString();
      // RECP
      labTE_RECP.Text = teStats.SingleOrDefault(x => x.StatCode == "RECP").StatValue.ToString();
      // REYD
      labTE_REYD.Text = teStats.SingleOrDefault(x => x.StatCode == "REYD").StatValue.ToString();
      // RETD
      labTE_RETD.Text = teStats.SingleOrDefault(x => x.StatCode == "RETD").StatValue.ToString();
      // RUYD
      labTE_RUYD.Text = teStats.SingleOrDefault(x => x.StatCode == "RUYD").StatValue.ToString();
      // RUTD
      labTE_RUTD.Text = teStats.SingleOrDefault(x => x.StatCode == "RUTD").StatValue.ToString();
      // FUM
      labTE_FUM.Text = teStats.SingleOrDefault(x => x.StatCode == "FUM").StatValue.ToString();
    }

    private void BuildQBTable()
    {
      panQBTable.Visible = true;
      List<SportSeasonPlayerSeasonStat> teStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", this.StatSeasonCode, this.TargetPlayer.PlayerID);
      // GAM
      labQB_GAM.Text = teStats.SingleOrDefault(x => x.StatCode == "GAM").StatValue.ToString();
      // PAYD
      labQB_PAYD.Text = teStats.SingleOrDefault(x => x.StatCode == "PAYD").StatValue.ToString();
      // PATD
      labQB_PATD.Text = teStats.SingleOrDefault(x => x.StatCode == "PATD").StatValue.ToString();
      // INT
      labQB_INT.Text = teStats.SingleOrDefault(x => x.StatCode == "INT").StatValue.ToString();
      // RETD
      labQB_RUCA.Text = teStats.SingleOrDefault(x => x.StatCode == "RUCA").StatValue.ToString();
      // RUYD
      labQB_RUYD.Text = teStats.SingleOrDefault(x => x.StatCode == "RUYD").StatValue.ToString();
      // RUTD
      labQB_RUTD.Text = teStats.SingleOrDefault(x => x.StatCode == "RUTD").StatValue.ToString();
      // FUM
      labQB_FUM.Text = teStats.SingleOrDefault(x => x.StatCode == "FUM").StatValue.ToString();
    }


    private void BuildRBTable()
    {
      panRBTable.Visible = true;
      List<SportSeasonPlayerSeasonStat> teStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", this.StatSeasonCode, this.TargetPlayer.PlayerID);
      // GAM
      labRB_GAM.Text = teStats.SingleOrDefault(x => x.StatCode == "GAM").StatValue.ToString();
      // RUCA
      labRB_RUCA.Text = teStats.SingleOrDefault(x => x.StatCode == "RUCA").StatValue.ToString();
      // RUYD
      labRB_RUYD.Text = teStats.SingleOrDefault(x => x.StatCode == "RUYD").StatValue.ToString();
      // RUTD
      labRB_RUTD.Text = teStats.SingleOrDefault(x => x.StatCode == "RUTD").StatValue.ToString();
      // RETR
      labRB_RETR.Text = teStats.SingleOrDefault(x => x.StatCode == "RETR").StatValue.ToString();
      // RECP
      labRB_RECP.Text = teStats.SingleOrDefault(x => x.StatCode == "RECP").StatValue.ToString();
      // REYD
      labRB_REYD.Text = teStats.SingleOrDefault(x => x.StatCode == "REYD").StatValue.ToString();
      // RETD
      labRB_RETD.Text = teStats.SingleOrDefault(x => x.StatCode == "RETD").StatValue.ToString();
      // FUM
      labRB_FUM.Text = teStats.SingleOrDefault(x => x.StatCode == "FUM").StatValue.ToString();
    }


    private void BuildKTable()
    {
      panKTable.Visible = true;
      List<SportSeasonPlayerSeasonStat> kStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", this.StatSeasonCode, this.TargetPlayer.PlayerID);
      // GAM
      labK_GAM.Text = kStats.SingleOrDefault(x => x.StatCode == "GAM").StatValue.ToString();
      // MAFG
      labK_MAFG.Text = kStats.SingleOrDefault(x => x.StatCode == "MAFG").StatValue.ToString();
      // MIFG
      labK_MIFG.Text = kStats.SingleOrDefault(x => x.StatCode == "MIFG").StatValue.ToString();
      // MAXP
      labK_MAXP.Text = kStats.SingleOrDefault(x => x.StatCode == "MAXP").StatValue.ToString();
      // MIXP
      labK_MIXP.Text = kStats.SingleOrDefault(x => x.StatCode == "MIXP").StatValue.ToString();
    }

    private void BuildDFTable()
    {
      panDFTable.Visible = true;
      List<SportSeasonPlayerSeasonStat> dfStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", this.StatSeasonCode, this.TargetPlayer.PlayerID);
      // GAM
      //labDF_GAM.Text = dfStats.SingleOrDefault(x => x.StatCode == "GAM").StatValue.ToString();
      // FREC
      labDF_FREC.Text = dfStats.SingleOrDefault(x => x.StatCode == "FREC").StatValue.ToString();
      // INT
      labDF_INT.Text = dfStats.SingleOrDefault(x => x.StatCode == "INT").StatValue.ToString();
      // SACK
      labDF_SACK.Text = dfStats.SingleOrDefault(x => x.StatCode == "SACK").StatValue.ToString();
      // DTD
      labDF_DTD.Text = dfStats.SingleOrDefault(x => x.StatCode == "DTD").StatValue.ToString();
      // PA
      labDF_PA.Text = dfStats.SingleOrDefault(x => x.StatCode == "PA").StatValue.ToString();
    }


  }
}