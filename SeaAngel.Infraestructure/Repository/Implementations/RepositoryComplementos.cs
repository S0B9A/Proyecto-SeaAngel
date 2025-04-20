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
    public class RepositoryComplementos: IRepositoryComplementos
    {
        private readonly SeanAngelContext _context;

        public RepositoryComplementos(SeanAngelContext context)
        {
            _context = context;
        }

        public async Task<Complementos> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Complementos>()
                                      .Where(x => x.Id == id)
                                      .Include(b => b.ReservaComplementos)
                                      .FirstAsync();
            return @object!;
        }

        public async Task<ICollection<Complementos>> ListAsync()
        {
            var collection = await _context.Set<Complementos>().AsNoTracking().ToListAsync();
            return collection;
        }

        public async Task<ICollection<Complementos>> FindByNameAsync(string nombre)
        {
            var collection = await _context
                                         .Set<Complementos>()
                                         .Where(p => p.Nombre.Contains(nombre))
                                         .ToListAsync();
            return collection;
        }

        public async Task<int> AddAsync(Complementos entity)
        {
            try
            {
                // Iniciar la transacción
                await _context.Database.BeginTransactionAsync();

                // Agregar el barco a la base de datos
                await _context.Set<Complementos>().AddAsync(entity);

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

        public async Task UpdateAsync(Complementos entity)
        {
            try
            {
                // Iniciar la transacción
                await _context.Database.BeginTransactionAsync();

                // Actualizar el com´lemento en la base de datos
                _context.Set<Complementos>().Update(entity);

                // Guardar cambios en la base de datos
                await _context.SaveChangesAsync();

                // Confirmar la transacción
                await _context.Database.CommitTransactionAsync();
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
