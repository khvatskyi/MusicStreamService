using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicStreamServiceApp.BLL.Interfaces.IServices;
using MusicStreamServiceApp.BLL.DTOs;

namespace MusicStreamServiceApp.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : Controller
    {
        private readonly IMusicService musicService;

        public MusicController(IMusicService musicService)
        {
            this.musicService = musicService;
        }


        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MusicViewDTO>>> GetAll()
        {
            var musicsDto = await musicService.GetAllMusicAsync();

            if (musicsDto == null)
            {
                return NotFound();
            }

            return Ok(musicsDto);
        }

        /// <summary>
        /// Get by AlbumId
        /// </summary>
        /// <param name="AlbumId"></param>
        /// <returns></returns>
        [Route("album/{AlbumId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MusicViewDTO>>> GetByAlbum(int AlbumId)
        {
            var MusicViewDTOList = await musicService.GetMusicByAlbumAsync(AlbumId);

            if(MusicViewDTOList == null)
            {
                return NotFound();
            }

            return Ok(MusicViewDTOList);
        }

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("{Id}")]
        [HttpGet]
        public async Task<ActionResult<MusicViewDTO>> GetForView(int Id)
        {
            MusicViewDTO musicDto = await musicService.GetMusicForViewAsync(Id);

            if (musicDto == null)
            {
                return NotFound();
            }

            return Ok(musicDto);
        }


        /// <summary>
        /// Get for Update by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("update/{Id}")]
        [HttpGet]
        public async Task<ActionResult<MusicCUDTO>> GetForUpdate(int Id)
        {
            var musicDto = await musicService.GetMusicForUpdateAsync(Id);

            if (musicDto == null)
            {
                return NotFound();
            }

            return Ok(musicDto);
        }


        /// <summary>
        /// Add music
        /// </summary>
        /// <param name="musicDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<MusicCUDTO>> Add([FromBody]MusicCUDTO musicDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await musicService.AddMusicAsync(musicDto);

            musicDto = await musicService.GetMusicForUpdateAsync(musicDto.Name, musicDto.Author, musicDto.Year);

            return CreatedAtAction(nameof(GetForUpdate), new { id = musicDto.Id }, musicDto);
        }

        /// <summary>
        /// Update music
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="musicDto"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        public async Task<ActionResult<MusicCUDTO>> Update(int? Id, [FromBody]MusicCUDTO musicDto)
        {
            if (Id == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await musicService.IsAnyMusicDefinedAsync(musicDto.Id))
            {
                return NotFound();
            }

            await musicService.UpdateMusicAsync(Id.Value, musicDto);
            return Ok(musicDto);
        }


        /// <summary>
        /// Delete by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<ActionResult<MusicViewDTO>> Delete(int Id)
        {
             var musicDto = await musicService.GetMusicForViewAsync(Id);

            if (musicDto == null)
            {
                return NotFound();
            }

            await musicService.DeleteMusicAsync(musicDto);
            return NoContent();
        }
    }
}