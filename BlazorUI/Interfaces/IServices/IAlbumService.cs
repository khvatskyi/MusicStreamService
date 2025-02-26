using BlazorUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorUI.Interfaces.IServices
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumViewModel>> GetAlbumsAsync();
        Task<AlbumViewModel> GetAlbumById(int AlbumId);
    }
}
