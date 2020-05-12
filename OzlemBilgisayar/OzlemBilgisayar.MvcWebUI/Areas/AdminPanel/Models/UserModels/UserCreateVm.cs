using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.UserModels
{
    public class UserCreateVm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int[] Roles { get; set; }
        public bool IsActive { get; set; }
    }
}