using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilverJewelry_Repositories;
using SilverJewelry_Repositories.Interfaces;
using SilverJewelry_Repositories.Models.Request;

namespace SilverJewelry_APIs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BranchAccountController : Controller
    {
        private readonly IBranchAccountRepository _branchAccountRepository;

        public BranchAccountController(IBranchAccountRepository branchAccountRepository)
        {
            _branchAccountRepository = branchAccountRepository;
        }

        [HttpGet]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetBranchAccounts()
        {
            var response = await _branchAccountRepository.GetAll();
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetBranchAccount(int id)
        {
            var response = await _branchAccountRepository.GetById(id);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _branchAccountRepository.Login(request.Email, request.Password);
                return Ok(response);
            }   
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
