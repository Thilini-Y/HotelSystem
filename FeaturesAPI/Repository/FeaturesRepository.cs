using AutoMapper;
using FeaturesAPI.Data;
using FeaturesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeaturesAPI.Repository
{
    public class FeaturesRepository : IFeaturesRepository
    {
        private readonly FeaturesStoreContext _context;
        private readonly IMapper _mapper;

        public FeaturesRepository(FeaturesStoreContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FeatureModel>> getAllFeaturesAsync()
        {
            try
            {

                var records = await _context.Features.ToListAsync();
                return _mapper.Map<List<FeatureModel>>(records);

            }
            catch
            {
                return null;
            }
            
        }

        public async Task<FeatureModel> GetFeatureByIdAsync(int id)
        {
            var Feature = await _context.Features.FindAsync(id);
            return _mapper.Map<FeatureModel>(Feature);
        }

        public async Task<int> AddFeatureAsync(FeatureModel featureModel)
        {
            try
            {
                Features feature = _mapper.Map<Features>(featureModel);
                _context.Features.Add(feature);
                await _context.SaveChangesAsync();

                return feature.Id;
            }

            catch
            {

                return -1;

            }

        }

        public async Task<bool> UpdateFeatureAsync(int _featureId, FeatureModel featureModel)
        {

            try
            {
                Features feature = _mapper.Map<Features>(featureModel);
                feature.Id = _featureId;

                _context.Features.Update(feature);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> DeleteFeatureAsync(int id)
        {

            try
            {

                var feature = new Features()
                {
                    Id = id
                };


                _context.Features.Remove(feature);

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
