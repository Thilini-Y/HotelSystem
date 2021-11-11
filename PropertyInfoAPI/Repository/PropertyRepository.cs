using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PropertyInfoAPI.Data;
using PropertyInfoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyInfoAPI.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly PropertyStoreContext _context;
        private readonly IMapper _mapper;

        public PropertyRepository(PropertyStoreContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<List<PropertyModel>> getAllPropertiesAsync()
        {
            try
            {

                var records = await _context.Property.ToListAsync();
                return _mapper.Map<List<PropertyModel>>(records);

            }
            catch
            {
                return null;
            }

        }

        public async Task<PropertyModel> GetPropertyByIdAsync(int id)
        {
            var property = await _context.Property.FindAsync(id);
            return _mapper.Map<PropertyModel>(property);
        }

        public async Task<int> AddPropertyAsync(PropertyModel propertyModel)
        {
            try
            {
                Property _property = _mapper.Map<Property>(propertyModel);
                _context.Property.Add(_property);
                await _context.SaveChangesAsync();

                return _property.PropertyId;
            }

            catch
            {

                return -1;

            }

        }

        public async Task<bool> UpdatePropertyAsync(int _propertyId, PropertyModel propertyModel)
        {

            try
            {
                Property _property = _mapper.Map<Property>(propertyModel);
                _property.PropertyId = _propertyId;

                _context.Property.Update(_property);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> DeletePropertyAsync(int id)
        {

            try
            {

                var _property = new Property()
                {
                    PropertyId = id
                };


                _context.Property.Remove(_property);

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
