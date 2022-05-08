using System;
using BP.CheatSheetWarRoom;

public partial class MasterPages_NewLayout_Admin : System.Web.UI.MasterPage
{
  protected void Page_Init(object sender, EventArgs e)
  {
    Helpers.AddStyleSheetReferences(this.Page);
    Helpers.AddScriptReferences(this.Page);
  }

}
