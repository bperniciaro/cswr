using System;
using System.Web.Security;

public partial class test_Roles_CheckRoles : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {

    Roles.AddUserToRole(  "Brad Perniciaro", "Administrator");
    //this.User
    //string[] users = Roles.GetUsersInRole("SupplementalSource");


  }
}