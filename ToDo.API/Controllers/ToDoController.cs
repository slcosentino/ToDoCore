using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.DTOs;
using ToDo.Entities;
using ToDo.Core.Services;
using ToDo.API.Utilities;
using Microsoft.Extensions.Logging;
using ToDo.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ToDo.API.Controllers
{
    [Route("api/todo")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> logger;
        private readonly IMapper mapper;
        private readonly IToDoService service;

        public ToDoController(ILogger<ToDoController> logger, IMapper mapper, IToDoService service)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.service = service;
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult<ToDoDTO>> GetByIdAsync(int id)
        {           
            try
            {
                var todo = await service.GetByIdAsync(id);
                return mapper.Map<ToDoDTO>(todo);
            }
            catch (LogicException ex)
            {
                logger.LogError("API_LOGIC_ERROR", string.Format("ToDoController.GetByIdAsync:: {0}", ex.Message), ex);
                var e = ex.ValidationResult.Select(e => string.Format("{0} - {1}", e.MemberNames.FirstOrDefault(), e.ErrorMessage)).ToArray();
                return BadRequest(new { Message = string.Format("We have an error to get the ToDo."), Errors = e });
            }
            catch (Exception ex)
            {
                logger.LogError("API_ERROR", string.Format("ToDoController.GetByIdAsync:: {0}", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "We have an error to get the ToDo.");
            }
        }

        [HttpPost, Route("GetAll")]
        public async Task<ActionResult<List<ToDoDTO>>> GetAllAsync(PaginationDTO paginationDto)
        {          
            try
            {
                var pagination = mapper.Map<Pagination>(paginationDto);
                var todos = await service.GetAllAsync(pagination);
                var total = await service.CountAsync();
                HttpContext.InsertTotalItemsHeader(total);

                return mapper.Map<List<ToDoDTO>>(todos);
            }
            catch (LogicException ex)
            {
                logger.LogError("API_LOGIC_ERROR", string.Format("ToDoController.GetAllAsync:: {0}", ex.Message), ex);
                var e = ex.ValidationResult.Select(e => string.Format("{0} - {1}", e.MemberNames.FirstOrDefault(), e.ErrorMessage)).ToArray();
                return BadRequest(new { Message = string.Format("We have an error to get ToDos."), Errors = e });
            }
            catch (Exception ex)
            {
                logger.LogError("API_ERROR", string.Format("ToDoController.GetAllAsync:: {0}", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "We have an error to get ToDos.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(ToDoDTO todoDto)
        {           
            try
            {
                var todo = mapper.Map<Entities.ToDo>(todoDto);
                await service.AddAsync(todo);
                return NoContent();
            }
            catch (LogicException ex)
            {
                logger.LogError("API_LOGIC_ERROR", string.Format("ToDoController.PostAsync:: {0}", ex.Message), ex);
                var e = ex.ValidationResult.Select(e => string.Format("{0} - {1}", e.MemberNames.FirstOrDefault(), e.ErrorMessage)).ToArray();
                return BadRequest(new { Message = string.Format("We have an error to save the ToDo."), Errors = e });
            }
            catch (Exception ex)
            {
                logger.LogError("API_ERROR", string.Format("ToDoController.PostAsync:: {0}", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "We have an error to save the ToDo.");
            }
        }
        
        [HttpPut]
        public async Task<ActionResult> PutAsync(ToDoDTO todoDto)
        {           
            try
            {
                var todo = mapper.Map<Entities.ToDo>(todoDto);
                await service.UpdateAsync(todo);

                return NoContent();
            }
            catch (LogicException ex)
            {
                logger.LogError("API_LOGIC_ERROR", string.Format("ToDoController.PutAsync:: {0}", ex.Message), ex);
                var e = ex.ValidationResult.Select(e => string.Format("{0} - {1}", e.MemberNames.FirstOrDefault(), e.ErrorMessage)).ToArray();
                return BadRequest(new { Message = string.Format("We have an error to update the ToDo."), Errors = e });
            }
            catch (Exception ex)
            {
                logger.LogError("API_ERROR", string.Format("ToDoController.PutAsync:: {0}", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "We have an error to update the ToDo.");
            }
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {            
            try
            {
                var todo = await service.GetByIdAsync(id);

                await service.DeleteAsync(todo);
                return NoContent();
            }
            catch (LogicException ex)
            {
                logger.LogError("API_LOGIC_ERROR", string.Format("ToDoController.DeleteAsync:: {0}", ex.Message), ex);
                var e = ex.ValidationResult.Select(e => string.Format("{0} - {1}", e.MemberNames.FirstOrDefault(), e.ErrorMessage)).ToArray();
                return BadRequest(new { Message = string.Format("We have an error to delete the ToDo."), Errors = e });
            }
            catch (Exception ex)
            {
                logger.LogError("API_ERROR", string.Format("ToDoController.DeleteAsync:: {0}", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "We have an error to delete the ToDo.");
            }
        }
    }
}
