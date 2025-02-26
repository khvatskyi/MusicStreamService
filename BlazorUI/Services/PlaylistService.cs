using BlazorUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorUI.Services
{
    public class PlaylistService
    {
        public Task<IEnumerable<PlaylistViewModel>> GetPlaylists()
        {
            IEnumerable<PlaylistViewModel> playlists = new PlaylistViewModel[]
            {
                new PlaylistViewModel {name = "Lyrics"},
                new PlaylistViewModel {name = "Rock"},
                new PlaylistViewModel {name = "Func"},
                new PlaylistViewModel {name = "Classic"},
                new PlaylistViewModel {name = "Pop"},
            };
            return Task.FromResult(playlists);
        }
    }
}
