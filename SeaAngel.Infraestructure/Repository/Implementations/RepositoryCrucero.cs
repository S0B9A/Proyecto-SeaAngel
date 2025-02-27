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
                .Include(c => c.EncReserva) // Cargar las reservas asociadas
                .Include(c => c.FechasPrecios)
                .ThenInclude(it => it.IdhabitacionNavigation)// Cargar las fechas y precios
                .Include(c => c.Itinerario) // Cargar los itinerarios
                .ThenInclude(it => it.IdpuertoNavigation)

                .FirstOrDefaultAsync();

            return @object!;
        }

        public async Task<ICollection<Crucero>> ListAsync()
        {
            // Obtener los barcos e incluir la relación BarcoHabitacion
            var collection = await _context.Set<Crucero>()
                .Include(c => c.IdbarcoNavigation) // Incluir información del Barco
                .Include(c => c.EncReserva) // Incluir las reservas
                .Include(c => c.FechasPrecios)
                .ThenInclude(it => it.IdhabitacionNavigation)// Incluir las fechas y precios
                .Include(c => c.Itinerario) // Incluir el itinerario
                .ThenInclude(it => it.IdpuertoNavigation)
                .ToListAsync();

            return collection;
        }
    }
}
