using AutoMapper;
using RoomInfoAPI.Data;
using RoomInfoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomInfoAPI.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Room, RoomModel>().ReverseMap();
        }
    }
}
