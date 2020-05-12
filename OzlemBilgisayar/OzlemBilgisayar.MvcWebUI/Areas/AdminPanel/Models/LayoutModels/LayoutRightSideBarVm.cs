using Core.CrossCuttingConcerns.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.LayoutModels
{
    public class LayoutRightSideBarVm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public List<string> Movies { get; set; }
    }
}