using Core.Aspects.Postsharp.CacheAspects;
using Core.Aspects.Postsharp.ValidationAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.DataAccess;
using OzlemBilgisayar.Business.Abstract;
using OzlemBilgisayar.Business.ValidationRules.FluentValidation;
using OzlemBilgisayar.DataAccess.Abstract;
using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.Business.Concrete
{
    public class MovieManager : IMovieService
    {
        private readonly IMovieDal _movieDal;
        private readonly IQueryableRepository<Movie> _queryable;
        public MovieManager(IMovieDal movieDal, IQueryableRepository<Movie> queryable)
        {
            _movieDal = movieDal;
            _queryable = queryable;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Movie> GetAll(Expression<Func<Movie, bool>> filter = null, params string[] includeList)
        {
            return includeList == null
                ? _movieDal.GetAll(filter)
                : _movieDal.GetAll(filter, includeList);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public Movie GetById(int id, params string[] includeList)
        {
            return includeList == null
                ? _movieDal.Get(p => p.Id == id)
                : _movieDal.Get(p => p.Id == id, includeList);
        }

        [FluentValidationAspect(typeof(MovieValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Movie Add(Movie movie, params int[] categoryIds)
        {
            return categoryIds == null
                ? _movieDal.Add(movie)
                : _movieDal.AddWithCategories(movie, categoryIds);
        }

        [FluentValidationAspect(typeof(MovieValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Movie Update(Movie movie, params int[] categoryIds)
        {
            return categoryIds == null
                ? _movieDal.Update(movie)
                : _movieDal.UpdateWithCategories(movie, categoryIds);
        }       

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Movie> GetLatestFiftyMovies()
        {
            return _queryable.Table.OrderByDescending(x => x.ReleaseDate).Take(50).ToList();
        }

        public int MovieCount()
        {
            return _queryable.Table.Count();
        }
    }
}
