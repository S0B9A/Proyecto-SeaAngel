using Microsoft.EntityFrameworkCore;
using SeaAngel.Infraestructure.Data;
using SeaAngel.Infraestructure.Models;
using SeaAngel.Infraestructure.Repository.Interfaces;
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
    public class RepositoryHabitacion : IRepositoryHabitacion
    {

        private readonly SeanAngelContext _context;

        public RepositoryHabitacion(SeanAngelContext context)
        {
            _context = context;
        }

        public async Task<Habitacion> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Habitacion>()
                                      .Where(x => x.Id == id)
                                      .Include(b => b.BarcoHabitacion)
                                      .FirstAsync();
            return @object!;
        }

        public async Task<ICollection<Habitacion>> ListAsync()
        {
            var collection = await _context.Set<Habitacion>().AsNoTracking().ToListAsync();
            return collection;
        }

        public async Task<ICollection<Habitacion>> FindByNameAsync(string nombre)
        {
            var collection = await _context
                                         .Set<Habitacion>()
                                         .Where(p => p.Nombre.Contains(nombre))
                                         .ToListAsync();
            return collection;
        }
    }
}
