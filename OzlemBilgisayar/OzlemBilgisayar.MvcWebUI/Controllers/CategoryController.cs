using OzlemBilgisayar.Business.Abstract;
using OzlemBilgisayar.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OzlemBilgisayar.MvcWebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [ChildActionOnly]
        public ActionResult _CategoryMenu()
        {
            var model = new CategoryMenuVm
            {
                Categories = _categoryService.GetAll(x => x.IsActive)
            };
            var categoryId = RouteData.Values["id"];
            if (categoryId!=null)
            {
                model.SelectedCategoryId = Convert.ToInt32(categoryId);
            }
           
            return PartialView(model);
        }
    }
}