using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.DataContext
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Data Source=Koko-ayman7\\SQLEXPRESS;Initial Catalog=Ecommerce_MCV;User Id=sa;password=testpass;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }



        public DbSet<User> Users { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<CoffeItem> CoffeItems { get; set; }

        public DbSet<Category> CategoryItems { get; set; }

        public DbSet<ItemsBuyed> ItemsBuyed { get; set; }

        public DbSet<Reset> Resets { get; set; }
    }
}
