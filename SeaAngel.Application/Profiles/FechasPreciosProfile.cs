using AutoMapper;
using SeaAngel.Application.DTOs;
using SeaAngel.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaAngel.Application.Profiles
{
    public class FechasPreciosProfile : Profile
    {
        public FechasPreciosProfile()
        {
            CreateMap<FechasPreciosDTO, FechasPrecios>().ReverseMap();
        }
    }
}
