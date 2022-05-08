using System;

namespace BP.CheatSheetWarRoom.UI.Admin.Mods 
{
  public partial class ManageFOOPlayers : BasePage
  {
    protected void Page_Init(object sender, EventArgs e)
    {
      Helpers.AddStyleSheetReferences(this.Page);
      Helpers.AddScriptReferences(this.Page);
    }

  }
}