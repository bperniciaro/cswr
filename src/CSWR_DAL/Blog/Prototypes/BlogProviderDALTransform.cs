using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Summary description for SheetsProvider
/// </summary>
/// 
namespace BP.CheatSheetWarRoom.DAL
{
  public abstract partial class BlogProvider : DataAccess
  {

    // Setting Readers
    protected virtual BlogPostDetails GetBlogPostFromReader(IDataReader reader)
    {
      BlogPostDetails blogPost = new BlogPostDetails(
          (Guid)reader["PostID"],
          reader["Title"].ToString(),
          reader["Description"].ToString(),
          reader["Slug"].ToString(),
          (DateTime)reader["DateCreated"]);
      return blogPost;
    }

    protected virtual List<BlogPostDetails> GetBlogPostCollectionFromReader(IDataReader reader)
    {
      List<BlogPostDetails> blogPosts = new List<BlogPostDetails>();
      while (reader.Read())
        blogPosts.Add(GetBlogPostFromReader(reader));
      return blogPosts;
    }


  }
}
