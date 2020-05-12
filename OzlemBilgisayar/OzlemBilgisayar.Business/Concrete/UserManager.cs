using Core.Aspects.Postsharp.CacheAspects;
using Core.Aspects.Postsharp.ValidationAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.DataAccess;
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
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IQueryableRepository<User> _queryable;
        public UserManager(IUserDal userDal, IQueryableRepository<User> queryable)
        {
            _userDal = userDal;
            _queryable = queryable;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<User> GetAll(Expression<Func<User, bool>> filter = null, params string[] includeList)
        {
            return includeList == null
                ? _userDal.GetAll(filter)
                : _userDal.GetAll(filter, includeList);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public User GetById(int id, params string[] includeList)
        {
            return includeList == null
                ? _userDal.Get(p => p.Id == id)
                : _userDal.Get(p => p.Id == id, includeList);
        }

        [FluentValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public User Add(User user, params int[] roleIds)
        {
            return roleIds == null
                ? _userDal.Add(user)
                : _userDal.AddWithRoles(user, roleIds);
        }

        [FluentValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public User Update(User user, params int[] roleIds)
        {
            return roleIds == null
                ? _userDal.Update(user)
                : _userDal.UpdateWithRoles(user, roleIds);
        }

        public User GetByEmailAndPassword(string email, string password, params string[] includeList)
        {
            return includeList == null
                ? _userDal.Get(x => x.Email == email && x.Password==password)
                : _userDal.Get(x => x.Email == email && x.Password == password, includeList);
        }
    }
}
