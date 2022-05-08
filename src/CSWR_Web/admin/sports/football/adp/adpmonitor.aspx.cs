using System;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.Admin.Sheets
{
  public partial class ADPMonitor : BasePage
  {

    private int _rowCounter = 0;
    private bool coloredRow = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      gvTest.DataSource = ADPCalculation.GetADPCalculations(FOO.FOOString, FOO.CurrentSeason).OrderByDescending(x => x.CalcTimestamp);
      gvTest.DataBind();
    }


    protected void gvTest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        ADPCalculation boundCalculation = (ADPCalculation)e.Row.DataItem;

        HyperLink hlPosition = (HyperLink)e.Row.FindControl("hlPosition");
        hlPosition.Text = boundCalculation.PositionCode;

        switch (boundCalculation.PositionCode)
        {
          case "QB":
            hlPosition.NavigateUrl = "~/fantasy-football/nfl/free/rankings/adp/quarterbacks.aspx";
            break;
          case "RB":
            hlPosition.NavigateUrl = "~/fantasy-football/nfl/free/rankings/adp/running-backs.aspx";
            break;
          case "WR":
            hlPosition.NavigateUrl = "~/fantasy-football/nfl/free/rankings/adp/wide-receivers.aspx";
            break;
          case "TE":
            hlPosition.NavigateUrl = "~/fantasy-football/nfl/free/rankings/adp/tight-ends.aspx";
            break;
          case "K":
            hlPosition.NavigateUrl = "~/fantasy-football/nfl/free/rankings/adp/kickers.aspx";
            break;
          case "DF":
            hlPosition.NavigateUrl = "~/fantasy-football/nfl/free/rankings/adp/defenses.aspx";
            break;
        }



        if (_rowCounter == 6)
        {
          coloredRow = !coloredRow;
          _rowCounter = 0;
        }

        if (coloredRow)
        {
          e.Row.BackColor = Color.FromName("#abd284");
        }

        _rowCounter++;
      }
    }
  }
}