using Microsoft.AspNetCore.Identity;
 using System.Threading.Tasks;
using ToDo.Entities;

namespace ToDo.Core.Services
{
    public interface IAccountService
    {        
        Task<string> CreateTokenAsync(UserCredential credential);
        Task<IdentityResult> NewUserAsync(UserCredential credential);
        Task<bool> UserExistAsync(UserCredential credential);
    }
}
