using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.UI.Services;
using Cswr.Web.Data;
using Cswr.Web.Models;
using Cswr.DAL.Models;
using Cswr.BLL;

namespace Cswr.Web
{
    /// <summary>
    /// Defines the request handling pipeline and configures
    /// the services that we need throughout the application
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SmartSettings>(Configuration.GetSection(SmartSettings.SectionName));

            // Note: This line is for demonstration purposes only, I would not recommend using this as a shorthand approach for accessing settings
            // While having to type '.Value' everywhere is driving me nuts (>_<), using this method means reloaded appSettings.json from disk will not work
            services.AddSingleton(s => s.GetRequiredService<IOptions<SmartSettings>>().Value);


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Configure SQLLite for SmartAdmin DB
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            // Configure SQLServer for CSWR DB
            services.AddDbContext<CswrDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CswrConnection")));


            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // added in 5.0
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddTransient<IEmailSender, EmailSender>();

            // custom repositories
            services.AddScoped<ICheatSheetService, CheatSheetService>();

            // replaces services.AddMvc and makes MVC Work
            services.AddControllersWithViews();

            // add support for razor pages
            services.AddRazorPages();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline (middleware)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // an exception handler for developers only
                app.UseDeveloperExceptionPage();

                // this line required Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore, but installing
                // anything but version 5.0 of this library causes entity framework errors.

                //app.UseDatabaseErrorPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                // an exception handler for non-dev environments
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Resources Http to Https requests
            app.UseHttpsRedirection();
            // Status Code
            app.UseStatusCodePages();
            // Allows site to serve static files
            app.UseStaticFiles();
            // Establishes routing
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=HtmlHelper}/{action=Index}/{id?}");

                // Route for custom sheet since it has static /cheatsheet/ static portion
                //endpoints.MapControllerRoute(
                //    name: "FantasyFootballCustom",
                //    pattern: "{area:exists}/cheatsheet/{controller=custom}/{action=rankings}/{id?}");

                // Route for printable sheets since they have the /cheatsheet/ static portion
                //endpoints.MapControllerRoute(
                //    name: "FantasyFootballPrintable",
                //    pattern: "{area:exists}/{controller=printable}/cheatsheet/{action=index}");

                // The generic route for the Fantasy Football Route
                //endpoints.MapControllerRoute(
                //    name: "FantasyFootball",
                //    pattern: "{area:exists}/{controller=cheatsheet}/{action=custom}/{id?}");

                // Setting the default route in the area
                //endpoints.MapControllerRoute(
                //    name: "DefaultFantasyFootball",
                //    pattern: "{area=fantasyfootball}/{controller=cheatsheet}/{action=custom}/{id?}");

                // map the razor pages for SmartAdmin
                endpoints.MapRazorPages();

            });
        }
    }
}
