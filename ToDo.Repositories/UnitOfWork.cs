using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core;
using ToDo.Core.Repositories;

namespace ToDo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContextDB context;
        private ToDoRepository toDoRepository;
        private FolderRepository folderRepository;

        public UnitOfWork(ContextDB context)
        {
            this.context = context;
        }
                
        public IToDoRepository ToDoRepository => toDoRepository = toDoRepository ?? new ToDoRepository(context);

        public IFolderRepository FolderRepository => folderRepository = folderRepository ?? new FolderRepository(context);

        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
