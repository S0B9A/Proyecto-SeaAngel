using SeaAngel.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaAngel.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryFecha
    {
        Task<ICollection<Fecha>> ListAsync();
        Task<Fecha> FindByIdAsync(int id);
    }
}
