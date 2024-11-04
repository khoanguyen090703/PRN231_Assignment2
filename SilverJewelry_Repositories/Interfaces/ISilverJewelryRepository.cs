using SilverJewelry_BOs;
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


        Task Insert(SilverJewelry silverJewelry);

        Task Update(SilverJewelry silverJewelry);

        Task DeleteById(string id);

        Task UpdateById(string id, SilverJewelry newSilverJewelry);
    }
}
