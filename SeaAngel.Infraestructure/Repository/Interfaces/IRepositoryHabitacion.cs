using SeaAngel.Infraestructure.Models;
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
        Task<ICollection<Habitacion>> FindByNameAsync(string nombre);
        Task<int> AddAsync(Habitacion entity);
        Task UpdateAsync(Habitacion entity);
        Task<int> GetNextNumber();
    }
}
