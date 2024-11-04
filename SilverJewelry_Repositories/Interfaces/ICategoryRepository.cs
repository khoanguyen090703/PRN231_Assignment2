using SilverJewelry_BOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverJewelry_Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetById(string id);

        Task<List<Category>> GetAll();
    }
}
