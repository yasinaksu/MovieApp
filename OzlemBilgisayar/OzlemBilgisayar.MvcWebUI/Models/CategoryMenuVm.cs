using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzlemBilgisayar.MvcWebUI.Models
{
    public class CategoryMenuVm
    {
        public List<Category> Categories { get; set; }
        public int SelectedCategoryId { get; set; }

    }
}