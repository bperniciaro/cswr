using System;
using System.Web.UI;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class FantasyRacingLinks : BasePage
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      int activeTab = 0;
      if (Request.QueryString["DefaultPanel"] != null)
      {
        activeTab = ProcessQueryString(Request.QueryString["DefaultPanel"]);
      }

      string accordionScript = "$(document).ready(function () { \n" +
                           "   $(function() { \n" +
                           "       $(\"#accordion\").accordion({ \n" +
                           "        heightStyle: \"content\", \n" +
                           "        active: " + activeTab.ToString() + ", \n" +
                           "        collapsible: true \n" +
                           "       }); \n" +
                           "     }); \n" +
                           "   }); \n";

      ScriptManager.RegisterStartupScript(Page, Page.GetType(), "accordionScript", accordionScript, true);
    }

    private int ProcessQueryString(string queryString)
    {
      int activeTab = 0;
      switch (queryString)
      {
        case "Websites":
          activeTab = 0;
          break;
        case "Blogs":
          activeTab = 1;
          break;
        case "Other":
          activeTab = 2;
          break;
      }

      return activeTab;
    }

  }
}