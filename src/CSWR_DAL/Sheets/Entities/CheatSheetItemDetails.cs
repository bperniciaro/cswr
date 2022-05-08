/// <summary>
/// Summary description for CheatSheetItem
/// </summary>
public class CheatSheetItemDetails
{
	public CheatSheetItemDetails(int cheatSheetID, int playerID, int seqno, bool? sleeperTag, bool? bustTag, bool? injuredTag, string note)
	{
    this.CheatSheetID = cheatSheetID;
    this.PlayerID = playerID;
    this.Seqno = seqno;
    this.SleeperTag = sleeperTag;
    this.BustTag = bustTag;
    this.InjuredTag = injuredTag;
    this.Note = note;
  }

  public int CheatSheetID { get; set; }

  public int PlayerID { get; set; }

  public int Seqno { get; set; }

  public bool? SleeperTag { get; set; }

  public bool? BustTag { get; set; }

  public bool? InjuredTag { get; set; }

  public string Note { get; set; }

}
