using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SeaAngel.Application.DTOs;
using SeaAngel.Application.Services.Interfaces;
using SeaAngel.Infraestructure.Models;
using SeaAngel.Infraestructure.Repository.Interfaces;

namespace SeaAngel.Application.Services.Implementations
{
    public class ServiceEncReserva : IServiceEncReserva
    {

        private readonly IRepositoryEncReserva _repository;
        private readonly IMapper _mapper;

        public ServiceEncReserva(IRepositoryEncReserva repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EncReservaDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<EncReservaDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<EncReservaDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<EncReservaDTO>>(list);
            return collection;
        }

        public async Task<int> AddAsync(EncReservaDTO dto)
        {
            try
            {
                var objectMapped = _mapper.Map<EncReserva>(dto); // Map BarcoDTO to Barco
                return await _repository.AddAsync(objectMapped);  // Return

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }

}