using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStreamServiceApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStreamServiceApp.DAL.Configuration
{
    public class MusicGenreConfiguration : IEntityTypeConfiguration<MusicGenre>
    {
        public void Configure(EntityTypeBuilder<MusicGenre> builder)
        {
            builder.HasData(
                new MusicGenre[] {
                    new MusicGenre { Id = 1, MusicId = 1, GenreId = 1 },
                    new MusicGenre { Id = 2, MusicId = 2, GenreId = 1 },
                    new MusicGenre { Id = 3, MusicId = 3, GenreId = 1 },
                    new MusicGenre { Id = 4, MusicId = 4, GenreId = 1 },
                    new MusicGenre { Id = 5, MusicId = 5, GenreId = 1 },
                    new MusicGenre { Id = 6, MusicId = 6, GenreId = 1 },
                    new MusicGenre { Id = 7, MusicId = 7, GenreId = 1 },
                    new MusicGenre { Id = 8, MusicId = 8, GenreId = 1 },
                    new MusicGenre { Id = 9, MusicId = 9, GenreId = 1 },
                    new MusicGenre { Id = 10, MusicId = 10, GenreId = 1 }
                });
        }
    }
}
