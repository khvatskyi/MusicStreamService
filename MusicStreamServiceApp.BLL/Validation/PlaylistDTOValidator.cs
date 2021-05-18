using FluentValidation;
using MusicStreamServiceApp.BLL.DTOs;

namespace MusicStreamServiceApp.BLL.Validation
{
    public class PlaylistDTOValidator : AbstractValidator<PlaylistDTO>
    {
        public PlaylistDTOValidator()
        {
            RuleFor(e => e.UserId)
                .NotEmpty();

            RuleFor(e => e.PlaylistName)
                .MinimumLength(2)
                .MaximumLength(50);
        }
    }
}
