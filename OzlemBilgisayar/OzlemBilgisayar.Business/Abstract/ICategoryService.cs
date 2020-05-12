using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll(Expression<Func<Category, bool>> filter = null, params string[] includeList);
        Category GetById(int id, params string[] includeList);
        Category Add(Category category);
        Category Update(Category category);
    }
}
