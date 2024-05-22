using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Spacdyna.DAL;
using Spacdyna.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = true;
}).AddEntityFrameworkStores<SpacContext>();

builder.Services.AddDbContext<SpacContext>();
var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
         );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
