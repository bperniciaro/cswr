using System;
using System.Configuration;
using System.Net;
using System.Web.Services;

public partial class test_2_37_TestRecaptcha : System.Web.UI.Page
{


  protected void Page_Load(object sender, EventArgs e)
  {
    labSecret.Text = ReCaptcha_Secret;
  }

  protected static string ReCaptcha_Key = ConfigurationManager.AppSettings["Google.ReCaptcha.SiteKey"];
  protected static string ReCaptcha_Secret = ConfigurationManager.AppSettings["Google.ReCaptcha.SecretKey"];

  [WebMethod]
  public static string VerifyCaptcha(string response)
  {
    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
    string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
    string result = (new WebClient()).DownloadString(url);
    
    return result;
  }

  protected void btn_Click(object sender, EventArgs e)
  {

  }

}