using System;
using System.Collections.Generic;
using System.Linq;
using BP.CheatSheetWarRoom;
using BP.CheatSheetWarRoom.BLL.Sheets;

public partial class test_TestPlayerStatus : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    List<Player> testPlayers = Player.GetPlayers("FOO");
    List<PlayerStatusLog> playerLogs = new List<PlayerStatusLog>();

    foreach (Player currentPlayer in testPlayers)
    {
      //PlayerStatusLog myStatusLoss = new PlayerStatusLog();
      List<PlayerStatusLog> playerSpecificLogs = PlayerStatusLog.GetPlayerStatusLogs(currentPlayer.PlayerID, FOO.CurrentSeason);
      playerLogs.AddRange(playerSpecificLogs);
    }

    gvTeamChanges.DataSource = playerLogs.Where(x => x.SupplementalInfo != String.Empty);
    gvTeamChanges.DataBind();

  }

  public class TeamStatus
  {

    public string PlayerName { get; set; }
    //public string 
  }


}