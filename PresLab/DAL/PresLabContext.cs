using PresLab.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PresLab.DAL
{
    public class PresLabContext : DbContext
    {

        public PresLabContext() : base("PresLabContext")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Product>()
                .HasMany<Order>(c => c.Samplings)
                .WithMany(s => s.Samplings)
                .Map(us =>
                {
                    us.MapLeftKey("ProductId");
                    us.MapRightKey("OrderId");
                    us.ToTable("Samplings");
                });
        }
    }
}