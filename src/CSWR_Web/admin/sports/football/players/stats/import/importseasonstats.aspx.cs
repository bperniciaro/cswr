using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;
using BP.CheatSheetWarRoom.CSV;

namespace BP.CheatSheetWarRoom.UI.Admin.Sheets
{
  public partial class ImportSeasonStats : BasePage
  {

    private string _statSeasonCode = String.Empty;
    private int _currentWeek = 0;

    private List<MappedPlayer> _unfoundPlayers = new List<MappedPlayer>();
    private List<MappedPlayer> UnfoundPlayers
    {
      get
      {
        return _unfoundPlayers;
      }
      set
      {
        _unfoundPlayers = value;
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

      // we want to calculate seasonal stats for the most current season for which the StatsLoaded flag is true
      if (tbSeasonStats.Text == String.Empty)
      {
        _statSeasonCode = SportSeason.GetSportSeasons(FOO.FOOString).OrderByDescending(x => x.SeasonCode).ToList()[0].SeasonCode;
      }
      else
      {
        _statSeasonCode = tbSeasonStats.Text;
      }


      if (!IsPostBack)
      {

        // dynamically load button text
        butImportStats.Text = "Load seasonal stats for the " + _statSeasonCode + " season";

        // determine previous processed weeks
        labImportedWeeks.Text = Helpers.DetermineProcessedStatWeeks(_statSeasonCode);

        // try to guess which week we want to download YTD stats for
        int nextStatWeek = Helpers.GetNextStatWeek() - 1;
        if (nextStatWeek != 0)
        {
          ddlWeek.Text = nextStatWeek.ToString();
        }
      }

      _currentWeek = int.Parse(ddlWeek.SelectedValue);

      Player testPlayer = Player.GetPlayerByStatMapID(1937);
    }

    /// <summary>
    /// Event which handles all aspects of the stat import
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void butImportStats_Click(object sender, EventArgs e)
    {
      // process all offensive players
      List<MappedPlayer> spreadSheetPlayers = QueryOffenseSeasonStats();
      if (spreadSheetPlayers == null)
      {
        return; // error reading file
      }

      // start processing offensive players players
      ProcessPlayers(spreadSheetPlayers);

      // process all defensive players
      List<MappedDSTPlayer> defSheetTeams = QueryDefenseSeasonStats();
      ProcessTeams(defSheetTeams);
      
      //Calculate Player Rankings
      CalculatePlayerRankings();
      
      //If we made it this far it must have worked
      mbResult.MessageType = MessageType.SUCCESS;
      mbResult.Message = new System.Text.StringBuilder("Import Successful");
    }

    private void CalculatePlayerRankings()
    {
      //Calculate rankings which don't consider PPR
      CalculateNonPPRRankings();
      //Calculate rankings which consider PPR
      CalculatePPRRankings();
    }


    private void CalculateNonPPRRankings()
    {
      /* ****************************************** */
      /*    Fantasy Points Per Game Rank      */
      /* ****************************************** */
      // QB
      List<SportSeasonPlayerSeasonStat> allQBFPPGStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", _statSeasonCode, "QB", "FPPG");
      InsertPlayerRanks(allQBFPPGStats, "FPGR");
      // RB
      List<SportSeasonPlayerSeasonStat> allRBFPPGStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", _statSeasonCode, "RB", "FPPG");
      InsertPlayerRanks(allRBFPPGStats, "FPGR");
      // WR
      List<SportSeasonPlayerSeasonStat> allWRFPPGStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", _statSeasonCode, "WR", "FPPG");
      InsertPlayerRanks(allWRFPPGStats, "FPGR");
      // TE
      List<SportSeasonPlayerSeasonStat> allTEFPPGStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", _statSeasonCode, "TE", "FPPG");
      InsertPlayerRanks(allTEFPPGStats, "FPGR");
      // K
      List<SportSeasonPlayerSeasonStat> allKFPPGStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", _statSeasonCode, "K", "FPPG");
      InsertPlayerRanks(allKFPPGStats, "FPGR");
      // DF
      List<SportSeasonPlayerSeasonStat> allDFFPPGStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", _statSeasonCode, "DF", "FPPG");
      InsertPlayerRanks(allDFFPPGStats, "FPGR");

      /* ****************************************** */
      /*    Total Fantasy Points Rank      */
      /* ****************************************** */
      // QB
      List<SportSeasonPlayerSeasonStat> allQBTFPStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", _statSeasonCode, "QB", "TFP");
      InsertPlayerRanks(allQBTFPStats, "TFPR");
      // RB
      List<SportSeasonPlayerSeasonStat> allRBTFPStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", _statSeasonCode, "RB", "TFP");
      InsertPlayerRanks(allRBTFPStats, "TFPR");
      // WR
      List<SportSeasonPlayerSeasonStat> allWRTFPStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", _statSeasonCode, "WR", "TFP");
      InsertPlayerRanks(allWRTFPStats, "TFPR");
      // TE
      List<SportSeasonPlayerSeasonStat> allTETFPStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", _statSeasonCode, "TE", "TFP");
      InsertPlayerRanks(allTETFPStats, "TFPR");
      // K
      List<SportSeasonPlayerSeasonStat> allKTFPStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", _statSeasonCode, "K", "TFP");
      InsertPlayerRanks(allKTFPStats, "TFPR");
      // DF
      List<SportSeasonPlayerSeasonStat> allDFTFPStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", _statSeasonCode, "DF", "TFP");
      InsertPlayerRanks(allDFTFPStats, "TFPR");

    }



    private void CalculatePPRRankings()
    {
      /* ****************************************** */
      /*    Fantasy Points Per Game PPR Rank      */
      /* ****************************************** */
      // RB
      List<SportSeasonPlayerSeasonStat> allRBFPGPStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", _statSeasonCode, "RB", "FPGP");
      InsertPlayerRanks(allRBFPGPStats, "FPPR");
      // WR
      List<SportSeasonPlayerSeasonStat> allWRFPGPStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", _statSeasonCode, "WR", "FPGP");
      InsertPlayerRanks(allWRFPGPStats, "FPPR");
      // TE
      List<SportSeasonPlayerSeasonStat> allTEFPGPStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", _statSeasonCode, "TE", "FPGP");
      InsertPlayerRanks(allTEFPGPStats, "FPPR");

      /* ****************************************** */
      /*    Total Fantasy Points PPR Rank      */
      /* ****************************************** */
      // RB
      List<SportSeasonPlayerSeasonStat> allRBTFPPStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", _statSeasonCode, "RB", "TFPP");
      InsertPlayerRanks(allRBTFPPStats, "TPPR");
      // WR
      List<SportSeasonPlayerSeasonStat> allWRTFPPStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", _statSeasonCode, "WR", "TFPP");
      InsertPlayerRanks(allWRTFPPStats, "TPPR");
      // TE
      List<SportSeasonPlayerSeasonStat> allTETFPPStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", _statSeasonCode, "TE", "TFPP");
      InsertPlayerRanks(allTETFPPStats, "TPPR");
    }


    /// <summary>
    /// Insert Fantasy Points Per Game Ranks
    /// </summary>
    /// <param name="stats"></param>
    private void InsertPlayerRanks(List<SportSeasonPlayerSeasonStat> stats, string statCode)  
    {
      int rank = 1;
      foreach (SportSeasonPlayerSeasonStat currentStat in stats)
      {
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentStat.PlayerID, statCode, rank);
        rank++;
      }
    }


