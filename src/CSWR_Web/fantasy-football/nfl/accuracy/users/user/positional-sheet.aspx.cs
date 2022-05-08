using System;
using BP.CheatSheetWarRoom.UI;

namespace NFL.Accuracy.Users.User
{
  public partial class UserQuarterbacksCheatSheet : BasePage
  {

    private string _archivedSheetUsername;
    private string _seasonCode;
    private string _positionCode;
      private BasePage _basePage;

    protected void Page_Load(object sender, EventArgs e)
    {

      if (ProcessRoutes())
      {
        fooGradedSheet.SeasonCode = _seasonCode;
        fooGradedSheet.Username = _archivedSheetUsername;
        fooGradedSheet.PositionCode = _positionCode;
      }
    }

    private bool ProcessRoutes()
    {
      var usernameFound = true;
      var seasonFound = true;
      var positionFound = true;

      // Determine if the Username route parameters is found, if so load it
      if (Page.RouteData.Values["Username"] != null)
      {
        _archivedSheetUsername = Page.RouteData.Values["Username"].ToString();
      }
      else
      {
        usernameFound = false;
      }

      // Determine if the SeasonCode route parameters is found, if so load it
      if (Page.RouteData.Values["SeasonCode"] != null)
      {
        _seasonCode = Page.RouteData.Values["SeasonCode"].ToString();
      }
      else
      {
        seasonFound = false;
      }

      // Determine if the PositionCode route parameters is found, if so load it
      if (Page.RouteData.Values["PositionCode"] != null)
      {
        _positionCode = Page.RouteData.Values["PositionCode"].ToString();
      }
      else
      {
        positionFound = false;
      }

        _basePage = (this.Page as BP.CheatSheetWarRoom.UI.BasePage);
      _basePage.Title = "User Accuracy for " + _positionCode + " Rankings";

            return (usernameFound && seasonFound && positionFound);
    }


  }
}