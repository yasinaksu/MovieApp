using Core.CrossCuttingConcerns.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.LayoutModels
{
    public class LayoutHeaderVm
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public int OrderCount { get; set; }
    }
}