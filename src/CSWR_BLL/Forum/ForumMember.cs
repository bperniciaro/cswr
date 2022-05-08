using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Represents a Season for a particular sport.
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Forum  {
  
  public class ForumMember : BaseForum
  {
    public ForumMember(int memberId, string login)
    {
      this.MemberID = memberId;
      this.Login = login;
    }

    public int MemberID { get; set; }
    public string Login { get; set; }



    public static List<ForumMember> GetForumMembers()
    {
      List<ForumMember> forumMembers = null;
      //string key = "Sheets_ForumMembers";

      //if (BaseForum.Settings.EnableCaching && BizObject.Cache[key] != null)
      //{
      //  forumMembers = (List<ForumMember>)BizObject.Cache[key];
      //}
      //else
      //{
      List<ForumMemberDetails> recordset = SiteProvider.Forum.GetForumMembers();
      forumMembers = GetForumMemberListFromForumMemberDetailsList(recordset);
      //  BaseForum.CacheData(key, forumMembers);
      //}
      return forumMembers.GetRange(0, forumMembers.Count);
    }


    public static int GetMemberID(string userName)
    {
      int memberID = SiteProvider.Forum.GetMemberID(userName);
      return memberID;
    }



    /// <summary>
    /// Converts a ForumMemberDetails entity object to a ForumMember business object
    /// </summary>
    private static ForumMember GetForumMemberFromForumMemberDetails(ForumMemberDetails forumMember)
    {
      if (forumMember == null)
        return null;
      else
      {
        return new ForumMember(forumMember.MemberID, forumMember.Login);
      }
    }

    /// <summary>
    /// Converts a collection of ForumMemberDeatils objects to a collection of ForumMember business objects
    /// </summary>
    private static List<ForumMember> GetForumMemberListFromForumMemberDetailsList(List<ForumMemberDetails> recordset)
    {
      List<ForumMember> forumMembers = new List<ForumMember>();
      foreach (ForumMemberDetails record in recordset) 
      { 
        forumMembers.Add(GetForumMemberFromForumMemberDetails(record));
      }
      return forumMembers;
    }


  }

}
