using FeaturesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeaturesAPI.Repository
{
    public interface IFeaturesRepository
    {
        Task<List<FeatureModel>> getAllFeaturesAsync();

        Task<FeatureModel> GetFeatureByIdAsync(int id);

        Task<int> AddFeatureAsync(FeatureModel featureModel);

        Task<bool> UpdateFeatureAsync(int _featureId, FeatureModel featureModel);

        Task<bool> DeleteFeatureAsync(int id);
    }
}
