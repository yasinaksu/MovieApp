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
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        [FluentValidationAspect(typeof(OrderValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Order Add(Order order)
        {
            return _orderDal.Add(order);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Order> GetAll(Expression<Func<Order, bool>> filter = null, params string[] includeList)
        {
            return includeList == null
                ? _orderDal.GetAll(filter)
                : _orderDal.GetAll(filter, includeList);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Order> GetAllInComplete()
        {
            return _orderDal.GetAll(x => !x.IsComplete, "Movie");
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public Order GetById(int id, params string[] includeList)
        {
            return includeList == null
                ? _orderDal.Get(x => x.Id == id)
                : _orderDal.Get(x => x.Id == id, includeList);
        }

        [FluentValidationAspect(typeof(OrderValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Order Update(Order order)
        {
            return _orderDal.Update(order);
        }
    }
}
