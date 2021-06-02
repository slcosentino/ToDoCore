using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core;
using ToDo.Core.Services;
using ToDo.Entities;
using ToDo.Service;
using Entities = ToDo.Entities;

namespace ToDoService
{
    public class ToDoService: BaseService, IToDoService
    {
        private readonly IUnitOfWork unitOfWork;
        public ToDoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Entities.ToDo> AddAsync(Entities.ToDo todo)
        {
            var folder = await unitOfWork.FolderRepository.GetByIdAsNoTrackingAsync(x => x.Id == todo.Folder.Id);
            validateFields(folder);

            todo.Id = 0;
            todo.Enabled = true;
            await unitOfWork.ToDoRepository.AddAsync(todo);
            await unitOfWork.CommitAsync();

            return todo;
        }

        public async Task DeleteAsync(Entities.ToDo todo)
        {
            validateEntityExist(todo);

            unitOfWork.ToDoRepository.Remove(todo);         
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Entities.ToDo>> GetAllAsync(Pagination pagination)
        {            
            return await unitOfWork.ToDoRepository.GetAllAsync(pagination);
        }

        public async Task<Entities.ToDo> GetByIdAsync(int id)
        {
            var todo = await unitOfWork.ToDoRepository.GetByIdAsync(id);
            validateEntityExist(todo);

            return todo;
        }

        public async Task UpdateAsync(Entities.ToDo todo)
        {
            var todoEntity = await unitOfWork.ToDoRepository.GetByIdAsync(todo.Id);
            validateEntityExist(todo);
            
            var folder = await unitOfWork.FolderRepository.GetByIdAsync(todo.Folder.Id);
            validateEntityExist(todo);
            
            todoEntity.Name = todo.Name;
            await unitOfWork.CommitAsync();                        
        }

        public async Task<int> CountAsync()
        {
            return await unitOfWork.ToDoRepository.CountAsync();
        }
    }
}
