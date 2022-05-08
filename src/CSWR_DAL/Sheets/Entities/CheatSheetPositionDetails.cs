/// <summary>
/// Summary description for CheatSheetPositionDetails
/// </summary>
public class CheatSheetPositionDetails
{
	public CheatSheetPositionDetails(int cheatSheetID, string positionCode)
	{
    this.CheatSheetID = cheatSheetID;
    this.PositionCode = positionCode;
  }

  // Cheat Sheet ID
  private int _cheatSheetID = 0;
  public int CheatSheetID
  {
    get { return _cheatSheetID; }
    set { _cheatSheetID = value; }
  }

  // Position Code
  private string _positionCode = "";
  public string PositionCode
  {
    get { return _positionCode; }
    set { _positionCode = value; }
  }

}
