using AutoMapper;
using SeaAngel.Application.DTOs;
using SeaAngel.Infraestructure.Models;
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
    public class HabitacionProfile : Profile
    {
        public HabitacionProfile()
        {
            CreateMap<HabitacionDTO, Habitacion>().ReverseMap();
        }
    }
}
