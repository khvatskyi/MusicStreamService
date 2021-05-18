using FluentValidation;
using MusicStreamServiceApp.BLL.DTOs;

namespace MusicStreamServiceApp.BLL.Validation
{
    public class UserUpdateDTOValidator : AbstractValidator<UserUpdateDTO>
    {
        public UserUpdateDTOValidator()
        {
            RuleFor(e => e.UserName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20);

            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(e => e.NewPassword)
                .MinimumLength(4);

            RuleFor(e => e.FirstName)
                .MinimumLength(2)
                .MaximumLength(30);

            RuleFor(e => e.LastName)
                .MinimumLength(2)
                .MaximumLength(30);
        }
    }
}
