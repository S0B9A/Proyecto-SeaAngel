using AutoMapper;
using SeaAngel.Application.DTOs;
using SeaAngel.Application.Services.Interfaces;
using SeaAngel.Infraestructure.Models;
using SeaAngel.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaAngel.Application.Services.Implementations
{
    public class ServiceFecha : IServiceFecha
    {
        private readonly IRepositoryFecha _repository;
        private readonly IMapper _mapper;

        public ServiceFecha(IRepositoryFecha repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FechaDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<FechaDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<FechaDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<FechaDTO>>(list);
            return collection;
        }
        public async Task<int> AddAsync(FechaDTO dto)
        {
            try
            {
                var objectMapped = _mapper.Map<Fecha>(dto); // Map BarcoDTO to Barco
                return await _repository.AddAsync(objectMapped);  // Return

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<int> GetNextNumber()
        {
            int nextReceipt = await _repository.GetNextNumber();
            return nextReceipt + 1;
        }
    }
}
