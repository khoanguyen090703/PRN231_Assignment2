using Microsoft.EntityFrameworkCore;
using SilverJewelry_BOs;
using SilverJewelry_DAO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverJewelry_DAO
{
    public class CategoryDAO
    {
        private SilverJewelry2023DbContext _context;
        private static CategoryDAO _instance;

        public static CategoryDAO Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new CategoryDAO();
                }
                return _instance;
            }
        }

        public CategoryDAO()
        {
            _context = new SilverJewelry2023DbContext();
        }

        public async Task<Category?> GetById(string id)
        {
            return await _context.Categories.SingleOrDefaultAsync(c => c.CategoryId.Equals(id));
        }

        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
