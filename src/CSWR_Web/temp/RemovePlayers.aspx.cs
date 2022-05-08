using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

public partial class temp_RemovePlayers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        


        CheatSheet.RemoveCheatSheetItem(719878, 1700);
        CheatSheet.RemoveCheatSheetItem(721304, 1700);
    }
}