using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using SilverJewelry_BOs;
using SilverJewelry_Repositories.Interfaces;
using SilverJewelry_Repositories.Models.Request;

namespace SilverJewelry_APIs.Controllers
{
    public class SilverJewelryController : ODataController
    {
        private readonly ISilverJewelryRepository _silverJewelryRepository;

        public SilverJewelryController(ISilverJewelryRepository silverJewelryRepository)
        {
            _silverJewelryRepository = silverJewelryRepository;
        }

        [EnableQuery]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> GetSilverJewelry([FromQuery] string? searchValue)
        {
            var response = await _silverJewelryRepository.GetAll(searchValue?.ToLower());
            return Ok(response);
        }

        [HttpGet("[controller]/{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetSilverJewelryById(string id)
        {
            var response = await _silverJewelryRepository.GetById(id);
            return Ok(response);
        }

        [HttpPost("[controller]")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CreateSilverJewelry([FromBody]CreateSilverJewelryRequest silverJewelry)
        {
            await _silverJewelryRepository.Insert(silverJewelry);
            return StatusCode(201, new { message = "Create silver jewelry success!" });
        }

        [HttpPut("[controller]/{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdateSilverJewelry([FromBody] SilverJewelry silverJewelry, string id)
        {
            await _silverJewelryRepository.UpdateById(id, silverJewelry);
            return Ok(new { message = "Update silver jewelry success!" });
        }

        [HttpDelete("[controller]/{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteSilverJewelry(string id)
        {
            await _silverJewelryRepository.DeleteById(id);
            return Ok(new { message = "Delete silver jewelry success!" });
        }
    }
}
