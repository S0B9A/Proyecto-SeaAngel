using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeaAngel.Infraestructure.Data;
using SeaAngel.Infraestructure.Models;
using SeaAngel.Infraestructure.Repository.Interfaces;

namespace SeaAngel.Infraestructure.Repository.Implementations
{
    public class RepositoryEncReserva : IRepositoryEncReserva
    {

        private readonly SeanAngelContext _context;

        public RepositoryEncReserva(SeanAngelContext context)
        {
            _context = context;
        }

        public async Task<EncReserva> FindByIdAsync(int id)
        {
            //Obtener un Libro con su autor y las lista de categorías
            var @object = await _context.Set<EncReserva>()
                                .Where(x => x.Id == id)
                                .Include(Reserva => Reserva.ReservaComplementos)
                                .ThenInclude(Complementos => Complementos.IdcomplementoNavigation)
                                .Include(x => x.IdusuarioNavigation)
                                .Include(x => x.IdfechaNavigation)
                                .ThenInclude(crucero => crucero.IdcruceroNavigation)
                                .ThenInclude(Itinerario => Itinerario.Itinerario)
                                .ThenInclude(puerto => puerto.IdpuertoNavigation)
                                .FirstAsync();
            return @object!;
        }

        public async Task<ICollection<EncReserva>> ListAsync()
        {
            var collection = await _context.Set<EncReserva>()
                .Include(Reserva => Reserva.ReservaComplementos)
                                .ThenInclude(Complementos => Complementos.IdcomplementoNavigation)
                                .Include(x => x.IdusuarioNavigation)
                                .Include(x => x.IdfechaNavigation)
                                .ThenInclude(x => x.IdcruceroNavigation)
                                .ThenInclude(Itinerario => Itinerario.Itinerario)
                                .ThenInclude(puerto => puerto.IdpuertoNavigation)
                                .AsNoTracking()
                                .ToListAsync();
            return collection;
        }

        public async Task<int> AddAsync(EncReserva entity)
        {

            try
            {
                // Iniciar la transacción
                await _context.Database.BeginTransactionAsync();

                // Agregar el barco a la base de datos
                await _context.Set<EncReserva>().AddAsync(entity);


                // Actualizar inventario
                foreach (var item in entity.DetReserva)
                {
                    //Buscar fechaHabitacion
                    var fechaHabitacion = await _context.Set<FechaHabitacion>().FindAsync(item.Idhabitacion, entity.Idfecha);
                    //Actualizar cantidad en stock
                    fechaHabitacion!.CantDisponible = fechaHabitacion.CantDisponible - 1;
                    //Actualizar libro
                    _context.Set<FechaHabitacion>().Update(fechaHabitacion);
                }


                // Guardar cambios en la base de datos
                await _context.SaveChangesAsync();

                // Confirmar la transacción
                await _context.Database.CommitTransactionAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                // Si hay un error, hacer rollback de la transacción
                await _context.Database.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }
    }
}
