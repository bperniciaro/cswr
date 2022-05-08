using System;
using System.IO;
using System.Web.Security;
using BP.CheatSheetWarRoom.CSV;

namespace BP.CheatSheetWarRoom.UI.Admin.Users
{
  public partial class ImportInvalidEmails : BasePage
  {

    protected void btnUpload_Click(object sender, EventArgs e)
    {
      if (filUpload.PostedFile != null && filUpload.PostedFile.ContentLength > 0)
      {
        try
        {
          // if not already present, create a directory named /Uploads/<CurrentUserName>
          string dirUrl = (this.Page as BP.CheatSheetWarRoom.UI.BasePage).BaseUrl + "TextFiles/InvalidEmails";
          string dirPath = Server.MapPath(dirUrl);
          if (!Directory.Exists(dirPath))
          {
            Directory.CreateDirectory(dirPath);
          }
          // save the file under the user's personal folder
          string fileUrl = dirUrl + "/" + Path.GetFileName(filUpload.PostedFile.FileName);
          string mappedServerPath = Server.MapPath(fileUrl);
          filUpload.PostedFile.SaveAs(mappedServerPath);

          ProcessList(mappedServerPath);

          //lblFeedbackOK.Visible = true;
          //lblFeedbackOK.Text = "File successfully uploaded: " + fileUrl;
        }
        catch (Exception ex)
        {
          lblFeedbackKO.Visible = true;
          lblFeedbackKO.Text = ex.Message;
        }
      }
    }

    private void ProcessList(string fileUrl)
    {
      MembershipUserCollection allUsers = Membership.GetAllUsers();
      int invalidEmailCounter = 0;
      int emailCounter = -1;

      // using the CSVReader object we spin through each line and process the data
      using (CSVReader csv = new CSVReader(fileUrl))
      {
        string[] fields;
        while ((fields = csv.GetCSVLine()) != null)
        {
          emailCounter++;
          foreach (MembershipUser currentUser in allUsers)
          {
            if (currentUser.Email == fields[0])
            {
              ProfileCommon targetProfile = Profile.GetProfile(currentUser.UserName);
              if (targetProfile.EmailValid == true)
              {
                targetProfile.EmailValid = false;
                targetProfile.Save();
                invalidEmailCounter++;
              }
            }
          }
        }
      }

      lblFeedbackOK.Text = invalidEmailCounter.ToString() + " invalid emails out of " + emailCounter.ToString() +" uploaded.";
    }
  
  
  }
}