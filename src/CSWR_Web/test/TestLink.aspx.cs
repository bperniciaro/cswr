using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

public partial class test_TestLink : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      string test = SportSetting.GetSportSetting("FOO", "TSNDAY").SettingValue;
    }
}