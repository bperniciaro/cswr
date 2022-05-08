using System;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.Admin.Sheets
{
  public partial class UserOverallAccuracy : BasePage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      gvOverallUserAccuracy.DataSource = UserSportSeasonLeaderboard.GetUserSportSeasonLeaderboards(FOO.FOOString, "2013");
      gvOverallUserAccuracy.DataBind();
    }

    protected void gvOverallUserAccuracy_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        UserSportSeasonLeaderboard boundBoard = (UserSportSeasonLeaderboard)e.Row.DataItem;
        HyperLink hlSheetLink = (HyperLink)e.Row.FindControl("hlSheetLink");

        hlSheetLink.Text = boundBoard.QBScore.ToString();
        hlSheetLink.NavigateUrl = "usersheetaccuracy.aspx?Position=QB&Username=" + boundBoard.Username;
      }
    }

    protected void gvOverallUserAccuracy_PreRender(object sender, EventArgs e)
    {
      if (gvOverallUserAccuracy.Rows.Count > 0)
      {
        //This replaces <td> with <th> and adds the scope attribute
        gvOverallUserAccuracy.UseAccessibleHeader = true;

        //This will add the <thead> and <tbody> elements
        gvOverallUserAccuracy.HeaderRow.TableSection = TableRowSection.TableHeader;

        //This adds the <tfoot> element. 
        //Remove if you don't have a footer row
        gvOverallUserAccuracy.FooterRow.TableSection = TableRowSection.TableFooter;
      }

    }
}
}