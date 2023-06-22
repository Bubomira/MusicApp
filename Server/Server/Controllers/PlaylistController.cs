﻿using AutoMapper;
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

        public PlaylistController(IPlaylistRepository playlistRepository,IMapper mapper)
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
    }
}
