using FluentValidation;
using OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzlemBilgisayar.MvcWebUI.ModelValidationRules.FluentValidation
{
    public class AccountChangePasswordValidator : AbstractValidator<AccountChangePasswordVm>
    {
        public AccountChangePasswordValidator()
        {
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(6);
            RuleFor(x => x.RePassword).NotEmpty().Equal(x=>x.NewPassword).WithMessage("Girilen şifre yeni şifreyle uyuşmuyor...");           
        }
    }
}