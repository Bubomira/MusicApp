using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.DTO.SongDTO;
using Server.Helper;
using Server.Interfaces;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : Controller
    {
        private readonly ISongRepository _songRepository;
        private readonly IMapper _mapper;
        public SongController(ISongRepository songRepository, IMapper mapper)
        {
            this._songRepository = songRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Song>))]
        public IActionResult GetSongs()
        {
            var songs = _songRepository.GetSongs();
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(songs);
        }

        [HttpGet("details/{songId}")]
        [ProducesResponseType(200, Type = typeof(Song))]
        [ProducesResponseType(400)]

        public async Task<IActionResult> GetSongById(int songId)
        {
            if (!_songRepository.CheckIfThereIsSongById(songId))
            {
                return NotFound();
            }
            var wantedSong = _mapper.Map<SongDetailsDto>(await _songRepository.GetSongById(songId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(wantedSong);

        }
    }
}
