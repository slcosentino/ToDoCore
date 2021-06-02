using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        public async Task<ActionResult<string>> LoginAsync([FromBody] UserCredentialDTO credentialDto)
        {
            try
            {
                var credential = mapper.Map<UserCredential>(credentialDto);
                var userExist = await service.UserExistAsync(credential);

                if (userExist)
                    return await service.CreateTokenAsync(credential);
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
        public async Task<ActionResult<string>> NewUserAsync([FromBody] UserCredentialDTO credentialDto)
        {
            try
            {
                var credential = mapper.Map<UserCredential>(credentialDto);
                var user = await service.NewUserAsync(credential);

                if (user.Succeeded)
                    return await service.CreateTokenAsync(credential);
                else
                    return BadRequest(new { Message = "Invalid login.", Errors = user.Errors });
            }
            catch (Exception ex)
            {
                logger.LogError("API_ERROR", string.Format("AccountController.NewUserAsync:: {0}", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "We have a problem to check the user.");
            }
        }

    }
}
