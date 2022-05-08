using System;
using BP.CheatSheetWarRoom.UI;

public partial class test_LayoutProblem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      SessionHandler.CurrentSportCode = "FOO";
    }
}