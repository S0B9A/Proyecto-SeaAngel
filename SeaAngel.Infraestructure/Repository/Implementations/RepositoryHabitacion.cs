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
    public class RepositoryHabitacion:IRepositoryHabitacion
    {
        private readonly SeanAngelContext _context;

        public RepositoryHabitacion(SeanAngelContext context)
        {
            _context = context;
        }

        public Task<Habitacion> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Habitacion>> ListAsync()
        {
            var collection = await _context.Set<Habitacion>().ToListAsync();
            return collection;
        }
    }
}
