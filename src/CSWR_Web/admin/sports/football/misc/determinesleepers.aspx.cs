using System;
using System.Collections.Generic;
using System.Linq;
using BP.CheatSheetWarRoom;
using BP.CheatSheetWarRoom.BLL.Sheets;

public partial class admin_sports_football_determinesleepers : System.Web.UI.Page
{
  private string _currentSeason;

  protected void Page_Load(object sender, EventArgs e)
  {
    _currentSeason = FOO.CurrentSeason;

    // Quarterbacks
    gvQBSleepers.DataSource = GetRankingDifferences(FOOPositionsOffense.QB).Where(x => x.PlayerRanking < 20).Where(x => x.Differential > 7);
    gvQBSleepers.DataBind();

    // Running Backs
    gvRBSleepers.DataSource = GetRankingDifferences(FOOPositionsOffense.RB).Where(x => x.PlayerRanking < 30).Where(x => x.Differential > 7);
    gvRBSleepers.DataBind();

    // Wide Receivers
    gvWRSleepers.DataSource = GetRankingDifferences(FOOPositionsOffense.WR).Where(x => x.PlayerRanking < 30).Where(x => x.Differential > 7);
    gvWRSleepers.DataBind();

    // Tight Ends
    gvTESleepers.DataSource = GetRankingDifferences(FOOPositionsOffense.TE).Where(x => x.PlayerRanking < 15).Where(x => x.Differential > 7);
    gvTESleepers.DataBind();

    // Kickers
    gvKSleepers.DataSource = GetRankingDifferences(FOOPositionsOffense.K).Where(x => x.PlayerRanking < 10).Where(x => x.Differential > 7);
    gvKSleepers.DataBind();

    // Defenses
    gvDFSleepers.DataSource = GetRankingDifferences(FOOPositionsOffense.DF).Where(x => x.PlayerRanking < 10).Where(x => x.Differential > 7);
    gvDFSleepers.DataBind();
  }

  private List<RankingDifference> GetRankingDifferences(FOOPositionsOffense targetPosition)
  {
    // get the current player rankings 
    List<SportSeasonPlayerSeasonStat> currentPlayerRankings =
      SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, _currentSeason, targetPosition.ToString(), "TFP");
    int i = 1;
    List<Ranking> playerRankings = new List<Ranking>();
    foreach (SportSeasonPlayerSeasonStat targetStat in currentPlayerRankings)
    {
      playerRankings.Add(new Ranking(targetStat.PlayerID, i));
      i++;
    }

    // get the ADP log right before the season started
    DateTime targetADPLog = new DateTime(2013, 9, 8);

    List<ADPPlayerLog> currentPlayerADPs = ADPPlayerLog.GetADPPlayerLogs(FOO.FOOString, _currentSeason, targetPosition.ToString(), targetADPLog);
    i = 1;
    List<Ranking> adpRankings = new List<Ranking>();
    foreach (ADPPlayerLog targetLog in currentPlayerADPs)
    {
      adpRankings.Add(new Ranking(targetLog.PlayerID, i));
      i++;
    }

    List<RankingDifference> rankingDifferences = new List<RankingDifference>();
    foreach (Ranking targetPlayerRanking in playerRankings)
    {
      Ranking targetADPRanking = adpRankings.SingleOrDefault(x => x.PlayerID == targetPlayerRanking.PlayerID);
      if (targetADPRanking != null)
      {
        int rankingDifference = targetADPRanking.Rank - targetPlayerRanking.Rank;
        if (rankingDifference > 0)
        {
          rankingDifferences.Add(new RankingDifference(targetPlayerRanking.PlayerID, targetPlayerRanking.Rank, targetADPRanking.Rank, rankingDifference));
        }
      }
    }

    int maxPlayers = Helpers.GetMaxRankPlayersConsideredBySportPosition(FOO.FOOString, targetPosition.ToString());

    return rankingDifferences.Where(x => x.PlayerRanking < maxPlayers).OrderBy(x => x.PlayerRanking).ToList();
  }



  public class Ranking
  {
    public Ranking(int playerID, int rank)
    {
      this.PlayerID = playerID;
      this.Rank = rank;
    }

    public int PlayerID { get; set; }
    public int Rank { get; set; }
  }


  public class RankingDifference
  {
    public RankingDifference(int playerID, int playerRanking, int adpRanking, int differential)
    {
      this.PlayerID = playerID;
      this.PlayerRanking = playerRanking;
      this.ADPRanking = adpRanking;
      this.Differential = differential;
    }

    public int PlayerID { get; set; }
    public int PlayerRanking { get; set; }
    public int ADPRanking { get; set; }
    public int Differential { get; set; }

    public string Name
    {
      get
      {
        return Player.GetPlayer(this.PlayerID).FullNameLastFirst;
      }
    }
  }

}