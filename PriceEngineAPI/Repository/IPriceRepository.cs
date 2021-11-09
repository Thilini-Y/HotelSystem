using PriceEngineAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceEngineAPI.Repository
{
    public interface IPriceRepository
    {
        Task<List<PriceModel>> getAllPricesAsync();

        Task<PriceModel> GetPriceByIdAsync(int id);

        Task<int> AddPriceAsync(PriceModel priceModel);

        Task<bool> UpdatePriceAsync(int _priceId, PriceModel priceModel);

        Task<bool> DeletePriceAsync(int id);
    }
}
