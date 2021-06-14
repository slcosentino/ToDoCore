using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core.Services;
using ToDo.Entities;

namespace ToDo.Service
{
    public class AccountService: BaseService, IAccountService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }        

        public async Task<bool> UserExistAsync(UserCredential credential)
        {
            var result = await signInManager.PasswordSignInAsync(credential.Username, credential.Password, isPersistent: false, lockoutOnFailure: false);
            return result.Succeeded;
        }
        public async Task<IdentityResult> NewUserAsync(UserCredential credential)
        {
            validateFields(credential);

            var user = new IdentityUser { UserName = credential.Username };
            var result = await userManager.CreateAsync(user, credential.Password);
            return result;
        }

        public async Task<string> CreateTokenAsync(UserCredential credential)
        {
            var claims = new List<Claim>()
            {
                new Claim("username", credential.Username)
            };

            var user = await userManager.FindByNameAsync(credential.Username);
            var claimsDb = await userManager.GetClaimsAsync(user);
            claims.AddRange(claimsDb);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT_SECRET_KEY"]));
            var signInCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(12);

            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: signInCredential);

            string strToken = new JwtSecurityTokenHandler().WriteToken(token);

            return await Task.FromResult(strToken);
        }
    }
}
