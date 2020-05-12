using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.UserModels
{
    public class UserIndexVm
    {
        public List<User> Users { get; set; }
    }
}