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
    public class ServiceBarco : IServiceBarco
    {

        private readonly IRepositoryBarco _repository;
        private readonly IMapper _mapper;

        public ServiceBarco(IRepositoryBarco repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BarcoDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<BarcoDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<BarcoDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<BarcoDTO>>(list);
            return collection;
        }
        public async Task<ICollection<HabitacionDTO>> ListHabitaciones(int id)
        {
            var list = await _repository.ListHabitaciones(id);
            var collection = _mapper.Map<ICollection<HabitacionDTO>>(list);
            return collection;
        }

        public async Task<int> AddAsync(BarcoDTO dto)
        {
            try
            {
                var objectMapped = _mapper.Map<Barco>(dto); // Map BarcoDTO to Barco
                return await _repository.AddAsync(objectMapped);  // Return

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task UpdateAsync(int id, BarcoDTO dto)
        {

            try
            {
                var @object = await _repository.FindByIdAsync(id);    //Obtenga el modelo original a actualizar

                var entity = _mapper.Map(dto, @object!);  //source, destination

                await _repository.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> GetNextNumberBarco()
        {
            int nextReceipt = await _repository.GetNextNumberBarco();
            return nextReceipt + 1;
        }

    }
}
