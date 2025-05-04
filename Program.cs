using GaryKAuthProject.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
string? GoogleClientID = builder.Configuration["GoogleClientID"];
string? GoogleClientSecret = builder.Configuration["GoogleClientSecret"];

if (GoogleClientID == null || GoogleClientSecret == null)
{
    throw new ApplicationException("Please configure \"GoogleClientID\" and \"GoogleClientSecret\" for operation.");
}

builder.Services.AddAuthentication()
                .AddGoogle(googleOptions => {
                    googleOptions.ClientId = GoogleClientID;// Configuration["Google:ClientId"];
                    googleOptions.ClientSecret = GoogleClientSecret;// Configuration["Google:ClientSecret"];
                });

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IEmailSender>(s => new GaryKAuthProject.Services.StubEmailProvider(builder.Configuration));
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
