using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using System.Security.Claims;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SongLikesController:Controller
    {
        private readonly ISongLikesRepository _likesRepository;
        public SongLikesController(ISongLikesRepository likesRepository)
        {
            _likesRepository = likesRepository;
        }

        [HttpGet("liked/songs")]
        public async Task<IActionResult> GetUserLikedSongs()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var songs = await _likesRepository.GetLikedSongs(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(songs);
        }
    }
}
