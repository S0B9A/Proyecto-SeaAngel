using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SeaAngel.Application.DTOs;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.Profiles
{
    public class ItinerarioProfile : Profile
    {
        public ItinerarioProfile()
        {
            CreateMap<ItinerarioDTO, Itinerario>().ReverseMap();
        }
    }
}
