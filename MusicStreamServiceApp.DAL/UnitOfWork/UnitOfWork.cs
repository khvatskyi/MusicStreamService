using MusicStreamServiceApp.DAL.Entities;
using MusicStreamServiceApp.DAL.Interfaces;
using MusicStreamServiceApp.DAL.Interfaces.IEntityRepositories;
using Microsoft.AspNetCore.Identity;

namespace MusicStreamServiceApp.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMusicRepository _musicRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMusicGenreRepository _musicGenreRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IMusicPlaylistRepository _musicPlaylistRepository;
        private readonly IUserPlaylistRepository _userPlaylistRepository;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UnitOfWork(IMusicRepository musicRepository,
            IGenreRepository genreRepository,
            IMusicGenreRepository musicGenreRepository,
            IAlbumRepository albumRepository,
            IMusicPlaylistRepository musicPlaylistRepository,
            IUserPlaylistRepository userPlaylistRepository,

            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager)
        {
            _musicRepository = musicRepository;
            _genreRepository = genreRepository;
            _musicGenreRepository = musicGenreRepository;
            _albumRepository = albumRepository;
            _musicPlaylistRepository = musicPlaylistRepository;
            _userPlaylistRepository = userPlaylistRepository;

            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IMusicRepository MusicRepository
        {
            get
            {
                return _musicRepository;
            }
        }
        public IGenreRepository GenreRepository
        {
            get
            {
                return _genreRepository;
            }
        }
        public IMusicGenreRepository MusicGenreRepository
        {
            get
            {
                return _musicGenreRepository;
            }
        }
        public IAlbumRepository AlbumRepository
        {
            get
            {
                return _albumRepository;
            }
        }
        public IMusicPlaylistRepository MusicPlaylistRepository
        {
            get
            {
                return _musicPlaylistRepository;
            }
        }
        public IUserPlaylistRepository UserPlaylistRepository
        {
            get
            {
                return _userPlaylistRepository;
            }
        }

        public UserManager<User> UserManager
        {
            get
            {
                return _userManager;
            }
        }
        public SignInManager<User> SignInManager
        {
            get
            {
                return _signInManager;
            }
        }
        public RoleManager<IdentityRole> RoleManager
        {
            get
            {
                return _roleManager;
            }
        }
    }
}
