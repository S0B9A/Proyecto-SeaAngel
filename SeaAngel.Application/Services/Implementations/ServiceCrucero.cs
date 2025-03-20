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
    public class ServiceCrucero : IServiceCrucero
    {

        private readonly IRepositoryCrucero _repository;
        private readonly IMapper _mapper;

        public ServiceCrucero(IRepositoryCrucero repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CruceroDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<CruceroDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<CruceroDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<CruceroDTO>>(list);
            return collection;
        }


        public async Task<int> AddAsync(CruceroDTO dto)
        {
            try
            {
                var objectMapped = _mapper.Map<Crucero>(dto); // Map CruceroDTO to Crucero
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
            return nextReceipt;
        }
    }
}
