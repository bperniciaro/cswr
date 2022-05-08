using System;
using System.Web.Security;

namespace BP.CheatSheetWarRoom.UI.Admin
{
    public partial class MakeEmailLists : System.Web.UI.Page
    {

        protected void butCreateEmailLists_Click(object sender, EventArgs e)
        {
            CreateEmailLists();
        }


        private void CreateEmailLists()
        {
            MembershipUserCollection allUsers = Membership.GetAllUsers();

            // maximum users we can import into MailChimp at one time
            //int maxRecipients = 2000;

            // count of recipients within one group
            //int recipientCount = 0;

            // count of all recipients
            int globalRecipientCount = 0;

            // sort all users by created date so we don't grab any new users
            //int userCount = 0;

            int invalidCount = 0;
            int validCount = 0;


            foreach (MembershipUser currentUser in allUsers)
            {
                if (Profile.GetProfile(currentUser.UserName).EmailValid)
                {
                    validCount++;
                }
                else
                {
                    invalidCount++;
                }

                // has the user opted-out of emailings?
                string emailAddress = currentUser.Email;

                // if the user has chosen to receive email notifications
                if (Profile.GetProfile(currentUser.UserName).EmailNotifications)
                {

                    if (Profile.GetProfile(currentUser.UserName).EmailValid)
                    {

                        //if (globalRecipientCount < maxRecipients)
                        //{
                        //    tbGroup1.Text += emailAddress + ", ";
                        //}
                        //else if ((globalRecipientCount >= maxRecipients) && (globalRecipientCount < (maxRecipients * 2)))
                        //{
                        //    tbGroup2.Text += emailAddress + ", ";
                        //}
                        //else if ((globalRecipientCount >= (maxRecipients * 2)) && (globalRecipientCount < (maxRecipients * 3)))
                        //{
                        //    tbGroup3.Text += emailAddress + ", ";
                        //}
                        //else if ((globalRecipientCount >= (maxRecipients * 3)) && (globalRecipientCount < (maxRecipients * 4)))
                        //{
                        //    tbGroup4.Text += emailAddress + ", ";
                        //}
                        //else if ((globalRecipientCount >= (maxRecipients * 4)) && (globalRecipientCount < (maxRecipients * 5)))
                        //{
                        //    tbGroup5.Text += emailAddress + ", ";
                        //}

                    }
                    //recipientCount++;
                    globalRecipientCount++;
                }

            }
            //userCount++;
            labSubscribers.Text = globalRecipientCount.ToString();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
        }




    }
}