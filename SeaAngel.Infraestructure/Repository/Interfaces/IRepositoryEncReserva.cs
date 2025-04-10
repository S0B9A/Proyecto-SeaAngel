using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryEncReserva
    {
        Task<ICollection<EncReserva>> ListAsync();
        Task<ICollection<EncReserva>> ListAsyncUser(int id);
        Task<EncReserva> FindByIdAsync(int id);

        Task<int> AddAsync(EncReserva entity);
        Task<int> GetNextNumberReserva();
        Task UpdateAsync(EncReserva entity);
    }
}
