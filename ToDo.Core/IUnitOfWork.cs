using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core.Repositories;

namespace ToDo.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IToDoRepository ToDoRepository { get; }
        IFolderRepository FolderRepository { get; }
        Task<int> CommitAsync();
    }
}
