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
    public class ServicePuerto : IServicePuerto
    {
        private readonly IRepositoryPuerto _repository;
        private readonly IMapper _mapper;

        public ServicePuerto(IRepositoryPuerto repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PuertoDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<PuertoDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<PuertoDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<PuertoDTO>>(list);
            return collection;
        }

        public async Task<ICollection<PuertoDTO>> FindByNameAsync(string nombre)
        {
            var list = await _repository.FindByNameAsync(nombre);

            var collection = _mapper.Map<ICollection<PuertoDTO>>(list);

            return collection;

        }
    }
}
