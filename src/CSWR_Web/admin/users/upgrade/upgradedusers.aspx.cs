using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.Admin.Users
{
  public partial class UpgradedUsers : BasePage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      gvUpgradedUsers.DataSource = UpgradeUser.GetUpgradeUsers(FOO.FOOString, FOO.CurrentSeason);
      gvUpgradedUsers.DataBind();
    }

    protected void butUpgrade_Click(object sender, EventArgs e)
    {
      MembershipUser userToUpgrade = Membership.GetUser(tbUsername.Text);
      if (userToUpgrade != null)
      {
        Guid userId = Guid.Parse(userToUpgrade.ProviderUserKey.ToString());
        UpgradeUser newUser = new UpgradeUser(FOO.FOOString, FOO.CurrentSeason, userId,
                                                  0, "ADMN", DateTime.Now, DateTime.MinValue);
        UpgradeUser.InsertUpgradeUser(newUser);
      }
    }


    /// <summary>
    /// This code forces the GridView control to output 'thead', 'tbody', and 'tfood' tags which are required
    /// by the DataTables JQuery plug-in. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUpgradedUsers_PreRender(object sender, EventArgs e)
    {
      if (gvUpgradedUsers.Rows.Count > 0)
      {
        //This replaces <td> with <th> and adds the scope attribute
        gvUpgradedUsers.UseAccessibleHeader = true;

        //This will add the <thead> and <tbody> elements
        gvUpgradedUsers.HeaderRow.TableSection = TableRowSection.TableHeader;

        //This adds the <tfoot> element. 
        //Remove if you don't have a footer row
        gvUpgradedUsers.FooterRow.TableSection = TableRowSection.TableFooter;
      }
    }

    /// <summary>
    /// Fired after a grid has been bound to its data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUpgradedUsers_DataBound(object sender, EventArgs e)
    {
      // will ensure that if there are no records the DataTables script won't be applied to the [non-existant] gridview
      if (gvUpgradedUsers.Rows.Count == 0)
      {
        panDataTablesScriptContainer.Visible = false;
      }
    }

                  
    protected void gvUpgradedUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        UpgradeUser boundUpgradeUser = (UpgradeUser)e.Row.DataItem;
        Label labUpgradeType = (Label)e.Row.FindControl("labUpgradeType");
        Label labUserName = (Label)e.Row.FindControl("labUserName");

        // determine username
        MembershipUser targetUser = Membership.GetUser(boundUpgradeUser.UserId);
        labUserName.Text = targetUser.UserName;

        // get upgrade type
        labUpgradeType.Text = UpgradeType.GetUpgradeType(boundUpgradeUser.UpgradeTypeCode).Name;
      }
    }
}
}