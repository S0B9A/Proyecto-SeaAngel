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
    public class RepositoryFechaHabitacion : IRepositoryFechaHabitacion
    {

        private readonly SeanAngelContext _context;

        public RepositoryFechaHabitacion(SeanAngelContext context)
        {
            _context = context;
        }

        public async Task<FechaHabitacion> FindByIdHabitacionAsync(int id)
        {
            var @object = await _context.Set<FechaHabitacion>()
                .Include(a => a.IdfechaNavigation)
                .Include(a => a.IdhabitacionNavigation)
                .Where(x => x.Idhabitacion == id).FirstOrDefaultAsync();

            return @object!;
        }
    }
}
