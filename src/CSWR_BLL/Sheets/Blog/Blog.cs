using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.CheatSheetWarRoom.DAL;

namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  /// <summary>
  /// Class for keeping count of player rankings and calculating the final ADP
  /// </summary>
  public class Blog : BaseSheet
  {
    public Blog() { }

    public Blog(Guid postID, string title, string description, string slug, DateTime dateCreated)
    {
      this.PostID = postID;
      this.Title = title;
      this.Description = description;
      this.Slug = slug;
      this.DateCreated = dateCreated;
    }

    public Guid  PostID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public DateTime DateCreated { get; set; }



    /// <summary>
    /// Returns a Blog 
    /// </summary>
    //public static Blog GetLatestBlogPost(string categoryName)
    //{
    //  Blog latestBlog = null;
    //  string key = "Sheets_LatestBlog_" + categoryName;

    //  if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
    //  {
    //    latestBlog = (Blog)BizObject.Cache[key];
    //  }
    //  else
    //  {
    //    latestBlog = GetBlogFromBlogDetails(SiteProvider.Sheets.GetLatestBlogPost(categoryName));
    //    BaseSheet.CacheData(key, latestBlog);
    //  }
    //  return latestBlog;
    //}


    /// <summary>
    /// Returns the latest blog post from a list of possible categories 
    /// </summary>
    //public static Blog GetLatestBlogPost(string[] categoryNames)
    //{
    //  List<Blog> latestBlogs = new List<Blog>();
    //  foreach (string categoryName in categoryNames)
    //  {
    //    latestBlogs.Add(GetBlogFromBlogDetails(SiteProvider.Sheets.GetLatestBlogPost(categoryName)));
    //  }

    //  // then sort the blogs descending by publication date
    //  return latestBlogs.OrderByDescending(x => x.DateCreated).Take(1).ToList()[0];
    //}





    /// <summary>
    /// Converts a Blog entity object to a Blog business object
    /// </summary>
    private static Blog GetBlogFromBlogDetails(BlogDetails blog)
    {
      if (blog == null)
        return null;
      else
      {
        return new Blog(blog.PostID, blog.Title, blog.Description, blog.Slug, blog.DateCreated);
      }
    }

    /// <summary>
    /// Converta a collection of ByeWeekDetails objects to a collection of ByeWeek business objects
    /// </summary>
    private static List<Blog> GetBlogListFromBlogDetailsList(List<BlogDetails> recordset)
    {
      List<Blog> blogs = new List<Blog>();
      foreach (BlogDetails record in recordset)
        blogs.Add(GetBlogFromBlogDetails(record));
      return blogs.GetRange(0, blogs.Count);
    }


  }

}
