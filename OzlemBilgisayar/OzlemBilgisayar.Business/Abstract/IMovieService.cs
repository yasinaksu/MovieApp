using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.Business.Abstract
{
    public interface IMovieService
    {
        List<Movie> GetAll(Expression<Func<Movie, bool>> filter = null, params string[] includeList);
        List<Movie> GetLatestFiftyMovies();
        Movie GetById(int id, params string[] includeList);
        Movie Add(Movie movie, params int[] categoryIds);
        Movie Update(Movie movie, params int[] categoryIds);
        int MovieCount();
    }
}