    private void ProcessPlayers(List<MappedPlayer> spreadSheetPlayers)
    {
      int unfoundQBs = 0;
      int unfoundRBs = 0;
      int unfoundWRs = 0;
      int unfoundTEs = 0;
      int unfoundKs = 0;

      // Delete QB Seasonal Stats
      SportSeasonPlayerSeasonStat.DeleteSportSeasonPlayerSeasonStats(FOO.FOOString, _statSeasonCode, FOOPositionsOffense.QB.ToString());
      // Delete RB Seasonal Stats
      SportSeasonPlayerSeasonStat.DeleteSportSeasonPlayerSeasonStats(FOO.FOOString, _statSeasonCode, FOOPositionsOffense.RB.ToString());
      // Delete WR Seasonal Stats
      SportSeasonPlayerSeasonStat.DeleteSportSeasonPlayerSeasonStats(FOO.FOOString, _statSeasonCode, FOOPositionsOffense.WR.ToString());
      // Delete TE Seasonal Stats
      SportSeasonPlayerSeasonStat.DeleteSportSeasonPlayerSeasonStats(FOO.FOOString, _statSeasonCode, FOOPositionsOffense.TE.ToString());
      // Delete K Seasonal Stats
      SportSeasonPlayerSeasonStat.DeleteSportSeasonPlayerSeasonStats(FOO.FOOString, _statSeasonCode, FOOPositionsOffense.K.ToString());

      foreach (MappedPlayer mappedPlayer in spreadSheetPlayers)
      {
        switch (mappedPlayer.Position)
        {
          case "QB":
            if (!ProcessQB(mappedPlayer))
            {
              unfoundQBs++;
              this.UnfoundPlayers.Add(mappedPlayer);
            }
            break;
          case "RB":
            if (!ProcessRB(mappedPlayer))
            {
              unfoundRBs++;
              this.UnfoundPlayers.Add(mappedPlayer);
            }
            break;
          case "WR":
            if (!ProcessWR(mappedPlayer))
            {
              unfoundWRs++;
              this.UnfoundPlayers.Add(mappedPlayer);
            }
            break;
          case "TE":
            if (!ProcessTE(mappedPlayer))
            {
              unfoundTEs++;
              this.UnfoundPlayers.Add(mappedPlayer);
            }
            break;
          case "PK":
            if (!ProcessK(mappedPlayer))
            {
              unfoundKs++;
              this.UnfoundPlayers.Add(mappedPlayer);
            }
            break;
          case "DF":
            break;
        }
      }

      labUnfoundQBs.Text = unfoundQBs.ToString();
      labUnfoundRBs.Text = unfoundRBs.ToString();
      labUnfoundWRs.Text = unfoundWRs.ToString();
      labUnfoundTEs.Text = unfoundTEs.ToString();
      labUnfoundKs.Text = unfoundKs.ToString();
      
      repUnfoundPlayers.DataSource = this.UnfoundPlayers;
      repUnfoundPlayers.DataBind();
    }


