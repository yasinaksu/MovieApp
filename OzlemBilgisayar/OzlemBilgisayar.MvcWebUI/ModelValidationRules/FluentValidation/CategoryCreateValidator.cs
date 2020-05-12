using FluentValidation;
using OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzlemBilgisayar.MvcWebUI.ModelValidationRules.FluentValidation
{
    public class CategoryCreateValidator:AbstractValidator<CategoryCreateVm>
    {
        public CategoryCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}