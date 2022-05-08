using System;
using System.Collections.Generic;

/// <summary>
/// Summary description for ForumProvider
/// </summary>
/// 
namespace BP.CheatSheetWarRoom.DAL
{
  public abstract partial class BlogProvider : DataAccess
  {
    public BlogProvider()
    {
      this.ConnectionString = Globals.BlogSettings.ConnectionString;
    }

    // BlogPost Methods
    public abstract BlogPostDetails GetLatestBlogPost(string categoryName);
    public abstract List<BlogPostDetails> GetBlogPosts(string tag, int totalPosts);


    /// <summary>
    /// Returns an instance of the provider type specified in the config file
    /// </summary>
    static private BlogProvider _instance = null;
    static public BlogProvider Instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = (BlogProvider)Activator.CreateInstance(Type.GetType(Globals.BlogSettings.ProviderType));
        }
        return _instance;
      }
    }


  }
}