using Core.DataAccess.EntityFramework;
using OzlemBilgisayar.DataAccess.Abstract;
using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.DataAccess.Concrete.EntityFramework
{
    public class EfRoleDal : EfEntityRepositoryBase<Role, OzlemBilgisayarDbContext>, IRoleDal
    {
        
    }
}
