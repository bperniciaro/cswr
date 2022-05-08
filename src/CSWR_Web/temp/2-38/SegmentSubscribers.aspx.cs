using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using BP.CheatSheetWarRoom;
using BP.CheatSheetWarRoom.BLL.Forum;
using BP.CheatSheetWarRoom.BLL.Sheets;
using CarlosAg.ExcelXmlWriter;

public partial class temp_2_26_DetermineUserDifferences : System.Web.UI.Page
{

  protected void Page_Load(object sender, EventArgs e)
  {
    this.Page.Server.ScriptTimeout = 28400;
    //var allSubscribers = GetSubscribers();

    //GetRecentSubscribers();
    //GetOldSubscribers();
    //GetSubscribedActiveCheatSheetUsers();

    //var newImportList = CombineRecentSubscribersAndRankgers();
    //var uniqueImportList = newImportList.GroupBy(p => p.Username).Select(x => x.First()).ToList();

    //List<UserSubscriber> reOptList = new List<UserSubscriber>();

    //foreach (var currentSubscriber in allSubscribers)
    //{
    //  if (newImportList.SingleOrDefault(x => x.Username == currentSubscriber.Username) == null)
    //  {
    //    reOptList.Add(currentSubscriber);
    //  }
    //}

    //var uniqueReOptList = reOptList.GroupBy(p => p.Username).Select(x => x.First()).ToList();
    //labInactiveSubscribers.Text = uniqueReOptList.Count.ToString();
  }

  protected List<UserSubscriber> GetSubscribers()
  {
    List<UserSubscriber> notFoundUsers = new List<UserSubscriber>();

    //var allCSWRUsers = Membership.GetAllUsers().Cast<MembershipUser>().Take(2000).ToList();
    var allCSWRUsers = Membership.GetAllUsers().Cast<MembershipUser>().ToList();
    labTotalUsers.Text = allCSWRUsers.Count.ToString();

    List<UserSubscriber> allSubscribers = new List<UserSubscriber>();

    foreach (MembershipUser currentCSWRUser in allCSWRUsers)
    {
      ProfileCommon userProfile = Profile.GetProfile(currentCSWRUser.UserName);
      if(userProfile.EmailNotifications)
      {
        allSubscribers.Add(new UserSubscriber(currentCSWRUser.UserName, userProfile.EmailNotifications,
            userProfile.FirstName, currentCSWRUser.Email, currentCSWRUser.CreationDate));
      }
    }

    labSubscribers.Text = allSubscribers.Count.ToString();

    return allSubscribers.Where(x => x.ReceiveNewsletter).ToList();
  }

  private List<UserSubscriber> GetRecentSubscribers()
  {
    var allSubscribers = GetSubscribers();
    var engagedSubscribers = allSubscribers.Where(x => x.AccountCreated >= new DateTime(2018, 7, 1)).ToList();
    labRecentSubscribers.Text = engagedSubscribers.Count.ToString();
    return engagedSubscribers;
  }

  private List<UserSubscriber> GetOldSubscribers()
  {
    var allSubscribers = GetSubscribers();
    var oldSubscribers = allSubscribers.Where(x => x.AccountCreated < new DateTime(2018, 7, 1)).ToList();
    labOldSubscribers.Text = oldSubscribers.Count.ToString();
    return oldSubscribers;
  }

  //private List<UserSubscriber> GetNonEngagedRegistrantsMinusActiveRankers()
  //{
  //  var nonEngagedSubsdribers = GetNonEngagedSubscribers();
  //  var activeRankerUsernames = GetSubscribedActiveCheatSheetUsers();

  //  List<UserSubscriber> nonEngagedRegistrantsMinusActiveRankers = new List<UserSubscriber>();
  //  foreach (var currentNonEngagedRegistrants in nonEngagedSubsdribers)
  //  {
  //    if (!activeRankerUsernames.Contains(currentNonEngagedRegistrants.Username))
  //    {
  //      nonEngagedRegistrantsMinusActiveRankers.Add(currentNonEngagedRegistrants);
  //    }
  //  }
  //  labNonEngagedRegistrantsMinusRankers.Text = nonEngagedRegistrantsMinusActiveRankers.Count.ToString();
  //  return nonEngagedRegistrantsMinusActiveRankers;
  //}

