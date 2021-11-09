using AutoMapper;
using ContactAPI.Data;
using ContactAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Contacts, ContactModel>().ReverseMap();
        }
    }
}
