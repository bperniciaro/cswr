/// <summary>
/// Summary description for ForumMemberDetails
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class ForumMemberDetails
  {
    public ForumMemberDetails(int memberId, string login)
    {
      this.MemberID = memberId;
      this.Login = login;
    }

    public int MemberID { get; set; }
    public string Login { get; set; }

  }
}