using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.UserControls
{

  public partial class FooPlayerStatusManager : System.Web.UI.UserControl
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      InitializeControls();
    }

    protected void butCreateStatus_Click(object sender, EventArgs e)
    {
      fpseStatusEditor.SaveStatus();
      BindPlayerStatuses();
    }


    private void InitializeControls()
    {
      BindPlayerStatuses();
    }

    private void BindPlayerStatuses()
    {
      gvPlayerStatuses.DataSource = SportSeasonPlayerStatus.GetSportSeasonPlayerStatuses(FOO.FOOString, FOO.CurrentSeason);
      gvPlayerStatuses.DataBind();
    }


    public int InsertPlayerStatus()
    {
      return 0;
    }


    public int SaveStatus()
    {
      return fpseStatusEditor.SaveStatus();
    }

    protected void gvPlayerStatuses_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      // if we're binding to a data row, manipulate the controls within
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        // get references to the appropriate controls
        var boundStatus = (SportSeasonPlayerStatus)e.Row.DataItem;
        var labFullName = (Label)e.Row.FindControl("labFullName");
        var labStatus = (Label)e.Row.FindControl("labStatus");
        var labSuppInfo = (Label)e.Row.FindControl("labSuppInfo");
        var labCount = (Label)e.Row.FindControl("labCount");
        var labCreatedBy = (Label)e.Row.FindControl("labCreatedBy");
        var labCreatedTimestamp = (Label) e.Row.FindControl("labCreatedTimestamp");
        var labModifiedBy = (Label)e.Row.FindControl("labModifiedBy");
        var labModifiedTimestamp = (Label)e.Row.FindControl("labModifiedTimestamp");

        // full name
        labFullName.Text = Player.GetPlayer(boundStatus.PlayerId).FullName;
        // status
        labStatus.Text = PlayerStatusCode.GetPlayerStatusCode(boundStatus.StatusCode).Name;
        // supp info
        labSuppInfo.Text = WebUtility.HtmlDecode(boundStatus.SuppInfo);
        // count
        if (boundStatus.Count != null)
        {
          labCount.Text = boundStatus.Count.ToString();
        }
        labCount.Text = "n/a";
        // created by
        labCreatedBy.Text = boundStatus.CreatedByUsername;
        // created by timestamp
        labCreatedTimestamp.Text = boundStatus.CreatedTimestamp.ToString("MM-dd-yyyy HH:mm");
        // modified by
        labModifiedBy.Text = boundStatus.ModifiedByUsername;
        // modified by timestamp
        if (boundStatus.ModifiedTimestamp != null)
        {
          labModifiedTimestamp.Text = (boundStatus.ModifiedTimestamp != null) ? boundStatus.ModifiedTimestamp.Value.ToString("MM-dd-yyyy HH:mm") : String.Empty;
        }


      }
    }


  }
}