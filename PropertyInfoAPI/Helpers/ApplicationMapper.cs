using AutoMapper;
using PropertyInfoAPI.Data;
using PropertyInfoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyInfoAPI.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Property, PropertyModel>().ReverseMap();
        }
    }
}
