using FarahStoreApplication.UnitOfWorkPattern;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FarahStoreWeb.Data;

var builder = WebApplication.CreateBuilder(args);
#region DataBaseCnetxt, Identity

var connectionString = builder.Configuration.GetConnectionString("DatabaseContexConnection") ?? throw new InvalidOperationException("Connection string 'DatabaseContexConnection' not found.");

builder.Services.AddDbContext<DatabaseContex>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<DatabaseContex>();

#endregion


#region Add services to the container

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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

