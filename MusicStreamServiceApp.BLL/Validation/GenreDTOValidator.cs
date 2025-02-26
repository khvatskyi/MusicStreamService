using FluentValidation;
using MusicStreamServiceApp.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStreamServiceApp.BLL.Validation
{
    public class GenreDTOValidator : AbstractValidator<GenreDTO>
    {
        public GenreDTOValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .MaximumLength(30);
        }
    }
}
