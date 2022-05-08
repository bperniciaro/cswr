using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BP.CheatSheetWarRoom
{
  /// <summary>
  /// Summary description for WebApiConfig
  /// </summary>
  public class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      config.Routes.MapHttpRoute(
      name: "DefaultApi",
      routeTemplate: "api/{controller}/{id}",
       defaults: new { id = RouteParameter.Optional }
     );
    }
  }
}
