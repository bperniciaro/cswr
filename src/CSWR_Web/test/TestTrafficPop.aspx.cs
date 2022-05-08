using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test_TestTrafficPop : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    string facebookTrafficPop = "$(document).ready(function() { \n" +
        "$().facebookTrafficPop({ \n" +
          "timeout: 0, \n" +
          "delay: 0, \n" +
          "title: \"Cheat Sheet War Room\", \n " +
          "message: 'Like this page or share it on Facebook and enter our competition to win free <span>test</test> <a rel=\"nofollow\" href=\"https://www.fantasyjocks.com?rfsn=105771.96046&subid=traffic-pop\"></a> Fantasy Jocks merchandise!<center><img src=\"https://www.cheatsheetwarroom.com/images/addons/trafficpop/fantasyjocksring.jpg\" border=\"0\" style=\"margin:10px 0px;\" /></center>', \n" +
          "url: \"https://www.cheatsheetwarroom.com/fantasy-football/nfl/create/custom-sheet.aspx\", \n" +
          "share_url: 'https://www.cheatsheetwarroom.com/fantasy-football/nfl/create/custom-sheet.aspx', \n" +
          "closeable: true \n" +
          "} \n" +
        " ); \n" +
        "});";

    ScriptManager.RegisterStartupScript(this, Page.GetType(), "traffiPop", facebookTrafficPop, true);
  }
}