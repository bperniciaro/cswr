using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using BP.CheatSheetWarRoom.BLL.Forum;
using CarlosAg.ExcelXmlWriter;

public partial class temp_2_26_DetermineUserDifferences : System.Web.UI.Page
{

  protected void Page_Load(object sender, EventArgs e)
  {
    this.Page.Server.ScriptTimeout = 28400;



  }

  protected List<UserSubscriber> GetSubscribers()
  {
    List<UserSubscriber> notFoundUsers = new List<UserSubscriber>();

    MembershipUserCollection allCSWRUsers = Membership.GetAllUsers();
    labTotalUsers.Text = allCSWRUsers.Count.ToString();

    List<UserSubscriber> usersToExport = new List<UserSubscriber>();
    int exportUsers = 1;

    foreach (MembershipUser currentCSWRUser in allCSWRUsers)
    {
      if( (currentCSWRUser.CreationDate >= new DateTime(2019,7,13) && (currentCSWRUser.CreationDate <= new DateTime(2019,7,27) ) ) )
      {
        ProfileCommon userProfile = Profile.GetProfile(currentCSWRUser.UserName);
        usersToExport.Add(new UserSubscriber(currentCSWRUser.UserName, userProfile.EmailNotifications, userProfile.FirstName, currentCSWRUser.Email));
      }
    }

    //CsvExport<UserSubscriber> cvsUsers = new CsvExport<UserSubscriber>(usersToExport);
    //Response.Write(cvsUsers.Export());
    return usersToExport.Where(x => x.ReceiveNewsletter).ToList();
  }

  public void CustomSheet_ExportHandler(object sender, EventArgs e)
  {

    Workbook book = new Workbook();

    // Specify which Sheet should be opened and the size of window by 
    book.ExcelWorkbook.ActiveSheetIndex = 1;
    book.ExcelWorkbook.WindowTopX = 100;
    book.ExcelWorkbook.WindowTopY = 200;
    book.ExcelWorkbook.WindowHeight = 7000;
    book.ExcelWorkbook.WindowWidth = 8000;

    // Some optional properties of the Document
    book.Properties.Author = "Cheat Sheet War Room";
    book.Properties.Title = "Site Subscribers";
    book.Properties.Created = DateTime.Now;

    // Create the Default Style for all cells
    WorksheetStyle defaultStyle = book.Styles.Add("DefaultStyle");
    defaultStyle.Font.FontName = "Tahoma";
    defaultStyle.Font.Size = 10;
    defaultStyle.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
    defaultStyle.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
    defaultStyle.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
    defaultStyle.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);

    // Get a reference to the main sheet
    Worksheet sheet = book.Worksheets.Add("Subscribers");

    // Build the title in the first row
    WorksheetRow titleRow = sheet.Table.Rows.Add();
    WorksheetCell titleCell = titleRow.Cells.Add("Users");
    titleCell.MergeAcross = 2;
    titleCell.StyleID = "DefaultStyle";

    // Header Row

    WorksheetRow row = sheet.Table.Rows.Add();
    // Name
    sheet.Table.Columns.Add(new WorksheetColumn(30));
    row.Cells.Add(new WorksheetCell("Name", "DefaultStyle"));
    // Email
    sheet.Table.Columns.Add(new WorksheetColumn(30));
    row.Cells.Add(new WorksheetCell("Email", "DefaultStyle"));
    // Username
    sheet.Table.Columns.Add(new WorksheetColumn(35));
    row.Cells.Add(new WorksheetCell("Username", "DefaultStyle"));


    var subscriberList = GetSubscribers();

    foreach (UserSubscriber currentSubscriber in GetSubscribers())
    {
      // Stats

      row = sheet.Table.Rows.Add();
      // Name
      row.Cells.Add(new WorksheetCell(currentSubscriber.FirstName, "DefaultStyle"));
      // Email
      row.Cells.Add(new WorksheetCell(currentSubscriber.Email, "DefaultStyle"));
      // Username
      row.Cells.Add(new WorksheetCell(currentSubscriber.Username, "DefaultStyle"));
    }

    Response.ContentType = "application/vnd.ms-excel";
    Response.Charset = String.Empty;
    Response.AddHeader("content-disposition", "attachment;filename=subscribers.xls");
    Response.Clear();
    book.Save(Response.OutputStream);
    Response.End();


    // since we normally don't bind on postback, we have to do so explicitly here
    //BindCheatSheet(int.Parse(ddlAvailableSheets.SelectedValue));
  }

  //protected void butExportProblemUsers_Click(object sender, EventArgs e)
  //{
  //  List<UserPreference> usersToExport = new List<UserPreference>();

  //  MembershipUserCollection allCSWRUsers = Membership.GetAllUsers();
  //  labTotalUsers.Text = allCSWRUsers.Count.ToString();

  //  List<ForumMember> allForumMembers = ForumMember.GetForumMembers();

  //  foreach (MembershipUser currentCSWRUser in allCSWRUsers)
  //  {
  //    if (allForumMembers.SingleOrDefault(x => x.Login.ToLower() == currentCSWRUser.UserName.ToLower()) != null)
  //    {
  //    }
  //    else
  //    {

  //      ProfileCommon userProfile = Profile.GetProfile(currentCSWRUser.UserName);
  //      usersToExport.Add(new UserPreference(currentCSWRUser.UserName, userProfile.EmailNotifications, userProfile.FirstName, userProfile.EmailNotifications));
  //      // delete user not found in the forum
  //      //Membership.DeleteUser(currentCSWRUser.UserName);
  //    }
  //  }

  //  int numOfSubscribers = usersToExport.Where(x => x.ReceiveNewsletter == true).Count();

  //  CsvExport < UserPreference> cvsUsers = new CsvExport<UserPreference>(usersToExport);
  //  Response.Write(cvsUsers.Export());
  //}


  public class UserSubscriber
  {
    public UserSubscriber(string username, bool newsletter, string firstName, string email)
    {
      this.Username = username;
      this.ReceiveNewsletter = newsletter;
      this.FirstName = firstName;
      this.Email = email;
    }

    public string Username { get; set; }
    public bool ReceiveNewsletter { get; set; }
    public string FirstName { get; set; }
    public string Email { get; set; }
  }

}