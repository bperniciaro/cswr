/// <summary>
/// Summary description for PositionDetails
/// </summary>
/// 
namespace BP.CheatSheetWarRoom.DAL
{
  public class PositionDetails
  {
    public PositionDetails() {}

    public PositionDetails(string positionCode, string name, string abbreviation)
    {
      this.PositionCode = positionCode;
      this.Name = name;
      this.Abbreviation = abbreviation;
    }

    // Position Code
    private string _positionCode = "";
    public string PositionCode  {
      get {return _positionCode;}
      set {_positionCode = value;}
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








  }
}