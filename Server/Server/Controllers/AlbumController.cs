using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.DTO.AlbumDto;
using Server.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController:Controller
    {
        private readonly IAlbumRepository _albumRepository;

        private readonly IMapper _mapper;

        public AlbumController(IAlbumRepository albumRepository,IMapper mapper)
        {
            _albumRepository = albumRepository;
            _mapper = mapper;
        }
        [Route("details/{albumId}")]
        public async Task<IActionResult> GetAlbumDetails(int albumId)
        {
            if(!await _albumRepository.CheckIfAlbumExists(albumId))
            {
                return NotFound();
            }
            var albumDetails = await _albumRepository.GetAlbumById(albumId);
            var albumDetailsDto = _mapper.Map<ExportDetailedAlbumDto>(albumDetails);
            return Ok(albumDetailsDto);
        }

    }
}
