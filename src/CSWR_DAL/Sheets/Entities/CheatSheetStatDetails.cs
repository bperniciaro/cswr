/// <summary>
/// Summary description for CheatSheetStatDetails
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class CheatSheetStatDetails
  {
    public CheatSheetStatDetails(int cheatSheetID, string statCode)
    {
      this.CheatSheetID = cheatSheetID;
      this.StatCode = statCode;
    }

    // Cheat Sheet ID
    private int _cheatSheetID = 0;
    public  int CheatSheetID  
    {
      get {return _cheatSheetID;}
      set {_cheatSheetID = value;}
    }

    // Stat Code
    private string _statCode = "";
    public string StatCode
    {
      get { return _statCode; }
      set { _statCode = value; }
    }
  }
}