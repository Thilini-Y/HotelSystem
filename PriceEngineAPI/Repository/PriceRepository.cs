using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PriceEngineAPI.Data;
using PriceEngineAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceEngineAPI.Repository
{
    public class PriceRepository : IPriceRepository
    {
        private readonly PriceStoreContext _context;
        private readonly IMapper _mapper;

        public PriceRepository(PriceStoreContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<List<PriceModel>> getAllPricesAsync()
        {
            try
            {

                var records = await _context.Price.ToListAsync();
                return _mapper.Map<List<PriceModel>>(records);

            }
            catch
            {
                return null;
            }

        }

        public async Task<PriceModel> GetPriceByIdAsync(int id)
        {
            var Feature = await _context.Price.FindAsync(id);
            return _mapper.Map<PriceModel>(Feature);
        }

        public async Task<int> AddPriceAsync(PriceModel priceModel)
        {
            try
            {
                Price _price = _mapper.Map<Price>(priceModel);
                _context.Price.Add(_price);
                await _context.SaveChangesAsync();

                return _price.PriceId;
            }

            catch
            {

                return -1;

            }

        }

        public async Task<bool> UpdatePriceAsync(int _priceId, PriceModel priceModel)
        {

            try
            {
                Price _price = _mapper.Map<Price>(priceModel);
                _price.PriceId = _priceId;

                _context.Price.Update(_price);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> DeletePriceAsync(int id)
        {

            try
            {

                var _price = new Price()
                {
                    PriceId = id
                };


                _context.Price.Remove(_price);

                await _context.SaveChangesAsync();

                return true;

            }

            catch
            {

                return false;

            }


        }
    }
}
