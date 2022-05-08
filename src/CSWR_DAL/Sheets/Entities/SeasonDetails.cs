/// <summary>
/// Summary description for SeasonDetails
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class SeasonDetails
  {
    public SeasonDetails(string seasonCode, string name)
    {
      this.SeasonCode = seasonCode;
      this.Name = name;
    }

    // Season Code
    private string _seasonCode = "";
    public string SeasonCode
    {
      get { return _seasonCode; }
      set { _seasonCode = value; }
    }

    // Name
    private string _name = "";
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

  }
}