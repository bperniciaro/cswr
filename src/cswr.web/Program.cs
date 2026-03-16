// <copyright file="Program.cs" company="Fornits Web Solutions">
// Copyright (c) Fornits Web Solutions. All rights reserved.
// </copyright>

using System.Reflection;
using System.Text.Json.Serialization;
using AspStudio.Data;
using Cswr.Bal.Lib.Mappers;
using Cswr.Bal.Services.Interfaces.Readers;
using Cswr.Bal.Services.Interfaces.Writers;
using Cswr.Bal.Services.Readers;
using Cswr.Bal.Services.Writers;
using Cswr.Dal.Context;
using Cswr.Web.Lib.Config;
using Cswr.Web.Lib.Middleware;
using Cswr.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

// Minimal Hosting Model
// Middleware is set up in the Program.cs file now

var builder = WebApplication.CreateBuilder(args);

/* ********************************************************************* */
// Configure the HTTP request pipeline. (what is historically in ConfigureServices)
/* ********************************************************************* */

// Get the connection string from the appsettings file.
var connectionString = builder.Configuration.GetConnectionString("cswr");

// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();
GlobalDiagnosticsContext.Set("connectionString", connectionString);

// Tell Identity about the connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Tell Entity Framework about the connection string, enable full logging, and don't use tracking to speed things up
//builder.Services.AddDbContext<CswrDbContext>(options =>
//    options.UseSqlServer(connectionString).EnableSensitiveDataLogging().UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
// we should use tracking by default, but add .AsNoTracking (i.e. "disconnected") to queries where it isn't required.
builder.Services.AddDbContext<CswrDbContext>(options =>
    options.UseSqlServer(connectionString).EnableSensitiveDataLogging());

// Developer debug page
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Initializing a default identity
builder.Services.AddDefaultIdentity<IdentityUser>(
        options =>
            options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure password complexity
builder.Services.Configure<IdentityOptions>(options =>
{
    if(builder.Environment.IsDevelopment())
    {
        // Default Password settings.
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;
    }
});

// Configure AutoMapper for the various mapping profiles
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<BalMappingProfile>();
    cfg.AddProfile<WebMappingProfile>();
});
builder.Services.AddControllersWithViews();

// Add controllers with views
// Tell EFCore to ignore recursive references to avoid exceptions
builder.Services.AddControllersWithViews().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add social login providers
//builder.Services.AddAuthentication()
//    .AddFacebook(options =>
//    {
//        IConfigurationSection FBAuthNSection =
//        builder.Configuration.GetSection("SocialLogins:Facebook");
//        options.ClientId = FBAuthNSection["ClientId"];
//        options.ClientSecret = FBAuthNSection["ClientSecret"];
//    });


// Appsettings.json is added by default in the minimal hosting model
// Add Sidebar menu json file
// May need more sidebars for different roles
builder.Configuration.AddJsonFile("sidebar.json", optional: true, reloadOnChange: true);

// Set up dependency injection for configuration

// SMTP Configuration
var smtpMailConfig = builder.Configuration.GetSection(SmtpMailConfig.Section)?.Get<SmtpMailConfig>();
if (smtpMailConfig != null)
{
    builder.Services.AddSingleton<SmtpMailConfig>(smtpMailConfig);
}

// Social Login Configuration
var sociaLoginConfig = builder.Configuration.GetSection(SocialLoginConfig.Section)?.Get<SocialLoginConfig>();
if (sociaLoginConfig != null)
{
    builder.Services.AddSingleton<SocialLoginConfig>(sociaLoginConfig);
}

// Set up dependency injection for custom services
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<ICheatSheetWriter, CheatSheetWriter>();
builder.Services.AddTransient<ICheatSheetReader, CheatSheetReader>();
builder.Services.AddTransient<ICheatSheetItemReader, CheatSheetItemReader>();
builder.Services.AddTransient<IPlayerReader, PlayerReader>();


var app = builder.Build();



/* ********************************************************************* */
// Configure the HTTP request pipeline. (what is normally in Configure)
/* ********************************************************************* */

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


// Configure application redirection from the old site to the new site,  Eventually, probably want to have
// a json file that maps the old URLs to new URLs

//app.Use(async (context, next) =>
//{
//    var url = context.Request.Path.Value;

//    // Redirect to an external URL
//    if (url.Contains("/fantasy-football/nfl/create/managesheets.aspx"))
//    {
//        context.Response.Redirect("https://localhost:44341/fantasy-football/create/cheatsheets/manage");
//        return;   // short circuit
//    }

//    await next();
//});


app.UseStaticFiles();

app.UseMiddleware<RedirectMiddleware>();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
        name: "Identity",
        areaName: "Identity",
        pattern: "Identity/{controller=Home}/{action=Index}");

app.MapControllers();

app.MapRazorPages();

app.Run();
