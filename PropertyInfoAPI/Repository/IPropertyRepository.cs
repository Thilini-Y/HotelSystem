using PropertyInfoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyInfoAPI.Repository
{
    public interface IPropertyRepository
    {
        Task<List<PropertyModel>> getAllPropertiesAsync();

        Task<PropertyModel> GetPropertyByIdAsync(int id);

        Task<int> AddPropertyAsync(PropertyModel propertyModel);

        Task<bool> UpdatePropertyAsync(int _propertyId, PropertyModel propertyModel);

        Task<bool> DeletePropertyAsync(int id);
    }
}
