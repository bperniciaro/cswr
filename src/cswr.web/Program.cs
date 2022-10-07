using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AspStudio.Data;
using Cswr.Web.Lib.Middleware;


// ASP.net Core 6 uses the minimal hosting model
// Middleware is set up in the Program.cs file now
// In c#10, there is no need for a main method

var builder = WebApplication.CreateBuilder(args);

// Add services to the container (what is normally in ConfigureServices)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// Add Sidebar menu json file
builder.Configuration.AddJsonFile("sidebar.json", optional: true, reloadOnChange: true);

var app = builder.Build();

// Configure the HTTP request pipeline. (what is normally in Configure)
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
app.UseEndpoints(endpoints =>
{
	endpoints.MapAreaControllerRoute(  
			name: "Identity",  
			areaName: "Identity",  
			pattern: "Identity/{controller=Home}/{action=Index}");  
	endpoints.MapControllers();
});
app.MapRazorPages();

app.Run();
