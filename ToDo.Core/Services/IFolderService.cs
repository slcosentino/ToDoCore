using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Entities;

namespace ToDo.Core.Services
{
    public interface IFolderService
    {
        Task<Folder> AddAsync(Folder folder);
        Task<int> CountAsync();
        Task DeleteAsync(Folder folder);
        Task<IEnumerable<Folder>> GetAllAsync(Pagination pagination);
        Task<IEnumerable<Folder>> GetAllAsync();
        Task<Folder> GetByIdAsync(int id);
        Task UpdateAsync(Folder folder);
    }
}
