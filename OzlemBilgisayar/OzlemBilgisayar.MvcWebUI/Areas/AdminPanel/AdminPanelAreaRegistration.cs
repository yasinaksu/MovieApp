using System.Web.Mvc;

namespace OzlemBilgisayar.MvcWebUI.Areas.AdminPanel
{
    public class AdminPanelAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AdminPanel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AdminPanel_default",
                "AdminPanel/{controller}/{action}/{id}",
                new {controller="Home", action = "DashBoard", id = UrlParameter.Optional },
                new[] { "OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Controllers" }
            );
        }
    }
}