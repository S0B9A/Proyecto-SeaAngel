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
using SeaAngel.Infraestructure.Models;

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
        public async Task<int> AddAsync(HabitacionDTO dto)
        {
            try
            {
                var objectMapped = _mapper.Map<Habitacion>(dto); // Map BarcoDTO to Barco
                return await _repository.AddAsync(objectMapped);  // Return

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task UpdateAsync(int id, HabitacionDTO dto)
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
        public async Task<ICollection<HabitacionDTO>> FindByNameAsync(string nombre)
        {
            var list = await _repository.FindByNameAsync(nombre);

            var collection = _mapper.Map<ICollection<HabitacionDTO>>(list);

            return collection;

        }
        public async Task<int> GetNextNumber()
        {
            int nextReceipt = await _repository.GetNextNumber();
            return nextReceipt + 1;
        }
    }
}
