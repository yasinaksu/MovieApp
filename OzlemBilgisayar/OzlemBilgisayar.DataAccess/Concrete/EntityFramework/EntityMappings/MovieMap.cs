using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.DataAccess.Concrete.EntityFramework.EntityMappings
{
    public class MovieMap : AuditableEntiyBaseMap<Movie>
    {
        public MovieMap()
        {
            ToTable(@"Movies", @"dbo");

            Property(x => x.Image)
                .HasColumnName("Image")
                .HasColumnType("nvarchar")
                .HasMaxLength(255)
                .IsRequired();

            Property(x => x.ImdbRate)
                .HasColumnName("ImdbRate")
                .HasColumnType("nvarchar")
                .HasMaxLength(3)
                .IsRequired();

            Property(x => x.ReleaseDate)
                .HasColumnName("ReleaseDate")
                .HasColumnType("datetime")
                .IsRequired();

            Property(x => x.StoryLine)
                .HasColumnName("StoryLine")
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

            Property(x => x.Title)
                .HasColumnName("Title")
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Trailer)
                .HasColumnName("Trailer")
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

            HasMany(x => x.Categories).WithMany(x => x.Movies).Map(m =>
            {
                m.ToTable("MovieCategories");
                m.MapLeftKey("MovieId");
                m.MapRightKey("CategoryId");
            });        
        }
    }
}
