using BlazorUI.Models.AccountModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace BlazorUI.Interfaces.IServices
{
    public interface IAccountService
    {
        Task RegisterUserAsync(RegisterViewModel user);
        Task AuthenticateUserAsync(UserLoginModel userParams);
        Task LogoutUserAsync();
    }
}
