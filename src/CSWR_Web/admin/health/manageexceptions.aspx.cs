using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace BP.CheatSheetWarRoom.UI.Admin.Health
{
  public partial class ManageExceptions : System.Web.UI.Page
  {
    protected void EventLog_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        DataRowView dataView = (DataRowView)e.Row.DataItem;
        Label labEventTime = (Label)e.Row.FindControl("labEventTime");
        
        string errorURL = dataView["RequestUrl"].ToString();
        string[] errorPageParts = errorURL.Split('/');
        string errorPage = errorPageParts[errorPageParts.Length - 1];

        string[] urlParts = errorPage.Split('%');
        string shortErrorPage = urlParts[urlParts.Length - 1];
        
        e.Row.Cells[3].Text = shortErrorPage;

        DateTime eventTime = (DateTime)dataView["EventTime"];

        labEventTime.Text = eventTime.ToCentralStandardDateTime().ToShortDateString() + " " + eventTime.ToCentralStandardDateTime().ToShortTimeString();
      }

    }


    protected void butDeleteAllExceptions_Click(object sender, EventArgs e)
    {
      SqlConnection cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
      SqlCommand cmd = new SqlCommand("DELETE FROM aspnet_WebEvent_Events", cn);
      cn.Open();
      int deletedRecords = cmd.ExecuteNonQuery();

      if(deletedRecords > 0)  
      {
        mbResult.MessageType = MessageType.SUCCESS;
        mbResult.Message = new StringBuilder(deletedRecords.ToString() + " records were deleted.");
      }

      cn.Close();

      EventLog.DataBind();
    }




    protected void LogEntryDetails_DataBound(object sender, EventArgs e)
    {
      if (LogEntryDetails.CurrentMode == DetailsViewMode.ReadOnly)
      {

        if (LogEntryDetails.DataItem != null)
        {
          Literal litDetails = LogEntryDetails.FindControl("litDetails") as Literal;
          if (litDetails != null)
          {
            DataRowView drv = (DataRowView)LogEntryDetails.DataItem;

            litDetails.Text = drv.DataView[0].Row.ItemArray[14].ToString().Replace("\n", "<br/>");
          }
        }
      }
    }

    protected void LogEntryDetails_DataBinding(object sender, EventArgs e)
    {
    }
}
}