using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.OrderModels
{
    public class OderIndexVm
    {
        public List<Order> Orders { get; set; }
    }
}