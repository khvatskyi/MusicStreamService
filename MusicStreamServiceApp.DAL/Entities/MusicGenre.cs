using MongoDB.Bson.Serialization.Attributes;
using MusicStreamServiceApp.DAL.Interfaces;

namespace MusicStreamServiceApp.DAL.Entities
{
    public class MusicGenre : IEntity<int>
    {
        [BsonId]
        public int Id { get; set; }

        public int MusicId { get; set; }
        public virtual Music Music { get; set; }

        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
