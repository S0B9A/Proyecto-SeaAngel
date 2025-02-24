using AutoMapper;
using SeaAngel.Application.DTOs;
using SeaAngel.Application.Services.Interfaces;
using SeaAngel.Infraestructure.Repository.Interfaces;
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
    public class ServiceHabitacion : IServiceHabitacion
    {

        private readonly IRepositoryHabitacion _repository;
        private readonly IMapper _mapper;

        public ServiceHabitacion(IRepositoryHabitacion repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<HabitacionDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<HabitacionDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<HabitacionDTO>> ListAsync()
        {
            //Obtener datos del repositorio
            var list = await _repository.ListAsync();
            // Map List<Autor> a ICollection<BodegaDTO>
            // Map List<Categoria> a ICollection<BodegaDTO>
            var collection = _mapper.Map<ICollection<HabitacionDTO>>(list);
            // Return lista
            return collection;
        }

    }
}
