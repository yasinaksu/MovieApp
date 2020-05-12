using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.Business.Abstract
{
    public interface IOrderService
    {
        List<Order> GetAll(Expression<Func<Order, bool>> filter = null, params string[] includeList);
        List<Order> GetAllInComplete();
        Order GetById(int id, params string[] includeList);
        Order Add(Order order);
        Order Update(Order order);
    }
}
