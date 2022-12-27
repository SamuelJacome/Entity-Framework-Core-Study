using Microsoft.EntityFrameworkCore;
using src.Domain;
using src.Provider;

namespace src.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Product> Products { get; set; }

        private readonly TenantData _tenant;

        public ApplicationContext(DbContextOptions<ApplicationContext> options,
        TenantData tenant

        ) : base(options)
        {
            _tenant = tenant;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, Name = "Samuel 1", TenantId = "tenant-1" },
                new Person { Id = 2, Name = "Carlos 2", TenantId = "tenant-2" },
                new Person { Id = 3, Name = "Neuton 3", TenantId = "tenant-2" });

            modelBuilder.Entity<Product>().HasData(
                new Person { Id = 1, Name = "Product 1", TenantId = "tenant-1" },
                new Person { Id = 2, Name = "Product 2", TenantId = "tenant-2" },
                new Person { Id = 3, Name = "Product 3", TenantId = "tenant-2" });

            modelBuilder.Entity<Person>().HasQueryFilter(_ => _.TenantId == _tenant.TenantId);
            modelBuilder.Entity<Product>().HasQueryFilter(_ => _.TenantId == _tenant.TenantId);
        }
    }
}