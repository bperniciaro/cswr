using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cswr.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // host configures a server and request processing pipeline
            // sets up the application with some defaults
            // we create the host, then build it, then run it
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            // configures a web server name the Kestrel
            // still uses IIS behind the scenes
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Startup type performs the configuration of the app
                    webBuilder.UseStartup<Startup>();
                });
    }
}
