using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.CategoryModels
{
    public class IndexVm
    {
        public List<Category> Categories { get; set; }
    }
}