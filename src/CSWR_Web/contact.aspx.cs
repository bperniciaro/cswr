using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Services;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class Contact : BasePage
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (Request.QueryString["Type"] != null)
        {
          switch (Request.QueryString["Type"])
          {
            case "Comment":
              ddlEmailType.SelectedValue = "General Comment";
              break;
            case "Advertising":
              ddlEmailType.SelectedValue = "Advertising Query";
              break;
            case "GeneralQuestion":
              ddlEmailType.SelectedValue = "General Question";
              break;
            case "Error":
              ddlEmailType.SelectedValue = "Report an Error";
              break;
          }
        }
        //mbInstructions.Message = new StringBuilder("If you have any suggestions or questions about this website, please use the form below to submit your query"); 
      }
    }

    protected static string ReCaptcha_Key = ConfigurationManager.AppSettings["Google.ReCaptcha.SiteKey"];
    protected static string ReCaptcha_Secret = ConfigurationManager.AppSettings["Google.ReCaptcha.SecretKey"];

    [WebMethod]
    public static string VerifyCaptcha(string response)
    {
      string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
      return (new WebClient()).DownloadString(url);
    }



    protected void txtSubmit_Click(object sender, EventArgs e)
    {

      if (tbSumCheck.Text == "7")
      {
        panFirstForm.Visible = false;
        panSecondForm.Visible = true;
      }
      else
      {
        labStatus.Text = "Please fill-out the form correctly.";
        panStatus.Visible = true;
        panStatus.CssClass = "alert alert-danger";
      }

    }



    protected void butSubmitName_Click(object sender, EventArgs e)
    {
      if (tbPlayerName.Text == "Brees")
      {
        try
        {
          // send the mail
          MailMessage msg = new MailMessage();
          msg.IsBodyHtml = false;
          msg.From = new MailAddress(tbEmail.Text, tbName.Text);
          msg.To.Add(new MailAddress(Globals.CSWRSettings.ContactForm.MailTo));
          if (!string.IsNullOrEmpty(Globals.CSWRSettings.ContactForm.MailCC))
            msg.CC.Add(new MailAddress(Globals.CSWRSettings.ContactForm.MailCC));
          msg.Subject = string.Format(Globals.CSWRSettings.ContactForm.MailSubject, tbTitle.Text);
          msg.Body = "From: " + tbEmail.Text + "\n" + tbBody.Text;
          new SmtpClient().Send(msg);
          // show a confirmation message, and reset the fields
          panStatus.CssClass = "alert alert-success";
          labStatus.Text = "Message successfully sent!";
          panStatus.Visible = true;
          panSecondForm.Visible = false;
          // clear all form fields
          tbName.Text = "";
          tbEmail.Text = "";
          tbTitle.Text = "";
          tbBody.Text = "";
        }
        catch (Exception)
        {
          labStatus.Text = "An unexpected error has occurred.  Please use the email address below to report the error.";
          panStatus.Visible = true;
          panStatus.CssClass = "alert alert-error";
        }
      }
      

    }
  }
}