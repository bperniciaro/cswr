using System;
using System.Threading;

public partial class test_AjaxAnimationTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void butSubmit_Click(object sender, EventArgs e)
    {
      int nextCount = int.Parse(labCounter.Text) + 1;
      labCounter.Text = nextCount.ToString();
      Thread.Sleep(1500);
    }
}