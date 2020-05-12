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
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Category> GetAll(Expression<Func<Category, bool>> filter = null, params string[] includeList)
        {
            return includeList == null
                ? _categoryDal.GetAll(filter)
                : _categoryDal.GetAll(filter, includeList);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public Category GetById(int id, params string[] includeList)
        {
            return includeList == null
                ? _categoryDal.Get(p => p.Id == id)
                : _categoryDal.Get(p => p.Id == id, includeList);
        }

        [FluentValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Category Add(Category category)
        {
            return _categoryDal.Add(category);
        }

        [FluentValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Category Update(Category category)
        {
            return _categoryDal.Update(category);
        }
    }
}
