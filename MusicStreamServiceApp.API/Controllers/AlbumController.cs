using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStreamServiceApp.BLL.DTOs;
using MusicStreamServiceApp.BLL.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace MusicStreamServiceApp.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : Controller
    {
        private readonly IAlbumService albumService;

        public AlbumController(IAlbumService albumService)
        {
            this.albumService = albumService;
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumDTO>>> GetAll()
        {
            var albumDTOList = await albumService.GetAllAlbumsAsync();

            if (albumDTOList == null)
            {
                return NotFound();
            }

            return Ok(albumDTOList);
        }

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<ActionResult<AlbumDTO>> Get(int Id)
        {
            var albumDTO = await albumService.GetAlbumAsync(Id);

            if (albumDTO == null)
            {
                return NotFound();
            }

            return albumDTO;
        }

        /// <summary>
        /// Create album
        /// </summary>
        /// <param name="albumDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AlbumDTO>> Post([FromBody]AlbumDTO albumDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await albumService.AddAlbumAsync(albumDTO);

            albumDTO = await albumService.GetAlbumAsync(albumDTO.Name, albumDTO.Author);

            return CreatedAtAction(nameof(Get), new {id = albumDTO.Id }, albumDTO);
        }

        /// <summary>
        /// Update by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="albumDTO"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        public async Task<ActionResult<AlbumDTO>> Put(int Id, [FromBody]AlbumDTO albumDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (! await albumService.IsAnyAlbumDefinedAsync(Id))
            {
                return NotFound();
            }

            albumDTO.Id = Id;

            await albumService.UpdateAlbumAsync(albumDTO);
            return Ok(albumDTO);
        }

        /// <summary>
        /// Delete by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<ActionResult<AlbumDTO>> Delete(int Id)
        {
            if(!await albumService.IsAnyAlbumDefinedAsync(Id))
            {
                return NotFound();
            }

            await albumService.DeleteAlbumAsync(Id);
            return NoContent();
        }
    }
}
