using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.CategoryModels
{
    public class CategoryCreateVm
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}