using System;
using System.Collections.Generic;
using System.Linq;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Summary description for SupplementalSource
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class SupplementalSource : BaseSheet, IComparable<SupplementalSource>
  {

    public int CompareTo(SupplementalSource other)
    {
      return Name.CompareTo(other.Name);
    }


    public SupplementalSource(int supplementalSourceID, string abbreviation, string name, string url, string imageUrl)
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

    // Url
    private string _imageUrl = "";
    public string ImageUrl
    {
      get { return _imageUrl; }
      set { _imageUrl = value; }
    }


    /// <summary>
    /// Returns a Supplemental Source object with the specified id
    /// </summary>
    public static SupplementalSource GetSupplementalSource(int supplementalSourceID)
    {
      SupplementalSource supplementalSource = null;
      string key = "Sheets_SupplementalSource_" + supplementalSourceID.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        supplementalSource = (SupplementalSource)BizObject.Cache[key];
      }
      else
      {
        supplementalSource = GetSupplementalSourceFromSupplementalSourceDetails(SiteProvider.Sheets.GetSupplementalSource(supplementalSourceID));
        BaseSheet.CacheData(key, supplementalSource);
      }
      return supplementalSource;
    }



    /// <summary>
    /// Returns a Supplemental Source object with the specified id
    /// </summary>
    public static SupplementalSource GetSupplementalSource(string abbreviation)
    {
      SupplementalSource supplementalSource = null;
      string key = "Sheets_SupplementalSource_" + abbreviation;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        supplementalSource = (SupplementalSource)BizObject.Cache[key];
      }
      else
      {
        supplementalSource = GetSupplementalSourceFromSupplementalSourceDetails(SiteProvider.Sheets.GetSupplementalSource(abbreviation));
        BaseSheet.CacheData(key, supplementalSource);
      }
      return supplementalSource;
    }





    /// <summary>
    /// Returns a collection with all the supplemental sources
    /// </summary>
    public static List<SupplementalSource> GetSupplementalSources()
    {
      List<SupplementalSource> supplementalSources = null;
      string key = "Sheets_SupplementalSources";

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        supplementalSources = (List<SupplementalSource>)BizObject.Cache[key];
      }
      else
      {
        List<SupplementalSourceDetails> recordset = SiteProvider.Sheets.GetSupplementalSources();
        supplementalSources = GetSupplementalSourceListFromSupplementalSourceDetailsList(recordset);
        BaseSheet.CacheData(key, supplementalSources);
      }
      return supplementalSources.GetRange(0, supplementalSources.Count);
    }


    /// <summary>
    /// Returns a collection with all the supplemental sources
    /// </summary>
    public static List<SupplementalSource> GetSupplementalSources(string seasonCode, string sportCode, string positionCode)
    {
      List<SupplementalSource> supplementalSources = null;
      string key = "Sheets_SupplementalSourcesBySeasonSportPositionCodes_" + seasonCode + "_" + sportCode + "_" + positionCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        supplementalSources = (List<SupplementalSource>)BizObject.Cache[key];
      }
      else
      {
        List<SupplementalSourceDetails> recordset = SiteProvider.Sheets.GetSupplementalSources(seasonCode, sportCode, positionCode);
        supplementalSources = GetSupplementalSourceListFromSupplementalSourceDetailsList(recordset);
        BaseSheet.CacheData(key, supplementalSources);
      }
      return supplementalSources.GetRange(0, supplementalSources.Count);
    }




    /// <summary>
    /// Returns a sorted collection with all the supplemental sources
    /// </summary>
    public static List<SupplementalSource> GetSupplementalSources(string orderBy)
    {
      List<SupplementalSource> listToSort = GetSupplementalSources();

      if(String.IsNullOrEmpty(orderBy))
        return listToSort;

      // sort list
      switch (orderBy)
      {
        case "Name":
          listToSort.OrderBy(x => x.Name);
          break;
        case "Abbreviation":
          listToSort.OrderBy(x => x.Abbreviation);
          break;
        case "SupplementalSourceID":
          listToSort.OrderBy(x => x.SupplementalSourceID);
          break;
        case "Url":
          listToSort.OrderBy(x => x.Url);
          break;
      }
            
      return listToSort;
    }



    // Static Insert
    public static int InsertSupplementalSource(string name, string abbreviation, string url, string imageUrl)
    {

      // create an article entity to inser and use the specific provider to do the insert
      SupplementalSourceDetails record = new SupplementalSourceDetails(0, abbreviation, name, url, imageUrl);
      int ret = SiteProvider.Sheets.InsertSupplementalSource(record);

      BizObject.PurgeCacheItems("Sheets_SupplementalSources");
      return ret;
    }

    // Static Delete
    public static bool DeleteSupplementalSource(int supplementalSourceID)
    {
      bool ret = SiteProvider.Sheets.DeleteSupplementalSource(supplementalSourceID);
      BizObject.PurgeCacheItems("Sheets_SupplementalSources");
      return ret;
    }


    public static bool UpdateSupplementalSource(int supplementalSourceID, string name, string abbreviation, string url, string imageUrl)
    {
      SupplementalSourceDetails record = new SupplementalSourceDetails(supplementalSourceID, abbreviation, name, url, imageUrl);
      bool ret = SiteProvider.Sheets.UpdateSupplementalSource(record);
      BizObject.PurgeCacheItems("Sheets_SupplementalSources");
      BizObject.PurgeCacheItems("Sheets_SupplementalSource_" + supplementalSourceID.ToString());
      BizObject.PurgeCacheItems("Sheets_SupplementalSource_" + abbreviation);
      
      return ret;
    }


    /// <summary>
    /// Returns a SupplementalSource object filled with the data taken from the input SupplementalSourceDetails
    /// </summary>
    private static SupplementalSource GetSupplementalSourceFromSupplementalSourceDetails(SupplementalSourceDetails supplementalSource)
    {
      if (supplementalSource == null)
        return null;
      else
      {
        return new SupplementalSource(supplementalSource.SupplementalSourceID, supplementalSource.Abbreviation, supplementalSource.Name, supplementalSource.Url, supplementalSource.ImageUrl);
      }
    }

    /// <summary>
    /// Returns a list of Supplemental Source objects filled with the data taken from the input list of SupplementalSourceDetails
    /// </summary>
    private static List<SupplementalSource> GetSupplementalSourceListFromSupplementalSourceDetailsList(List<SupplementalSourceDetails> recordset)
    {
      List<SupplementalSource> supplementalSources = new List<SupplementalSource>();
      foreach (SupplementalSourceDetails record in recordset)
        supplementalSources.Add(GetSupplementalSourceFromSupplementalSourceDetails(record));
      return supplementalSources;
    }


  }
}