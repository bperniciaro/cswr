using System;
using System.Collections.Generic;
using System.Linq;
using BP.CheatSheetWarRoom.DAL;

namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class ADPManager : SheetItem
  {

    /// <summary>
    /// </summary>
    public static void CalculateADP(string sportCode, string statSeasonCode, string positionCode, int timespanInDays)
    {
      string sheetSeason = String.Empty;

      // get all relevant players from the last season
      List<Player> allPlayers = Player.GetPlayersBySportSeasonPositionCodes(sportCode, statSeasonCode, positionCode, false, true);

      // determine the year of the sheet that the ADPs will be loaded-upon
      sheetSeason = SportSeason.GetCurrentSportSeason(sportCode).SeasonCode;

      // we'll use CSWR player rankings when processing a sheet where the player is not ranked
      SupplementalSource cswrSource = SupplementalSource.GetSupplementalSource("CSWR");
      SupplementalSheet cswrSupplementalSheet = SupplementalSheet.GetSupplementalSheet(sheetSeason, cswrSource.SupplementalSourceID, sportCode, positionCode);
      List<SupplementalSheetItemDetails> cswrSuppSheetItems = SiteProvider.Sheets.GetSupplementalSheetItems(cswrSupplementalSheet.SupplementalSheetID);

      // get all cheat sheets for the particular sport/position (FOO sheets may include multiple positions)
      List<CheatSheet> allCheatSheets = CheatSheet.GetCheatSheets(sportCode, sheetSeason, positionCode).Where
                                                                 (x => (x.Username != String.Empty) &&
                                                                 (x.LastUpdated > DateTime.Now - new TimeSpan(timespanInDays, 0, 0, 0))).ToList();

      // if no sheets are found in the specified date range, just take the most recent 5 cheat sheets
    if (allCheatSheets.Count == 0)
      {
        allCheatSheets = CheatSheet.GetCheatSheets(sportCode, sheetSeason, positionCode)
                                                    .Where(x => (x.Username != String.Empty))
                                                    .OrderBy(x => x.LastUpdated)
                                                    .Take(5).ToList();
      }


      
      // limit cheat sheets based on the sport being considered
      List<CheatSheet> allRelevantCheatSheets = new List<CheatSheet>();
      switch (sportCode)
      {
        case Globals.FooString:
                      allRelevantCheatSheets = allCheatSheets.Where(x => 
                        ((x.Positions.Count == 1) && ((bool)x.MappedProperties[CSProperty.PPRLeague.ToString()] == false))).ToList();
          break;
        case Globals.RacString:
                      allRelevantCheatSheets = allCheatSheets;
          break;
      }

      // limit sheet consideration to those based on a single position
      List<CheatSheet> allRelevantPositionCheatSheets = allRelevantCheatSheets.Where(x => x.Positions.Count == 1).ToList();

      // build classes to keep track of ADP calculations, initially empty
      List<PlayerADPCalcs> allPlayerCalcs = new List<PlayerADPCalcs>();
      foreach (Player currentPlayer in allPlayers)
      {
        allPlayerCalcs.Add(new PlayerADPCalcs(currentPlayer.PlayerID, 0, 0));
      }
      
      TotalADPRankings(ref allRelevantCheatSheets, ref allPlayers, ref allPlayerCalcs, ref cswrSuppSheetItems, sportCode);

      // create a list of cheat sheets which we'll count to determine (administratively) how many days
      // worth of cheat sheets we'll consider when calculating ADP

      //switch (sportCode)
      //{
      //  case Globals.FOOString:
      //    sheetsFromLastThreeDays = allCheatSheets.Where
      //                  (x => (x.Username != String.Empty) &&
      //                  (x.Positions.Count == 1) &&
      //                  (x.LastUpdated > DateTime.Now - new TimeSpan(3, 0, 0, 0)) &&
      //                  ((bool)x.MappedProperties[CSProperty.PPRLeague.ToString()] == false)).ToList();
      //    break;
      //  case Globals.RACString:
      //    sheetsFromLastThreeDays = allCheatSheets.Where
      //                  (x => (x.Username != String.Empty) &&
      //                  (x.Positions.Count == 1) &&
      //                  (x.LastUpdated > DateTime.Now - new TimeSpan(3, 0, 0, 0))).ToList();
      //    break;
      //}

      //List<CheatSheet> sheetsFromLastThreeDays = allRelevantCheatSheets.Where(x => (x.LastUpdated > DateTime.Now - new TimeSpan(3, 0, 0, 0))).ToList();
      SaveADPRankings(sportCode, sheetSeason, statSeasonCode, positionCode, timespanInDays, ref allPlayerCalcs, ref allRelevantCheatSheets);

    }



    /// <summary>
    /// This method spin through all of the player rankings and adds them so that the ADP can be calculated
    /// </summary>
    /// <param name="allRelevantCheatSheets"></param>
    /// <param name="allPlayers"></param>
    /// <param name="allPlayerCalcs"></param>
    /// <param name="cswrSuppSheetItems"></param>
    /// <param name="sportCode"></param>
    private static void TotalADPRankings(ref List<CheatSheet> allRelevantCheatSheets, ref List<Player> allPlayers, 
                                  ref List<PlayerADPCalcs> allPlayerCalcs, ref List<SupplementalSheetItemDetails> cswrSuppSheetItems,
                                    string sportCode)  
    {

      // only process user sheets if there is at least one, otherwise use CSWR rankings
      if (allRelevantCheatSheets.Count > 0)
      {
        // spin through all relevant cheat sheets and tally the total rankings for each player
        foreach (CheatSheet currentSheet in allRelevantCheatSheets)
        {
          // get all items in the current, relevant sheet being processed
          List<CheatSheetItem> cheatSheetItems = CheatSheetItem.GetCheatSheetItems(currentSheet.CheatSheetID);

          // go through all players to see if they're in the current cheat sheet, if they are consider their
          // rank in the current sheet, if not use the CSWR rank
          foreach (Player currentPlayer in allPlayers)
          {
            // see if the player is found in this sheet
            CheatSheetItem targetPlayerItem = cheatSheetItems.SingleOrDefault(x => x.PlayerID == currentPlayer.PlayerID);

            // find the adp calculation object for this player
            PlayerADPCalcs targetADPCalc = allPlayerCalcs.SingleOrDefault(x => x.PlayerID == currentPlayer.PlayerID);

            // if the player being considered is in the relevant sheet being processed, sum their rank total and their rankCounter 
            if ((targetPlayerItem != null) && (targetADPCalc != null))
            {
              targetADPCalc.RankTotal += targetPlayerItem.Seqno;
              targetADPCalc.RankCounter++;
            }
            // if the player being considered was not found in the user sheet, we need to try to find the player in the CSWR supplemental rankings so 
            // we can determine the CSWR rank to use for this player
            else
            {
              // try to find the player in CSWR rankings
              SupplementalSheetItemDetails targetSuppSheetItem = cswrSuppSheetItems.SingleOrDefault(x => x.PlayerID == currentPlayer.PlayerID);

              if (targetSuppSheetItem != null)
              {
                targetADPCalc.RankTotal += targetSuppSheetItem.Seqno;
                targetADPCalc.RankCounter++;
              }
              // if the player isn't in the user's sheet, and isn't in the CSWR supp rankings, give the player a ranking equal to the worst ranking in the CSWR supp sheet
              else
              {
                targetADPCalc.RankTotal += cswrSuppSheetItems.Count;
                targetADPCalc.RankCounter++;
              }
            }
          }
        }
      }
      else
      {
        // go through all players to see where they are in the CSWR supp sheet
        foreach (Player currentPlayer in allPlayers)
        {

          // try to find the player in CSWR rankings
          SupplementalSheetItemDetails targetSuppSheetItem = cswrSuppSheetItems.SingleOrDefault(x => x.PlayerID == currentPlayer.PlayerID);

          // find the adp calculation object for this player
          PlayerADPCalcs targetADPCalc = allPlayerCalcs.SingleOrDefault(x => x.PlayerID == currentPlayer.PlayerID);

          if (targetSuppSheetItem != null)
          {
            targetADPCalc.RankTotal += targetSuppSheetItem.Seqno;
            targetADPCalc.RankCounter++;
          }
          // if the player isn't in the user's sheet, and isn't in the CSWR supp rankings, give the player a ranking equal to the worst ranking in the CSWR supp sheet
          else
          {
            targetADPCalc.RankTotal += cswrSuppSheetItems.Count;
            targetADPCalc.RankCounter++;
          }
        }
      }
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="sheetSeason"></param>
    /// <param name="statSeasonCode"></param>
    /// <param name="positionCode"></param>
    /// <param name="timespanInDays"></param>
    /// <param name="allPlayerCalcs"></param>
    /// <param name="allRelevantCheatSheets"></param>
    private static void SaveADPRankings(string sportCode, string sheetSeason, string statSeasonCode, string positionCode, int timespanInDays,
                                        ref List<PlayerADPCalcs> allPlayerCalcs, ref List<CheatSheet> allRelevantCheatSheets)
    {
      // Each round of ADP Calculation is stored in the database; we then use the index generated to tie this round
      // of ADP calculation to the player logs themselves
      

      int last24Sheets = allRelevantCheatSheets.Count(x => x.LastUpdated > (DateTime.Now - new TimeSpan(1, 0, 0, 0)));
      int last48Sheets = allRelevantCheatSheets.Count(x => x.LastUpdated > (DateTime.Now - new TimeSpan(2, 0, 0, 0)));
      int last72Sheets = allRelevantCheatSheets.Count(x => x.LastUpdated > (DateTime.Now - new TimeSpan(3, 0, 0, 0)));
      int totalSheetsConsidered = allRelevantCheatSheets.Count(x => x.LastUpdated > (DateTime.Now - new TimeSpan(timespanInDays, 0, 0, 0)));

      int adpCalculationID = ADPCalculation.InsertADPCalculation(sportCode, sheetSeason, positionCode, DateTime.Now, 
                                                                totalSheetsConsidered, last24Sheets, last48Sheets, last72Sheets, timespanInDays);

      // now that we have processed all of the players, we need to update all players' ADP in the database
      foreach (PlayerADPCalcs currentPlayerADPCalc in allPlayerCalcs)
      {
        double adp = currentPlayerADPCalc.GetADP();

        // if a particular player has an ADP, we need to update it
        if (!double.IsNaN(adp))
        {

          /******************/
          /* ADP Player Log */
          /******************/
          // insert into player adp log
          ADPPlayerLog.InsertADPPlayerLog(adpCalculationID, sportCode, sheetSeason.ToString(), currentPlayerADPCalc.PlayerID,
            currentPlayerADPCalc.GetADP(), DateTime.Now);


          // update the player's seasonal ADP, but first make sure they have an ADP.  if the player didn't record a stat the previous
          // year then they won't have an ADP yet so we need to insert it
          SportSeasonPlayerSeasonStat targetStat = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat(sportCode, statSeasonCode,
            currentPlayerADPCalc.PlayerID, "ADP");
          if (targetStat != null)
          {
            SportSeasonPlayerSeasonStat.UpdateSportSeasonPlayerSeasonStat(sportCode, statSeasonCode, currentPlayerADPCalc.PlayerID,
                "ADP", Math.Round(adp, 1));
          }
          else
          {
            SportSeasonPlayerSeasonStat.InsertSportSeasonPlayerSeasonStat(sportCode, statSeasonCode, currentPlayerADPCalc.PlayerID, "ADP", Math.Round(adp, 1));
          }



        }
      }

      BizObject.PurgeCacheItems("Sheets_LatestADPPlayerLogsBySportSeasonPosition_" + sportCode + "_" + sheetSeason + "_" + positionCode);
    }

  }
}
