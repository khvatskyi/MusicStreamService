using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStreamServiceApp.BLL.DTOs;
using MusicStreamServiceApp.BLL.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace MusicStreamServiceApp.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : Controller
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDTO>>> GetAll()
        {
            var genreDTOList = await genreService.GetAllGenresAsync();

            if (genreDTOList == null)
            {
                return NotFound();
            }

            return Ok(genreDTOList);
        }

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDTO>> Get(int id)
        {
            var genreDTO = await genreService.GetGenreAsync(id);

            if (genreDTO == null)
            {
                return NotFound();
            }

            return Ok(genreDTO);
        }

        /// <summary>
        /// Create genre
        /// </summary>
        /// <param name="genreDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GenreDTO>> Post([FromBody]GenreDTO genreDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await genreService.AddGenreAsync(genreDTO);

            genreDTO = await genreService.GetGenreAsync(genreDTO.Name);

            return CreatedAtAction(nameof(Get), new { id = genreDTO.Id }, genreDTO);
        }

        /// <summary>
        /// Update by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="genreDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<GenreDTO>> Put(int id, [FromBody]GenreDTO genreDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await genreService.IsAnyGenreDefinedAsync(id))
            {
                return NotFound();
            }

            genreDTO.Id = id;

            await genreService.UpdateGenreAsync(genreDTO);

            return Ok(genreDTO);
        }

        /// <summary>
        /// Delete by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenreDTO>> Delete(int id)
        {
            

            if (!await genreService.IsAnyGenreDefinedAsync(id))
            {
                return NotFound();
            }

            await genreService.DeleteGenreAsync(id);

            return NoContent();
        }
    }
}
