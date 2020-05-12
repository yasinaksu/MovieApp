using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.Business.Abstract
{
    public interface IRoleService
    {
        List<Role> GetAll(Expression<Func<Role, bool>> filter = null, params string[] includeList);
        Role GetById(int id, params string[] includeList);
        Role Add(Role role);
        Role Update(Role role);
    }
}
