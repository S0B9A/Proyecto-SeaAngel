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
    public class ServiceComplementos : IServiceComplementos
    {
        private readonly IRepositoryComplementos _repository;
        private readonly IMapper _mapper;

        public ServiceComplementos(IRepositoryComplementos repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ComplementosDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<ComplementosDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<ComplementosDTO>> ListAsync()
        {
            
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<ComplementosDTO>>(list);
            return collection;
        }

        public async Task<ICollection<ComplementosDTO>> FindByNameAsync(string nombre)
        {
            var list = await _repository.FindByNameAsync(nombre);
            var collection = _mapper.Map<ICollection<ComplementosDTO>>(list);
            return collection;
        }
    }
}
