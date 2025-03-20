using SeaAngel.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaAngel.Application.Services.Interfaces
{
    public interface IServiceFecha
    {
        Task<ICollection<FechaDTO>> ListAsync();
        Task<FechaDTO> FindByIdAsync(int id);
    }
}
