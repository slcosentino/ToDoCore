using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.DTOs;
using AutoMapper;
using ToDo.Entities;
using System;
using ToDo.Core.Services;
using ToDo.API.Utilities;
using ToDo.Core;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ToDo.API.Controllers
{
    [ApiController]
    [Route("api/folder")]
    [Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FolderController : ControllerBase
    {
        private readonly ILogger<FolderController> logger;
        private readonly IMapper mapper;
        private readonly IFolderService service;

        public FolderController(ILogger<FolderController> logger, IMapper mapper, IFolderService service)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.service = service;
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult<FolderDTO>> GetByIdAsync(int id)
        {  
            try
            {
                var folder = await service.GetByIdAsync(id);
                return mapper.Map<FolderDTO>(folder);
            }
            catch (LogicException ex)
            {
                logger.LogError("API_LOGIC_ERROR", string.Format("FolderController.GetByIdAsync:: {0}", ex.Message), ex);
                var e = ex.ValidationResult.Select(e => string.Format("{0} - {1}", e.MemberNames.FirstOrDefault(), e.ErrorMessage)).ToArray();
                return BadRequest(new { Message = string.Format("We have an error to get the folder."), Errors = e });
            }
            catch (Exception ex)
            {
                logger.LogError("API_ERROR", string.Format("FolderController.GetByIdAsync:: {0}", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "We have an error to get the folder.");
            }

        }

        [HttpPost, Route("GetAll")]
        public async Task<ActionResult<List<FolderDTO>>> GetAllAsync( PaginationDTO paginationDto)
        {
            try
            {
                var pagination = mapper.Map<Pagination>(paginationDto);
                var folders = await service.GetAllAsync(pagination);
                var total = await service.CountAsync();
                HttpContext.InsertTotalItemsHeader(total);
                return mapper.Map<List<FolderDTO>>(folders);
            }
            catch (LogicException ex)
            {
                logger.LogError("API_LOGIC_ERROR", string.Format("FolderController.GetAllAsync:: {0}", ex.Message), ex);
                var e = ex.ValidationResult.Select(e => string.Format("{0} - {1}", e.MemberNames.FirstOrDefault(), e.ErrorMessage)).ToArray();
                return BadRequest(new { Message = string.Format("We have an error to get the folders."), Errors = e });
            }
            catch (Exception ex)
            {
                logger.LogError("API_ERROR", string.Format("FolderController.GetAllAsync:: {0}", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "We have an error to get the folders.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(FolderDTO folderDto) 
        {
            try
            {
                var folder = mapper.Map<Folder>(folderDto);
                await service.AddAsync(folder);
                return NoContent();
            }
            catch (LogicException ex)
            {                
                logger.LogError("API_LOGIC_ERROR", string.Format("FolderController.PostAsync:: {0}", ex.Message), ex);
                var e = ex.ValidationResult.Select(e => string.Format("{0} - {1}", e.MemberNames.FirstOrDefault(), e.ErrorMessage)).ToArray();
                return BadRequest(new { Message = string.Format("We have an error to save the folder."), Errors = e });
            }
            catch (Exception ex)
            {
                logger.LogError("API_ERROR", string.Format("FolderController.PostAsync:: {0}", ex.Message), ex);                 
                return StatusCode(StatusCodes.Status500InternalServerError, "We have an error to save the folder.");
            }
            
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(FolderDTO folderDto)
        {
            try
            {
                var folder = mapper.Map<Entities.Folder>(folderDto);
                await service.UpdateAsync(folder);
                return NoContent();
            }
            catch (LogicException ex)
            {
                logger.LogError("API_LOGIC_ERROR", string.Format("FolderController.PutAsync:: {0}", ex.Message), ex);
                var e = ex.ValidationResult.Select(e => string.Format("{0} - {1}", e.MemberNames.FirstOrDefault(), e.ErrorMessage)).ToArray();
                return BadRequest(new { Message = string.Format("We have an error to update the folder."), Errors = e });
            }
            catch (Exception ex)
            {
                logger.LogError("API_ERROR", string.Format("FolderController.PutAsync:: {0}", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "We have an error to update the folder.");
            }
            
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var folder = await service.GetByIdAsync(id);
                await service.DeleteAsync(folder);
                return NoContent();
            }
            catch (LogicException ex)
            {
                logger.LogError("API_LOGIC_ERROR", string.Format("FolderController.DeleteAsync:: {0}", ex.Message), ex);
                var e = ex.ValidationResult.Select(e => string.Format("{0} - {1}", e.MemberNames.FirstOrDefault(), e.ErrorMessage)).ToArray();
                return BadRequest(new { Message = string.Format("We have an error to delete the folder."), Errors = e });
            }
            catch (Exception ex)
            {
                logger.LogError("API_ERROR", string.Format("FolderController.DeleteAsync:: {0}", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "We have an error to delete the folder.");
            }
        }
    }
}
