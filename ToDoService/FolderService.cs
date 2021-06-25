using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core;
using ToDo.Core.Services;
using ToDo.Entities;
using ToDo.Service;

namespace ToDoService
{
    public class FolderService: BaseService, IFolderService
    {
        private readonly IUnitOfWork unitOfWork;
        public FolderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Folder> AddAsync(Folder folder)
        {
            validateFields(folder);

            folder.Id = 0;
            folder.Enabled = true;
            await unitOfWork.FolderRepository.AddAsync(folder);
            await unitOfWork.CommitAsync();

            return folder;
        }       

        public async Task DeleteAsync(Folder folder)
        {
            validateEntityExist(folder);

            unitOfWork.FolderRepository.Remove(folder);
            unitOfWork.ToDoRepository.RemoveRange(folder.Todos);
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Folder>> GetAllAsync(Pagination pagination)
        {
            return await unitOfWork.FolderRepository.GetAllAsync(pagination);
        }

        public async Task<IEnumerable<Folder>> GetAllAsync()
        {
            return await unitOfWork.FolderRepository.GetAllAsync();
        }

        public async Task<Folder> GetByIdAsync(int id)
        {
            var folder = await unitOfWork.FolderRepository.GetByIdAsync(id);
            validateEntityExist(folder);

            return folder;
        }

        public async Task UpdateAsync(Folder folder)
        {
            var folderEntity = await unitOfWork.FolderRepository.GetByIdAsync(folder.Id);
            validateEntityExist(folderEntity);
            validateFields(folderEntity);

            folderEntity.Name = folder.Name;
            await unitOfWork.CommitAsync();
        }      

        public async Task<int> CountAsync()
        {
            return await unitOfWork.FolderRepository.CountAsync();
        }

        
    }
}
