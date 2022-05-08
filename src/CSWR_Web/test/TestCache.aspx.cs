using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

public partial class test_TestCache : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    for (int i = 0; i < 1000; i++)
    {
      var sportSeason = SportSeason.GetCurrentSportSeason("FOO");
    }
  }
}