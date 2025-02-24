using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Identity.Client;
using SeaAngel.Application.DTOs;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Application.Profiles
{
    public class DestinoProfile : Profile
    {
        public DestinoProfile()
        {
            CreateMap<DestinoDTO, Destino>().ReverseMap();
        }
    }
}
