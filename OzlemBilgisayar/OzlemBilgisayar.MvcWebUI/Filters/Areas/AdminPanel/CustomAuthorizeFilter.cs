using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace OzlemBilgisayar.MvcWebUI.Filters.Areas.AdminPanel
{
    public class CustomAuthorizeFilter : AuthorizeAttribute
    {
        private string[] _allowedRoles;     

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAuthorize = false;
            _allowedRoles = Roles.Split(',');
            var user = httpContext.User;
            foreach (var role in _allowedRoles)
            {
                if (user.IsInRole(role))
                {
                    isAuthorize = true;
                }
            }          
            return isAuthorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "area", "AdminPanel" },
                    { "controller", "Home" },
                    { "action", "UnAuthorized" }
               });
        }

    }
}