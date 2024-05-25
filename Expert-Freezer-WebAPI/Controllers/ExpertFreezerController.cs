using Microsoft.AspNetCore.Mvc;
using ExpertFreezerAPI.Models;
using ExpertFreezerAPI.Service;
using Microsoft.AspNetCore.Authorization;

namespace ExpertFreezerAPI.Controllers
{
    [Route("api/ExpertFreezerProfile/")]
    [ApiController]
    public class ExpertFreezerController : ControllerBase
    {
        private readonly IExpertFreezerService _expertFreezerService;
        private readonly ITokenService _tokenService;

        public ExpertFreezerController(IExpertFreezerService expertFreezerService, ITokenService tokenService)
        {
            _expertFreezerService = expertFreezerService;
            _tokenService = tokenService;
        }


        [AllowAnonymous] // Allows access without authentication
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register([FromForm] RegistrationDTO registrationDTO)
        {
            try
            {
                var userDTO = await _expertFreezerService.Register(registrationDTO);
                return CreatedAtAction(nameof(GetUser), new { id = userDTO.Id }, userDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous] // Allows access without authentication
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromForm] LoginDTO loginDTO)
        {
            var user = await _expertFreezerService.Login(loginDTO);

            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            var token = _tokenService.GenerateToken(user.Username);
            return Ok(token);
        }

        [AllowAnonymous] // Allows access without authentication
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(string username)
        {
            var userDTO = await _expertFreezerService.GetUser(username);

            if (userDTO == null)
            {
                return NotFound();
            }

            return Ok(userDTO);
        }

        [AllowAnonymous] // Allows access without authentication
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpertFreezerProfileDTO>> GetExpertFreezer(long id)
        {
            var expertFreezer = await _expertFreezerService.GetExpertFreezer(id);

            if (expertFreezer == null)
            {
                return NotFound();
            }

            return Ok(expertFreezer);
        }

        [Authorize] // Requires authentication for this endpoint
         [HttpPost("profile")]
        public async Task<ActionResult<ExpertFreezerProfileDTO>> CreateExpertFreezer([FromForm] ExpertFreezerProfileDTO expertFreezerProfileDTO)
        {
            var createdExpertFreezer = await _expertFreezerService.CreateExpertFreezer(expertFreezerProfileDTO);

            return CreatedAtAction(nameof(GetExpertFreezer),
                new { id = createdExpertFreezer.Id },
                createdExpertFreezer);
        }

    }
}