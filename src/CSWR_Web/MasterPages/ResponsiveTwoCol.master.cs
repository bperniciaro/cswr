using BP.CheatSheetWarRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace BP.CheatSheetWarRoom.UI
{
  public partial class ResponsiveTwoCol : System.Web.UI.MasterPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (Globals.isPageInSitemapTree("/fantasy-football/nfl/free/printable"))
      {
        liSecondaryNav_Printable.Attributes.Add("class", "active");
      }
      // Configure Secondary Navigation
      else if (Globals.isPageInSitemapTree("/fantasy-football/nfl/create"))
      {
        liSecondaryNav_Sheets.Attributes.Add("class", "active");
      }
      else if (Globals.isPageInSitemapTree("/fantasy-football/nfl/free/rankings/adp"))
      {
        liSecondaryNav_Adp.Attributes.Add("class", "active");
      }
      else if (Globals.isPageInSitemapTree("/fantasy-football/nfl/free/rankings"))
      {
        liSecondaryNav_Rankings.Attributes.Add("class", "active");
      }
      else if (Globals.isPageInSitemapTree("/fantasy-football/nfl/free/sleepers"))
      {
        liSecondaryNav_Sleepers.Attributes.Add("class", "active");
      }
      else if (Globals.isPageInSitemapTree("/fantasy-football/nfl/free/busts"))
      {
        liSecondaryNav_Busts.Attributes.Add("class", "active");
      }

    }
  }
}