using FarahStoreModel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FarahStoreWeb.Data;

public class DatabaseContex : IdentityDbContext<IdentityUser>
{
    public DatabaseContex(DbContextOptions<DatabaseContex> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<ChaildCategory> ChaildCategories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductColor> ProductColors { get; set; }
    public DbSet<Warranty> Warranties { get; set; }
    public DbSet<ProductSpecifications> ProductSpecifications { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Cart> Carts { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
