using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test_TestEmail : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    //MailMessage mail = new MailMessage();

    ////set the addresses 
    //mail.From = new MailAddress("accounts@cheatsheetwarroom.com");
    //mail.To.Add("expinch@yahoo.com");

    ////set the content 
    //mail.Subject = "This is an email from my local .net 5";
    //mail.Body = "This is from system.net.mail using C sharp with smtp authentication.";
    //SmtpClient smtp = new SmtpClient("smtp.gmail.com");

    ////NetworkCredential Credentials = new NetworkCredential("brad@fornitsweb.com", "hoh4MkDw7yyFCF");
    //NetworkCredential Credentials = new NetworkCredential("accounts@cheatsheetwarroom.com", "17jzdNRE@se^");
    //smtp.Credentials = Credentials;
    //smtp.EnableSsl = true;
    //smtp.Send(mail);
    //lblMessage.Text = "Mail Sent";


    /* Works */

   


  }

  protected void butSendEmail_Click(object sender, EventArgs e)
  {
    //var fromAddress = new MailAddress("accounts@cheatsheetwarroom.com", "CSWR Accounts");
    //const string fromPassword = "hoh4MkDw7yyFCF";

    var fromAddress = new MailAddress("admin@cheatsheetwarroom.com", "Cheat Sheet War Room Accounts");
    var toAddress = new MailAddress("expinch@yahoo.com", "Brad");
    const string subject = "interserver email test";
    const string body = "This is the body of a test email";

    var smtp = new SmtpClient
    {
      Host = tbHost.Text,
      Port = int.Parse(tbPort.Text),
      EnableSsl = true,
      DeliveryMethod = SmtpDeliveryMethod.Network,
      UseDefaultCredentials = false,
      Credentials = new NetworkCredential("brad@fornitsweb.com", "hoh4MkDw7yyFCF")
    };
    using (var message = new MailMessage(fromAddress, toAddress)
    {
      Subject = subject,
      Body = body
    })
    {
      try
      {
        smtp.Send(message);

      }
      catch (Exception ex)
      {
        if(ex.InnerException != null)
        {
          lblMessage.Text = ex.Message.ToString() + ex.InnerException.Message;
        }
        else
        {
          lblMessage.Text = ex.Message.ToString();
        }
      }
    }
  }
}