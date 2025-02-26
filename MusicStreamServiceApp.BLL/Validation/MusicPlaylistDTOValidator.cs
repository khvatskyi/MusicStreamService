using FluentValidation;
using MusicStreamServiceApp.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStreamServiceApp.BLL.Validation
{
    public class MusicPlaylistDTOValidator : AbstractValidator<MusicPlaylistDTO>
    {
        public MusicPlaylistDTOValidator()
        {
            RuleFor(e => e.MusicId)
                .NotNull();

            RuleFor(e => e.UserPlaylistId)
                .NotNull();
        }
    }
}
