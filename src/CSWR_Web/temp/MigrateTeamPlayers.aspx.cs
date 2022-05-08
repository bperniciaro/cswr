using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using BP.CheatSheetWarRoom;
using BP.CheatSheetWarRoom.BLL.Sheets;

public partial class temp_MigrateTeamPlayers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void butMovePlayers_Click(object sender, EventArgs e)
    {
      // get all 2014 players from current data source
      List<Player> all2014Players = Player.GetPlayers(FOO.FOOString, "2014", true);
      
      // spin through old DB to get player teams
      List<SportSeasonPlayerTeam> playerTeams2014 = new List<SportSeasonPlayerTeam>();
      foreach (Player currentPlayer in all2014Players)
      {
        using (SqlConnection cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["2014PlayerTeams"].ConnectionString))
        {
          SqlCommand selectCommand = new SqlCommand("SELECT * FROM Sheets_Players WHERE PlayerID = " + currentPlayer.PlayerID, cn);

          cn.Open();
          SqlDataReader myReader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
          if (myReader.Read())
          {
            playerTeams2014.Add(new SportSeasonPlayerTeam("FOO", "2014", currentPlayer.PlayerID, myReader["TeamCode"].ToString()));
          }

        }
      }

      // insert new player teams into current data source
      foreach (var playerTeam in playerTeams2014)
      {
        SportSeasonPlayerTeam.InsertSportSeasonPlayerTeam("FOO", "2014", playerTeam.PlayerID, playerTeam.TeamCode);
      }

    }
}