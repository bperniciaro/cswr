using System;
using System.Net;

public partial class test_TinyMCE_TestScriptPlacement : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {

  }

  protected void butSubmit_Click(object sender, EventArgs e)
  {
    string htmlEncoded = WebUtility.HtmlEncode(tbTest.Text);

    string htmlDecoded = WebUtility.HtmlDecode(htmlEncoded);
    tbDefault.Text = htmlDecoded;

    litOnPageContent.Text = htmlDecoded;
  }
}