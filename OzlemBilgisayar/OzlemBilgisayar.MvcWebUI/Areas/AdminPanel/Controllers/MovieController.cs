using OzlemBilgisayar.Business.Abstract;
using OzlemBilgisayar.Entities.Domains;
using OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.MovieModels;
using OzlemBilgisayar.MvcWebUI.Filters.Areas.AdminPanel;
using OzlemBilgisayar.MvcWebUI.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorizeFilter(Roles = "SuperAdmin,Admin")]
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
        public ActionResult Index()
        {
            var model = new MovieIndexVm
            {
                Movies = _movieService.GetAll()
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categories = _categoryService.GetAll(x=>x.IsActive).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Create(MovieCreateVm model)
        {
            if (ModelState.IsValid)
            {
                var image = model.Image;
                var movie = new Movie
                {
                    Created = DateTime.Now,
                    ImdbRate = model.ImdbRate,
                    IsActive = model.IsActive,
                    Modified = DateTime.Now,
                    ReleaseDate = model.ReleaseDate,
                    StoryLine = model.StoryLine,
                    Title = model.Title,
                    Trailer = model.Trailer
                };

                var fileName =
                    Path.GetRandomFileName().Replace('.', '-')
                    + Path.GetExtension(image.FileName);

                var path = Path.Combine(Server.MapPath("~/Uploads/Img"), fileName);

                if (ImageManager.Upload(image, path))
                {
                    movie.Image = fileName;
                }
                _movieService.Add(movie, model.Categories);
                TempData["Message"] = $"{movie.Title} başarıyla eklendi";
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _categoryService.GetAll(x => x.IsActive).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            var movie = _movieService.GetById(id.Value, "Categories");
            if (movie == null)
            {
                return RedirectToAction("Index");
            }

            var model = new MovieEditVm();
            model.Id = movie.Id;
            model.ImageName = movie.Image;
            model.ImdbRate = movie.ImdbRate;
            model.IsActive = movie.IsActive;
            model.ReleaseDate = movie.ReleaseDate;
            model.StoryLine = movie.StoryLine;
            model.Title = movie.Title;
            model.Trailer = movie.Trailer;
            model.Categories = movie.Categories.Select(x => x.Id).ToArray();

            model.CategoryList = _categoryService.GetAll(x=>x.IsActive).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToArray();


            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(MovieEditVm model)
        {
            var movie = _movieService.GetById(model.Id);

            if (model.Image != null)
            {
                var fileName = Path.GetRandomFileName().Replace('.', '-') + Path.GetExtension(model.Image.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads/Img"), fileName);

                if (ImageManager.Upload(model.Image, path))
                {
                    var oldImagePath = Path.Combine(Server.MapPath("~/Uploads/Img"), movie.Image);
                    ImageManager.Delete(oldImagePath);
                    movie.Image = fileName;
                }
            }
            movie.ImdbRate = model.ImdbRate;
            movie.IsActive = model.IsActive;
            movie.Modified = DateTime.Now;
            movie.ReleaseDate = model.ReleaseDate;
            movie.StoryLine = model.StoryLine;
            movie.Title = model.Title;
            movie.Trailer = model.Trailer;

            _movieService.Update(movie, model.Categories);
            TempData["Message"] = $"{movie.Title} başarıyla güncellendi";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var movie = _movieService.GetById(id.Value);
            if (movie == null)
            {
                return RedirectToAction("Index");
            }
            if (!movie.IsActive)
            {
                TempData["message"] = $"{movie.Title} Filmi zaten kullanım dışı bırakılmış";
                return RedirectToAction("Index");
            }
            var model = new MovieDeleteVm
            {
                Movie = movie
            };
            return View(model);

        }

        [HttpPost]
        public ActionResult Delete(MovieDeleteVm model)
        {
            var movie = _movieService.GetById(model.Movie.Id);
            movie.Modified = DateTime.Now;
            movie.IsActive = false;
            _movieService.Update(movie);
            TempData["message"] = $"{movie.Title} filmi kullanım dışı bırakıldı";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var movie = _movieService.GetById(id.Value,"Categories");
            if (movie == null)
            {
                return RedirectToAction("Index");
            }
            var model = new MovieDetailsVm
            {
                Categories = movie.Categories.Select(x => x.Name).ToList(),
                Image = movie.Image,
                ImdbRate = movie.ImdbRate,
                ReleaseDate = movie.ReleaseDate,
                StoryLine = movie.StoryLine,
                Title = movie.Title,
                Trailer = movie.Trailer
            };
            return View(model);
        }

    }
}