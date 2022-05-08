using System;
using System.Text;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class Error : BasePage
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      StringBuilder errorMessage = new StringBuilder();
      errorMessage.Append("An unexpected error occurred.<br/>");

      if(this.Request.QueryString["code"] != null)  
      {
        switch(this.Request.QueryString["code"])  
        {
          case "404":
            errorMessage.Append("<strong>The requested page or resource was not found.</strong>");
            //Response.Redirect("https://www.cheatsheetwarroom.com");
            break;
          case "408":
            errorMessage.Append("<strong>Your request has timed out. This could be due to high traffic. Please try again.</strong>");
            break;
          case "505":
            errorMessage.Append("<strong>The server encountered an unexpected condition which prevented it from fulfilling the request. Please try again.</strong>");
            break;
        }
      }
      errorMessage.Append("<br/>An e-mail with details about this error has been sent to the administrator.");
      mbErrorMessage.Message = errorMessage;
      mbErrorMessage.MessageType = MessageType.ERROR;
    }
  }
}