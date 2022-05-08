using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.BLL.Blog;

public partial class test_Forum_BlogForumQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


      List<BlogPost> peytonManningPosts = BlogPost.GetBlogPosts("#peytonmanning", 1);

    }
}