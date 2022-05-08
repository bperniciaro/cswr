using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;
using BP.CheatSheetWarRoom;
using BP.CheatSheetWarRoom.BLL.Sheets;

public partial class temp_SaveCurrentPlayerTeams : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    LogTeams();
  }

  private void LogTeams()
  {
    List<Player> allCurrentPlayers = Player.GetPlayers(FOO.FOOString, "2013", false);
    foreach (Player currentPlayer in allCurrentPlayers)
    {
      using (SqlConnection cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("INSERT INTO Sheets_SportSeasonPlayerTeams VALUES ('FOO', '2013', " + currentPlayer.PlayerID.ToString() + ", '" + currentPlayer.TeamCode + "')");
        cmd.Connection = cn;
        cn.Open();

        cmd.ExecuteNonQuery();
      }
    }
  }

  
}