using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = ToDo.Entities;
namespace ToDo.Core.Repositories
{
    public interface IToDoRepository: IRepository<Entities.ToDo>
    {
    }
}
