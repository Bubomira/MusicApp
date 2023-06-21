using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public PlaylistController(IPlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        [HttpGet("playlist/details/{playlistId}")]
        public async Task<IActionResult> GetPlaylistById(int playlistId)
        {
            if (!await _playlistRepository.CheckIfPlaylistExist(playlistId))
            {

                return NotFound();
            }
            return Ok(await _playlistRepository.GetPlaylistDetails(playlistId));
        }

        [Authorize]
        [HttpPost("playlist/create")]
        public async Task<IActionResult> CreatePlaylist([FromBody] string playlistName)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            _playlistRepository.CreatePlaylist(userId, playlistName);

            return Ok();
        }
    }
}