    private void ProcessTeams(List<MappedDSTPlayer> spreadSheetTeams)
    {
      int unfoundTeams = 0;

      foreach (MappedDSTPlayer mappedTeam in spreadSheetTeams)
      {
        if (!ProcessTeam(mappedTeam))
        {
          unfoundTeams++;
        }
      }

      labUnfoundTeams.Text = unfoundTeams.ToString();
    }

    private bool ProcessTeam(MappedDSTPlayer mappedTeam)
    {
      // transform their team abbreviations to the abbreviation we use, otherwise, we'll
      // miss these teams
      switch (mappedTeam.TeamAbbreviation.Trim())
      {
        case "GB":
          mappedTeam.TeamAbbreviation = "GBP";
          break;
        case "KC":
          mappedTeam.TeamAbbreviation = "KCC";
          break;
        case "LAR":
          mappedTeam.TeamAbbreviation = "LAR";
          break;
        case "NE":
          mappedTeam.TeamAbbreviation = "NEP";
          break;
        case "NO":
          mappedTeam.TeamAbbreviation = "NOS";
          break;
        case "LAC":
          mappedTeam.TeamAbbreviation = "SDC";
          break;
        case "SF":
          mappedTeam.TeamAbbreviation = "SFO";
          break;
        case "TB":
          mappedTeam.TeamAbbreviation = "TBB";
          break;
      }

      Player currentTeam = Player.GetDefensiveTeamPlayer(mappedTeam.TeamAbbreviation + "N");

      if (currentTeam != null)
      {
        // Delete all stats associated with this 'player'
        SportSeasonPlayerSeasonStat.DeleteSportSeasonPlayerSeasonStats("FOO", _statSeasonCode, currentTeam.PlayerID);

        // GAM
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentTeam.PlayerID,
          "GAM", mappedTeam.Games);

        // FREC
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentTeam.PlayerID,
          "FREC", mappedTeam.FumbleRecoveries);

        // INT
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentTeam.PlayerID,
          "INT", mappedTeam.Interceptions);

