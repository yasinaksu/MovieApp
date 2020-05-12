using Core.DataAccess.EntityFramework;
using OzlemBilgisayar.DataAccess.Abstract;
using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.DataAccess.Concrete.EntityFramework
{
    public class EfMovieDal : EfEntityRepositoryBase<Movie, OzlemBilgisayarDbContext>, IMovieDal
    {
        public Movie AddWithCategories(Movie movie, params int[] categoryIds)
        {
            using (var context = new OzlemBilgisayarDbContext())
            {
                if (categoryIds != null && categoryIds.Length > 0)
                {
                    foreach (var categoryId in categoryIds)
                    {
                        movie.Categories.Add(context.Categories.SingleOrDefault(x => x.Id == categoryId));
                    }
                }
                context.Movies.Add(movie);
                context.SaveChanges();
                return movie;
            }
        }

        public Movie UpdateWithCategories(Movie movie, params int[] categoryIds)
        {
            using (var context = new OzlemBilgisayarDbContext())
            {

                var entity = context.Movies.Include("Categories").SingleOrDefault(x => x.Id == movie.Id);
                entity.Created = movie.Created;
                entity.Image = movie.Image;
                entity.ImdbRate = movie.ImdbRate;
                entity.IsActive = movie.IsActive;
                entity.Modified = movie.Modified;
                entity.ReleaseDate = movie.ReleaseDate;
                entity.StoryLine = movie.StoryLine;
                entity.Title = movie.Title;
                entity.Trailer = movie.Trailer;
                if (categoryIds != null && categoryIds.Length > 0)
                {
                    entity.Categories.Clear();
                    foreach (var categoryId in categoryIds)
                    {
                        entity.Categories.Add(context.Categories.SingleOrDefault(c => c.Id == categoryId));
                    }
                }
                context.SaveChanges();
                return entity;
            }
        }
    }
}
