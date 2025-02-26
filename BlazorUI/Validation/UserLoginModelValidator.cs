using BlazorUI.Models.AccountModels;
using FluentValidation;

namespace BlazorUI.Validation
{
    public class UserLoginModelValidator : AbstractValidator<UserLoginModel>
    {
        public UserLoginModelValidator()
        {
            RuleFor(e => e.email)
                .NotNull()
                .EmailAddress();

            RuleFor(e => e.password)
                .NotNull();
        }
    }
}
