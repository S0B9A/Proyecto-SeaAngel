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
    }
}
