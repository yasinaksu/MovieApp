using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Ninject.Modules;
using OzlemBilgisayar.Business.Abstract;
using OzlemBilgisayar.Business.Concrete;
using OzlemBilgisayar.DataAccess.Abstract;
using OzlemBilgisayar.DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.Business.DependencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICategoryService>().To<CategoryManager>().InSingletonScope();
            Bind<ICategoryDal>().To<EfCategoryDal>().InSingletonScope();          

            Bind<IMovieService>().To<MovieManager>().InSingletonScope();
            Bind<IMovieDal>().To<EfMovieDal>().InSingletonScope();

            Bind<IUserService>().To<UserManager>().InSingletonScope();
            Bind<IUserDal>().To<EfUserDal>().InSingletonScope();

            Bind<IRoleService>().To<RoleManager>().InSingletonScope();
            Bind<IRoleDal>().To<EfRoleDal>().InSingletonScope();

            Bind<IOrderService>().To<OrderManager>().InSingletonScope();
            Bind<IOrderDal>().To<EfOrderDal>().InSingletonScope();

            Bind(typeof(IQueryableRepository<>)).To(typeof(EfQueryableRepository<>));
            Bind<DbContext>().To<OzlemBilgisayarDbContext>();
        }
    }
}
