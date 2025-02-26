using MusicStreamServiceApp.BLL.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MusicStreamServiceApp.BLL.Interfaces.IServices
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateUserAsync(UserDTO user);

        Task<SignInResult> AuthenticateUserAsync(UserLoginDTO userDTO);

        Task SignOutUserAsync();

        Task DeleteUserAsync(string Id);

        Task<IEnumerable<UserDTO>> GetAllUsersAsync();

        Task<UserDTO> GetUserByIdAsync(string Id);

        Task<UserDTO> GetUserByUsernameAsync(string Username);

        Task<UserDTO> GetUserByEmailAsync(string Email);

        Task<UserUpdateDTO> GetUserForUpdateAsync(string UserId);

        Task<IdentityResult> UpdateUserAsync(string UserId, UserUpdateDTO userParam);

        Task<bool> IsEmailUniqueAsync(string Email);

        Task<bool> IsUserNameUniqueAsync(string userName);

        Task<bool> CheckUserPasswordAsync(UserDTO user, string password);
    }
}
