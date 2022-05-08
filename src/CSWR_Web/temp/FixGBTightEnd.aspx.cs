using System;
using System.Collections.Generic;
using System.Linq;
using BP.CheatSheetWarRoom;
using BP.CheatSheetWarRoom.BLL.Sheets;

public partial class temp_FixGBTightEnd : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    // if the sheet contains both richards, remove the wrong one
    int rightAndWrongCount = 0;
    // if the sheet contains the wrong richard, but not the right richard, remove the wrong richard and replace him with the right richard
    int wrongAndNotRightCount = 0;

    foreach (CheatSheet targetSheet in CheatSheet.GetCheatSheets("FOO", "2014", "TE"))
    {
      CheatSheetItem rightRichard = targetSheet.Items.SingleOrDefault(x => x.PlayerID == 2746);
      CheatSheetItem wrongRichard = targetSheet.Items.SingleOrDefault(x => x.PlayerID == 1699);
      

      if( (rightRichard != null) && (wrongRichard != null) )  
      {
        CheatSheet.RemoveCheatSheetItem(targetSheet.CheatSheetID, wrongRichard.PlayerID);
        rightAndWrongCount++;
      }

      if ((rightRichard == null) && (wrongRichard != null))
      {
        // add false tags representing football tags
        Dictionary<string, object> emptyFootballTags = new Dictionary<string, object>();
        emptyFootballTags.Add(CSIProperty.Sleeper.ToString(), false);
        emptyFootballTags.Add(CSIProperty.Bust.ToString(), false);
        emptyFootballTags.Add(CSIProperty.Injured.ToString(), false);

        CheatSheetItem.InsertCheatSheetItem(new CheatSheetItem(targetSheet.CheatSheetID, 2746, wrongRichard.Seqno, String.Empty, emptyFootballTags));
        CheatSheet.RemoveCheatSheetItem(targetSheet.CheatSheetID, wrongRichard.PlayerID);
        wrongAndNotRightCount++;

      }

      labRightAndWrongCount.Text = rightAndWrongCount.ToString();
      labWrongAndNotRightCount.Text = wrongAndNotRightCount.ToString();

    }

  }
}