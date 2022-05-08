using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Summary description for CheatSheetVisitor
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets  {
  public class CheatSheetVisitor : BaseSheet
  {
    //public CheatSheetVisitor(int cheatSheetID, string visitorID)
    //{
    //  this.CheatSheetID = cheatSheetID;
    //  this.VisitorID = visitorID;
    //}

    //private int _cheatSheetID = 0;
    //public int CheatSheetID
    //{
    //  get { return _cheatSheetID; }
    //  set { _cheatSheetID = value; }
    //}

    //private string _visitorID = String.Empty;
    //public string VisitorID
    //{
    //  get { return _visitorID; }
    //  set { _visitorID = value; }
    //}

    //public static string GetCheatSheetVisitorIDBySheetID(int cheatSheetID)
    //{
    //  return SiteProvider.Sheets.GetCheatSheetVisitor(cheatSheetID).VisitorID;
    //}



    //public static List<CheatSheetVisitor> GetUnclaimedCheatSheetVisitors()
    //{
    //  List<CheatSheetVisitor> cheatSheetDistinctVisitors = GetCheatSheetDistinctVisitors();
    //  for (int i = 0; i < cheatSheetDistinctVisitors.Count; i++)
    //  {
    //    //if(HttpContext.Current.Session[cheatSheetDistinctVisitors[i].VisitorID]
    //    //if (cheatSheetDistinctVisitors[i].VisitorID) ;
    //  }
    //}

    /// <summary>
    /// Returns a collection with all the categories
    /// </summary>
    //public static List<CheatSheetVisitor> GetCheatSheetDistinctVisitors()
    //{
    //  List<CheatSheetVisitor> cheatSheetDistinctVisitors = null;
    //  string key = "Sheets_CheatSheetDistinctVisitors";

    //  if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
    //  {
    //    cheatSheetDistinctVisitors = (List<CheatSheetVisitor>)BizObject.Cache[key];
    //  }
    //  else
    //  {
    //    List<CheatSheetVisitorDetails> recordset = SiteProvider.Sheets.GetCheatSheetDistinctVisitors();
    //    cheatSheetDistinctVisitors = GetCheatSheetVisitorListFromCheatSheetVisitorDetailsList(recordset);
    //    BaseSheet.CacheData(key, cheatSheetDistinctVisitors);
    //  }
    //  return cheatSheetDistinctVisitors;
    //}

        


    /// <summary>
    /// Returns a CheatSheetVisitor object filled with the data taken from the input CheatSheetVisitorDetails
    /// </summary>
    //private static CheatSheetVisitor GetCheatSheetVisitorFromCheatSheetVisitorDetails(CheatSheetVisitorDetails cheatSheetVisitor)
    //{
    //  if (cheatSheetVisitor == null)
    //    return null;
    //  else
    //  {
    //    return new CheatSheetVisitor(cheatSheetVisitor.CheatSheetID, cheatSheetVisitor.VisitorID);
    //  }
    //}

    /// <summary>
    /// Returns a list of CheatSheetVisitor objects filled with the data taken from the input list of CheatSheetUVisitorDetails
    /// </summary>
    //private static List<CheatSheetVisitor> GetCheatSheetVisitorListFromCheatSheetVisitorDetailsList(List<CheatSheetVisitorDetails> recordset)
    //{
    //  List<CheatSheetVisitor> cheatSheetVisitors = new List<CheatSheetVisitor>();
    //  foreach (CheatSheetVisitorDetails record in recordset)
    //    cheatSheetVisitors.Add(GetCheatSheetVisitorFromCheatSheetVisitorDetails(record));
    //  return cheatSheetVisitors;
    //}


  }
}
