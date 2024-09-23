using BusinessLogic;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using E_starta.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the DbContext with the connection string
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register session services
builder.Services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Register cookie-based authentication services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index"; // Redirect to login page if not authenticated
        options.LogoutPath = "/Login/Logout"; // Redirect after logout
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Expiration time for authentication
        options.SlidingExpiration = true; // Renew cookie expiration if the user is active
    });

// Register application services
builder.Services.AddSingleton<DbContextController>();
builder.Services.AddTransient<AdminLogic>();
builder.Services.AddTransient<LoginLogic>();
builder.Services.AddTransient<EmployeeLogic>();
builder.Services.AddTransient<AdminModel>();
builder.Services.AddTransient<RegularLogic>();
builder.Services.AddTransient<RegularModel>();
builder.Services.AddTransient<TicketLogic>();
builder.Services.AddTransient<ManagerLogic>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Use authentication before authorization
app.UseAuthentication(); // Add this line to enable authentication
app.UseAuthorization();
app.UseSession(); // Add this line to enable session middleware

// Define routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
