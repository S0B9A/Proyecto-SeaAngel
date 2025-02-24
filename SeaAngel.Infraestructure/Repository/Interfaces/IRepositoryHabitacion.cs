using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryHabitacion
    {
        Task<ICollection<Habitacion>> ListAsync();
        Task<Habitacion> FindByIdAsync(int id);
    }
}
