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

        public override async Task<IEnumerable<Entities.Folder>> GetAllAsync(Pagination pagination)
        {            
            return await Context.Folders                
                .Include(e => e.Todos)
                .Where(e => e.Enabled)
                .OrderBy( e => e.Name)
                .Skip((pagination.Page - 1) * pagination.RecordsPerPage) 
                .Take(pagination.RecordsPerPage)                
                .ToListAsync();
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
