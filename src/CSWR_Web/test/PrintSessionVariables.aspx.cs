using System;
using System.Web.UI.WebControls;

public partial class Test_PrintSessionVariables : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

      BindGrid();
    }


    private void BindGrid()
    {
      // Actual work of binding Session to the grid
      gvSession.DataSource = Session.Contents;
      gvSession.DataBind();
    }

    protected void gvSession_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        Object boundObject = (Object)e.Row.DataItem;

        Label labKey = (Label)e.Row.FindControl("labKey");
        Label labValue = (Label)e.Row.FindControl("labValue");


        labKey.Text = boundObject.ToString();
        labValue.Text = Session[boundObject.ToString()].ToString();
      }
    }

    protected void butClearSession_Click(object sender, EventArgs e)
    {
      Session.Clear();
      BindGrid();
    }
}