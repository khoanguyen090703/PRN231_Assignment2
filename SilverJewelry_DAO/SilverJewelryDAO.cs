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
    public class SilverJewelryDAO
    {
        private SilverJewelry2023DbContext _context;
        private static SilverJewelryDAO _instance;

        public static SilverJewelryDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SilverJewelryDAO();
                }
                return _instance;
            }
        }

        public SilverJewelryDAO()
        {
            _context = new SilverJewelry2023DbContext();
        }

        public async Task<SilverJewelry?> GetById(string id)
        {
            return await _context.SilverJewelries.Include(sj => sj.Category).SingleOrDefaultAsync(sj => sj.SilverJewelryId.Equals(id));
        }

        public async Task<List<SilverJewelry>> GetAll(string? searchValue)
        {
            var query = _context.SilverJewelries.Include(sj => sj.Category).AsQueryable();

            // Search on name or weight
            if(searchValue != null && !string.IsNullOrEmpty(searchValue) && !string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(sj => sj.SilverJewelryName.ToLower().Contains(searchValue) 
                    || sj.MetalWeight.ToString().Contains(searchValue));
            }

            return await query.ToListAsync();

        }

        public async Task Update(SilverJewelry silverJewelry)
        {
            _context.Attach<SilverJewelry>(silverJewelry);
            _context.SilverJewelries.Update(silverJewelry);

            await _context.SaveChangesAsync();
        }

        public async Task Insert(SilverJewelry silverJewelry)
        {
            await _context.SilverJewelries.AddAsync(silverJewelry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(string id)
        {
            var silverJewelry = await _context.SilverJewelries.FindAsync(id);
            if(silverJewelry == null)
                return;
            _context.SilverJewelries.Remove(silverJewelry);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateById(string id, SilverJewelry newSilverJewelry)
        {
            var silverJewelry = await _context.SilverJewelries.FindAsync(id);
            if (silverJewelry == null)
                return;
            _context.Entry(silverJewelry).CurrentValues.SetValues(newSilverJewelry);

            await _context.SaveChangesAsync();
        }
    }
}
