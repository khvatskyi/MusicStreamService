using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStreamServiceApp.BLL.DTOs
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string PhotoPath { get; set; }
    }
}