        // SACK
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentTeam.PlayerID,
          "SACK", mappedTeam.Sacks);

        // DTD
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentTeam.PlayerID,
          "DTD", mappedTeam.DefensiveTouchdowns);

        // PA
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentTeam.PlayerID,
          "PA", mappedTeam.PointsAllowed);

        // SAFT
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentTeam.PlayerID,
          "SAFT", mappedTeam.Safeties);

        /***************************/
        /*  Calculate TFP & FPPG   */
        /***************************/

        double FREC_Points = mappedTeam.FumbleRecoveries  * 2;
        double SACK_Points = mappedTeam.Sacks * 1;
        double DTD_Points = mappedTeam.DefensiveTouchdowns * 6;
        double Safety_Points = mappedTeam.Safeties * 4;
        double Interception_Points = mappedTeam.Interceptions * 2;

        
        double totalFantasyPoints = FREC_Points + SACK_Points + DTD_Points + Safety_Points + Interception_Points ;
        double fantasyPointsPerGame = totalFantasyPoints / mappedTeam.Games;

        // TFP
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentTeam.PlayerID,
          "TFP", Math.Round(totalFantasyPoints, 0));

        // FPPG
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentTeam.PlayerID,
          "FPPG", Math.Round(fantasyPointsPerGame, 1));
        return true;
      }
      else
      {
        return false;
      }

    }

    

    
    private bool ProcessQB(MappedPlayer mappedPlayer)
    {
      Player currentPlayer = Player.GetPlayerByStatMapID(mappedPlayer.PlayerStatMapID);

      if (currentPlayer != null)
      {

        // GAM
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "GAM", mappedPlayer.GamesPlayed);
        // PAAT
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "PAAT", mappedPlayer.PassAttempts);
        // COMP
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "COMP", mappedPlayer.Completions);
        // PAYD
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "PAYD", mappedPlayer.PassingYards);
        // PATD
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "PATD", mappedPlayer.PassingTDs);
        // RUAT
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RUCA", mappedPlayer.RushingAttempts);
        // RUYD
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RUYD", mappedPlayer.RushingYards);
        // RUTD
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RUTD", mappedPlayer.RushingTDs);
        // INT
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "INT", mappedPlayer.Interceptions);
        // FUM
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "FUM", mappedPlayer.FumblesLost);
        // QSCK
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "QSCK", mappedPlayer.PassingSacks);

        /***************************/
        /*  Calculate TFP & FPPG   */
        /***************************/

        double PAYD_Points = (double)mappedPlayer.PassingYards / 25; // need to cast so it won't round
        double PATD_Points = mappedPlayer.PassingTDs * 6;
        double RUYD_Points = (double)mappedPlayer.RushingYards / 10;
        double RUTD_Points = mappedPlayer.RushingTDs * 6;
        double FUM_Points = mappedPlayer.FumblesLost * 2;
        double INT_Points = mappedPlayer.Interceptions * 2;

        double totalFantasyPoints = PAYD_Points + PATD_Points + RUYD_Points + RUTD_Points - INT_Points - FUM_Points;
        double fantasyPointsPerGame = totalFantasyPoints / mappedPlayer.GamesPlayed;

        // TFP
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "TFP", Math.Round(totalFantasyPoints, 0));

        // FPPG
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "FPPG", Math.Round(fantasyPointsPerGame, 1));
        return true;
      }
      else
      {
        return false;
      }

    }



    private bool ProcessRB(MappedPlayer mappedPlayer)
    {
      Player currentPlayer = Player.GetPlayerByStatMapID(mappedPlayer.PlayerStatMapID);

      if (currentPlayer != null)
      {
        // GAM
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "GAM", mappedPlayer.GamesPlayed);

        // RUCA
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RUCA", mappedPlayer.RushingAttempts);

        // RUYD
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RUYD", mappedPlayer.RushingYards);

        // RYPC
        decimal rushingYardsPerCarry = 0;
        if (mappedPlayer.RushingAttempts > 0)
        {
          rushingYardsPerCarry = (decimal)mappedPlayer.RushingYards / (decimal)mappedPlayer.RushingAttempts;
        }
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RYPC", (double)Math.Round(rushingYardsPerCarry, 1));
        
        // RUTD
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RUTD", mappedPlayer.RushingTDs);

        // RETR
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RETR", mappedPlayer.ReceivingTargets);

        // RECP
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RECP", mappedPlayer.Receptions);

        // REYD
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "REYD", mappedPlayer.ReceivingYards);

        // RETD
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RETD", mappedPlayer.ReceivingTDs);

        // FUM
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "FUM", mappedPlayer.FumblesLost);
        

        /*****************************************/
        /*  Calculate TFP & FPPG & TFPP & FPGP   */
        /*****************************************/

        double REYD_Points = (double)mappedPlayer.ReceivingYards / 10;
        double RETD_Points = mappedPlayer.ReceivingTDs * 6;
        double RUYD_Points = (double)mappedPlayer.RushingYards / 10;
        double RUTD_Points = mappedPlayer.RushingTDs * 6;
        double FUM_Points = mappedPlayer.FumblesLost * 2;

        double totalFantasyPoints = RUYD_Points + RUTD_Points + REYD_Points + RETD_Points - FUM_Points;
        double fantasyPointsPerGame = totalFantasyPoints / mappedPlayer.GamesPlayed;
        
        double totalFantasyPointsPPR = totalFantasyPoints + mappedPlayer.Receptions;
        double fantasyPointsPerGamePPR = totalFantasyPointsPPR / mappedPlayer.GamesPlayed;

        // TFP
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "TFP", Math.Round(totalFantasyPoints, 0));

        // FPPG
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "FPPG", Math.Round(fantasyPointsPerGame, 1));

        // TFPP 
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "TFPP", Math.Round(totalFantasyPointsPPR, 0));

        // FPGP
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "FPGP", Math.Round(fantasyPointsPerGamePPR, 1));
        return true;
      }
      else
      {
        return false;
      }

    }


    private bool ProcessWR(MappedPlayer mappedPlayer)
    {
      Player currentPlayer = Player.GetPlayerByStatMapID(mappedPlayer.PlayerStatMapID);

      if (currentPlayer != null)
      {
        // GAM
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "GAM", mappedPlayer.GamesPlayed);

        // RECP
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RECP", mappedPlayer.Receptions);

        // RETR
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RETR", mappedPlayer.ReceivingTargets);

        // REYD
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "REYD", mappedPlayer.ReceivingYards);

        // RETD
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RETD", mappedPlayer.ReceivingTDs);

        // RUYD
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RUYD", mappedPlayer.RushingYards);

        // RUTD
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RUTD", mappedPlayer.RushingTDs);

        // FUM
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "FUM", mappedPlayer.FumblesLost);


        /*****************************************/
        /*  Calculate TFP & FPPG & TFPP & FPGP   */
        /*****************************************/

        double REYD_Points = (double)mappedPlayer.ReceivingYards / 10;
        double RETD_Points = mappedPlayer.ReceivingTDs * 6;
        double RUYD_Points = (double)mappedPlayer.RushingYards / 10;
        double RUTD_Points = mappedPlayer.RushingTDs * 6;
        double FUM_Points = mappedPlayer.FumblesLost * 2;

        double totalFantasyPoints = RUYD_Points + RUTD_Points + REYD_Points + RETD_Points - FUM_Points;
        double fantasyPointsPerGame = totalFantasyPoints / mappedPlayer.GamesPlayed;
        double totalFantasyPointsPPR = totalFantasyPoints + mappedPlayer.Receptions;
        double fantasyPointsPerGamePPR = totalFantasyPointsPPR / mappedPlayer.GamesPlayed;

        // TFP
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "TFP", Math.Round(totalFantasyPoints, 0));

        // FPPG
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "FPPG", Math.Round(fantasyPointsPerGame, 1));

        // TFPP 
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "TFPP", Math.Round(totalFantasyPointsPPR, 0));

        // FPGP
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "FPGP", Math.Round(fantasyPointsPerGamePPR, 1));
        return true;
      }
      else
      {
        return false;
      }

    }



    private bool ProcessTE(MappedPlayer mappedPlayer)
    {
      Player currentPlayer = Player.GetPlayerByStatMapID(mappedPlayer.PlayerStatMapID);

      if (currentPlayer != null)
      {
        // GAM
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "GAM", mappedPlayer.GamesPlayed);

        // RECP
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RECP", mappedPlayer.Receptions);

        // RETR
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RETR", mappedPlayer.ReceivingTargets);

        // REYD
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "REYD", mappedPlayer.ReceivingYards);

        // RETD
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RETD", mappedPlayer.ReceivingTDs);

        // RUYD
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RUYD", mappedPlayer.RushingYards);

        // RUTD
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "RUTD", mappedPlayer.RushingTDs);

        // FUM
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "FUM", mappedPlayer.FumblesLost);


        /*****************************************/
        /*  Calculate TFP & FPPG & TFPP & FPGP   */
        /*****************************************/

        double REYD_Points = (double)mappedPlayer.ReceivingYards / 10;
        double RETD_Points = mappedPlayer.ReceivingTDs * 6;
        double RUYD_Points = (double)mappedPlayer.RushingYards / 10;
        double RUTD_Points = mappedPlayer.RushingTDs * 6;
        double FUM_Points = mappedPlayer.FumblesLost * 2;

        double totalFantasyPoints = RUYD_Points + RUTD_Points + REYD_Points + RETD_Points - FUM_Points;
        double fantasyPointsPerGame = totalFantasyPoints / mappedPlayer.GamesPlayed;
        double totalFantasyPointsPPR = totalFantasyPoints + mappedPlayer.Receptions;
        double fantasyPointsPerGamePPR = totalFantasyPointsPPR / mappedPlayer.GamesPlayed;

        // TFP
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "TFP", Math.Round(totalFantasyPoints, 0));

        // FPPG
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "FPPG", Math.Round(fantasyPointsPerGame, 1));

        // TFPP 
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "TFPP", Math.Round(totalFantasyPointsPPR, 0));

        // FPGP
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "FPGP", Math.Round(fantasyPointsPerGamePPR, 1));
        return true;
      }
      else
      {
        return false;
      }

    }


    private bool ProcessK(MappedPlayer mappedPlayer)
    {
      Player currentPlayer = Player.GetPlayerByStatMapID(mappedPlayer.PlayerStatMapID);

      if (currentPlayer != null)
      {
        // GAM
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "GAM", mappedPlayer.GamesPlayed);

        // MAFG
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "MAFG", mappedPlayer.FieldGoalsMade);

        // MIFG
        int fieldGoalsMissed = mappedPlayer.FieldGoalAttempts - mappedPlayer.FieldGoalsMade;
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "MIFG", fieldGoalsMissed);

        // MAXP
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "MAXP", mappedPlayer.ExtraPointsMade);

        // MIXP
        int extraPointsMissed = mappedPlayer.ExtraPointAttempts - mappedPlayer.ExtraPointsMade;
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "MIXP", extraPointsMissed);


        /*****************************************/
        /*  Calculate TFP & FPPG & TFPP & FPGP   */
        /*****************************************/

        double MAFG_Points = mappedPlayer.FieldGoalsMade * 3;
        double MIFG_Points = fieldGoalsMissed * 3;
        double MAXP_Points = mappedPlayer.ExtraPointsMade * 1;
        double MIXP_Points = extraPointsMissed * 1;

        double totalFantasyPoints = MAFG_Points - MIFG_Points + MAXP_Points - MIXP_Points;
        double fantasyPointsPerGame = totalFantasyPoints / mappedPlayer.GamesPlayed;

        // TFP
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "TFP", Math.Round(totalFantasyPoints, 0));

        // FPPG
        SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat("FOO", _statSeasonCode, currentPlayer.PlayerID,
          "FPPG", Math.Round(fantasyPointsPerGame, 1));
        return true;
      }
      else
      {
        return false;
      }
    }






    private List<MappedPlayer> QueryOffenseSeasonStats()
    {
      List<MappedPlayer> mappedPlayers = new List<MappedPlayer>();

      // build the file location based on the season and week under consideration
      string fileLocation = Helpers.GetFOOStatFileLocation(SportSide.Offense, StatImportType.SEASONAL, _currentWeek, _statSeasonCode);
      if (!File.Exists(fileLocation))
      {
        return null;
      }

      // using the CSVReader object we spin through each line and process the data
      using (CSVReader csv = new CSVReader(@fileLocation))
      {
        string[] fields;
        while ((fields = csv.GetCSVLine()) != null)
        {
          mappedPlayers.Add(new MappedPlayer(fields[(int)StatLayout.FULLNAMELASTFIRST], fields[(int)StatLayout.TEAM], 
            int.Parse(fields[(int)StatLayout.PLAYERSTATMAPID]), int.Parse(fields[(int)StatLayout.TEAMSTATMAPID]), fields[(int)StatLayout.POSITION],
            int.Parse(fields[(int)StatLayout.GAMS]), int.Parse(fields[(int)StatLayout.COMP]),
            int.Parse(fields[(int)StatLayout.PATT]), int.Parse(fields[(int)StatLayout.PAYD]), int.Parse(fields[(int)StatLayout.INT]),
            int.Parse(fields[(int)StatLayout.PATD]), int.Parse(fields[(int)StatLayout.PA2P]), int.Parse(fields[(int)StatLayout.PSAC]),
            int.Parse(fields[(int)StatLayout.SCYD]), int.Parse(fields[(int)StatLayout.RATT]), int.Parse(fields[(int)StatLayout.RUYD]),
            int.Parse(fields[(int)StatLayout.RUTD]), int.Parse(fields[(int)StatLayout.RU2P]), int.Parse(fields[(int)StatLayout.RETG]),
            int.Parse(fields[(int)StatLayout.RREC]), int.Parse(fields[(int)StatLayout.REYD]), int.Parse(fields[(int)StatLayout.RETD]), 
            int.Parse(fields[(int)StatLayout.RE2P]),
            int.Parse(fields[(int)StatLayout.XPMA]), int.Parse(fields[(int)StatLayout.XPAT]), int.Parse(fields[(int)StatLayout.XPBL]),
            int.Parse(fields[(int)StatLayout.FGMA]), int.Parse(fields[(int)StatLayout.FGAT]), int.Parse(fields[(int)StatLayout.FGBL]),
            int.Parse(fields[(int)StatLayout.FG29]), int.Parse(fields[(int)StatLayout.FG39]), int.Parse(fields[(int)StatLayout.FG49]),
            int.Parse(fields[(int)StatLayout.FG50]), int.Parse(fields[(int)StatLayout.FUML])));
          }
      }

      return mappedPlayers;
    }



    private List<MappedDSTPlayer> QueryDefenseSeasonStats()
    {
      List<MappedDSTPlayer> mappedTeams = new List<MappedDSTPlayer>();

      // build the file location based on the season and week under consideration
      string fileLocation = Helpers.GetFOOStatFileLocation(SportSide.Defense, StatImportType.SEASONAL, _currentWeek, _statSeasonCode);
      if (!File.Exists(fileLocation))
      {
        return null;
      }

      // using the CSVReader object we spin through each line and process the data
      using (CSVReader csv = new CSVReader(@fileLocation))
      {
        string[] fields;
        while ((fields = csv.GetCSVLine()) != null)
        {
          mappedTeams.Add(new MappedDSTPlayer(fields[(int)DefStatLayout.CITY], fields[(int)DefStatLayout.TEAMABBREVIATION],
            int.Parse(fields[(int)DefStatLayout.TEAMID]), int.Parse(fields[(int)DefStatLayout.GAMES]), 
            int.Parse(fields[(int)DefStatLayout.POINTSALLOWED]), int.Parse(fields[(int)DefStatLayout.SACKS]),
            int.Parse(fields[(int)DefStatLayout.INTERCEPTIONS]), int.Parse(fields[(int)DefStatLayout.FUMBLERECS]), 
            int.Parse(fields[(int)DefStatLayout.SAFTIES]), int.Parse(fields[(int)DefStatLayout.DEFENSIVETDS])));
        }
      }

      return mappedTeams;
    }



    private class QBRecord
    {
      public int PlayerStatMapID { get; set; }
      public string Name { get; set; }
      public string Team { get; set; }
      public string Position { get; set; }
      public int GAMP { get; set; }
      public int GAMS { get; set; }
      public int COMP { get; set; }
      public int PAAT { get; set; }
      public int PAYD { get; set; }
      public int PATD { get; set; }
      public int RUYD { get; set; }
      public int RUTD { get; set; }
      public int FUM { get; set; }
      public int INT { get; set; }
      public int SAKD { get; set; }
      public int SKYD { get; set; }
    }


    private class RBRecord
    {
      public int PlayerStatMapID { get; set; }
      public string Name { get; set; }
      public string Team { get; set; }
      public string Position { get; set; }
      public int GAMP { get; set; }
      public int GAMS { get; set; }
      public int RUCA { get; set; }  // rushing carries
      public int RUYD { get; set; }
      public int RYPC { get; set; }  // rushing yards per carry (CALCULATED)
      public int RUTD { get; set; }
      public int RETR { get; set; }  // receiving targets
      public int RECP { get; set; }  // receptions
      public int REYD { get; set; }
      public int RETD { get; set; }
      public int FUM { get; set; }
    }


    private class WRRecord
    {
      public int PlayerStatMapID { get; set; }
      public string Name { get; set; }
      public string Team { get; set; }
      public string Position { get; set; }

      public int GAMP { get; set; }
      public int GAMS { get; set; }

      public int RUYD { get; set; }
      public int RUTD { get; set; }

      public int RETR { get; set; }  // receiving targets
      public int RECP { get; set; }  // receptions
      public int REYD { get; set; }
      public int RETD { get; set; }

      public int FUM { get; set; }
    }


    private class TERecord
    {
      public int PlayerStatMapID { get; set; }
      public string Name { get; set; }
      public string Team { get; set; }
      public string Position { get; set; }
      public int GAMP { get; set; }
      public int GAMS { get; set; }

      public int RUYD { get; set; }
      public int RUTD { get; set; }

      public int RETR { get; set; }  // receiving targets
      public int RECP { get; set; }  // receptions
      public int REYD { get; set; }
      public int RETD { get; set; }

      public int FUM { get; set; }

    }



    private class KRecord
    {
      public int PlayerStatMapID { get; set; }

      public string Name { get; set; }
      public string Team { get; set; }

      public string Position { get; set; }
      public int MAFG { get; set; }
      public int MIFG { get; set; }

      public int MAXP { get; set; }
      public int MIXP { get; set; }

      public int FG_0029 { get; set; }
      public int FG_3039 { get; set; }
      public int FG_4049 { get; set; }
      public int FG_50PL { get; set; }
    }




    private class DEFRecord
    {
      public int PlayerStatMapID { get; set; }
      public string Team { get; set; }
      public int FREC { get; set; }
      public int INT { get; set; }
      public int SACK { get; set; }
      public int DTD { get; set; }
      public int PA { get; set; }
      public int SAF { get; set; }
      public int BFG { get; set; }
    }

    protected void repUnfoundPlayers_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
      {
        MappedPlayer boundMappedPlayer = (MappedPlayer)e.Item.DataItem;

        Label labPlayerName = (Label)e.Item.FindControl("labPlayerName");
        Label labPlayerPosition = (Label)e.Item.FindControl("labPlayerPosition");
        Label labStatMapID = (Label)e.Item.FindControl("labStatMapID");

        labPlayerName.Text = boundMappedPlayer.FullNameLastFirst;
        labPlayerPosition.Text = boundMappedPlayer.Position;
        labStatMapID.Text = boundMappedPlayer.PlayerStatMapID.ToString();
      }
    }


    /// <summary>
    /// The layout of offensive statistics
    /// </summary>
    private enum StatLayout
    {
      FULLNAMELASTFIRST = 0,  // first name last first
      PLAYERSTATMAPID = 1,    // player stat map id
      TEAM = 2,               // team abbreviation
      //TEAMREPEAT = 3,      // team abbreviation 2

      TEAMSTATMAPID = 3,      // team abbreviation 2
      POSITION = 4,           // position

      //GAM = 5,      // games played
      GAMS = 5,     // games started

      COMP = 6,     // pass completions
      PATT = 7,     // pass attempts
      PAYD = 8,     // pass yards
      INT = 9,     // pass interceptions
      PATD = 10,    // pass touchdowns
      PA2P = 11,    // pass 2 point conversions
      PSAC = 12,    // pass sacks
      SCYD = 13,    // pass sack yards lost

      RATT = 14,    // rush attempts
      RUYD = 15,    // rush yards
      RUTD = 16,    // rush touchdowns
      RU2P = 17,    // rush 2 point conversions

      RETG = 18,    // receiving targets
      RREC = 19,    // receiving receptions
      REYD = 20,    // receiving yards
      RETD = 21,    // recieving touchdowns
      RE2P = 22,    // receiving 2 point conversions

      XPMA = 23,    // extra points made
      XPAT = 24,    // extra points attempts
      XPBL = 25,    // extra points blocked

      FGMA = 26,    // field goals made 
      FGAT = 27,    // field goal attempts 
      FGBL = 28,    // field goals blocked
      FG29 = 29,    // field goals 29 yards and under 
      FG39 = 30,    // field goals between 30 and 39 yards 
      FG49 = 31,    // field goals between 40 and 49 yards 
      FG50 = 32,    // field goals 50 yards and over 
      FUML = 33     // fumbles lost
    }


    /// <summary>
    /// The layout of defensive statistics, admittedly trimmed-down manually before importation
    /// </summary>
    private enum DefStatLayout
    {
      CITY = 0,
      TEAMABBREVIATION = 1,
      TEAMID = 2,
      GAMES = 3,
      POINTSALLOWED = 5,
      SACKS = 12,
      INTERCEPTIONS = 20,
      FUMBLERECS = 24,
      SAFTIES = 27,
      DEFENSIVETDS = 45
    }


    private class MappedDSTPlayer
    {
      public MappedDSTPlayer(string city, string teamAbbreviation, int teamStatMapID, int games, int pointsAllowed, int sacks,
        int interceptions, int fumbleRecoveries, int safeties, int defensiveTouchdowns)
      {
        this.City = city;
        this.TeamAbbreviation = teamAbbreviation;
        this.TeamStatMapID = teamStatMapID;
        this.Games = games;
        this.PointsAllowed = pointsAllowed;
        this.Sacks = sacks;
        this.Interceptions = interceptions;
        this.FumbleRecoveries = fumbleRecoveries;
        this.Safeties = safeties;
        this.DefensiveTouchdowns = defensiveTouchdowns;
      }

      public string City { get; set; }
      public string TeamAbbreviation { get; set; }
      public int TeamStatMapID { get; set; }
      public int Games { get; set; }
      public int PointsAllowed { get; set; }
      public int Sacks { get; set; }
      public int Interceptions { get; set; }
      public int FumbleRecoveries { get; set; }
      public int Safeties { get; set; }
      public int DefensiveTouchdowns { get; set; }
    }

    private class MappedPlayer
    {
      public MappedPlayer(string fullNameLastFirst, string teamAbbreviation, int playerStatMapID, int teamStatMapID, string position,
        int gamesStarted, int completions, int passAttempts, int passingYards, int interceptions, int passingTDs, int passing2PntConversions,
        int passingSacks, int passingSackYardsLost, int rushingAttempts, int rushingYards, int rushingTDs, int rushing2PntConversions, int receivingTargets,
        int receptions, int receivingYards, int receivingTDs, int receiving2PntConversions, int extraPointsMade, int extraPointAttempts, int extraPointsBlocked,
        int fieldGoalsMade, int fieldGoalAttempts, int fieldGoalsBlocked, int fieldGoals0029, int fieldGoals3039, int fieldGoals4049, int fieldGoals50PL,
        int fumblesLost)
      {
        this.FullNameLastFirst = fullNameLastFirst;
        this.TeamAbbreviation = teamAbbreviation;
        this.PlayerStatMapID = playerStatMapID;
        this.TeamStatMapID = teamStatMapID;
        this.Position = position;
        this.GamesPlayed = gamesStarted;
        //this.GamesStarted = gamesStarted;
        this.Completions = completions;
        this.PassAttempts = passAttempts;
        this.PassingYards = passingYards;
        this.Interceptions = interceptions;
        this.PassingTDs = passingTDs;
        this.Passing2PntConversions = passing2PntConversions;
        this.PassingSacks = passingSacks;
        this.PassingSackYardsLost = passingSackYardsLost;
        this.RushingAttempts = rushingAttempts;
        this.RushingYards = rushingYards;
        this.RushingTDs = rushingTDs;
        this.Rushing2PntConversions = rushing2PntConversions;
        this.ReceivingTargets = receivingTargets;
        this.Receptions = receptions;
        this.ReceivingYards = receivingYards;
        this.ReceivingTDs = receivingTDs;
        this.Receiving2PntConversions = receiving2PntConversions;
        this.ExtraPointsMade = extraPointsMade;
        this.ExtraPointAttempts = extraPointAttempts;
        this.ExtraPointsBlocked = extraPointsBlocked;
        this.FieldGoalsMade = fieldGoalsMade;
        this.FieldGoalAttempts = fieldGoalAttempts;
        this.FieldGoalsBlocked = fieldGoalsBlocked;
        this.FieldGoals0029 = fieldGoals0029;
        this.FieldGoals3039 = fieldGoals3039;
        this.FieldGoals4049 = fieldGoals4049;
        this.FieldGoals50PL = fieldGoals50PL;
        this.FumblesLost = fumblesLost;
      }

      public string FullNameLastFirst { get; set; }
      public string TeamAbbreviation { get; set; }
      public int PlayerStatMapID { get; set; }
      public int TeamStatMapID { get; set; }
      public string Position { get; set; }
      public int GamesPlayed { get; set; }
      //public int GamesStarted { get; set; }
      public int Completions { get; set; }

      public int PassAttempts { get; set; }
      public int PassingYards { get; set; }
      public int Interceptions { get; set; }
      public int PassingTDs { get; set; }
      public int Passing2PntConversions { get; set; }
      public int PassingSacks { get; set; }
      public int PassingSackYardsLost { get; set; }

      public int RushingAttempts { get; set; }
      public int RushingYards { get; set; }
      public int RushingTDs { get; set; }
      public int Rushing2PntConversions { get; set; }

      public int ReceivingTargets { get; set; }
      public int Receptions { get; set; }
      public int ReceivingYards { get; set; }
      public int ReceivingTDs { get; set; }
      public int Receiving2PntConversions { get; set; }

      public int ExtraPointsMade { get; set; }
      public int ExtraPointAttempts { get; set; }
      public int ExtraPointsBlocked { get; set; }

      public int FieldGoalsMade { get; set; }
      public int FieldGoalAttempts { get; set; }
      public int FieldGoalsBlocked { get; set; }

      public int FieldGoals0029 { get; set; }
      public int FieldGoals3039 { get; set; }
      public int FieldGoals4049 { get; set; }
      public int FieldGoals50PL { get; set; }

      public int FumblesLost { get; set; }
    }


  }
}