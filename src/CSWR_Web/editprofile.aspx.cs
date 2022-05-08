using System;
using System.Text;
using System.Web.UI.WebControls;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class EditProfile : BasePage
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      upUserProfile.UserName = this.User.Identity.Name;
      // wire an event handler to catch messages
      upUserProfile.MessageEvent += new Globals.MessageEventHandler(userControl_MessageHandler);
    }

    public void userControl_MessageHandler(object sender, Globals.MessageBoxEventArgs e)
    {
      mbStatus.MessageType = e.MessageType;
      mbStatus.Message = new StringBuilder(e.Text);
      mbStatus.SetMessage();
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
      upUserProfile.SaveProfile();
    }

    protected void cpChangePassword_SendingMail(object sender, MailMessageEventArgs e)
    {
      if (Globals.CSWRSettings.ApplicationState == ApplicationState.Local.ToString().ToLower())
      {
        e.Cancel = true;
      }
    }

    protected void cpChangePassword_ChangedPassword(object sender, EventArgs e)
    {
      mbStatus.MessageType = MessageType.SUCCESS;
      mbStatus.Message = new StringBuilder("Password changed and confirmation sent to your email address.");
      mbStatus.SetMessage();
    }
  }
}