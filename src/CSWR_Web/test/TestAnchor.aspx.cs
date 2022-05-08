using System;

public partial class test_TestAnchor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      hlTargetPlayer.NavigateUrl = "~/fantasyfootball/nfl/free/rankings/offense/quarterbacks.aspx#AndyDalton";
      hlTargetPlayer.Text = "Andy Dalton";
    }
}