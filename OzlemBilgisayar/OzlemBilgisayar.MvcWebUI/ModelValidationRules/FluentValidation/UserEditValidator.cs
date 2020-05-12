using FluentValidation;
using OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzlemBilgisayar.MvcWebUI.ModelValidationRules.FluentValidation
{
    public class UserEditValidator : AbstractValidator<UserEditVm>
    {
        public UserEditValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Roles).NotEmpty().WithMessage("Lütfen en az bir tane rol seçiniz.");
        }
    }
}