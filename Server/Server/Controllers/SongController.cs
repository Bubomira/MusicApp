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
        public SongController(ISongRepository songRepository)
        {
            this._songRepository = songRepository;
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
