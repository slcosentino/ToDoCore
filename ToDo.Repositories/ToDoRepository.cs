using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core.Repositories;
using ToDo.Entities;
using Entities = ToDo.Entities;
namespace ToDo.Repositories
{

    public class ToDoRepository : Repository<Entities.ToDo>, IToDoRepository
    {
        public ToDoRepository(ContextDB context)
            : base(context)
        { }
        private ContextDB Context
        {
            get { return context as ContextDB; }
        }

        public override async Task AddAsync(Entities.ToDo entity)
        {  
            Context.Entry<Entities.Folder>(entity.Folder).State = EntityState.Unchanged;            
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public override async Task<IEnumerable<Entities.ToDo>> GetAllAsync(Pagination? pagination)
        {
            return await Context.Todos
                .Include(e => e.Folder)
                .Where(e => e.Enabled)
                .OrderBy( e => e.Name)
                .Skip((pagination.Page - 1) * pagination.RecordsPerPage)
                .Take(pagination.RecordsPerPage)
                .ToListAsync();
        }

        public override ValueTask<Entities.ToDo> GetByIdAsync(int id)
        {
            var todo = Context.Todos                
                .Include(e => e.Folder)
                .Where(e => e.Id == id && e.Folder.Enabled)
                .FirstOrDefault();

            return ValueTask.FromResult(todo);
        }


        public override void Remove(Entities.ToDo entity)
        {
            entity.Enabled = false;
        }

        public override void RemoveRange(IEnumerable<Entities.ToDo> entities)
        {
            foreach (var entity in entities)
            {
                this.Remove(entity);
            }
        }
        public override async Task<int> CountAsync()
        {
            return await Context.Todos.AsQueryable()
                .Where(e => e.Enabled)
                .CountAsync();
        }
        
    }
}
