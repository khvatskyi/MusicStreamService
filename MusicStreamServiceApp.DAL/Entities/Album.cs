using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MusicStreamServiceApp.DAL.Interfaces;

namespace MusicStreamServiceApp.DAL.Entities
{
    public class Album : IEntity<int>
    {
        [BsonId]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string PhotoPath { get; set; }

        public IEnumerable<Music> Musics { get; set; }
    }
}
