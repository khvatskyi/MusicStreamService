using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MusicStreamServiceApp.DAL.Interfaces;

namespace MusicStreamServiceApp.DAL.Entities
{
    public partial class Music : IEntity<int>
    {
        [BsonId]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int? Year { get; set; }
        public int? AlbumId { get; set; }
        public string PhotoPath { get; set; }
        public string FilePath { get; set; }
        public Album Album { get; set; }
        public virtual IEnumerable<MusicPlaylist> MusicPlaylists { get; set; }
        public virtual IEnumerable<MusicGenre> MusicGenres { get; set; }
    }
}
