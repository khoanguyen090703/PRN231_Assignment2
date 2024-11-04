using Microsoft.AspNetCore.Mvc;
using SilverJewelry_BOs;
using SilverJewelry_Repositories.Interfaces;

namespace SilverJewelry_APIs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SilverJewelryController : Controller
    {
        private readonly ISilverJewelryRepository _silverJewelryRepository;

        public SilverJewelryController(ISilverJewelryRepository silverJewelryRepository)
        {
            _silverJewelryRepository = silverJewelryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetSilverJewelries([FromQuery] string? searchValue)
        {
            var response = await _silverJewelryRepository.GetAll(searchValue?.ToLower());
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSilverJewelry(string id)
        {
            var response = await _silverJewelryRepository.GetById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSilverJewelry([FromBody]SilverJewelry silverJewelry)
        {
            await _silverJewelryRepository.Insert(silverJewelry);
            return StatusCode(201, new { message = "Create silver jewelry success!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> CreateSilverJewelry([FromBody] SilverJewelry silverJewelry, string id)
        {
            await _silverJewelryRepository.UpdateById(id, silverJewelry);
            return Ok(new { message = "Update silver jewelry success!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CreateSilverJewelry(string id)
        {
            await _silverJewelryRepository.DeleteById(id);
            return Ok(new { message = "Delete silver jewelry success!" });
        }
    }
}
