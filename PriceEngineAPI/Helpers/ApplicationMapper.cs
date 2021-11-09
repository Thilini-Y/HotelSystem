using AutoMapper;
using PriceEngineAPI.Data;
using PriceEngineAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceEngineAPI.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Price, PriceModel>().ReverseMap();
        }
    }
}
