using System;
using System.Collections.Generic;
using System.Linq;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Represents a Season for a particular sport.
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Blog  {
  
  public class BlogPost : BaseBlog
  {
    public BlogPost() { }

    public BlogPost(Guid postId, string title, string description, string slug, DateTime dateCreated)
    {
      this.PostId = postId;
      this.Title = title;
      this.Description = description;
      this.Slug = slug;
      this.DateCreated = dateCreated;
    }

    public Guid  PostId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public DateTime DateCreated { get; set; }



    public static BlogPost GetLatestBlogPost(string categoryName)
    {
      BlogPost blogPost = new BlogPost();

      BlogPostDetails recordset = SiteProvider.Blog.GetLatestBlogPost(categoryName);
      return GetBlogPostFromBlogPostDetails(recordset);
    }


    public static BlogPost GetLatestBlogPost(string[] categoryNames)
    {
      List<BlogPost> latestBlogPosts = new List<BlogPost>();
      foreach (string categoryName in categoryNames)
      {
        latestBlogPosts.Add(GetBlogPostFromBlogPostDetails(SiteProvider.Blog.GetLatestBlogPost(categoryName)));
      }

      // then sort the blogs descending by publication date
      return latestBlogPosts.OrderByDescending(x => x.DateCreated).Take(1).ToList()[0];
    }

    public static List<BlogPost> GetBlogPosts(string tag, int totalPosts)
    {
      return GetBlogPostListFromBlogPostDetailsList(SiteProvider.Blog.GetBlogPosts(tag, totalPosts));
    }


    /// <summary>
    /// Converts a BlogPost entity object to a BlogPostDetails DAL object
    /// </summary>
    private static BlogPost GetBlogPostFromBlogPostDetails(BlogPostDetails blogPostDetails)
    {
      if (blogPostDetails == null) {
        return null;
      }
      else
      {
        return new BlogPost(blogPostDetails.PostID, blogPostDetails.Title, blogPostDetails.Description, blogPostDetails.Slug,
                                  blogPostDetails.DateCreated);
      }
    }

    private static List<BlogPost> GetBlogPostListFromBlogPostDetailsList(List<BlogPostDetails> recordset)
    {
      List<BlogPost> blogPosts = new List<BlogPost>();
      foreach (BlogPostDetails record in recordset) 
      {
        blogPosts.Add(GetBlogPostFromBlogPostDetails(record));
      }
      return blogPosts;
    }


  }

}
