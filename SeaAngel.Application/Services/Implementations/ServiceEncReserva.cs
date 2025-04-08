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

        public async Task<int> GetNextNumberReserva()
        {
            int nextReceipt = await _repository.GetNextNumberReserva();
            return nextReceipt;
        }

        public async Task UpdateAsync(int id, EncReservaDTO dto)
        {
            try
            {
                var @object = await _repository.FindByIdAsync(id);

                var precioPendiente = Convert.ToInt32(@object.PrecioPendiente);
                var precioTotal= Convert.ToInt32(@object.PrecioTotal);

                if (precioPendiente == 0)
                {
                    @object.Estado = "Pagado";
                }
                else {
                    @object.Estado = "Pendiente";
                }
                
                decimal monto = Convert.ToInt32(@object.PrecioTotal);

                var nuevoPago = new Pago
                {
                    Id=0,
                    IdencReserva= id,
                    Monto= monto,
                    FechaPago = DateTime.Today,
                    MetodoPago = "Tarjeta de Crédito",
                    NumeroTarjeta = dto.NuevoPago.NumeroTarjeta,
                    FechaExpiracion = dto.NuevoPago.FechaExpiracion,
                    Cvv= dto.NuevoPago.Cvv,
                    TitularTarjeta= dto.NuevoPago.TitularTarjeta,
                    IdencReservaNavigation= dto.NuevoPago.IdencReservaNavigation
                };

                @object.Pago.Add(nuevoPago);

                @object.PrecioPendiente = (precioPendiente - monto).ToString();
                await _repository.UpdateAsync(@object);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }

}