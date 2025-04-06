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
    public class PagoProfile:Profile
    {
        public PagoProfile()
        {
            CreateMap<PagoDTO, Pago>().ReverseMap();
        }
    }
}
