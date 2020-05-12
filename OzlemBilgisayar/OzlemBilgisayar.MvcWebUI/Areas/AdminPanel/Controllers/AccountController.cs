using OzlemBilgisayar.Business.Abstract;
using OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Web.Security;
using Core.CrossCuttingConcerns.Security.Web;
using OzlemBilgisayar.MvcWebUI.Filters.Areas.AdminPanel;
using Core.CrossCuttingConcerns.Security;
using System.IO;
using OzlemBilgisayar.MvcWebUI.Utilities;

namespace OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Controllers
{
    //[CustomAuthenticationFilter]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)//string returnUrl
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "Home", new { area = "AdminPanel" });
            }
            else
            {
                var model = new AccountLoginVm
                {
                    ReturnUrl = returnUrl
                };
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Login(AccountLoginVm model, string returnUrl)
        {
            var hashedPassword = Crypto.SHA256(model.Password);
            var user = _userService.GetByEmailAndPassword(model.Email, hashedPassword, "Roles");
            if (user != null)
            {
                AuthenticationHelper.CreateAuthCookie(
                user.Id,
                user.UserName,
                user.Email,
                DateTime.Now.AddDays(15),
                user.Roles.Select(u => u.Name).ToArray(),
                false,
                user.FirstName,
                user.LastName);
            }
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("DashBoard", "Home", new { area = "AdminPanel" });
        }

        [HttpGet]
        [CustomAuthenticationFilter]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [CustomAuthenticationFilter]
        [CustomAuthorizeFilter(Roles = "SuperAdmin,Admin")]
        public ActionResult UserProfile(int? id)
        {
            if (id.HasValue)
            {
                var model = new AccountUserProfileVm
                {
                    User = _userService.GetById(id.Value, "Roles")
                };
                return View(model);
            }

            return RedirectToAction("Dashboard", "Home");
        }

        [HttpGet]
        [CustomAuthenticationFilter]
        [CustomAuthorizeFilter(Roles = "SuperAdmin,Admin")]
        public ActionResult ChangePassword()
        {
            var activeUser = _userService.GetById((User.Identity as Identity).Id);
            if (activeUser != null)
            {
                var model = new AccountChangePasswordVm
                {
                    Id = activeUser.Id
                };
                return View(model);
            }

            return RedirectToAction("Dashboard", "Home");
        }

        [HttpPost]
        [CustomAuthenticationFilter]
        [CustomAuthorizeFilter(Roles = "SuperAdmin,Admin")]
        public ActionResult ChangePassword(AccountChangePasswordVm model)
        {
            var user = _userService.GetById(model.Id);
            var currentPasswordFromModel = Crypto.SHA256(model.Password);
            if (user.Password != currentPasswordFromModel)
            {
                ModelState.AddModelError("Password", "Parolanız yanlış");
                return View(model);
            }
            var hashedNewPassword = Crypto.SHA256(model.NewPassword);
            user.Password = hashedNewPassword;
            user.Modified = DateTime.Now;
            _userService.Update(user);
            return RedirectToAction("Logout");
        }

        [HttpGet]
        [CustomAuthenticationFilter]
        [CustomAuthorizeFilter(Roles = "SuperAdmin,Admin")]
        public ActionResult Edit()
        {
            var user = _userService.GetById((User.Identity as Identity).Id);
            if (user != null)
            {
                var model = new AccountEditVm
                {
                    CurrentImage = user.Image,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Id = user.Id,
                    IsActive = user.IsActive,
                    LastName = user.LastName
                };
                return View(model);
            }

            return RedirectToAction("UserProfile", "Account",new { id= (User.Identity as Identity).Id });
        }

        [HttpPost]
        [CustomAuthenticationFilter]
        [CustomAuthorizeFilter(Roles = "SuperAdmin,Admin")]
        public ActionResult Edit(AccountEditVm model)
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
            user.LastName = model.LastName;
            user.Modified = DateTime.Now;
            _userService.Update(user);
            TempData["Message"] = "Bilgileriniz başarıyla güncellendi.";
            return RedirectToAction("UserProfile", "Account", new { id = user.Id });
        }
    }
}