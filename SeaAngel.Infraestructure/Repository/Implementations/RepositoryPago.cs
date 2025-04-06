using Microsoft.EntityFrameworkCore;
using SeaAngel.Infraestructure.Data;
using SeaAngel.Infraestructure.Models;
using SeaAngel.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaAngel.Infraestructure.Repository.Implementations
{
    public class RepositoryPago : IRepositoryPago
    {

        private readonly SeanAngelContext _context;

        public RepositoryPago(SeanAngelContext context)
        {
            _context = context;
        }

        public async Task<Pago> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Pago>()
                                      .Where(x => x.Id == id)
                                      .Include(b => b.IdencReservaNavigation)
                                      .FirstAsync();
            return @object!;
        }

        public async Task<ICollection<Pago>> ListAsync()
        {
            var collection = await _context.Set<Pago>().AsNoTracking().ToListAsync();
            return collection;
        }
         public async Task<int> AddAsync(Pago entity)
        {

            try
            {
                // Iniciar la transacción
                await _context.Database.BeginTransactionAsync();

                // Agregar el barco a la base de datos
                await _context.Set<Pago>().AddAsync(entity);

                // Guardar cambios en la base de datos
                await _context.SaveChangesAsync();

                // Confirmar la transacción
                await _context.Database.CommitTransactionAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }

        }
    }
}
