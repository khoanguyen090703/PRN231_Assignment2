using SilverJewelry_BOs;
using SilverJewelry_Repositories.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverJewelry_Repositories.Interfaces
{
    public interface IBranchAccountRepository
    {
        Task<BranchAccount?> GetById(int id);

        Task<List<BranchAccount>> GetAll();

        Task<LoginResponse> Login(string email, string password);
    }
}
