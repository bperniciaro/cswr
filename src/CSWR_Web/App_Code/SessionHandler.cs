using System;
using System.Web;

/// <summary>
/// Summary description for SessionHandler
/// </summary>
namespace BP.CheatSheetWarRoom.UI
{
  public class SessionHandler
  {

    //public static int CurrentUserPersonnelID
    //{
    //  get
    //  {
    //    if (HttpContext.Current.Session["CurrentUserPersonnelID"] != null)
    //    {
    //      return (int)HttpContext.Current.Session["CurrentUserPersonnelID"];
    //    }
    //    else
    //    {
    //      return 0;
    //    }
    //  }
    //  set { HttpContext.Current.Session["CurrentUserPersonnelID"] = value; }
    //}

    public static string CurrentFOOVisitorSheetPosition
    {
      get
      {
        if (HttpContext.Current.Session["CurrentFOOVisitorSheetPosition"] != null)
        {
          return HttpContext.Current.Session["CurrentFOOVisitorSheetPosition"].ToString();
        }
        else
        {
          return null;
        }
      }
      set { HttpContext.Current.Session["CurrentFOOVisitorSheetPosition"] = value; }
    }


    public static string CurrentRACVisitorSheetPosition
    {
      get
      {
        if (HttpContext.Current.Session["CurrentRACVisitorSheetPosition"] != null)
        {
          return HttpContext.Current.Session["CurrentRACVisitorSheetPosition"].ToString();
        }
        else
        {
          return null;
        }
      }
      set { HttpContext.Current.Session["CurrentRACVisitorSheetPosition"] = value; }
    }


    public static bool FiguredOutReordering
    {
      get
      {
        if (HttpContext.Current.Session["FiguredOutReordering"] != null)
        {
          return (bool)HttpContext.Current.Session["FiguredOutReordering"];
        }
        else
        {
          return false;
        }
      }
      set { HttpContext.Current.Session["FiguredOutReordering"] = value; }
    }

    public static bool PrintablePopupShown
    {
      get
      {
        if (HttpContext.Current.Session["PrintablePopupShown"] != null)
        {
          return (bool)HttpContext.Current.Session["PrintablePopupShown"];
        }
        else
        {
          return false;
        }
      }
      set { HttpContext.Current.Session["PrintablePopupShown"] = value; }
    }

    public static string CurrentSportCode
    {
      get
      {
        if (HttpContext.Current.Session["CurrentSportCode"] != null)
        {
          return (string)HttpContext.Current.Session["CurrentSportCode"];
        }
        else
        {
          return "FOO";  
        }
      }
      set { HttpContext.Current.Session["CurrentSportCode"] = value; }
    }


    public static bool IsRefresh
    {
      get
      {
        if (HttpContext.Current.Session["IsRefresh"] != null)
        {
          return (bool)HttpContext.Current.Session["IsRefresh"];
        }
        else
        {
          return false;
        }
        //return (bool)HttpContext.Current.Session["IsRefresh"];
      }
      set { HttpContext.Current.Session["IsRefresh"] = value; }
    }

    public static string GetCurrentVisitorSheetPosition(string sportCode)
    {
      string visitorSheetPosition = String.Empty;
      switch (sportCode)
      {
        case "FOO":
          visitorSheetPosition = SessionHandler.CurrentFOOVisitorSheetPosition;
          break;
        case "RAC":
          visitorSheetPosition = SessionHandler.CurrentRACVisitorSheetPosition;
          break;
      }
      return visitorSheetPosition;
    }

    public static void SetCurrentVisitorSheetPosition(string sportCode, string positionCode)
    {
      switch (sportCode)
      {
        case "FOO":
          SessionHandler.CurrentFOOVisitorSheetPosition = positionCode;
          break;
        case "RAC":
          SessionHandler.CurrentRACVisitorSheetPosition = positionCode;
          break;
      }
    }

  }
}