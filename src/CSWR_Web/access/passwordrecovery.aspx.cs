using System;
using System.Web.Services;
using System.Configuration;
using System.Net;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class PasswordRecovery : BasePage
  {

    protected void Page_Init(object sender, EventArgs e)
    {
      if (this.User.Identity.IsAuthenticated)
      {
        Response.Redirect("~/");
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      
    }


    protected void SubmitButton_Click(object sender, EventArgs e)
    {

    }



    protected static string ReCaptcha_Key = ConfigurationManager.AppSettings["Google.ReCaptcha.SiteKey"];
    protected static string ReCaptcha_Secret = ConfigurationManager.AppSettings["Google.ReCaptcha.SecretKey"];

    [WebMethod]
    public static string VerifyCaptcha(string response)
    {
      string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
      return (new WebClient()).DownloadString(url);
    }
  }
}