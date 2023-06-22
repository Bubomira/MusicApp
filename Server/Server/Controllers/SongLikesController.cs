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
        private readonly ISongLikesRepository _likesSongsRepository;
        public SongLikesController(ISongLikesRepository likesRepository)
        {
            _likesSongsRepository = likesRepository;
        }

        [HttpPost("like/song/{songId}")]

        public async Task<IActionResult> LikeSong([FromBody]int songId)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            if(await _likesSongsRepository.CheckIfUserHasLikedSong(userId, songId))
            {
                return BadRequest("Song has already been liked!");
            }
             _likesSongsRepository.LikeSong(userId, songId);
             return Ok();
        }

        [HttpPost("dislike/song/{songId}")]

        public async Task<IActionResult> DislikeSong([FromBody] int songId)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            if (!await _likesSongsRepository.CheckIfUserHasLikedSong(userId, songId))
            {
                return BadRequest("Song has never been liked!");
            }
            _likesSongsRepository.DislikeSong(userId, songId);
            return Ok();
        }
    }
}
