using System;
using System.Web.Security;

public partial class temp_CreateRole : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {

  }
  protected void butCreateModeratorRole_Click(object sender, EventArgs e)
  {
    Roles.CreateRole("Moderator");
    labMessage.Text = "role created";
  }
}