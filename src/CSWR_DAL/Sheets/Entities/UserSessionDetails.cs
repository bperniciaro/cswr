using System;

/// <summary>
/// Summary description for SeasonDetails
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class UserSessionDetails
  {
    public UserSessionDetails(Guid userId, int sessionCount, string emailAddress, string userName)
    {
      this.UserId = userId;
      this.SessionCount = sessionCount;
      this.EmailAddress = emailAddress;
      this.UserName = userName;
    }

    // UserID
    public Guid UserId { get; set; }

    // UserName
    public string UserName { get; set; }

    // SessionCount
    public int SessionCount { get; set; }

    // Email
    public string EmailAddress { get; set; }

  }
}