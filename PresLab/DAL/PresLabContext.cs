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
        public DbSet<Test> Tests { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}