using FluentValidation;
using MusicStreamServiceApp.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStreamServiceApp.BLL.Validation
{
    public class UserLoginDTOValidator : AbstractValidator<UserLoginDTO>
    {
        public UserLoginDTOValidator()
        {
            RuleFor(e => e.Email)
                .NotNull()
                .EmailAddress();

            RuleFor(e => e.Password)
                .NotNull();
        }
    }
}
