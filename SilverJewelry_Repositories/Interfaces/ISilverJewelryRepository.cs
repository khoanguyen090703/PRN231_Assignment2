using SilverJewelry_BOs;
using SilverJewelry_Repositories.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverJewelry_Repositories.Interfaces
{
    public interface ISilverJewelryRepository
    {
        Task<SilverJewelry?> GetById(string id);

        Task<List<SilverJewelry>> GetAll(string? searchValue);


        Task Insert(CreateSilverJewelryRequest silverJewelry);

        Task Update(SilverJewelry silverJewelry);

        Task DeleteById(string id);

        Task UpdateById(string id, SilverJewelry newSilverJewelry);
    }
}
