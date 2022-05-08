using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.Admin.Sheets
{
  public partial class GradeUserSheets : BasePage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      this.Page.Server.ScriptTimeout = 20000;
      if (!IsPostBack)
      {
        tbSheetYear.Text = FOO.CurrentSeason;
      }
    }


    protected void butGrade_Click(object sender, EventArgs e)
    {
      // determine if we'll use the current season or the season entered by the user
      GradeAllSheets(tbSheetYear.Text != String.Empty ? tbSheetYear.Text : FOO.CurrentSeason);
    }


    private void GradeAllSheets(string seasonCode)
    {
      // QB
      GradePositionSheets(FOOPositionsOffense.QB, seasonCode);
      // RB
      GradePositionSheets(FOOPositionsOffense.RB, seasonCode);
      // WR
      GradePositionSheets(FOOPositionsOffense.WR, seasonCode);
      // TE
      GradePositionSheets(FOOPositionsOffense.TE, seasonCode);
      // K
      GradePositionSheets(FOOPositionsOffense.K, seasonCode);
      // DF
      GradePositionSheets(FOOPositionsOffense.DF, seasonCode);

      PopulateLeaderboard(seasonCode);

      mbStatus.Message = new StringBuilder("All sheets graded");
      mbStatus.MessageType = MessageType.SUCCESS;
      mbStatus.SetMessage();
    }


    private List<ArchivedCheatSheetRanking> GradePositionSheets(FOOPositionsOffense targetPosition, string seasonCode)
    {
      // get all archived cheat sheets for the specified position (only those that have been modified at some point)
      var allArchivedPositionalSheets = ArchivedCheatSheet.GetArchivedCheatSheets(FOO.FOOString, seasonCode, targetPosition.ToString())
        .Where(x => (x.LastUpdated != x.Created)).ToList();

      int sheetLimit = 0;
      if(tbSheetLimit.Text != String.Empty)
      {
        sheetLimit = int.Parse(tbSheetLimit.Text);
      }

      // if we want to limit the sheets processed, then only take the specified amount
      var archivedPositionalSheets = (cbLimitGradedSheets.Checked)
        ? allArchivedPositionalSheets.Take(sheetLimit).ToList()
        : allArchivedPositionalSheets;

      // create a dictionary for storing the rank differential for each player so we can average them out for comparison purposes
      Dictionary<int, PlayerDifferential> positionalPlayerDifferentials = new Dictionary<int, PlayerDifferential>();

      // process all of the user sheets 
      var processedSheets = ProcessPositionalSheets(archivedPositionalSheets, targetPosition, seasonCode, positionalPlayerDifferentials);

      // save the positional user grades
      SaveSheetPositionGrades(archivedPositionalSheets, positionalPlayerDifferentials, seasonCode, targetPosition, processedSheets);

      // store the player differentials
      SavePlayerDifferenials(positionalPlayerDifferentials, seasonCode, targetPosition);

      return processedSheets;
    }


    private List<ArchivedCheatSheetRanking> ProcessPositionalSheets(List<ArchivedCheatSheet> archivedPositionalSheets, FOOPositionsOffense targetPosition,
                                                                    string seasonCode, Dictionary<int, PlayerDifferential> positionalPlayerDifferentials)
    {
      // create a new list for storing processed sheets along with their total rank differential
      var processedSheetRanking = new List<ArchivedCheatSheetRanking>();

      // create a collection of players ranked by total points
      var positionalSeasonStatRankings = LoadPositionalSeasonStats(seasonCode, targetPosition);

      // determine the maximum number of top players we want to hold users responsible for
      var maxRankedPlayersConsidered = Helpers.GetMaxRankPlayersConsideredBySportPosition(FOO.FOOString, targetPosition.ToString());

      // we don't want to grade as many players as CSWR ranks because they'll be lots of 'corner cases' toward, so we use about half
      var maxCheatSheetItemsGraded = Helpers.GetUserSheetGradedItemsBySportPosition(FOO.FOOString, targetPosition.ToString());

      var afreseQbs = new Dictionary<string, int>();

      // spin through all archived sheets so we can calculate the total rank differential for each sheet
      foreach (ArchivedCheatSheet targetSheet in archivedPositionalSheets)
      {

        var totalRankDifferential = 0;

        for (var i = 0; i < maxCheatSheetItemsGraded; i++)
        {

          var playerRankDifferential = 0;

          if (targetSheet.Items.Count > i)
          {

            // if we find the targetted player, determine the rank differential
            var targetRankedPlayer = positionalSeasonStatRankings.SingleOrDefault(x => x.PlayerID == targetSheet.Items[i].PlayerID);

            // if the user's player recorded a stat, determine the rank differential and add it to the running count
            if (targetRankedPlayer != null)
            {
              // we want to use the true difference, but we also want to set a cap equal to the max grading
              // size for the respective position
              var relativeRankDifferential = Math.Abs(targetSheet.Items[i].Seqno - targetRankedPlayer.Rank);
              // if the user is farther off than the maximum number of items scored for that position, then just give them the max 
              // sheet size for that position
              playerRankDifferential = (relativeRankDifferential < maxRankedPlayersConsidered) ? relativeRankDifferential : maxRankedPlayersConsidered;
            }
            // if the user's player didn't record a stat, we'll assign a default differential equal to the maximum 
            // grading size minus theuser's rank for that player
            else
            {
              // if we cannot find the targetted player in year-end rankings, determine the rank differential by using the 
              // default page size for this position minus the user's rank for the targetted player
              playerRankDifferential = maxRankedPlayersConsidered - targetSheet.Items[i].Seqno;
            }

            // determine the differential by player so that we can average them later and use them as a
            // baseline for which to determine who made good player-specific picks
            if (positionalPlayerDifferentials.ContainsKey(targetSheet.Items[i].PlayerID))
            {
              var targetPlayerDifferential = positionalPlayerDifferentials[targetSheet.Items[i].PlayerID];
              //if (targetSheet.Items[i].PlayerID == 177)
              //{

              //}
              targetPlayerDifferential.DifferentialTotal += playerRankDifferential;
              targetPlayerDifferential.InstanceCount += 1;
              positionalPlayerDifferentials[targetSheet.Items[i].PlayerID] = targetPlayerDifferential;
            }
            else
            {
              positionalPlayerDifferentials[targetSheet.Items[i].PlayerID] = new PlayerDifferential(playerRankDifferential, 1);
            }

            totalRankDifferential += playerRankDifferential;

          }
          else
          {
            // if the user's sheet doesn't have enough players to grade, each additional spot will graded by
            // using the max positional grading size for the respective sheet
            totalRankDifferential += maxCheatSheetItemsGraded;
          }

          //if (targetSheet.Username == "afrese")
          //{
          //  afreseQbs.Add(targetSheet.Items[i].Player.FullName, playerRankDifferential);
          //}

        }
        processedSheetRanking.Add(new ArchivedCheatSheetRanking(targetSheet, totalRankDifferential));
      }

      return processedSheetRanking;
    }


    private void SaveSheetPositionGrades(List<ArchivedCheatSheet> archivedPositionalSheets, Dictionary<int, PlayerDifferential> positionalPlayerDifferentials, 
                                                string seasonCode, FOOPositionsOffense targetPosition, List<ArchivedCheatSheetRanking> sheetRankings)
    {
      // sort sheet rankings by rank differential then split into percentage point groups
      var archivedCheatSheetRankings = sheetRankings.OrderBy(x => x.TotalRankDifferential).ToList();
      int rankCounter = 1;

      // figure out how many sheets will fit into each grading percentage point
      decimal sheetsPerPercentagePointDecimal = archivedPositionalSheets.Count / 100m;
      decimal sheetsPerPercentagePoint = Math.Round(sheetsPerPercentagePointDecimal, 0, MidpointRounding.AwayFromZero);

      foreach (ArchivedCheatSheetRanking targetCheatSheet in archivedCheatSheetRankings)
      {
        int sheetScore = CalculateSheetScore(rankCounter, archivedCheatSheetRankings.Count());
        UserSheetPositionGrade.InsertUserSheetPositionGrade(seasonCode, FOO.FOOString, targetPosition.ToString(),
          targetCheatSheet.ArchivedSheet.ArchivedCheatSheetID, targetCheatSheet.ArchivedSheet.Username, targetCheatSheet.TotalRankDifferential, sheetScore, rankCounter);
        rankCounter++;
      }

    }

    /// <summary>
    /// Calculate the average player differential so that we'll have a baseline for grading user picks
    /// </summary>
    /// <param name="positionalPlayerDifferentials"></param>
    /// <param name="seasonCode"></param>
    /// <param name="targetPosition"></param>
    private void SavePlayerDifferenials(Dictionary<int, PlayerDifferential> positionalPlayerDifferentials, string seasonCode, FOOPositionsOffense targetPosition)
    {
      // determine the average rank differential by player, which is the the average of how far-off each user sheet ranking
      // is off for each player
      foreach (KeyValuePair<int, PlayerDifferential> targetDifferential in positionalPlayerDifferentials)
      {
        decimal averageDifferential = 0;
        int playerID = targetDifferential.Key;

        //if (playerID == 177)
        //{

        //}


        Player actualPlayer = Player.GetPlayer(playerID);

        averageDifferential = (decimal)targetDifferential.Value.DifferentialTotal / targetDifferential.Value.InstanceCount;

        UserSheetPlayerDifferential.InsertUserSheetPlayerDifferential(seasonCode, FOO.FOOString, targetPosition.ToString(), actualPlayer.PlayerID, averageDifferential);
        int i = 0;
      }

    }



    private void PopulateLeaderboard(string seasonCode)
    {
      List<UserSheetPositionGrade> userGrades = UserSheetPositionGrade.GetUserSheetPositionGrades(FOO.FOOString, seasonCode);
      List<UserSportSeasonLeaderboard> userLeaderboard = new List<UserSportSeasonLeaderboard>();


      foreach (MembershipUser targetUser in Membership.GetAllUsers())
      {
        List<UserSheetPositionGrade> kSheets = userGrades.Where(x => x.PositionCode == FOOPositionsOffense.K.ToString()).ToList();

        List<UserSheetPositionGrade> userSheets = userGrades.Where(x => x.UserName == targetUser.UserName).ToList();

        // make sure the user created at least one sheet
        if (userSheets.Count > 0)
        {

          int qbScore = 0;
          int rbScore = 0;
          int wrScore = 0;
          int teScore = 0;
          int kScore = 0;
          int dfScore = 0;

          // Find QB Grade
          try
          {
            UserSheetPositionGrade qbGrade =
              userSheets.SingleOrDefault(x => x.PositionCode == FOOPositionsOffense.QB.ToString());
            if (qbGrade != null)
            {
              qbScore = qbGrade.Score;
            }
          }
          catch (Exception ex)
          {


          }

          // Find RB Grade
          UserSheetPositionGrade rbGrade = new UserSheetPositionGrade();
          try
          {
            rbGrade =
              userSheets.SingleOrDefault(x => x.PositionCode == FOOPositionsOffense.RB.ToString());
          }
          catch (Exception ex)
          {

          }

          if (rbGrade != null)
          {
            rbScore = rbGrade.Score;
          }

          // Find WR Grade
          UserSheetPositionGrade wrGrade = userSheets.SingleOrDefault(x => x.PositionCode == FOOPositionsOffense.WR.ToString());
          if (wrGrade != null)
          {
            wrScore = wrGrade.Score;
          }

          // Find TE Grade
          UserSheetPositionGrade teGrade = userSheets.SingleOrDefault(x => x.PositionCode == FOOPositionsOffense.TE.ToString());
          if (teGrade != null)
          {
            teScore = teGrade.Score;
          }

          // Find K Grade
          UserSheetPositionGrade kGrade = userSheets.SingleOrDefault(x => x.PositionCode == FOOPositionsOffense.K.ToString());
          if (kGrade != null)
          {
            kScore = kGrade.Score;
          }

          // Find DF Grade
          UserSheetPositionGrade dfGrade = userSheets.SingleOrDefault(x => x.PositionCode == FOOPositionsOffense.DF.ToString());
          if (dfGrade != null)
          {
            dfScore = dfGrade.Score;
          }

          int totalDifferential = qbScore + rbScore + wrScore + teScore + kScore + dfScore;

          userLeaderboard.Add(new UserSportSeasonLeaderboard(0, FOO.FOOString, seasonCode, targetUser.UserName, qbScore,
                                    rbScore, wrScore, teScore, kScore, dfScore, totalDifferential, 0));

        }

      }

      int overallRank = 1;
      foreach (UserSportSeasonLeaderboard targetItem in userLeaderboard.OrderByDescending(x => x.OverallScore))
      {
        UserSportSeasonLeaderboard.InsertUserSportSeasonLeaderboard(FOO.FOOString, seasonCode, targetItem.Username, targetItem.QBScore,
            targetItem.RBScore, targetItem.WRScore, targetItem.TEScore, targetItem.KScore, targetItem.DFScore,
            targetItem.OverallScore, overallRank);
        overallRank++;
      }
    }


    public List<Ranking> LoadPositionalSeasonStats(string seasonCode, FOOPositionsOffense targetPosition)
    {
      var positionalSeasonStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(FOO.FOOString, seasonCode, targetPosition.ToString(), "TFP");
      var positionalSeasonStatRankings = new List<Ranking>();
      var rank = 1;
      foreach (var targetStat in positionalSeasonStats)
      {
        positionalSeasonStatRankings.Add(new Ranking(targetStat.PlayerID, rank));
        rank++;
      }
      
      return positionalSeasonStatRankings;
    }



    private int CalculateSheetScore(int rank, int totalSheets)
    {
      int score = 100 - ((100 * (rank - 1)) / (totalSheets - 1));
      return score;
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

    public class ArchivedCheatSheetRanking
    {

      public ArchivedCheatSheetRanking(ArchivedCheatSheet archivedSheet, int totalRankDifferential)
      {
        this.ArchivedSheet = archivedSheet;
        this.TotalRankDifferential = totalRankDifferential;
      }

      public ArchivedCheatSheet ArchivedSheet { get; set; }
      public int Grade { get; set; }
      public int TotalRankDifferential { get; set; }

    }

    public class PlayerDifferential
    {
      public PlayerDifferential(int differentialTotal, int instanceCount)
      {
        this.DifferentialTotal = differentialTotal;
        this.InstanceCount = instanceCount;
      }

      public int DifferentialTotal { get; set; }
      public int InstanceCount { get; set; }
    }

}
}