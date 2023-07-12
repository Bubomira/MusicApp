using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Server.DTO.AlbumDto;
using Server.DTO.PlaylistDto;
using Server.DTO.SongDTO;
using Server.Interfaces;
using System.Security.Claims;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public ProfileController(IProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        [HttpGet("likedSongs")]
        public async Task<IActionResult> GetUserLikedSongs()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var likedSongs = await _profileRepository.GetLikedSongs(userId);

            var songListDto = _mapper.Map<List<NormalSongDto>>(likedSongs);

            return Ok(songListDto);
        }

        [HttpGet("likedPlaylists")]
        public async Task<IActionResult> GetLikedPlaylists()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var likedPlaylistListDto = _mapper.Map<ExportNormalPlaylistDto>(await _profileRepository.GetLikedPlaylists(userId));

            return Ok(likedPlaylistListDto);
        }

        [HttpGet("ownedPlaylists")]

        public async Task<IActionResult> GetOwnedPlaylists()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var ownedPlaylistListDto = _mapper.Map<ExportNormalPlaylistDto>(await _profileRepository.GetOwnedPlaylists(userId));
            
            return Ok(ownedPlaylistListDto);
        }

        [HttpGet("likedAlbums")]
        public async Task<IActionResult> GetLikedAlbums()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var likedAlbumsListDto = _mapper.Map<ExportNormalAlbumDto>(await _profileRepository.GetLikedAlbums(userId));

            return Ok(likedAlbumsListDto);
        }

    }
}
