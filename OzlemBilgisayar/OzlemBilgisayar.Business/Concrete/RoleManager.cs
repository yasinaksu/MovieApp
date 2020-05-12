using Core.Aspects.Postsharp.CacheAspects;
using Core.Aspects.Postsharp.ValidationAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using OzlemBilgisayar.Business.Abstract;
using OzlemBilgisayar.Business.ValidationRules.FluentValidation;
using OzlemBilgisayar.DataAccess.Abstract;
using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace OzlemBilgisayar.Business.Concrete
{
    public class RoleManager : IRoleService
    {
        private readonly IRoleDal _roleDal;
        public RoleManager(IRoleDal roleDal)
        {
            _roleDal = roleDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Role> GetAll(Expression<Func<Role, bool>> filter = null, params string[] includeList)
        {
            return includeList == null
                ? _roleDal.GetAll(filter)
                : _roleDal.GetAll(filter, includeList);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public Role GetById(int id, params string[] includeList)
        {
            return includeList == null
                ? _roleDal.Get(p => p.Id == id)
                : _roleDal.Get(p => p.Id == id, includeList);
        }

        [FluentValidationAspect(typeof(RoleValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Role Add(Role role)
        {
            return _roleDal.Add(role);
        }

        [FluentValidationAspect(typeof(RoleValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Role Update(Role role)
        {
            return _roleDal.Update(role);
        }
    }
}
