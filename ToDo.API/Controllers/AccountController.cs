using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.API.DTOs;
using ToDo.Core.Services;
using ToDo.Entities;
 
namespace ToDo.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService service;
        private readonly IMapper mapper;
        private readonly ILogger<AccountController> logger;

        public AccountController(IAccountService service, IMapper mapper, ILogger<AccountController> logger)
        {
            this.service = service;
            this.mapper = mapper;
            this.logger = logger;
        }
        
        [HttpPost, Route("Login")]
        public async Task<ActionResult<AppTokenDTO>> LoginAsync([FromBody] UserCredentialDTO credentialDto)
        {
            try
            {
                var credential = mapper.Map<UserCredential>(credentialDto);
                var userExist = await service.UserExistAsync(credential);

                if (userExist)
                {
                    var appToken = await service.CreateTokenAsync(credential);
                    return mapper.Map<AppTokenDTO>(appToken);
                }                    
                else
                    return BadRequest(new { Message = "Invalid login." });
            }           
            catch (Exception ex)
            {
                logger.LogError("API_ERROR", string.Format("AccountController.Login:: {0}", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "We have a problem to check the user.");
            }           
        }

        [HttpPost, Route("NewUser")]
        public async Task<ActionResult<AppTokenDTO>> NewUserAsync([FromBody] UserCredentialDTO credentialDto)
        {
            try
            {
                var credential = mapper.Map<UserCredential>(credentialDto);
                var user = await service.NewUserAsync(credential);

                if (user.Succeeded)
                {
                    var appToken = await service.CreateTokenAsync(credential);
                    return mapper.Map<AppTokenDTO>(appToken);
                }
                else
                    return BadRequest(new { Message = "We have a problem to create the user.", Errors = user.Errors });
            }
            catch (Exception ex)
            {
                logger.LogError("API_ERROR", string.Format("AccountController.NewUserAsync:: {0}", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "We have a problem to check the user.");
            }
        }

    }
}
