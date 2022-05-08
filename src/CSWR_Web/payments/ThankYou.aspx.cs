using System;

public partial class payments_ThankYou : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

      if (Request.QueryString["ID"] != null)
      {
        litGuid.Text = Request.QueryString["ID"];
      }

      litUsername.Text = this.User.Identity.Name;

    }
}