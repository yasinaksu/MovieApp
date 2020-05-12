using OzlemBilgisayar.Business.Abstract;
using OzlemBilgisayar.Entities.Domains;
using OzlemBilgisayar.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OzlemBilgisayar.MvcWebUI.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ICategoryService _categoryService;
        public MovieController(IMovieService movieService, ICategoryService categoryService)
        {
            _movieService = movieService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult Index(int? page, int? id)
        {
            List<Movie> movies = new List<Movie>();
            if (id.HasValue)
            {
                movies = _categoryService.GetById(id.Value, "Movies").Movies.Where(x => x.IsActive).OrderByDescending(x=>x.ReleaseDate).ToList();
            }
            else
            {
                movies = _movieService.GetAll(x => x.IsActive).OrderByDescending(x => x.ReleaseDate).ToList();
            }
            var pager = new Pager(movies.Count, page,12);

            var viewModel = new MovieIndexVm
            {
                Movies = movies.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager,
                MovieCount = movies.Count
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Search(string q, int? page)
        {
            List<Movie> movies = _movieService.GetAll(x => x.Title.ToLower().Contains(q.ToLower()) && x.IsActive).OrderByDescending(x => x.ReleaseDate).ToList();
            var pager = new Pager(movies.Count, page,12);

            var viewModel = new MovieSearchVm
            {
                Movies = movies.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager,
                MovieCount = movies.Count,
                q=q
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult LatestMovies()
        {
            var model = new LatestMoviesVm
            {
                Movies = _movieService.GetLatestFiftyMovies()
            };
            return View(model);
        }

        [HttpPost]
        public JsonResult GetMovieDetails(int movieId)
        {
            var movie = _movieService.GetById(movieId, "Categories");
            var model = new GetMovieDetailsVm
            {
                Categories = movie.Categories.Select(x => x.Name).ToList(),
                Image = movie.Image,
                ImdbRate = movie.ImdbRate,
                ReleaseYear = movie.ReleaseDate.Year,
                StoryLine = movie.StoryLine,
                Title = movie.Title,
                Trailer = movie.Trailer
            };

            return Json(new { Movie = model });
        }

        [HttpPost]
        public JsonResult GetMovieInfoToAddShoppingCart(int movieId)
        {
            var movie = _movieService.GetById(movieId);
            return Json(new { movie.Id, movie.Title, movie.Image });
        }


    }
}