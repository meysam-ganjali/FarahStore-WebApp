using FarahStoreApplication.UnitOfWorkPattern;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FarahStoreWeb.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using FarahStoreUtilities;

var builder = WebApplication.CreateBuilder(args);
#region DataBaseCnetxt, Identity

var connectionString = builder.Configuration.GetConnectionString("DatabaseContexConnection") ?? throw new InvalidOperationException("Connection string 'DatabaseContexConnection' not found.");

builder.Services.AddDbContext<DatabaseContex>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DatabaseContex>().AddDefaultTokenProviders();


#endregion


#region Add services to the container

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/Identity/Account/Login";
    option.LogoutPath = "/Identity/Account/Logout";
    option.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

#endregion

#region Configure the HTTP request pipeline.

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();

#endregion

