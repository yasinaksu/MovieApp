using Core.DataAccess;
using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        User AddWithRoles(User user, params int[] roleIds);
        User UpdateWithRoles(User user, params int[] roleIds);
    }
}
