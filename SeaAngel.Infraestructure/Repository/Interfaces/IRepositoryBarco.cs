using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryBarco
    {
        Task<ICollection<Barco>> ListAsync();
        Task<ICollection<Habitacion>> ListHabitaciones(int id);
        Task<Barco> FindByIdAsync(int id);
        Task<int> AddAsync(Barco entity);
        Task UpdateAsync(Barco entity);
        Task<int> GetNextNumberBarco();
    }
}
