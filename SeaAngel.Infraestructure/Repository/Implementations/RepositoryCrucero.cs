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
    public class RepositoryCrucero : IRepositoryCrucero
    {
        private readonly SeanAngelContext _context;

        public RepositoryCrucero(SeanAngelContext context)
        {
            _context = context;
        }

        public async Task<Crucero> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Crucero>()
                .Where(c => c.Id == id)
                .Include(c => c.IdbarcoNavigation) // Cargar la relación con el Barco
                .Include(c => c.Fecha)
                .ThenInclude(it => it.FechaHabitacion)
                .ThenInclude(it => it.IdhabitacionNavigation)
                .Include(c => c.Itinerario) // Cargar los itinerarios
                .ThenInclude(it => it.IdpuertoNavigation)

                .FirstOrDefaultAsync();

            if (@object != null)
            {
                @object.Itinerario = @object.Itinerario.OrderBy(it => it.Dia).ToList();
            }

            return @object!;
        }

        public async Task<ICollection<Crucero>> ListAsync()
        {
            // Obtener los barcos e incluir la relación BarcoHabitacion
            var collection = await _context.Set<Crucero>()
                .Include(c => c.IdbarcoNavigation) // Incluir información del Barco
                .Include(c => c.Fecha)
                .Include(c => c.Itinerario) // Incluir el itinerario
                .ThenInclude(it => it.IdpuertoNavigation)
                .ToListAsync();

            return collection;
        }

        public async Task<int> AddAsync(Crucero entity)
        {
            try
            {
                // Iniciar la transacción
                await _context.Database.BeginTransactionAsync();

                // Agregar el crucero a la base de datos
                await _context.Set<Crucero>().AddAsync(entity);

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
