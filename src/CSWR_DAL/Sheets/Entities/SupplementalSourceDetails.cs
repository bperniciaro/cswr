/// <summary>
/// Summary description for SupplementalSheetSourceDetails
/// </summary>
/// 
namespace BP.CheatSheetWarRoom.DAL
{
  public class SupplementalSourceDetails
  {
    public SupplementalSourceDetails()  {}

    public SupplementalSourceDetails(int supplementalSourceID, string abbreviation, string name, string url, string imageUrl)
    {
      this.SupplementalSourceID = supplementalSourceID;
      this.Abbreviation = abbreviation;
      this.Name = name;
      this.Url = url;
      this.ImageUrl = imageUrl;
    }

    // Supplement Source ID
    private int _supplementalSourceID = 0;
    public int SupplementalSourceID
    {
      get { return _supplementalSourceID; }
      set { _supplementalSourceID = value; }
    }

    // Abbreviation
    private string _abbreviation = "";
    public string Abbreviation
    {
      get { return _abbreviation; }
      set { _abbreviation = value; }
    }

    // Name
    private string _name = "";
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    // Url
    private string _url = "";
    public string Url
    {
      get { return _url; }
      set { _url = value; }
    }

    // Image Url
    private string _imageUrl = "";
    public string ImageUrl
    {
      get { return _imageUrl; }
      set { _imageUrl = value; }
    }

  }
}