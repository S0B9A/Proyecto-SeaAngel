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
    public class RepositoryBarco : IRepositoryBarco
    {
        private readonly SeanAngelContext _context;

        public RepositoryBarco(SeanAngelContext context)
        {
            _context = context;
        }

        public async Task<Barco> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Barco>()
                .Include(Barco => Barco.BarcoHabitacion)
                    .ThenInclude(Habitacion => Habitacion.IdhabitacionNavigation)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            return @object!;
        }

        public async Task<ICollection<Barco>> ListAsync()
        {
            // Obtener los barcos e incluir la relación BarcoHabitacion
            var collection = await _context.Set<Barco>()
                .Include(b => b.BarcoHabitacion)
                 .ThenInclude(Habitacion => Habitacion.IdhabitacionNavigation)
                .ToListAsync();

            return collection;
        }
    }
}
