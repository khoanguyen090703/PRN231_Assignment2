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
    public class BranchAccountDAO
    {
        private SilverJewelry2023DbContext _context;
        private static BranchAccountDAO _instance;

        public static BranchAccountDAO Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new BranchAccountDAO();
                }
                return _instance;
            }
        }

        public BranchAccountDAO()
        {
            _context = new SilverJewelry2023DbContext();
        }

        public async Task<BranchAccount?> GetById(int id)
        {
            return await _context.BranchAccounts.SingleOrDefaultAsync(ba => ba.AccountId == id);
        }

        public async Task<List<BranchAccount>> GetAll()
        {
            return await _context.BranchAccounts.ToListAsync();
        }

        public async Task<BranchAccount?> GetByEmail(string email)
        {
            return await _context.BranchAccounts.SingleOrDefaultAsync(ba => ba.EmailAddress.Equals(email));
        }
    }
}
