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
    public class RepositoryFecha : IRepositoryFecha
    {
        private readonly SeanAngelContext _context;

        public RepositoryFecha(SeanAngelContext context)
        {
            _context = context;
        }

        public async Task<Fecha> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Fecha>()
                .Include(a => a.Idcrucero)
                .Include(a => a.FechaInicio)
                .Include(a => a.FechaLimitePago)
                .Include(a => a.IdcruceroNavigation)
                .Include(a => a.FechaHabitacion)
                    .ThenInclude(b => b.IdhabitacionNavigation)

                .Where(x => x.Id == id).FirstOrDefaultAsync();

            return @object!;
        }

        public async Task<ICollection<Fecha>> ListAsync()
        {
            // Obtener los Puertoa e incluir la relación Itinerario
            var collection = await _context.Set<Fecha>()
                .Include(a => a.Idcrucero)
                .Include(a => a.FechaInicio)
                .Include(a => a.FechaLimitePago)
                .Include(a => a.IdcruceroNavigation)
                .Include(a => a.FechaHabitacion)
                    .ThenInclude(b => b.IdhabitacionNavigation)
                .ToListAsync();

            return collection;
        }
    }
}
