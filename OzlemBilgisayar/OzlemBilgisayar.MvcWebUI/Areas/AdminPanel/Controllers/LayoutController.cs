using OzlemBilgisayar.MvcWebUI.Filters.Areas.AdminPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.LayoutModels;
using Core.CrossCuttingConcerns.Security;
using OzlemBilgisayar.Business.Abstract;
using OzlemBilgisayar.Entities.Domains;

namespace OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Controllers
{
    //[CustomAuthenticationFilter]
    //[CustomAuthorizeFilter(Roles ="SuperAdmin,Admin")]
    public class LayoutController : Controller
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        public LayoutController(IUserService userService,IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }
        // GET: AdminPanel/Layout
        [ChildActionOnly]
        public PartialViewResult _LayoutHeader()
        {
            var _user = _userService.GetById((User.Identity as Identity).Id);
            var model = new LayoutHeaderVm
            {
                FirstName = _user.FirstName,
                Image = _user.Image,
                LastName = _user.LastName,
                Id=_user.Id,
                OrderCount=_orderService.GetAll(x=>!x.IsComplete && x.IsActive).Count
            };
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult _LayoutLeftSideBar()
        {
            var _user = _userService.GetById((User.Identity as Identity).Id);
            var model = new LayoutLeftSideBarVm
            {
                Id=_user.Id,
                FirstName = _user.FirstName,
                Image = _user.Image,
                LastName = _user.LastName
            };
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult _LayoutRightSideBar()
        {
            var _user = _userService.GetById((User.Identity as Identity).Id);
            var model = new LayoutRightSideBarVm
            {
                FirstName = _user.FirstName,
                Image = _user.Image,
                LastName = _user.LastName,
                Movies=_orderService.GetAllInComplete().Select(x=>x.Movie.Title).ToList()
            };
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult _PageHeader()
        {
            var model = (User.Identity as Identity).Id;
            return PartialView(model);
        }
    }
}