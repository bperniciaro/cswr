using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class FantasyFootballLeaderboard : BasePage
  {

    public const int MaxLeaderboardRows = 2000;

    public List<string> LeaderboardSeasons
    {
      get { return (ViewState["LeaderboardSeasons"] != null) ? (List<string>)ViewState["LeaderboardSeasons"] : null; }
      set { ViewState["LeaderboardSeasons"] = value; }
    } 

    public string SeasonCode 
    {
      get
      {
        return (ViewState["SeasonCode"] != null) ? ViewState["SeasonCode"].ToString() : "2017";
      }
      set
      {
        ViewState["SeasonCode"] = value;
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        InititalizeControls();
        BindSeasonButtons();
        BindLeaderboard();
      }
    }

    public void InititalizeControls()
    {
      this.LeaderboardSeasons = UserSportSeasonLeaderboard.GetLeaderboardYears(FOO.FOOString);
      this.LeaderboardSeasons.Reverse();
      this.SeasonCode = this.LeaderboardSeasons[0];
    }


    private void BindSeasonButtons()
    {
      repSeasons.DataSource = this.LeaderboardSeasons;
      repSeasons.DataBind();
    }


    private void BindLeaderboard()
    {
      litPageTitle.Text = this.SeasonCode + " User Overall Accuracy Leaderboard";

      gvOverallUserAccuracy.DataSource = UserSportSeasonLeaderboard.GetUserSportSeasonLeaderboards(FOO.FOOString, this.SeasonCode).Take(MaxLeaderboardRows).ToList();
      gvOverallUserAccuracy.DataBind();
    }



    
    protected void gvOverallUserAccuracy_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        UserSportSeasonLeaderboard boundBoard = (UserSportSeasonLeaderboard)e.Row.DataItem;

        //QB Controls
        HyperLink hlQbSheetLink = (HyperLink)e.Row.FindControl("hlQBSheetLink");
        Label labQbSheetMessage = (Label) e.Row.FindControl("labQBSheetMessage");
        
        //RB Controls
        HyperLink hlRbSheetLink = (HyperLink)e.Row.FindControl("hlRBSheetLink");
        Label labRbSheetMessage = (Label)e.Row.FindControl("labRBSheetMessage");
        
        //WR Controls
        HyperLink hlWrSheetLink = (HyperLink)e.Row.FindControl("hlWRSheetLink");
        Label labWrSheetMessage = (Label)e.Row.FindControl("labWRSheetMessage");
        
        //TE Controls
        HyperLink hlTeSheetLink = (HyperLink)e.Row.FindControl("hlTESheetLink");
        Label labTeSheetMessage = (Label)e.Row.FindControl("labTESheetMessage");
        
        //K Controls
        HyperLink hlKSheetLink = (HyperLink)e.Row.FindControl("hlKSheetLink");
        Label labKSheetMessage = (Label)e.Row.FindControl("labKSheetMessage");
        
        //DST Controls
        HyperLink hlDstSheetLink = (HyperLink)e.Row.FindControl("hlDSTSheetLink");
        Label labDstSheetMessage = (Label)e.Row.FindControl("labDSTSheetMessage");

        //Overall Score
        Label labOverallScore = (Label) e.Row.FindControl("labOverallScore");

        if (boundBoard != null)
        {

          // QB Sheet
          if (boundBoard.QBScore != 0)
          {
            hlQbSheetLink.Text = boundBoard.QBScore.ToString();
            hlQbSheetLink.NavigateUrl =
              "~/fantasy-football/nfl/accuracy/users/user/" + boundBoard.Username + "/" + boundBoard.SeasonCode + "/QB";
          }
          else
          {
            hlQbSheetLink.Text = "0";
            hlQbSheetLink.NavigateUrl = "#";
            hlQbSheetLink.CssClass = "inactiveLink";
          }

          // RB Sheet
          if (boundBoard.RBScore != 0)
          {
            hlRbSheetLink.Text = boundBoard.RBScore.ToString();
            hlRbSheetLink.NavigateUrl =
              "~/fantasy-football/nfl/accuracy/users/user/" + boundBoard.Username + "/" + boundBoard.SeasonCode + "/RB";
          }
          else
          {
            hlRbSheetLink.Text = "0";
            hlRbSheetLink.NavigateUrl = "#";
            hlRbSheetLink.CssClass = "inactiveLink";
          }

          // WR Sheet
          if (boundBoard.WRScore != 0)
          {
            hlWrSheetLink.Text = boundBoard.WRScore.ToString();
            hlWrSheetLink.NavigateUrl =
              "~/fantasy-football/nfl/accuracy/users/user/" + boundBoard.Username + "/" + boundBoard.SeasonCode + "/WR";
          }
          else
          {
            hlWrSheetLink.Text = "0";
            hlWrSheetLink.NavigateUrl = "#";
            hlWrSheetLink.CssClass = "inactiveLink";
          }

          // TE Sheet
          if (boundBoard.TEScore != 0)
          {
            hlTeSheetLink.Text = boundBoard.TEScore.ToString();
            hlTeSheetLink.NavigateUrl =
              "~/fantasy-football/nfl/accuracy/users/user/" + boundBoard.Username + "/" + boundBoard.SeasonCode + "/TE";
          }
          else
          {
            hlTeSheetLink.Text = "0";
            hlTeSheetLink.NavigateUrl = "#";
            hlTeSheetLink.CssClass = "inactiveLink";
          }

          // K Sheet
          if (boundBoard.KScore != 0)
          {
            hlKSheetLink.Text = boundBoard.KScore.ToString();
            hlKSheetLink.NavigateUrl =
              "~/fantasy-football/nfl/accuracy/users/user/" + boundBoard.Username + "/" + boundBoard.SeasonCode + "/K";
          }
          else
          {
            hlKSheetLink.Text = "0";
            hlKSheetLink.NavigateUrl = "#";
            hlKSheetLink.CssClass = "inactiveLink";
          }

          // DF Sheet
          if (boundBoard.DFScore != 0)
          {
            hlDstSheetLink.Text = boundBoard.DFScore.ToString();
            hlDstSheetLink.NavigateUrl =
              "~/fantasy-football/nfl/accuracy/users/user/" + boundBoard.Username + "/" + boundBoard.SeasonCode + "/DF";
          }
          else
          {
            hlDstSheetLink.Text = "0";
            hlDstSheetLink.NavigateUrl = "#";
            hlDstSheetLink.CssClass = "inactiveLink";
          }

          labOverallScore.Text = boundBoard.OverallScore.ToString();

        }

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

    protected void butSeason_Click(object sender, EventArgs e)
    {
      this.SeasonCode = ((Button)sender).CommandArgument;
      BindSeasonButtons();
      BindLeaderboard();
    }


    protected void repSeasons_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        var boundSeasonCode = (string) e.Item.DataItem;
        var butSeasonButton = (Button) e.Item.FindControl("butSeasonButton");

        butSeasonButton.Text = boundSeasonCode;
        butSeasonButton.CommandArgument = boundSeasonCode;

        if (this.SeasonCode == boundSeasonCode)
        {
          butSeasonButton.CssClass = "btn btn-primary";
        }
        else
        {
          butSeasonButton.CssClass = "btn";
        }

      }
    }
  }
}