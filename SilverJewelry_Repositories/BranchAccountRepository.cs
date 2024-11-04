using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SilverJewelry_BOs;
using SilverJewelry_DAO;
using SilverJewelry_Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SilverJewelry_Repositories
{
    public class BranchAccountRepository : IBranchAccountRepository
    {
        private readonly BranchAccountDAO _branchAccountDAO;
        private readonly IConfiguration _configuration;

        public BranchAccountRepository(BranchAccountDAO branchAccountDAO, IConfiguration configuration)
        {
            _branchAccountDAO = branchAccountDAO;
            _configuration = configuration;
        }

        public async Task<List<BranchAccount>> GetAll()
        {
            return await _branchAccountDAO.GetAll();
        }

        public async Task<BranchAccount?> GetById(int id)
        {
            return await _branchAccountDAO.GetById(id);
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _branchAccountDAO.GetByEmail(email);
            if (user == null || !user.AccountPassword.Equals(password))
            {
                throw new Exception("You are not allowed to access this function!");
            }

            var accessToken = GenerateAccessTokenString(user);
            return accessToken;
        }

        private string GenerateAccessTokenString(BranchAccount user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("sub", user.AccountId.ToString()),
                new Claim("username", user.EmailAddress),
                new Claim("role", user.Role.ToString())
            };

            var staticKey = _configuration["JWT:SigningKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(staticKey));
            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken
            (

                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signingCred
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }
    }
}
