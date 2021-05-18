using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MusicStreamServiceApp.DAL.Interfaces;

namespace MusicStreamServiceApp.DAL.Entities
{
    public class UserPlaylist : IEntity<int>
    {
        public UserPlaylist()
        {
            MusicPlaylists = new HashSet<MusicPlaylist>();
        }

        [BsonId]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PlaylistName { get; set; }

        public virtual User User { get; set; }
        public virtual IEnumerable<MusicPlaylist> MusicPlaylists { get; set; }
    }
}
