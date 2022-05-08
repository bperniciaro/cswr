using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Summary description for CheatSheetUser
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class CheatSheetUser : BaseSheet
  {
    //public CheatSheetUser(int cheatSheetID, string userName)
    //{
    //  this.CheatSheetID = cheatSheetID;
    //  this.UserName = userName;
    //}

    //private int _cheatSheetID = 0;
    //public int CheatSheetID
    //{
    //  get { return _cheatSheetID; }
    //  set { _cheatSheetID = value; }
    //}

    //private string _userName = "";
    //public string UserName
    //{
    //  get { return _userName; }
    //  set { _userName = value; }
    //}


    //public static string GetCheatSheetUserNameBySheetID(int cheatSheetID)
    //{
    //  return SiteProvider.Sheets.GetCheatSheetUserBySheetID(cheatSheetID).UserName;
    //}


    // Static Insert
    //public static int InsertCheatSheetUser(int cheatSheetID, string userName)
    //{

    //  // convert any nulls to empty strings
    //  userName = BizObject.ConvertNullToEmptyString(userName);

    //  // create an article entity to inser and use the specific provider to do the insert
    //  //CheatSheetDetails record = new CheatSheetDetails(0, sportCode, sheetName, DateTime.Now);
    //  CheatSheetUserDetails record = new CheatSheetUserDetails(cheatSheetID, userName);
    //  int ret = SiteProvider.Sheets.InsertCheatSheetUser(record);
    //  return ret;
    //}


    /// <summary>
    /// Returns a CheatSheetUser object filled with the data taken from the input CheatSheetUserDetails
    /// </summary>
    //private static CheatSheetUser GetCheatSheetUserFromCheatSheetUserDetails(CheatSheetUserDetails cheatSheetUser)
    //{
    //  if (cheatSheetUser == null)
    //    return null;
    //  else
    //  {
    //    return new CheatSheetUser(cheatSheetUser.CheatSheetID, cheatSheetUser.UserName);
    //  }
    //}

    /// <summary>
    /// Returns a list of CheatSheetUser objects filled with the data taken from the input list of CheatSheetUserDetails
    /// </summary>
    //private static List<CheatSheetUser> GetCheatSheetUserListFromCheatSheetUserDetailsList(List<CheatSheetUserDetails> recordset)
    //{
    //  List<CheatSheetUser> cheatSheetUsers = new List<CheatSheetUser>();
    //  foreach (CheatSheetUserDetails record in recordset)
    //    cheatSheetUsers.Add(GetCheatSheetUserFromCheatSheetUserDetails(record));
    //  return cheatSheetUsers;
    //}


  }
}