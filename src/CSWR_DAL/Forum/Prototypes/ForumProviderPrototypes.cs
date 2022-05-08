using System;
using System.Collections.Generic;

/// <summary>
/// Summary description for ForumProvider
/// </summary>
/// 
namespace BP.CheatSheetWarRoom.DAL
{
  public abstract partial class ForumProvider : DataAccess
  {
    public ForumProvider()
    {
      this.ConnectionString = Globals.ForumSettings.ConnectionString;
    }

    // ForumMember Methods
    public abstract List<ForumMemberDetails> GetForumMembers();
    public abstract int GetMemberID(string userName);

    /// <summary>
    /// Returns an instance of the provider type specified in the config file
    /// </summary>
    static private ForumProvider _instance = null;
    static public ForumProvider Instance
    {
      get
      {
        if (_instance == null)
          _instance = (ForumProvider)Activator.CreateInstance(Type.GetType(Globals.ForumSettings.ProviderType));
        return _instance;
      }
    }


  }
}