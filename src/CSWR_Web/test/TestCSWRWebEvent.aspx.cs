using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class TestExceptions : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

      int cheatSheetID = 2;
      CheatSheet targetSheet = CheatSheet.GetCheatSheet(cheatSheetID);
      if (targetSheet != null)
      {

      }
      else
      {
        //new InvalidReferenceEvent("CheatSheet", cheatSheetID, null, new Exception).Raise();
        //Exception testException = new Exception();
      }
      try
      {
        int i = 0;
        int j = 5;
        int y = j / i;
      }
      catch (Exception ex)
      {

        new CSWRWebEvent("This is a message that could say just about anything.", Request.Url.ToString(), ex).Raise();
      }
    }
  }
}