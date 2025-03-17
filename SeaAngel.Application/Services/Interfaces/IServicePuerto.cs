using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Application.DTOs;

namespace SeaAngel.Application.Services.Interfaces
{
    public interface IServicePuerto
    {
        Task<ICollection<PuertoDTO>> ListAsync();
        Task<PuertoDTO> FindByIdAsync(int id);
        Task<ICollection<PuertoDTO>> FindByNameAsync(string nombre);
    }
}
