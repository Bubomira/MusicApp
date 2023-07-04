using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DTO.AlbumDto;
using Server.Interfaces;
using System.Security.Claims;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : Controller
    {
        private readonly IAlbumRepository _albumRepository;

        private readonly IMapper _mapper;

        public AlbumController(IAlbumRepository albumRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
            _mapper = mapper;
        }
        [HttpGet("details/{albumId}")]
        public async Task<IActionResult> GetAlbumDetails(int albumId)
        {
            if (!await _albumRepository.CheckIfAlbumExists(albumId))
            {
                return NotFound();
            }
            var albumDetails = await _albumRepository.GetAlbumById(albumId);
            var albumDetailsDto = _mapper.Map<ExportDetailedAlbumDto>(albumDetails);
            return Ok(albumDetailsDto);
        }

        [Authorize]
        [HttpGet("like/{albumId}")]
        public async Task<IActionResult> LikeAlbum(int albumId)
        {
            if (!await _albumRepository.CheckIfAlbumExists(albumId))
            {
                return NotFound("Album does not exist!");
            }
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            if (await _albumRepository.CheckIfAlbumIsLikedByUser(userId, albumId))
            {
                return Forbid("User has already liked the album!");
            }

            _albumRepository.LikeAlbum(userId, albumId);
            return NoContent();
        }

        [Authorize]
        [HttpGet("dislike/{albumId}")]

        public async Task<IActionResult> DislikeAlbum(int albumId)
        {
            if (!await _albumRepository.CheckIfAlbumExists(albumId))
            {
                return NotFound("Album does not exist!");
            }
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            if (!await _albumRepository.CheckIfAlbumIsLikedByUser(userId, albumId))
            {
                return Forbid("User has not liked the current album!");
            }

            _albumRepository.LikeAlbum(userId, albumId);
            return NoContent();
        }



    }
}
