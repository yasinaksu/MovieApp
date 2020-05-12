namespace OzlemBilgisayar.CopyDbConsoleApp.OzlemBilgisayarDb
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OzlemBilgisayarDbContext : DbContext
    {
        public OzlemBilgisayarDbContext()
            : base("name=OzlemBilgisayarDbContext")
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>()
                .HasMany(e => e.Movies)
                .WithMany(e => e.Categories)
                .Map(m => m.ToTable("MovieCategories").MapLeftKey("CategoryId").MapRightKey("MovieId"));
        }
    }
}
