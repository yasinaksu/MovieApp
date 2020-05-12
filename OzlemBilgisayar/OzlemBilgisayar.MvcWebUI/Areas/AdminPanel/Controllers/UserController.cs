using OzlemBilgisayar.Business.Abstract;
using OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using OzlemBilgisayar.Entities.Domains;
using OzlemBilgisayar.MvcWebUI.Utilities;
using System.IO;
using OzlemBilgisayar.MvcWebUI.Filters.Areas.AdminPanel;

namespace OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorizeFilter(Roles = "SuperAdmin,Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            UserIndexVm model = new UserIndexVm
            {
                Users = _userService.GetAll(null, "Roles")
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Roles = _roleService.GetAll(x => x.Name != "SuperAdmin").Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            return View();
        }

        //post metodunu tamamla
        [HttpPost]
        public ActionResult Create(UserCreateVm model)
        {
            var user = new User()
            {
                Created = DateTime.Now,
                Email = model.Email,
                FirstName = model.FirstName,
                IsActive = model.IsActive,
                LastName = model.LastName,
                Modified = DateTime.Now,
                Password = Crypto.SHA256("123456"),
                UserName = $"{model.FirstName}_{model.LastName}_{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}"
            };
            if (model.Image != null && model.Image.ContentLength > 0)
            {
                var fileName = Path.GetRandomFileName() + Path.GetExtension(model.Image.FileName);
                var path = Server.MapPath(Path.Combine("~/Uploads/Img", fileName));
                ImageManager.Upload(model.Image, path);
                user.Image = fileName;
            }
            else
            {
                user.Image = "default.jpg";
            }
            _userService.Add(user, model.Roles);
            if (user.Id > 0)
            {
                TempData["Message"] = $"{user.FirstName} {user.LastName} başarıyla eklendi";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Roles = _roleService.GetAll(x => x.Name != "SuperAdmin").Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
                return View(model);
            }

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                TempData["Message"] = "Güncelleme yapmak için lütfen önce bir kullanıcı seçin";
                return RedirectToAction("Index");
            }
            var user = _userService.GetById(id.Value, "Roles");
            var model = new UserEditVm
            {
                CurrentImage = user.Image,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                IsActive = user.IsActive,
                LastName = user.LastName,
                Roles = user.Roles.Select(x => x.Id).ToArray(),
                RoleList = _roleService.GetAll(x => x.Name != "SuperAdmin").Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToArray()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserEditVm model)
        {
            var user = _userService.GetById(model.Id);
            if (model.Image != null)
            {
                var fileName = Path.GetRandomFileName() + Path.GetExtension(model.Image.FileName);
                var path = Server.MapPath(Path.Combine("~/Uploads/Img", fileName));
                if (ImageManager.Upload(model.Image, path))
                {
                    if (user.Image != "default.jpg")
                    {
                        var pathToDelete = Server.MapPath(Path.Combine("~/Uploads/Img", user.Image));
                        ImageManager.Delete(pathToDelete);
                    }
                    user.Image = fileName;

                }
            }
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.IsActive = model.IsActive;
            user.LastName = model.LastName;
            user.Modified = DateTime.Now;
            var entity = _userService.Update(user, model.Roles);
            if (user.Modified == entity.Modified)
            {
                TempData["Message"] = $"{user.FirstName} {user.LastName} kullanıcısı güncellendi.";
                return RedirectToAction("Index");
            }
            model.RoleList = _roleService.GetAll(x => x.Name != "SuperAdmin").Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToArray();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                TempData["Message"] = "Lütfen önce bir kullanıcı seçin";
                return RedirectToAction("Index");
            }
            var user = _userService.GetById(id.Value, "Roles");
            var model = new UserDetailsVm
            {
                Image = user.Image,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                IsActive = user.IsActive,
                LastName = user.LastName,
                Created=user.Created,
                Roles = user.Roles.Select(x => x.Name).ToArray()                
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int ? id)
        {
            if (!id.HasValue)
            {
                TempData["Message"] = "Lütfen önce bir kullanıcı seçin";
                return RedirectToAction("Index");
            }
            var user = _userService.GetById(id.Value, "Roles");
            if (!user.IsActive)
            {
                TempData["message"] = $"{user.FirstName} {user.LastName} kullanıcısı zaten kullanım dışı bırakılmış";
                return RedirectToAction("Index");
            }
            var model = new UserDeleteVm
            {
                Image = user.Image,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                IsActive = user.IsActive,
                LastName = user.LastName,
                Created = user.Created,
                Roles = user.Roles.Select(x => x.Name).ToArray()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(UserDeleteVm model)
        {
            var user = _userService.GetById(model.Id);
            user.Modified = DateTime.Now;
            user.IsActive = false;
            _userService.Update(user);
            TempData["message"] = $"{user.FirstName} {user.LastName} kullanıcısı kullanım dışı bırakıldı";
            return RedirectToAction("Index");
        }

    }
}