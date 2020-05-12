using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.UserModels
{
    public class UserEditVm
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public HttpPostedFileBase Image { get; set; }       
        public bool IsActive { get; set; }
        public string CurrentImage { get; set; }
        public int[] Roles { get; set; }
        public SelectListItem[] RoleList { get; set; }
    }
}