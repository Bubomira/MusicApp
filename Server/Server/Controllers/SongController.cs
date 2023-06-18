using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DTO.SongDTO;
using Server.Helper;
using Server.Interfaces;
using Server.Models;
using System.Runtime.CompilerServices;
using System.Security.Claims;

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

        [HttpGet("weekly")]
        [ProducesResponseType(200, Type = typeof(ICollection<Song>))]
        public async Task<IActionResult> GetSongs()
        {
            var songs =await _songRepository.GetWeeklySongs();
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var dtos = PrepareSongsDto(songs);

            return Ok(dtos);
        }

        [HttpGet("details/{songId}")]
        [ProducesResponseType(200, Type = typeof(SongDetailsDto))]
        [ProducesResponseType(400)]

        public async Task<IActionResult> GetSongById(int songId)
        {
            if (!await _songRepository.CheckIfThereIsSongById(songId))
            {
                return NotFound();
            }
            var wantedSong = _mapper.Map<SongDetailsDto>(await _songRepository.GetSongById(songId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(wantedSong);

        }

        [Authorize]
        [HttpGet("likedSongs")]
        public async Task<IActionResult> GetUserLikedSongs()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var songs = await _songRepository.GetLikedSongs(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dtos = PrepareSongsDto(songs);
            return Ok(dtos);
        }

        private List<NormalSongDto> PrepareSongsDto(List<Song> songs)
        {
            List<NormalSongDto> dtos = new List<NormalSongDto>();
            foreach (var song in songs)
            {
                dtos.Add(_mapper.Map<NormalSongDto>(song));
            }
            return dtos;
        }
    }
}
