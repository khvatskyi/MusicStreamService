using FluentValidation;
using MusicStreamServiceApp.BLL.DTOs;

namespace MusicStreamServiceApp.BLL.Validation
{
    public class PlaylistCUDTOValidator : AbstractValidator<PlaylistCUDTO>
    {
        public PlaylistCUDTOValidator()
        {
            RuleFor(e => e.Name)
                 .MinimumLength(2)
                 .MaximumLength(50);
        }
    }
}
