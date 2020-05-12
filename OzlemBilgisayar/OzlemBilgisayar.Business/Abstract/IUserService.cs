using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.Business.Abstract
{
    public interface IUserService
    {
        List<User> GetAll(Expression<Func<User, bool>> filter = null, params string[] includeList);
        User GetById(int id, params string[] includeList);
        User GetByEmailAndPassword(string email, string password, params string[] includeList);
        User Add(User user, params int[] roleIds);
        User Update(User user, params int[] roleIds);
    }
}
