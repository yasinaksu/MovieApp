using FluentValidation;
using OzlemBilgisayar.MvcWebUI.Areas.AdminPanel.Models.MovieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OzlemBilgisayar.MvcWebUI.ModelValidationRules.FluentValidation
{
    public class MovieEditValidator:AbstractValidator<MovieEditVm>
    {
        public MovieEditValidator()
        {
            RuleFor(x => x.ImdbRate).NotEmpty();
            RuleFor(x => x.ReleaseDate).NotEmpty();
            RuleFor(x => x.StoryLine).NotEmpty().MaximumLength(750);
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Trailer).NotEmpty().MaximumLength(750);
            RuleFor(x => x.Categories).NotEmpty().WithMessage("Lütfen en az bir tane kategori seçiniz.");
        }
    }
}