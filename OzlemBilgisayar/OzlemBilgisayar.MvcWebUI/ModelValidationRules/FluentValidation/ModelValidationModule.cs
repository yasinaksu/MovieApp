using FluentValidation;
using Ninject.Modules;
using OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.AccountModels;
using OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.CategoryModels;
using OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.MovieModels;
using OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzlemBilgisayar.MvcWebUI.ModelValidationRules.FluentValidation
{
    public class ModelValidationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IValidator<MovieCreateVm>>()
                .To<MovieCreateValidator>().InSingletonScope();
            
            Bind<IValidator<MovieEditVm>>()
                .To<MovieEditValidator>().InSingletonScope();

            Bind<IValidator<CategoryCreateVm>>()
                .To<CategoryCreateValidator>().InSingletonScope();

            Bind<IValidator<UserCreateVm>>()
                .To<UserCreateValidator>().InSingletonScope();

            Bind<IValidator<UserEditVm>>()
               .To<UserEditValidator>().InSingletonScope();

            Bind<IValidator<AccountLoginVm>>()
              .To<AccountLoginValidator>().InSingletonScope();

            Bind<IValidator<AccountChangePasswordVm>>()
             .To<AccountChangePasswordValidator>().InSingletonScope();

            Bind<IValidator<AccountEditVm>>()
            .To<AccountEditValidator>().InSingletonScope();
        }
    }
}