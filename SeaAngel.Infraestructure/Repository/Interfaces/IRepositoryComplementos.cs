using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Infraestructure.Models;

namespace SeaAngel.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryComplementos
    {
        Task<ICollection<Complementos>> ListAsync();
        Task<Complementos> FindByIdAsync(int id);
        Task<ICollection<Complementos>> FindByNameAsync(string nombre);
    }
}
