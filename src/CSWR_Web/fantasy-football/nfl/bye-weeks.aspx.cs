using System;
using System.Web;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class ByeWeeks : BasePage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      string currentUrl = HttpContext.Current.Request.Path.ToLower();

      Response.Status = "301 Moved Permanently";
      Response.AddHeader("Location", "https://www.cheatsheetwarroom.com/blog/football/bye-weeks");
      Response.End();
    }

  }
}