using BP.CheatSheetWarRoom;
using BP.CheatSheetWarRoom.BLL.Sheets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class temp_2_40_RemovetMostert : System.Web.UI.Page
{

  protected void Page_Load(object sender, EventArgs e)
  {
    int mostertPlayerId = 2952;
    var currentCheatSheets = CheatSheet.GetCheatSheets(Globals.FooString).Where(x => x.LastUpdated > new DateTime(2018, 7, 1));
    int cheatSheetCount = 0;
    int cheatSheetcount2 = 0;
    foreach(var currentSheet in currentCheatSheets)
    {
      var mostertCheatSheetItem = currentSheet.Items.Find(x => x.PlayerID == 2952);
      if(mostertCheatSheetItem != null)
      {
        if(currentSheet.Positions.Count == 1)
        {
          if (currentSheet.Positions[0].PositionCode != "RB")
          {
            CheatSheet.RemoveCheatSheetItem(currentSheet.CheatSheetID, mostertPlayerId);
            //currentSheet.Items.Remove(currentSheet.Items.SingleOrDefault(x => x.PlayerID == mostertPlayerId));
            cheatSheetCount++;
          }
        }
        else if(currentSheet.Positions.Count > 1 && !currentSheet.Positions.Contains(Position.GetPosition("RB")))
        {
          //currentSheet.Items.Remove(currentSheet.Items.SingleOrDefault(x => x.PlayerID == mostertPlayerId));
          cheatSheetcount2++;
        }
      }
      labCount.Text = cheatSheetCount.ToString();
      labCount2.Text = cheatSheetcount2.ToString();
    }

  }
} 
