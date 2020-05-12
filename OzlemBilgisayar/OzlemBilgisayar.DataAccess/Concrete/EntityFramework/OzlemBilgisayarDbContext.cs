using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OzlemBilgisayar.DataAccess.Concrete.EntityFramework.EntityMappings;
using OzlemBilgisayar.Entities.Domains;

namespace OzlemBilgisayar.DataAccess.Concrete.EntityFramework
{
    public class OzlemBilgisayarDbContext : DbContext
    {
        public OzlemBilgisayarDbContext() : base("OzlemBilgisayarDbContext")
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<OzlemBilgisayarDbContext>());
            Database.SetInitializer(new DbInitializer());

            //close lazy loading DbInitializer
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new MovieMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new OrderMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
