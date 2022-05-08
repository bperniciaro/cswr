/// <summary>
/// Summary description for CheatSheetUser
/// </summary>
public class CheatSheetUserDetails
{
	public CheatSheetUserDetails(int cheatSheetID, string userName)
	{
    this.CheatSheetID = cheatSheetID;
    this.UserName = userName;
	}

  private int _cheatSheetID = 0;
  public int CheatSheetID
  {
    get { return _cheatSheetID; }
    set { _cheatSheetID = value; }
  }

  private string _userName = "";
  public string UserName
  {
    get { return _userName; }
    set { _userName = value; }
  }

}
