using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.DTO.AlbumDto;
using Server.DTO.PerformerDto;
using Server.Interfaces;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerformerController:Controller
    {
        private readonly IPerformerRepository _performerRepository;
        private readonly IMapper _mapper;
        public PerformerController(IPerformerRepository performerRepository,IMapper mapper)
        {
            _performerRepository = performerRepository;
            _mapper = mapper;
        }

        [HttpGet("performer/{performerId}")]
        public async Task<IActionResult> GetPerformerDetails(int performerId)
        {
            if (! await _performerRepository.CheckIfPerformerExists(performerId))
            {
                return NotFound();
            }
            var performer = await _performerRepository.GetPerformerById(performerId);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var performerDto = _mapper.Map<PerformerDto>(performer);

            return Ok(performerDto);

        }

        [HttpGet("albums/{performerId}")]
        public async Task<IActionResult> GetPerformerAlbums(int performerId)
        {
            if (!await _performerRepository.CheckIfPerformerExists(performerId))
            {
                return NotFound();
            }
            var albums = await _performerRepository.GetAlbumsByPerformer(performerId);

            if (!ModelState.IsValid){
                return BadRequest();
            }

            var albumsDto = _mapper.Map<ExportNormalAlbumDto>(albums);

            return Ok(albumsDto);

        }

        [HttpGet("standaloneSongs/{performerId}")]
        public async Task<IActionResult> GetPerformerStandaloneSongs(int performerId)
        {
            if (!await _performerRepository.CheckIfPerformerExists(performerId))
            {
                return NotFound();
            }
            var standaloneSongs = await _performerRepository.GetStandaloneSongsByPerformer(performerId);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var standaloneSongsDtos = _mapper.Map<ExportNormalAlbumDto>(standaloneSongs);

            return Ok(standaloneSongsDtos);

        }



    }
}
