using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryPuerto
    {
        Task<ICollection<Puerto>> ListAsync();
        Task<Puerto> FindByIdAsync(int id);
        Task<ICollection<Puerto>> FindByNameAsync(string nombre);
    }
}
