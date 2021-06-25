using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core.Repositories;
using ToDo.Entities;

namespace ToDo.Repositories
{
    public class FolderRepository: Repository<Entities.Folder>, IFolderRepository
    {
        public FolderRepository(ContextDB context)
          : base(context)
        { }

        private ContextDB Context
        {
            get { return context as ContextDB; }
        }

        public override async Task<IEnumerable<Folder>> GetAllAsync(Pagination pagination)
        {            
            return await Context.Folders                
                .Include(e => e.Todos.Where(t => t.Enabled))
                .Where(e => e.Enabled)
                .OrderBy( e => e.Name)
                .Skip((pagination.Page - 1) * pagination.RecordsPerPage) 
                .Take(pagination.RecordsPerPage)                
                .ToListAsync();
        }

        public override async Task<IEnumerable<Folder>> GetAllAsync()
        {
            return await Context.Folders
                .Include(e => e.Todos.Where(t => t.Enabled))
                .Where(e => e.Enabled)
                .OrderBy(e => e.Name)                
                .ToListAsync();
        }

        public override ValueTask<Folder> GetByIdAsync(int id)
        {
            var folder =  Context.Folders
                .Where(e => e.Id == id)
                .Include(e => e.Todos.Where(t => t.Enabled))
                .FirstOrDefault();

            return ValueTask.FromResult(folder);
        }

        public override void Remove(Entities.Folder entity) 
        {
            entity.Enabled = false;            
        }

        public override  async Task<int> CountAsync() 
        {
            return await Context.Folders.AsQueryable()
                .Where(e => e.Enabled)
                .CountAsync();
        }
    }
}
