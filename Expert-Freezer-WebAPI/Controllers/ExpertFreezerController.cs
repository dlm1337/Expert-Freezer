using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ExpertFreezerAPI.Models;
using ExpertFreezerAPI.Service;

namespace ExpertFreezerAPI.Controllers
{
    [Route("api/ExpertFreezerProfile")]
    [ApiController]
    public class ExpertFreezerController : ControllerBase
    {
        private readonly IExpertFreezerService _ExpertFreezerService;

        public ExpertFreezerController(IExpertFreezerService ExpertFreezerService)
        {
            _ExpertFreezerService = ExpertFreezerService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegistrationDTO registrationDTO)
        {
            try
            {
                var userDTO = await _ExpertFreezerService.Register(registrationDTO);
                return CreatedAtAction(nameof(GetUser), new { id = userDTO.Id }, userDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(long id)
        {
            var userDTO = await _ExpertFreezerService.GetUser(id);

            if (userDTO == null)
            {
                return NotFound();
            }

            return Ok(userDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExpertFreezerProfileDTO>> GetExpertFreezer(long id)
        {
            var ExpertFreezer = await _ExpertFreezerService.GetExpertFreezer(id);

            if (ExpertFreezer == null)
            {
                return NotFound();
            }

            return Ok(ExpertFreezer);
        }

        [HttpPost]
        public async Task<ActionResult<ExpertFreezerProfileDTO>> CreateExpertFreezer([FromForm] ExpertFreezerProfileDTO ExpertFreezerProfileDTO)
        {
            var createdExpertFreezer = await _ExpertFreezerService.CreateExpertFreezer(ExpertFreezerProfileDTO);

            return CreatedAtAction(nameof(GetExpertFreezer),
                new { id = createdExpertFreezer.Id },
                createdExpertFreezer);
        }

        [HttpGet("Latest")]
        public async Task<ActionResult<ExpertFreezerProfileDTO>> GetLatestExpertFreezer()
        {
            var latestExpertFreezer = await _ExpertFreezerService.GetLatestExpertFreezer();

            if (latestExpertFreezer == null)
            {
                return NotFound();
            }

            return Ok(latestExpertFreezer);
        }
    }
}