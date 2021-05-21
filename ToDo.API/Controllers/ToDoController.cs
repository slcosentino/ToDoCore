using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.DTOs;
using Microsoft.EntityFrameworkCore;
using ToDo.Entities;

namespace ToDo.API.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ContextDB contextDb;
        private readonly IMapper mapper;

        public ToDoController(ContextDB contextDb, IMapper mapper)
        {
            this.contextDb = contextDb;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDoDTO>>> GetAsync()
        {
            var todos = await contextDb.Todos
                .Include(e => e.Folder)
                .ToListAsync();            
            return mapper.Map<List<ToDoDTO>>(todos);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(ToDoDTO todoDto)
        {
            var todo = mapper.Map<Entities.ToDo>(todoDto);
            
            bool folderExist = await contextDb.Folders.AnyAsync(x => x.Id == todo.FolderId);
            if (!folderExist)
                throw new Exception("Folder inexistente");

            contextDb.Entry<Folder>(todo.Folder).State = EntityState.Unchanged;
            todo.Id = 0;
            contextDb.Add(todo);
            await contextDb.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(ToDoDTO todoDto)
        {

            var actualTodo = contextDb.Todos               
                .Where(e => e.Id == todoDto.Id)
                .FirstOrDefault();

            if (actualTodo == null)
                throw new Exception("No se encontro el todo");

            var todo = mapper.Map<Entities.ToDo>(todoDto);           

            actualTodo.FolderId = todo.FolderId;
            actualTodo.Name = todo.Name;

            try
            {
                await contextDb.SaveChangesAsync();
            }
            catch (Exception e)
            {

                var a = e;
            }
            

            return NoContent();
        }
    }
}