  protected List<UserSubscriber> GetSubscribedActiveCheatSheetUsers()
  {
    var currentCheatSheets = CheatSheet.GetCheatSheets(Globals.FooString).Where(x => x.LastUpdated > new DateTime(2018, 7, 1));
    var userCheatSheets = currentCheatSheets.Where(x => x.Username != String.Empty);
    
    List<UserSubscriber> subscribedActiveSheetUsers = new List<UserSubscriber>();
    foreach (CheatSheet currentSheet in userCheatSheets)
    {
      ProfileCommon userProfile = Profile.GetProfile(currentSheet.Username);
      MembershipUser currentUser = Membership.GetUser(currentSheet.Username);
      if(userProfile.EmailNotifications)
      {
        //var sheetUsername = ;
        subscribedActiveSheetUsers.Add(new UserSubscriber(currentSheet.Username, userProfile.EmailNotifications, userProfile.FirstName,
                                  currentUser.Email, currentUser.CreationDate));
      }
    }
    List<UserSubscriber> distinctSubscribedActiveSheet = subscribedActiveSheetUsers.GroupBy(p => p.Username).Select(x => x.First()).ToList();
    labSubscribedRankers.Text = distinctSubscribedActiveSheet.Count.ToString();
    return distinctSubscribedActiveSheet;
  }

  //protected List<string> GetActiveRankerUsernames()
  //{
  //  var currentCheatSheets = CheatSheet.GetCheatSheets(Globals.FooString).Where(x => x.LastUpdated > new DateTime(2018, 7, 1));

  //  List<string> activeSheetSheetUsers = new List<string>();
  //  foreach (CheatSheet currentSheet in currentCheatSheets)
  //  {
  //    //var sheetUsername = ;
  //    activeSheetSheetUsers.Add(currentSheet.Username);
  //  }

  //  labSubscribedRankers.Text = activeSheetSheetUsers.Count.ToString();
  //  return activeSheetSheetUsers;
  //}


  private List<UserSubscriber> CombineRecentSubscribersAndRankgers()
  {
    var recentSubscribers = GetRecentSubscribers();

    var activeRankers = GetSubscribedActiveCheatSheetUsers();

    List<UserSubscriber> combinedUsers = new List<UserSubscriber>();

    combinedUsers.AddRange(recentSubscribers);
    combinedUsers.AddRange(activeRankers);


    var uniqueCombinedUsers = combinedUsers.GroupBy(p => p.Username).Select(x => x.First()).ToList();
    labRecentSubscribersAndRankers.Text = uniqueCombinedUsers.Count.ToString();
    return uniqueCombinedUsers;

  }



  public void CustomSheet_ExportEngagers(object sender, EventArgs e)
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


    var subscriberList = CombineRecentSubscribersAndRankgers();

    foreach (UserSubscriber currentSubscriber in subscriberList)
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




  public void CustomSheet_ExportNonEngagers(object sender, EventArgs e)
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


    var engagedUsers = CombineRecentSubscribersAndRankgers();

    var uniqueImportList = engagedUsers.GroupBy(p => p.Username).Select(x => x.First()).ToList();

    var oldSubscribers = GetOldSubscribers();
    var reOptList = new List<UserSubscriber>();

    foreach (var oldSubscriber in oldSubscribers)
    {
      if (engagedUsers.SingleOrDefault(x => x.Username == oldSubscriber.Username) == null)
      {
        reOptList.Add(oldSubscriber);
      }
    }

    foreach (UserSubscriber currentSubscriber in reOptList)
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



  public class UserSubscriber
  {
    public UserSubscriber(string username, bool newsletter, string firstName, string email, DateTime accountCreated)
    {
      this.Username = username;
      this.ReceiveNewsletter = newsletter;
      this.FirstName = firstName;
      this.Email = email;
      this.AccountCreated = accountCreated;
    }

    public string Username { get; set; }
    public bool ReceiveNewsletter { get; set; }
    public string FirstName { get; set; }
    public string Email { get; set; }
    public DateTime AccountCreated { get; set; }
  }

}