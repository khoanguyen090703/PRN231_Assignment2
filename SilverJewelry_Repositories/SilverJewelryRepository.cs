using SilverJewelry_BOs;
using SilverJewelry_DAO;
using SilverJewelry_Repositories.Interfaces;
using SilverJewelry_Repositories.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverJewelry_Repositories
{
    public class SilverJewelryRepository : ISilverJewelryRepository
    {
        private readonly SilverJewelryDAO _silverJewelryDAO;

        public SilverJewelryRepository(SilverJewelryDAO silverJewelryDAO)
        {
            _silverJewelryDAO = silverJewelryDAO;
        }

        public async Task DeleteById(string id)
        {
            await _silverJewelryDAO.DeleteById(id);
        }

        public async Task<List<SilverJewelry>> GetAll(string? searchValue)
        {
            return await _silverJewelryDAO.GetAll(searchValue);
        }

        public async Task<SilverJewelry?> GetById(string id)
        {
            return await _silverJewelryDAO.GetById(id);
        }

        public async Task Insert(CreateSilverJewelryRequest silverJewelry)
        {
            var newSilverJewelry = new SilverJewelry
            {
                SilverJewelryId = Guid.NewGuid().ToString(),
                SilverJewelryName = silverJewelry.SilverJewelryName,
                SilverJewelryDescription = silverJewelry.SilverJewelryDescription,
                CreatedDate = DateTime.Now,
                MetalWeight = silverJewelry.MetalWeight,
                Price = silverJewelry.Price,
                ProductionYear = silverJewelry.ProductionYear,
                CategoryId = silverJewelry.CategoryId
            };

            await _silverJewelryDAO.Insert(newSilverJewelry);
        }

        public async Task Update(SilverJewelry silverJewelry)
        {
            await _silverJewelryDAO.Update(silverJewelry);
        }

        public async Task UpdateById(string id, SilverJewelry newSilverJewelry)
        {
            await _silverJewelryDAO.UpdateById(id, newSilverJewelry);
        }
    }
}
