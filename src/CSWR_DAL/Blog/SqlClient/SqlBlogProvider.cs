using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BP.CheatSheetWarRoom.DAL.SqlClient
{
  public class SqlBlogProvider : BlogProvider
  {
    public SqlBlogProvider() { }

    public override BlogPostDetails GetLatestBlogPost(string categoryName)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {

        string query = @"SELECT TOP(1) be_Posts.PostID, be_Posts.Title, be_Posts.Description, be_Posts.Slug, be_Posts.DateCreated
                        FROM be_Posts JOIN be_PostCategory
                        ON be_Posts.PostID = be_PostCategory.PostID JOIN be_Categories
                        ON be_PostCategory.CategoryID = be_Categories.CategoryID
                        WHERE
                          be_Categories.CategoryName = @CategoryName AND IsPublished = 1
                        ORDER BY DateCreated DESC";
        SqlCommand selectCommand = new SqlCommand(query, cn);
        selectCommand.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = categoryName;

        cn.Open();
        try
        {

          IDataReader reader = selectCommand.ExecuteReader();

          if (reader.Read())
          {
            return GetBlogPostFromReader(reader);
          }
        }
        catch (Exception ex)
        {
          int i = 0;
        }
        cn.Close();

        return null;
      }
    }

    public override List<BlogPostDetails> GetBlogPosts(string tag, int totalPosts)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {

        SqlCommand selectCommand = new SqlCommand("select TOP " + totalPosts + " * FROM be_Posts INNER JOIN be_PostTag ON be_Posts.PostID = be_PostTag.PostID WHERE be_PostTag.Tag = '" + tag + "' AND be_Posts.IsPublished = 1 ORDER BY be_Posts.DateCreated DESC", cn);

        cn.Open();
        try
        {

          IDataReader reader = selectCommand.ExecuteReader();
          return GetBlogPostCollectionFromReader(reader);
        }
        catch (Exception ex)
        {
          int i = 0;
        }
        cn.Close();

        return null;
      }
    }
  }
}