/// <summary>
/// Summary description for SupplementalSheetPlayerDetails
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class SupplementalSheetItemDetails
  {
    public SupplementalSheetItemDetails()  {}

    public SupplementalSheetItemDetails(int supplementalSheetID, int playerID, int seqno, bool? sleeperTag, bool? bustTag, string note)
    {
      this.SupplementalSheetID = supplementalSheetID;
      this.PlayerID = playerID;
      this.Seqno = seqno;
      this.SleeperTag = sleeperTag;
      this.BustTag = bustTag;
      this.Note = note;
    }

    public int SupplementalSheetID {get;set;}
    public int PlayerID {get;set;}
    public int Seqno {get;set;}
    public bool? SleeperTag {get;set;}
    public bool? BustTag {get;set;}
    public string Note { get; set; }

  }
}