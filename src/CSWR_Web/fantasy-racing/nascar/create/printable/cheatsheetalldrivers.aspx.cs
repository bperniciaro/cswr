using System;
using System.Text;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class CheatSheetAllDrivers : System.Web.UI.Page
  {

    protected void Page_Init(object sender, EventArgs e)
    {
      Helpers.AddStyleSheetReferences(this);
    }

    private int _cheatSheetID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (ProcessQueryString())
      {
        ppstSheetTemplate.CheatSheetID = _cheatSheetID;
      }
      else
      {
        panPrintableSheetContainer.Visible = false;
        mbError.Message = new StringBuilder("No sheet specified.");
        mbError.MessageType = MessageType.ERROR;
        mbError.SetMessage();
      }
    }

    private bool ProcessQueryString()
    {
      bool success = false;

      // try to part the SheetID
      if (Request.QueryString["SheetID"] != null)
      {
        if (int.TryParse(Request.QueryString["SheetID"], out _cheatSheetID))
        {
          success = true;
        }
      }
      return success;
    }


  }

}