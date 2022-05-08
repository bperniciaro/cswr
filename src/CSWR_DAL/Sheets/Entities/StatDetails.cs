/// <summary>
/// Summary description for StatDetails
/// </summary>
/// 
namespace BP.CheatSheetWarRoom.DAL
{
  public class StatDetails
  {
    public StatDetails(string statCode, string name, string abbreviation, string description)
    {
      this.StatCode = statCode;
      this.Name = name;
      this.Abbreviation = abbreviation;
      this.Description = description;
    }

    // Stat Code
    private string _statCode = "";
    public string StatCode
    {
      get { return _statCode; }
      set { _statCode = value; }
    }

    // Name
    private string _name = "";
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    // Abbreviation
    private string _abbreviation = "";
    public string Abbreviation
    {
      get { return _abbreviation; }
      set { _abbreviation = value; }
    }

    // Description
    private string _description = "";
    public string Description
    {
      get { return _description; }
      set { _description = value; }
    }
  }
}