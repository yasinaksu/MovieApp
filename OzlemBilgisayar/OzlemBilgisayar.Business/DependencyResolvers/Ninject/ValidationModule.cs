using FluentValidation;
using Ninject.Modules;
using OzlemBilgisayar.Business.ValidationRules.FluentValidation;
using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.Business.DependencyResolvers.Ninject
{
    public class ValidationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IValidator<Category>>().To<CategoryValidator>().InSingletonScope();
            Bind<IValidator<Movie>>().To<MovieValidator>().InSingletonScope();
            Bind<IValidator<User>>().To<UserValidator>().InSingletonScope();
            Bind<IValidator<Role>>().To<RoleValidator>().InSingletonScope();
            Bind<IValidator<Order>>().To<OrderValidator>().InSingletonScope();
        }
    }
}
