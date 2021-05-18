using FluentValidation;
using MusicStreamServiceApp.BLL.DTOs;

namespace MusicStreamServiceApp.BLL.Validation
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(e => e.UserName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20);

            RuleFor(e => e.Email)
                .NotNull()
                .EmailAddress();

            RuleFor(e => e.Password)
                .NotNull();

            RuleFor(e => e.ConfirmPassword)
                .Equal(e => e.Password);
        }
    }
}
