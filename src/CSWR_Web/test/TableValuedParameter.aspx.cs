using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom;
using BP.CheatSheetWarRoom.BLL.Sheets;

public partial class test_TableValuedParameter : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {

    List<Position> positions = new List<Position>();
    Position qbPosition = Position.GetPosition("QB");
    Position rbPosition = Position.GetPosition("RB");

    positions.Add(qbPosition);
    positions.Add(rbPosition);

    List<Player> multiPositionPlayers = Player.GetPlayers(FOO.FOOString, "2013", positions, false, "TFP");
    gvPlayers.DataSource = multiPositionPlayers;
    gvPlayers.DataBind();
  }
}