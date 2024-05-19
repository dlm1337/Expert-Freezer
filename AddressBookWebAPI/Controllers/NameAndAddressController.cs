using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AddressBookAPI.Models;
using AddressBookAPI.Service;

namespace AddressBookAPI.Controllers
{
    [Route("api/NameAndAddress")]
    [ApiController]
    public class NameAndAddressController : ControllerBase
    {
        private readonly INameAndAddressService _nameAndAddressService;

        public NameAndAddressController(INameAndAddressService nameAndAddressService)
        {
            _nameAndAddressService = nameAndAddressService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NameAndAddressDTO>> GetNameAndAddress(long id)
        {
            var nameAndAddress = await _nameAndAddressService.GetNameAndAddress(id);

            if (nameAndAddress == null)
            {
                return NotFound();
            }

            return Ok(nameAndAddress);
        }

        [HttpPost]
        public async Task<ActionResult<NameAndAddressDTO>> CreateNameAndAddress(NameAndAddressDTO nameAndAddressDTO)
        {
            var createdNameAndAddress = await _nameAndAddressService.CreateNameAndAddress(nameAndAddressDTO);

            return CreatedAtAction(nameof(GetNameAndAddress),
                new { id = createdNameAndAddress.Id },
                createdNameAndAddress);
        }

        [HttpGet("Latest")]
        public async Task<ActionResult<NameAndAddressDTO>> GetLatestNameAndAddress()
        {
            var latestNameAndAddress = await _nameAndAddressService.GetLatestNameAndAddress();

            if (latestNameAndAddress == null)
            {
                return NotFound();
            }

            return Ok(latestNameAndAddress);
        }
    }
}