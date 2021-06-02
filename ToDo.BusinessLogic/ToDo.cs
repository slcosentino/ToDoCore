using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Repositories.ContextDB;

namespace ToDo.BusinessLogic
{
    public class ToDo
    {       

        public ToDo()
        {
            
        }

        public async Task<List<Entities.ToDo>> GetAsync() {

            var todoRepository = new ToDoRepository();

            var todos = await contextDb.Todos
                .Include(e => e.Folder)
                .ToListAsync();

            return todos;
        }
    }
}
