using System;
using System.Web.UI;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class PrintableCheatSheets : BasePage
  {
    private string _currentSportSeason;
    private string _lastSportSeason;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        LoadSocialTags();
        LoadInstructionalPopup();

        _currentSportSeason = SportSeason.GetCurrentSportSeason("FOO").SeasonCode;
        _lastSportSeason = SportSeason.GetCurrentSportSeason("FOO").LastSeasonCode;

        LoadSEO();
        LoadC2A();
        LoadContent();
      }

    }

    private void LoadContent()
    {
      // position descriptions
      if (Helpers.IsMiddleOfSeason(FOO.FOOString))
      {
        labQBDescription.Text = "teams, bye weeks, and each player's " + _currentSportSeason + " ranking using total fantasy points.";
        labRBDescription.Text = "teams, bye weeks, and each player's " + _currentSportSeason + " ranking using total fantasy points.";
        labWRDescription.Text = "teams, bye weeks, and each player's " + _currentSportSeason + " ranking using total fantasy points.";
        labTEDescription.Text = "teams, bye weeks, and each player's " + _currentSportSeason + " ranking using total fantasy points.";
        labKDescription.Text = "teams, bye weeks, and each player's " + _currentSportSeason + " ranking using total fantasy points.";
        labDFDescription.Text = "teams, bye weeks, and each player's " + _currentSportSeason + " ranking using total fantasy points.";
      }
      else
      {
        labQBDescription.Text = "teams, bye weeks, and each player's final " + _lastSportSeason + " ranking using total fantasy points.";
        labRBDescription.Text = "teams, bye weeks, and each player's final " + _lastSportSeason + " ranking using total fantasy points.";
        labWRDescription.Text = "teams, bye weeks, and each player's final " + _lastSportSeason + " ranking using total fantasy points.";
        labTEDescription.Text = "teams, bye weeks, and each player's final " + _lastSportSeason + " ranking using total fantasy points.";
        labKDescription.Text = "teams, bye weeks, and each player's final " + _lastSportSeason + " ranking using total fantasy points.";
        labDFDescription.Text = "teams, bye weeks, and each player's final " + _lastSportSeason + " ranking using total fantasy points.";
      }



      // max players
      labMaxQBs.Text = Helpers.GetMaxRankPlayersConsideredBySportPosition(FOO.FOOString, FOOPositionsOffense.QB.ToString()).ToString();
      labMaxRBs.Text = Helpers.GetMaxRankPlayersConsideredBySportPosition(FOO.FOOString, FOOPositionsOffense.RB.ToString()).ToString();
      labMaxWRs.Text = Helpers.GetMaxRankPlayersConsideredBySportPosition(FOO.FOOString, FOOPositionsOffense.WR.ToString()).ToString();
      labMaxTEs.Text = Helpers.GetMaxRankPlayersConsideredBySportPosition(FOO.FOOString, FOOPositionsOffense.TE.ToString()).ToString();
      labMaxKs.Text = Helpers.GetMaxRankPlayersConsideredBySportPosition(FOO.FOOString, FOOPositionsOffense.K.ToString()).ToString();
      labMaxDFs.Text = Helpers.GetMaxRankPlayersConsideredBySportPosition(FOO.FOOString, FOOPositionsOffense.DF.ToString()).ToString();

      // years
      labSingleSheetWithRosterYear.Text = _currentSportSeason;
      labQBsYear.Text = _currentSportSeason;
      labRBsYear.Text = _currentSportSeason;
      labWRsYear.Text = _currentSportSeason;
      labTEsYear.Text = _currentSportSeason;
      labKsYear.Text = _currentSportSeason;
      labDFsYear.Text = _currentSportSeason;

      // link to player rankings
      hlRankingsLink.Text = _currentSportSeason + " football player rankings";

    }

    private void LoadSEO()
    {
      Page.Title = "Printable Fantasy Football Cheat Sheets for " + _currentSportSeason;
      Page.MetaDescription = "Printable fantasy football cheat sheets of the top NFL players for " + _currentSportSeason + ". One page and position-specific draft sheet templates, free & regularly updated.";
      litPageHeader.Text = "Printable Fantasy Football Cheat Sheets in Various Formats";
    }

    private void LoadC2A()
    {
      if (this.User.Identity.IsAuthenticated)
      {
        panVisitorMessage.Visible = false;
      }
    }

    /// <summary>
    /// This method will inject JQuery into the page which will show an instructional popup, if necessaryz
    /// </summary>
    private void LoadInstructionalPopup()
    {

      //string instructionalPopupScript = "$(document).ready(function () { \n" +
      //                           "   $('#" + hlPrintableCheatSheetWithRoster.ClientID + "').qtip({ \n" +
      //                           "   content: { \n" +
      //                           "     text: 'Click any header link to generate an updated, printable cheat sheet', \n" +
      //                           "     title: \n" +
      //                          "     {  " +
      //                           "      text: \"Printable Sheets\", \n" +
      //                           "       button: true \n" +
      //                           "     } \n" +
      //                           "   }, \n" +
      //                           "   show: { \n" +
      //                           "     ready: true, \n" +
      //                           "     event: false \n" +
      //                           "  }, \n" +
      //                           "   position: { \n" +
      //                           "      my: 'rightMiddle', \n" +
      //                           "     at: 'leftMiddle' \n" +
      //                           "   }, \n" +
      //                           "   hide: false, \n" +
      //                           "   style: { \n" +
      //                           "     classes: 'ui-tooltip-instructional', \n" +
      //                           "      width: 170, \n" +
      //                           "     height: 80 \n" +
      //                           "   } \n" +
      //                           " }); \n" +
      //                           " });\n";

      //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "emailPopup", instructionalPopupScript, true);
      
    }

 

    private void LoadSocialTags()
    {
      ResponsiveTwoCol myMaster = (ResponsiveTwoCol)this.Page.Master;
      //myMaster.OpenGraphImage = "https://www.cheatsheetwarroom.com/images/socialsharing/printable/printable-fantasy-football-cheat-sheets.jpg";
      //myMaster.SchemaOrgImage = "https://www.cheatsheetwarroom.com/images/socialsharing/printable/printable-fantasy-football-cheat-sheets.jpg";
      //myMaster.TwitterImage = "https://www.cheatsheetwarroom.com/images/socialsharing/printable/printable-fantasy-football-cheat-sheets.jpg";
    }

   
  }
}