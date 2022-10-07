using System.Text.RegularExpressions;

namespace Cswr.Web.Lib.Middleware
{
    public class RedirectMiddleware
    {
        private RequestDelegate _nextDelegate;
        private IServiceProvider _serviceProvider;

        public RedirectMiddleware(RequestDelegate nextDelegate, IServiceProvider serviceProvider)
        {
            _nextDelegate = nextDelegate;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string requestURL = httpContext.Request.Path.ToString().ToLower();

            //redirect domain/home/index and domain/home to domain
            if (requestURL.Contains("/fantasy-football/nfl/create/managesheets.aspx"))
            {
                httpContext.Response.Redirect("/fantasy-football/create/cheatsheets/manage");
            }
            // redirect domain/home/something  to domain/something
            else if (requestURL.Contains("/home/"))
            {
                Regex reg = new Regex("/home/(.+)");
                Match match = reg.Match(requestURL);
                string value = match.Groups[1].Value;
                httpContext.Response.Redirect("/" + value);
            }
            else
            {

                await _nextDelegate.Invoke(httpContext);
            }

        }
    }
}
