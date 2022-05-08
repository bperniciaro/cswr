using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Summary description for Team
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  [Serializable()]
  public class UserSession : BaseSheet
  {

    public UserSession(Guid userId, int sessionCount, string emailAddres, string userName)
    {
      this.UserId = userId;
      this.SessionCount = sessionCount;
      this.EmailAddress = emailAddres;
      this.UserName = userName;
    }

    // UserID
    public Guid UserId { get; set; }

    // Username
    public string UserName { get; set; }

    // SessionCount
    public int SessionCount { get; set; }

    // Email
    public string EmailAddress { get; set; }


    public static bool LogUserSession(Guid userId)
    {
      return SiteProvider.Sheets.LogUserSession(userId);
    }

    /// <summary>
    /// Returns all user sessions
    /// </summary>
    public static List<UserSession> GetUserSessions()
    {
      List<UserSessionDetails> recordset = SiteProvider.Sheets.GetUserSessions();
      return GetUserSessionListFromUserSessionDetailsList(recordset);
    }
    /// <summary>
    /// Returns a Team object filled with the data taken from the input TeamDetails
    /// </summary>
    private static UserSession GetUserSessionFromTeamDetails(UserSessionDetails userSession)
    {
      if (userSession == null)
        return null;
      else
      {
        return new UserSession(userSession.UserId, userSession.SessionCount, userSession.EmailAddress, userSession.UserName);
      }
    }

    /// <summary>
    /// Returns a list of UserSession objects filled with the data taken from the input list of UserSession Details
    /// </summary>
    private static List<UserSession> GetUserSessionListFromUserSessionDetailsList(List<UserSessionDetails> recordset)
    {
      List<UserSession> userSessions = new List<UserSession>();
      foreach (UserSessionDetails record in recordset)
        userSessions.Add(GetUserSessionFromTeamDetails(record));
      return userSessions;
    }


  }
}