using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.Admin.Sheets
{

  public partial class ArchiveUserSheets : BasePage
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      // this is a long data port, let's give it 4 hours to complete
      this.Page.Server.ScriptTimeout = 14400;
    }

   

    protected void butArchiveCheatSheets_Click(object sender, EventArgs e)
    {
      int groupCounter = 0;
      int errorCounter = 0;
      int sheetCounter = 0;
      string currrentFOOSeason = FOO.CurrentSeason;

      
      litSeasonCode.Text = currrentFOOSeason;

      // focus on sheets from the current year, no PPR
      DateTime kickoffDate = new DateTime();
      DateTime.TryParse(tbKickoffDate.Text.ToString(), out kickoffDate);

      List<CheatSheet> allRelevantCheatSheets = CheatSheet.GetCheatSheets(FOO.FOOString)   // only grade football sheets
                                                  .Where(x => x.Username != String.Empty)      // only grade user sheets
                                                  .Where(x => x.SeasonCode == currrentFOOSeason)  // only grade sheet for the current season
                                                  .Where(x => x.LastUpdated < kickoffDate)         // only grade sheets edited before the kickoff date
                                                  .Where(x => x.LastUpdated != x.Created)       // only grade sheets which were edited at least once
                                                  .Where(x => x.Positions.Count == 1)    // only grade single-position sheets
                                                  .Where(x => (bool)x.MappedProperties[CSProperty.PPRLeague.ToString()] == false) // only grade standard socring sheets
                                                  .ToList();

      foreach (var userSheetGroup in allRelevantCheatSheets.GroupBy(x => x.Username).OrderBy(x => x.Key))
      {

        int newCheatSheetID = 0;
        groupCounter++;

        // then group userSheets by position
        foreach (var targetUserPositionalSheetGroup in userSheetGroup.GroupBy(x => x.Positions[0].PositionCode).OrderBy(x => x.Key))
          {
          // finally limit the type of sheet returned to only 1 (the latest one ordered by date), must cast to list in order to avoid casting error, then take first item
          CheatSheet userTopPositionSheet = (CheatSheet)targetUserPositionalSheetGroup.OrderBy(x => x.CheatSheetID).OrderBy(x => x.Positions[0]).OrderByDescending(x => x.LastUpdated).Take(1).ToList()[0];
          newCheatSheetID = ArchiveCheatSheet(userTopPositionSheet);
          if (newCheatSheetID == 0)
          {
            errorCounter++;
          }
          else
          {
            sheetCounter++;

            ArchiveCheatSheetItems(userTopPositionSheet.CheatSheetID, newCheatSheetID);
          }
        }
      }

      butMovePlayers.Visible = false;
      litUserCount.Text = groupCounter.ToString();
      litErrors.Text = errorCounter.ToString();
      litSheetCount.Text = sheetCounter.ToString();
      panResults.Visible = true;
    }


    private int ArchiveCheatSheet(CheatSheet sheetToSave)
    {
      using (SqlConnection cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString))
      {
        string pprLeague = ((bool)sheetToSave.MappedProperties[CSProperty.PPRLeague.ToString()] == true) ? "1" : "0";

        SqlParameter userName = new SqlParameter("UserName", sheetToSave.Username);
        SqlParameter seasonCode = new SqlParameter("SeasonCode", sheetToSave.SeasonCode);
        SqlParameter sportcode = new SqlParameter("SportCode", FOO.FOOString);
        SqlParameter positionCode = new SqlParameter("PositionCode", sheetToSave.Positions[0].PositionCode);
        SqlParameter sheetName = new SqlParameter("SheetName", sheetToSave.SheetName);
        SqlParameter createdDate = new SqlParameter("Created", sheetToSave.Created.ToShortDateString());
        SqlParameter lastUpdated = new SqlParameter("LastUpdated", sheetToSave.LastUpdated.ToShortDateString());
        SqlParameter pprLeagueParam = new SqlParameter("PPRLeague", pprLeague);

        SqlCommand insertCommand = new SqlCommand("INSERT INTO Sheets_ArchivedCheatSheets (Username, SeasonCode, SportCode, PositionCode, SheetName, Created, LastUpdated, PPRLeague) " + 
                                                    "VALUES (@UserName, @SeasonCode, @SportCode, @PositionCode, @SheetName, @Created, @LastUpdated, @PPRLeague);" + 
                                                    "SELECT CAST(scope_identity() AS int)", cn);
        insertCommand.Parameters.Add(userName);
        insertCommand.Parameters.Add(seasonCode);
        insertCommand.Parameters.Add(sportcode);
        insertCommand.Parameters.Add(positionCode);
        insertCommand.Parameters.Add(sheetName);
        insertCommand.Parameters.Add(createdDate);
        insertCommand.Parameters.Add(lastUpdated);
        insertCommand.Parameters.Add(pprLeagueParam);

        cn.Open();
        return (int)insertCommand.ExecuteScalar();
      }

    }


    private void ArchiveCheatSheetItems(int oldCheatSheetID, int archivedCheatSheetID)
    {
      List<CheatSheetItem> targetCheatSheetItems = CheatSheetItem.GetCheatSheetItems(oldCheatSheetID);

      using (SqlConnection cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString))
      {

        foreach (CheatSheetItem targetItem in targetCheatSheetItems)
        {

          SqlParameter sheetID = new SqlParameter("ArchivedCheatSheetID", archivedCheatSheetID);
          SqlParameter playerID = new SqlParameter("PlayerID", targetItem.PlayerID);
          SqlParameter seqNo = new SqlParameter("Seqno", targetItem.Seqno);
          SqlParameter sleeperTag = new SqlParameter("SleeperTag", (bool)targetItem.MappedProperties[CSIProperty.Sleeper.ToString()]);
          SqlParameter bustTag = new SqlParameter("BustTag", (bool)targetItem.MappedProperties[CSIProperty.Bust.ToString()]); ;
          SqlParameter injuredTag = new SqlParameter("InjuredTag", (bool)targetItem.MappedProperties[CSIProperty.Injured.ToString()]); ;
          SqlParameter note = new SqlParameter("Note", targetItem.Note);

          SqlCommand insertCommand = new SqlCommand("INSERT INTO Sheets_ArchivedCheatSheetItems (ArchivedCheatSheetID, PlayerID, Seqno, SleeperTag, BustTag, InjuredTag, Note) " +
                                                      "VALUES (@ArchivedCheatSheetID, @PlayerID, @Seqno, @SleeperTag, @BustTag, @InjuredTag, @Note)", cn);
          insertCommand.Parameters.Add(playerID);
          insertCommand.Parameters.Add(seqNo);
          insertCommand.Parameters.Add(sleeperTag);
          insertCommand.Parameters.Add(bustTag);
          insertCommand.Parameters.Add(injuredTag);
          insertCommand.Parameters.Add(note);
          insertCommand.Parameters.Add(sheetID);

          cn.Open();
          insertCommand.ExecuteNonQuery();
          cn.Close();
        }

      }

    }

  }
}