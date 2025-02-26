using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStreamServiceApp.DAL.Entities;

namespace MusicStreamServiceApp.DAL.Configuration
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasData(
                new Album[] {
                    new Album { Id = 1, Name = "Голий король", Author = "Бумбокс", Year = 2017, PhotoPath = "Images/NakedKing.jpg"  },
                    new Album { Id = 2, Name = "Музасфера", Author = "Сергій Бабкін", Year = 2018, PhotoPath = "Images/Muzasfera.jpg" },
                    new Album { Id = 3, Name = "Один в каное", Author = "Один в каное", Year = 2016, PhotoPath = "Images/OdinVKanoe.jpg" },
                    new Album { Id = 4, Name = "III", Author = "Бумбокс", Year = 2008, PhotoPath = "Images/III.jpg" }
                });
        }
    }
}
