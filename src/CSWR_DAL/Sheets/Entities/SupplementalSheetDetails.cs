using System;

/// <summary>
/// Summary description for SupplementalSheetDetails
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class SupplementalSheetDetails
  {
    public SupplementalSheetDetails()  {}

    public SupplementalSheetDetails(int supplementalSheetID, string seasonCode, int supplementalSourceID, string sportCode, string positionCode, DateTime lastUpdated, string url)
    {
      this.SupplementalSheetID = supplementalSheetID;
      this.SeasonCode = seasonCode;
      this.SupplementalSourceID = supplementalSourceID;
      this.SportCode = sportCode;
      this.PositionCode = positionCode;
      this.LastUpdated = lastUpdated;
      this.Url = url;
    }

    // Supplemental Sheet ID
    private int _supplementalSheetID = 0;
    public int SupplementalSheetID
    {
      get { return _supplementalSheetID; }
      set { _supplementalSheetID = value; }
    }

    // Season Code
    private string _seasonCode = "";
    public string SeasonCode
    {
      get { return _seasonCode; }
      set { _seasonCode = value; }
    }

    // Supplemental Source ID
    private int _supplementalSourceID = 0;
    public int SupplementalSourceID
    {
      get { return _supplementalSourceID; }
      set { _supplementalSourceID = value; }
    }

    // Sport Code
    private string _sportCode = "";
    public string SportCode
    {
      get { return _sportCode; }
      set { _sportCode = value; }
    }

    // Position Code
    private string _positionCode = "";
    public string PositionCode
    {
      get { return _positionCode; }
      set { _positionCode = value; }
    }

    // Last Updated
    private DateTime _lastUpdated = DateTime.Now;
    public DateTime LastUpdated
    {
      get { return _lastUpdated; }
      set { _lastUpdated = value; }
    }

    // Url
    private string _url = "";
    public string Url
    {
      get { return _url; }
      set { _url = value; }
    }

  }
}