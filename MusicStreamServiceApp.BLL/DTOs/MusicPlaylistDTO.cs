using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStreamServiceApp.BLL.DTOs
{
    public class MusicPlaylistDTO
    {
        public int Id { get; set; }
        public int UserPlaylistId { get; set; }
        public int MusicId { get; set; }
    }
}
