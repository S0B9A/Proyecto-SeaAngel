using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SeaAngel.Application.DTOs;
using SeaAngel.Application.Services.Interfaces;
using SeaAngel.Infraestructure.Repository.Interfaces;

namespace SeaAngel.Application.Services.Implementations
{
    public class ServiceBarcoHabitacion : IServiceBarcoHabitacion
    {
        private readonly IRepositoryBarcoHabitacion _repository;
        private readonly IMapper _mapper;

        public ServiceBarcoHabitacion(IRepositoryBarcoHabitacion repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BarcoHabitacionDTO> FindByIdAsync(int idbarco , int idhabitacion)
        {
            var @object = await _repository.FindByIdAsync(idbarco, idhabitacion);
            var objectMapped = _mapper.Map<BarcoHabitacionDTO>(@object);
            return objectMapped;
        }
    }
}
