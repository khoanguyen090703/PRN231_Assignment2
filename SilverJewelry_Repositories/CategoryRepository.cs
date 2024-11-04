using SilverJewelry_BOs;
using SilverJewelry_DAO;
using SilverJewelry_Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverJewelry_Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDAO _categoryDAO;

        public CategoryRepository(CategoryDAO categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }

        public async Task<List<Category>> GetAll()
        {
            return await _categoryDAO.GetAll();
        }

        public async Task<Category?> GetById(string id)
        {
            return await _categoryDAO.GetById(id);
        }
    }
}
