using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStreamServiceApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStreamServiceApp.DAL.Configuration
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(
               new Genre[] {
                    new Genre { Id = 1, Name = "Поп-музика" },
                    new Genre { Id = 2, Name = "Рок" },
                    new Genre { Id = 3, Name = "Фанк"}
               });
        }
    }
}
