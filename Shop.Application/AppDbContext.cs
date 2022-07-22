using Microsoft.EntityFrameworkCore;
using Shop.Domain.Products;
using System.Reflection;

namespace Shop.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Регистрация всех IEntityTypeConfiguration<TEntity>
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            #region Products

            builder.Entity<Product>().ToTable("Products", "Products");

            #endregion
        }

            /// <summary>
            /// Товары
            /// </summary>
            public DbSet<Product> Products { get; set; }
    }
}
