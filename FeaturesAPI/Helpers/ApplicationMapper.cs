using AutoMapper;
using AutoMapper.Features;
using FeaturesAPI.Data;
using FeaturesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeaturesAPI.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Features, FeatureModel>().ReverseMap();
        }
    }
}
