using System.Collections.Generic;
using System.Data;

/// <summary>
/// Summary description for SheetsProvider
/// </summary>
/// 
namespace BP.CheatSheetWarRoom.DAL
{
  public abstract partial class ForumProvider : DataAccess
  {

    // Setting Readers
    protected virtual ForumMemberDetails GetForumMemberFromReader(IDataReader reader)
    {
      ForumMemberDetails forumMember = new ForumMemberDetails(
          (int)reader["MemberID"],
          reader["Login"].ToString());
      return forumMember;
    }

    protected virtual List<ForumMemberDetails> GetForumMemberCollectionFromReader(IDataReader reader)
    {
      List<ForumMemberDetails> forumMembers = new List<ForumMemberDetails>();
      while (reader.Read())
        forumMembers.Add(GetForumMemberFromReader(reader));
      return forumMembers;
    }


  }
}
