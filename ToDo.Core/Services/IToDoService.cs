using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Entities;

namespace ToDo.Core.Services
{
    public interface IToDoService
    {
        Task<Entities.ToDo> AddAsync(Entities.ToDo todo);
        Task<int> CountAsync();
        Task DeleteAsync(Entities.ToDo todo);
        Task<IEnumerable<Entities.ToDo>> GetAllAsync(Pagination pagination);
        Task<Entities.ToDo> GetByIdAsync(int id);
        Task UpdateAsync(Entities.ToDo todo);
    }
}
