using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Ninject;
using Ninject.Modules;

namespace Core.CrossCuttingConcerns.Validation.FluentValidation
{
    public class NinjectValidationFactory:ValidatorFactoryBase
    {
        private IKernel _kernel;

        public NinjectValidationFactory(params INinjectModule[] modules)
        {
            _kernel =new StandardKernel(modules);
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return (IValidator)_kernel.TryGet(validatorType);
        }
    }
}
