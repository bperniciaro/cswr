using System;
using System.Web.Management;

namespace BP.CheatSheetWarRoom
{
  public abstract class WebCustomEvent : WebRequestErrorEvent
  {
    public WebCustomEvent(string message, object eventSource, int eventCode, Exception e)
      : base(message, eventSource, eventCode, e) { }
  }

  public class CSWRWebEvent : WebCustomEvent
  {
    private const int eventCode = WebEventCodes.WebExtendedBase + 11;
    //private const string message = "The {0} with ID = {1} was not found.";

    public CSWRWebEvent(string message, object eventSource, Exception e)
      : base(message, eventSource, eventCode, e)
    { }
  }
}

