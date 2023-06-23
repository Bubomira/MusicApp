using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DTO.PlaylistDto;
using Server.DTO.SongDTO;
using Server.Interfaces;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : Controller
    {
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IMapper _mapper;

        public PlaylistController(IPlaylistRepository playlistRepository, IMapper mapper)
        {
            _playlistRepository = playlistRepository;
            _mapper = mapper;
        }

        [HttpGet("/details/{playlistId}")]
        public async Task<IActionResult> GetPlaylistById(int playlistId)
        {
            if (!await _playlistRepository.CheckIfPlaylistExist(playlistId))
            {

                return NotFound();
            }
            var playlist = await _playlistRepository.GetPlaylistById(playlistId);
            var detailedPlaylist = _mapper.Map<ExportDetailedPlaylistDto>(playlist);
            return Ok(detailedPlaylist);
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreatePlaylist([FromBody] string playlistName)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var playlist = await _playlistRepository.CreatePlaylist(userId, playlistName);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var mappedPlaylist = _mapper.Map<ExportNormalPlaylistDto>(playlist);

            return Ok(mappedPlaylist);
        }

        [Authorize]
        [HttpPut("update/{playlistId}")]

        public async Task<IActionResult> UpdatePlaylist([FromBody] string playlistName, int playlistId)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            if (!await _playlistRepository.CheckIfPlaylistExist(playlistId))
            {
                return NotFound();
            }
            if (!await _playlistRepository.CheckIfPlaylistIsOwnedByCurrentUser(userId, playlistId))
            {
                return Forbid();
            }
            _playlistRepository.UpdatePlaylist(playlistId, playlistName);
            return NoContent();

        }


        [Authorize]
        [HttpDelete("delete/{playlistId}")]

        public async Task<IActionResult> DeletePlaylist(int playlistId)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            if (!await _playlistRepository.CheckIfPlaylistExist(playlistId))
            {
                return NotFound();
            }
            if (!await _playlistRepository.CheckIfPlaylistIsOwnedByCurrentUser(userId, playlistId))
            {
                return Forbid();
            }

            _playlistRepository.DeletePlaylist(playlistId);

            return NoContent();
        }

        [Authorize]
        [HttpGet("like/{playlistId}")]

        public async Task<IActionResult> LikePlaylist(int playlistId)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            if (!await _playlistRepository.CheckIfPlaylistExist(playlistId))
            {
                return NotFound();
            }
            if (await _playlistRepository.CheckIfPlaylistIsOwnedByCurrentUser(userId, playlistId)
                || await _playlistRepository.CheckIfPlaylistIsLikedByCurrentUser(userId,playlistId))
            {
                return Forbid();
            }

            _playlistRepository.LikePlaylist(userId, playlistId);

            return NoContent();
        }

        [Authorize]
        [HttpGet("dislike/{playlistId}")]

        public async Task<IActionResult> Dislike(int playlistId)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            if (!await _playlistRepository.CheckIfPlaylistExist(playlistId))
            {
                return NotFound();
            }
            if (!await _playlistRepository.CheckIfPlaylistIsLikedByCurrentUser(userId, playlistId)
                || await _playlistRepository.CheckIfPlaylistIsOwnedByCurrentUser(userId, playlistId))
            {
                return Forbid();
            }

            _playlistRepository.DislikePlaylist(userId, playlistId);

            return NoContent();
        }


    }
}
