using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController :Controller
    {
        private readonly ISongRepository _songRepository;
        private readonly IMapper _mapper;
        public SongController(ISongRepository songRepository, IMapper mapper)
        {
            this._songRepository = songRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200,Type= typeof(ICollection<Song>))]
        public IActionResult GetSongs()
        {
            var songs = _songRepository.GetSongs();
            if(!ModelState.IsValid){ return BadRequest(ModelState);}

            return Ok(songs);
        }
    }
}
