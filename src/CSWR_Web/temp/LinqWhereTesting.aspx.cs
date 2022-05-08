using System;
using System.Collections.Generic;
using System.Linq;
using BP.CheatSheetWarRoom;
using BP.CheatSheetWarRoom.BLL.Sheets;

public partial class temp_LinqWhereTesting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

      string myString = FOO.FOOString;
      List<CheatSheet> fooSheets = CheatSheet.GetCheatSheets("FOO");
      int abcSheets = fooSheets.Where(x => x.SportCode == "ABC").Count();
    }
}