namespace OzlemBilgisayar.CopyDbConsoleApp.MovieListDb
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MovieListDbModel : DbContext
    {
        public MovieListDbModel()
            : base("name=MovieListDbContext")
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
