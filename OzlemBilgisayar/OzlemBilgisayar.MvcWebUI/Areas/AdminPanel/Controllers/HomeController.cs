using OzlemBilgisayar.MvcWebUI.Filters.Areas.AdminPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthenticationFilter]
        [CustomAuthorizeFilter(Roles ="SuperAdmin,Admin")]
        public ActionResult DashBoard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UnAuthorized()
        {
            return View();
        }
    }
}