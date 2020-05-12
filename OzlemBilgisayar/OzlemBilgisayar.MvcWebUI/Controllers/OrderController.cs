using OzlemBilgisayar.Business.Abstract;
using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OzlemBilgisayar.MvcWebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public ActionResult ConfirmOrder(int[] movieIds)
        {
            if (movieIds.Length > 0)
            {
                foreach (var movieId in movieIds)
                {
                    var order = new Order
                    {
                        Created = DateTime.Now,
                        IsActive = true,
                        IsComplete = false,
                        Modified = DateTime.Now,
                        MovieId = movieId
                    };
                    _orderService.Add(order);
                }
            }
            return Json(new { Message = "Siparişiniz alınmıştır.." });
        }
    }
}