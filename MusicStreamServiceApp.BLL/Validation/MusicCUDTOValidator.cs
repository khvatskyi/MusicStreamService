using FluentValidation;
using MusicStreamServiceApp.BLL.DTOs;
using System;

namespace MusicStreamServiceApp.BLL.Validation
{
    public class MusicCUDTOValidator : AbstractValidator<MusicCUDTO>
    {
        public MusicCUDTOValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(e => e.Author)
                .NotEmpty()
                .Length(2, 50);

            RuleFor(e => e.Year)
                .LessThanOrEqualTo(DateTime.Now.Year)
                .GreaterThanOrEqualTo(1970);

            RuleFor(e => e.Genre)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30);

            RuleFor(e => e.Album)
                .MinimumLength(3)
                .MaximumLength(50);
        }
    }
}
