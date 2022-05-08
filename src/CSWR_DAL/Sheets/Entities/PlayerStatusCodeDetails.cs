namespace BP.CheatSheetWarRoom.DAL
{
  public class PlayerStatusCodeDetails
  {
    public PlayerStatusCodeDetails()  {}


    public PlayerStatusCodeDetails(string statusCode, string name, string description, bool suppInfoRequired, string suppInfoLabel,
      string suppInfoExample, string suppInfoInstructions, bool countRequired, string countLabel, int countExample, string countInstructions,
      int seqno, int priority, bool dynamic)
    {
      this.StatusCode = statusCode;
      this.Name = name;
      this.Description = description;
      this.SuppInfoRequired = suppInfoRequired;
      this.SuppInfoLabel = suppInfoLabel;
      this.SuppInfoExample = suppInfoExample;
      this.SuppInfoInstructions = suppInfoInstructions;
      this.CountRequired = countRequired;
      this.CountLabel = countLabel;
      this.CountExample = countExample;
      this.CountInstructions = countInstructions;
      this.Seqno = seqno;
      this.Priority = priority;
      this.Dynamic = dynamic;
    }

    public string StatusCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool SuppInfoRequired { get; set; }
    public string SuppInfoLabel { get; set; }
    public string SuppInfoExample { get; set; }
    public string SuppInfoInstructions { get; set; }
    public bool CountRequired { get; set; }
    public string CountLabel { get; set; }
    public int CountExample { get; set; }
    public string CountInstructions { get; set; }
    public int Seqno { get; set; }
    public int Priority { get; set; }
    public bool Dynamic { get; set; }


  }
}