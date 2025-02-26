using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStreamServiceApp.BLL.DTOs;
using MusicStreamServiceApp.BLL.Interfaces.IServices;

namespace MusicStreamServiceApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : Controller
    {
        private readonly IPlaylistService playlistService;

        public PlaylistController(IPlaylistService playlistService)
        {
            this.playlistService = playlistService;
        }

        /// <summary>
        /// Get all playlists
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PlaylistDTO>>> GetAll()
        {
            var playlists = await playlistService.GetAllAsync();
            if (playlists == null)
            {
                return NotFound();
            }
            return Ok(playlists);
        }

        /// <summary>
        /// Get all playlists of user
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet("{UserId}")]
        public async Task<ActionResult<IEnumerable<PlaylistDTO>>> GetAllUserPlaylists(string UserId)
        {
            if(string.IsNullOrEmpty(UserId))
            {
                return BadRequest();
            }
            var playlists = await playlistService.GetPlaylistDTOListAsync(UserId);
            if (playlists == null)
            {
                return NotFound();
            }
            return Ok(playlists);
        }

        /// <summary>
        /// Get Playlist by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("getSomeById/{Id}")]
        [HttpGet]
        public async Task<ActionResult<PlaylistDTO>> Get(int Id)
        {
            var playlist = await playlistService.GetPlaylistAsync(Id);
            if (playlist == null)
            {
                return NotFound();
            }
            return Ok(playlist);
        }

        /// <summary>
        /// Get Playlist by UserId
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="PlaylistName"></param>
        /// <returns></returns>
        [Route("GetSome")]
        [HttpGet]
        public async Task<ActionResult<PlaylistDTO>> GetUserPlaylist(string UserId, string PlaylistName)
        {
            if (string.IsNullOrEmpty(PlaylistName) || string.IsNullOrEmpty(UserId))
            {
                return BadRequest();
            }
            var playlist = await playlistService.GetPlaylistAsync(UserId, PlaylistName);
            if (playlist == null)
            {
                return NotFound();
            }
            return Ok(playlist);
        }

        /// <summary>
        /// Add Music to Playlist
        /// </summary>
        /// <param name="musicPlaylistDTO"></param>
        /// <returns></returns>
        [Route("AddMusicToPlaylist")]
        [HttpPost]
        public async Task<ActionResult> PostMusicToPlaylist([FromBody]MusicPlaylistDTO musicPlaylistDTO)
        {
            if (musicPlaylistDTO == null)
            {
                BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await playlistService.AddMusicToPlaylistAsync(musicPlaylistDTO);
            return Ok(musicPlaylistDTO);
        }

        /// <summary>
        /// Create playlist
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="playlistCreateDTO"></param>
        /// <returns></returns>
        [Route("CreatePlaylist/{UserId}")]
        [HttpPost]
        public async Task<ActionResult> PostPlaylist(string UserId, [FromBody]PlaylistCUDTO playlistCreateDTO) 
        {
            if (playlistCreateDTO == null || UserId == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await playlistService.AddPlaylistAsync(UserId, playlistCreateDTO);
            var playlistDTO = await playlistService.GetPlaylistAsync(UserId, playlistCreateDTO.Name);
            if(playlistDTO == null)
            {
                return NotFound(playlistDTO);
            }
            return Ok(playlistDTO);
        }

        /// <summary>
        /// Update the playlist name
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="playlistCUDTO"></param>
        /// <returns></returns>
        [Route("ChangeName/{Id}")]
        [HttpPut]
        public async Task<ActionResult> PutPlaylistName(int Id, [FromBody]PlaylistCUDTO playlistCUDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var playlistDTO = await playlistService.GetPlaylistAsync(Id);
            if(playlistDTO == null)
            {
                return NotFound();
            }
            await playlistService.UpdatePlaylistNameAsync(playlistDTO, playlistCUDTO.Name);
            return Ok("Succeeded");
        }

        /// <summary>
        /// Delete playlist
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("{Id}")]
        [HttpDelete]
        public async Task<ActionResult> DeletePlaylist(int Id)
        {
            PlaylistDTO playlistDTO = await playlistService.GetPlaylistAsync(Id);
            if(playlistDTO == null)
            {
                return NotFound();
            }
            await playlistService.DeletePlaylistAsync(playlistDTO);
            return NoContent();
        }

        /// <summary>
        /// Delete music from playlist
        /// </summary>
        /// <param name="musicPlaylistDTO"></param>
        /// <returns></returns>
        [Route("DeleteMusicFromPlaylist")]
        [HttpDelete]
        public async Task<ActionResult> DeleteMusicFromPlaylist([FromBody]MusicPlaylistDTO musicPlaylistDTO)
        {
            if(musicPlaylistDTO == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await playlistService.DeleteMusicFromPlaylistAsync(musicPlaylistDTO);
            return NoContent();
        }
    }
}
