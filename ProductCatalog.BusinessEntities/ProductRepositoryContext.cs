using Microsoft.EntityFrameworkCore;
using ProductCatalog.BusinessEntities.Models;

namespace ProductCatalog.BusinessEntities
{
    public class ProductRepositoryContext : DbContext
    {
        public ProductRepositoryContext(DbContextOptions<ProductRepositoryContext> options)
            : base(options)
           
        {
        }
        public DbSet<ProductCatalogModel> PoductsCatalog { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCatalogModel>().Property(p => p.DateCreated)
            .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<ProductCatalogModel>().HasKey(k => k.Id).ForSqlServerIsClustered();
            base.OnModelCreating(modelBuilder);
        }
    }
}
