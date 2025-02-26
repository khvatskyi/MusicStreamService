using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using MusicStreamServiceApp.DAL.Interfaces;

namespace MusicStreamServiceApp.DAL.Entities
{
    public class User : IdentityUser, IEntity<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoPath { get; set; }

        public virtual IEnumerable<UserPlaylist> UserPlaylists { get; set; }
    }
}
