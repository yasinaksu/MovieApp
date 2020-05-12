using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.AccountModels
{
    public class AccountEditVm
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public bool IsActive { get; set; }
        public string CurrentImage { get; set; }       
    }
}