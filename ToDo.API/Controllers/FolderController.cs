using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.DTOs;
using AutoMapper;
using ToDo.Entities;
using Microsoft.EntityFrameworkCore;

namespace ToDo.API.Controllers
{
    [ApiController]
    [Route("api/folder")]
    public class FolderController : ControllerBase
    {
        private readonly ContextDB contextDb;
        private readonly IMapper mapper;

        public FolderController(ContextDB contextDb, IMapper mapper)
        {            
            this.contextDb = contextDb;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<FolderDTO>>> GetAsync()
        {
            var folders = await contextDb.Folders
                .Include( e => e.Todos)
                .ToListAsync();

            return mapper.Map<List<FolderDTO>>(folders);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(FolderDTO folderDto) 
        {
            var folder = mapper.Map<Folder>(folderDto);
            contextDb.Add(folder);
            await contextDb.SaveChangesAsync();
            return NoContent();
        }
    }
}
