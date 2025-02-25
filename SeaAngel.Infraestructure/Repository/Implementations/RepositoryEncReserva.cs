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
                                .Include(x => x.IdcruceroNavigation)
                                .ThenInclude(Itinerario => Itinerario.Itinerario)
                                .ThenInclude(puerto => puerto.IdpuertoNavigation)
                                .FirstAsync();
            return @object!;
        }

        public async Task<ICollection<EncReserva>> ListAsync()
        {
            var collection = await _context.Set<EncReserva>().AsNoTracking().ToListAsync();
            return collection;
        }
    }
}
