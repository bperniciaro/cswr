using System;

namespace BP.CheatSheetWarRoom.DAL
{
  public class BlogPostDetails {

    public BlogPostDetails(Guid postID, string title, string description, string slug, DateTime dateCreated)
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

  }

}
