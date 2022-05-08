using System;

/// <summary>
/// This is the base class for the Sheet module
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  [Serializable()]
  public class BaseSheet : BizObject
  {

    /// <summary>
    /// This is the default constructor for the BaseSheet class
    /// </summary>
    public BaseSheet()  {}

    /// <summary>
    /// A read-only properly to access sheet settings in the web.config file
    /// </summary>
    protected static SheetsElement Settings
    {
      get { return Globals.CSWRSettings.Sheets; }
    }

    /// <summary>
    /// A method which caches data according to a key value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="data"></param>
    protected static void CacheData(string key, object data)
    {
      if (Settings.EnableCaching && data != null)
      {
        BizObject.Cache.Insert(key, data, null, DateTime.UtcNow.AddSeconds(Settings.CacheDuration), TimeSpan.Zero);
      }
    }


  }
}