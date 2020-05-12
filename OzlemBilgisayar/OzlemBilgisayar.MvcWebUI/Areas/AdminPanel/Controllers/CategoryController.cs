using OzlemBilgisayar.Business.Abstract;
using OzlemBilgisayar.Entities.Domains;
using OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.CategoryModels;
using OzlemBilgisayar.MvcWebUI.Filters.Areas.AdminPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorizeFilter(Roles = "SuperAdmin,Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new IndexVm
            {
                Categories = _categoryService.GetAll()
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryCreateVm model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category();
                category.Created = category.Modified = DateTime.Now;
                category.Name = model.Name;
                category.IsActive = model.IsActive;

                _categoryService.Add(category);
                TempData["message"] = $"{category.Name} kategorisi başarıyla eklendi";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
               return RedirectToAction("Index");
            }
            var category = _categoryService.GetById(id.Value);
            if (category == null)
            {
               return RedirectToAction("Index");                
            }
            var editVm = new EditVm
            {
                Category = category
            };
            return View(editVm);

        }

        [HttpPost]
        public ActionResult Edit(EditVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var category = model.Category;
            category.Modified = DateTime.Now;
            _categoryService.Update(category);
            TempData["message"] = $"{category.Name} kategorisi başarıyla güncellendi";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var category = _categoryService.GetById(id.Value,"Movies");
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            if (!category.IsActive)
            {
                TempData["message"] = $"{category.Name} kategorisi zaten kullanım dışı bırakılmış";
                return RedirectToAction("Index");
            }
            var deleteVm = new DeleteVm
            {
                Category = category
            };
            return View(deleteVm);

        }

        [HttpPost]
        public ActionResult Delete(DeleteVm model)
        {
            var category = _categoryService.GetById(model.Category.Id);
            category.Modified = DateTime.Now;
            category.IsActive = false;
            _categoryService.Update(category);
            TempData["message"] = $"{category.Name} kategorisi kullanım dışı bırakıldı";
            return RedirectToAction("Index");
        }
    }
}