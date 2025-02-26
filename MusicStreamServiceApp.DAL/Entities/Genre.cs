using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MusicStreamServiceApp.DAL.Interfaces;

namespace MusicStreamServiceApp.DAL.Entities
{
    public class Genre : IEntity<int>
    {
        [BsonId]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<MusicGenre> MusicGenres { get; set; }
    }
}
