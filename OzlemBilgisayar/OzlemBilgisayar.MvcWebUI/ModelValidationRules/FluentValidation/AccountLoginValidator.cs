using FluentValidation;
using OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzlemBilgisayar.MvcWebUI.ModelValidationRules.FluentValidation
{
    public class AccountLoginValidator : AbstractValidator<AccountLoginVm>
    {
        public AccountLoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}