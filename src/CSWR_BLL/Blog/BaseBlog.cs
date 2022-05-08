using System;

/// <summary>
/// This is the base class for the Sheet module
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Blog
{
  [Serializable()]
  public class BaseBlog : BizObject
  {

    /// <summary>
    /// This is the default constructor for the BaseBlog class
    /// </summary>
    public BaseBlog() { }

    /// <summary>
    /// A method which caches data according to a key value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="data"></param>
    //protected static void CacheData(string key, object data)
    //{
    //  if (Settings.EnableCaching && data != null)
    //  {
    //    BizObject.Cache.Insert(key, data, null, DateTime.Now.AddSeconds(Settings.CacheDuration), TimeSpan.Zero);
    //  }
    //}


  }
}