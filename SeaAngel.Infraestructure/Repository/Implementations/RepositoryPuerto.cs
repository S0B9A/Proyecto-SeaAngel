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
    public class RepositoryPuerto : IRepositoryPuerto
    {
        private readonly SeanAngelContext _context;

        public RepositoryPuerto(SeanAngelContext context)
        {
            _context = context;
        }

        public async Task<Puerto> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Puerto>()
                .Include(Puerto => Puerto.Itinerario)
                    .ThenInclude(Itinerario => Itinerario.IdcruceroNavigation)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            return @object!;
        }

        public async Task<ICollection<Puerto>> ListAsync()
        {
            // Obtener los Puertoa e incluir la relación Itinerario
            var collection = await _context.Set<Puerto>()
                .Include(b => b.Itinerario)
                 .ThenInclude(Itinerario => Itinerario.IdcruceroNavigation)
                .ToListAsync();

            return collection;
        }

        public async Task<ICollection<Puerto>> FindByNameAsync(string nombre)
        {
            var collection = await _context
                                         .Set<Puerto>()
                                         .Where(p => p.Nombre.Contains(nombre))
                                         .ToListAsync();
            return collection;
        }
    }
}
