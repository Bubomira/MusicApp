using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

            var performerDto = _mapper.Map<PerformerDto>(performer);

            return Ok(performer);

        }
    }
}
