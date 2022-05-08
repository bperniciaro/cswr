using System;

/// <summary>
/// Summary description for CheatSheetVisitor
/// </summary>
public class CheatSheetVisitorDetails
{
	public CheatSheetVisitorDetails(int cheatSheetID, string visitorID)
	{
    this.CheatSheetID = cheatSheetID;
    this.VisitorID = visitorID;
  }



  private int _cheatSheetID = 0;
  public int CheatSheetID
  {
    get { return _cheatSheetID; }
    set { _cheatSheetID = value; }
  }

  private string _visitorID = String.Empty;
  public string VisitorID
  {
    get { return _visitorID; }
    set { _visitorID = value; }
  }



}
