using System;
using System.IO;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
   public partial class FileUploader : System.Web.UI.UserControl
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         // this control can only work for authenticated users
         //if (!this.Page.User.Identity.IsAuthenticated)
         //   throw new SecurityException("Anonymous users cannot upload files.");

         lblFeedbackKO.Visible = false;
         lblFeedbackOK.Visible = false;
      }

      protected void btnUpload_Click(object sender, EventArgs e)
      {
         if (filUpload.PostedFile != null && filUpload.PostedFile.ContentLength > 0)
         {
            try
            {
               // if not already present, create a directory named /Uploads/<CurrentUserName>
               string dirUrl = (this.Page as BP.CheatSheetWarRoom.UI.BasePage).BaseUrl +
                  "Images/Sheets/SuppSourceLogos/";
               string dirPath = Server.MapPath(dirUrl);
               if (!Directory.Exists(dirPath))
                  Directory.CreateDirectory(dirPath);
               // save the file under the user's personal folder
               string fileUrl = dirUrl + "/" + Path.GetFileName(filUpload.PostedFile.FileName);
               filUpload.PostedFile.SaveAs(Server.MapPath(fileUrl));

               lblFeedbackOK.Visible = true;
               lblFeedbackOK.Text = "File successfully uploaded: " + fileUrl;
            }
            catch (Exception ex)
            {
               lblFeedbackKO.Visible = true;
               lblFeedbackKO.Text = ex.Message;
            }
         }
      }
   }
}