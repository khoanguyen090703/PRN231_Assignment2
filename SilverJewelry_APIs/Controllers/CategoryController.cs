using Microsoft.AspNetCore.Mvc;
using SilverJewelry_Repositories;
using SilverJewelry_Repositories.Interfaces;

namespace SilverJewelry_APIs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var response = await _categoryRepository.GetAll();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(string id)
        {
            var response = await _categoryRepository.GetById(id);
            return Ok(response);
        }
    }
}
