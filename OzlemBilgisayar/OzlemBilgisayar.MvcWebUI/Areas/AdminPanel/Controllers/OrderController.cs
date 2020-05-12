using OzlemBilgisayar.Business.Abstract;
using OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.OrderModels;
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
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new OderIndexVm
            {
                Orders = _orderService.GetAll(x=>x.IsActive, "Movie").OrderByDescending(x=>x.Created).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult CompleteOrder(int orderId)
        {
            var order = _orderService.GetById(orderId);
            order.Modified = DateTime.Now;
            order.IsComplete = true;
            _orderService.Update(order);
            return Json(new { Message = "Sipariş tamamlandı" });
        }

        [HttpPost]
        public ActionResult CancelOrder(int orderId)
        {
            var order = _orderService.GetById(orderId);
            order.Modified = DateTime.Now;
            order.IsComplete = false;
            order.IsActive = false;
            _orderService.Update(order);
            return Json(new { Message = "Sipariş iptal edildi." });
        }
    }
}