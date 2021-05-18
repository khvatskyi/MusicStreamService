using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStreamServiceApp.DAL.Entities;

namespace MusicStreamServiceApp.DAL.Configuration
{
    public class MusicConfiguration : IEntityTypeConfiguration<Music>
    {
        public void Configure(EntityTypeBuilder<Music> builder)
        {
            builder.HasData(
                new Music[]
                {
                    new Music { Id = 1, Name = "Де би я", Author = "Сергій Бабкін", AlbumId = 2, Year = 2018 },
                    new Music { Id = 2, Name = "Квіти у волоссі", Author = "Бумбокс", Year = 2006 },
                    new Music { Id = 3, Name = "Ми вільні", Author = "Letay", Year = 2020 },
                    new Music { Id = 4, Name = "Наодинці", Author = "Бумбокс", AlbumId = 4, Year = 2006 },
                    new Music { Id = 5, Name = "8-Ий колір", Author = "Мотор'Ролла", Year = 2005 },
                    new Music { Id = 6, Name = "Човен", Author = "Один в каное", AlbumId = 3, Year = 2016 },
                    new Music { Id = 7, Name = "Сталеві квіти", Author = "Бумбокс", AlbumId = 1, Year = 2017 },
                    new Music { Id = 8, Name = "Мила моя", Author = "Letay", Year = 2018 },
                    new Music { Id = 9, Name = "Пообіцяй мені", Author = "Один в каное", AlbumId = 3, Year = 2016 },
                    new Music { Id = 10, Name = "Дихай повільно", Author = "Сергій Бабкін", Year = 2018 }
                });
        }
    }
}
