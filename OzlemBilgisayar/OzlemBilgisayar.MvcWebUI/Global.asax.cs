using Core.CrossCuttingConcerns.Security.Web;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Mvc.Infrastructure;
using FluentValidation.Mvc;
using OzlemBilgisayar.Business.DependencyResolvers.Ninject;
using OzlemBilgisayar.MvcWebUI.ModelValidationRules.FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace OzlemBilgisayar.MvcWebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ControllerBuilder
                .Current
                .SetControllerFactory(new NinjectControllerFactory(new BusinessModule()));

            ModelValidatorProviders.Providers.Clear();// mvc bnin kendi validasyon mekanizmasını devreden çıkartıyor

            FluentValidationModelValidatorProvider.Configure(provider =>
            {
                provider.ValidatorFactory = new NinjectValidationFactory(new ValidationModule(), new ModelValidationModule());
                provider.AddImplicitRequiredValidator = false;
            });
        }

        //Authentication Process
        public override void Init()
        {
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        private void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            try
            {
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie == null)
                {
                    return;
                }

                var encTicket = authCookie.Value;
                if (String.IsNullOrEmpty(encTicket))
                {
                    return;
                }

                var ticket = FormsAuthentication.Decrypt(encTicket);

                var securityUtlities = new SecurityUtilities();
                var identity = securityUtlities.FormsAuthTicketToIdentity(ticket);
                var principal = new GenericPrincipal(identity, identity.Roles);

                HttpContext.Current.User = principal;
                Thread.CurrentPrincipal = principal;
            }
            catch (Exception)
            {

            }


        }
    }
}
