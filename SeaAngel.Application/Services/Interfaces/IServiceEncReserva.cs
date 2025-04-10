using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Application.DTOs;

namespace SeaAngel.Application.Services.Interfaces
{
    public interface IServiceEncReserva
    {
        Task<ICollection<EncReservaDTO>> ListAsync();
        Task<ICollection<EncReservaDTO>> ListAsyncUser(int id);
        Task<EncReservaDTO> FindByIdAsync(int id);
        Task<int> AddAsync(EncReservaDTO dto);
        Task<int> GetNextNumberReserva();
        Task UpdateAsync(int id, EncReservaDTO dto);
    }
}
