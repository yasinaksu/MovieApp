using FluentValidation;
using OzlemBilgisayar.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzlemBilgisayar.Business.ValidationRules.FluentValidation
{
    public class MovieValidator : AbstractValidator<Movie>
    {
        public MovieValidator()
        {
            RuleFor(x => x.Image).NotEmpty();
            RuleFor(x => x.ImdbRate).NotEmpty();
            RuleFor(x => x.ReleaseDate).NotEmpty();
            RuleFor(x => x.StoryLine).NotEmpty().MaximumLength(750);
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Trailer).NotEmpty().MaximumLength(750);
            //RuleFor(x => x.Categories).NotEmpty().WithMessage("Lütfen en az bir tane kategori seçiniz.");
        }
    }
}
