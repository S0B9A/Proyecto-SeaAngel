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
    public class ServiceFechaHabitacion : IServiceFechaHabitacion
    {

        private readonly IRepositoryFechaHabitacion _repository;
        private readonly IMapper _mapper;

        public ServiceFechaHabitacion(IRepositoryFechaHabitacion repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<FechaHabitacionDTO> FindByIdHabitacionAsync(int id)
        {
            var @object = await _repository.FindByIdHabitacionAsync(id);
            var objectMapped = _mapper.Map<FechaHabitacionDTO>(@object);
            return objectMapped;
        }

    }
}
