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
    public class RepositoryBarcoHabitacion : IRepositoryBarcoHabitacion
    {
        private readonly SeanAngelContext _context;

        public RepositoryBarcoHabitacion(SeanAngelContext context)
        {
            _context = context;
        }

        public async Task<BarcoHabitacion> FindByIdAsync(int idBarco, int idHabitacion)
        {
            var @object = await _context.Set<BarcoHabitacion>()
                .Include(Barco => Barco.IdbarcoNavigation)
                .Include(Habitacion => Habitacion.IdhabitacionNavigation)
                .Where(x => x.Idhabitacion == idHabitacion && x.Idbarco == idBarco)
                .FirstOrDefaultAsync();
            return @object!;
        }
    }
}
