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
            // Obtener datos del repositorio
            var list = await _repository.ListAsync();

            // Mapear la lista de Barco a BarcoDTO, incluyendo la columna CantidadHabitaciones
            var collection = list.Select(b => new BarcoDTO
            {
                Id = b.Id,
                Nombre = b.Nombre,
                Descripcion = b.Descripcion,
                Imagen = b.Imagen,
                CantidadHabitaciones = b.BarcoHabitacion.Count() // Calculamos la cantidad de habitaciones directamente en el mapeo
            }).ToList();

            return collection;
        }

    }
}
