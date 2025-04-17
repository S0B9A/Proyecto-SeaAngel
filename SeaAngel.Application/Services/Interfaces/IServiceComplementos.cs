using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaAngel.Application.DTOs;

namespace SeaAngel.Application.Services.Interfaces
{
    public interface IServiceComplementos
    {
        Task<ICollection<ComplementosDTO>> ListAsync();
        Task<ComplementosDTO> FindByIdAsync(int id);
        Task<ICollection<ComplementosDTO>> FindByNameAsync(string nombre);
        Task<int> AddAsync(ComplementosDTO dto);
    }
}
