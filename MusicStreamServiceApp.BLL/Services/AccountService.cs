using AutoMapper;
using MusicStreamServiceApp.BLL.DTOs;
using MusicStreamServiceApp.BLL.Interfaces.IServices;
using MusicStreamServiceApp.DAL.Entities;
using MusicStreamServiceApp.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;

namespace MusicStreamServiceApp.BLL.Services
{
    public class AccountService : BaseService, IAccountService
    {
        public AccountService(IUnitOfWork unitOfWork,
             IMapper mapper)
             : base(unitOfWork, mapper)
        {
        }

        public async Task<IdentityResult> CreateUserAsync(UserDTO userDTO)
        {
            var user = Mapper.Map<User>(userDTO);

            user.NormalizedEmail = UnitOfWork.UserManager.NormalizeEmail(userDTO.Email);

            user.NormalizedUserName = UnitOfWork.UserManager.NormalizeName(userDTO.UserName);

            user.EmailConfirmed = true;

            var result = await UnitOfWork.UserManager.CreateAsync(user, userDTO.Password);

            if (result.Succeeded)
            {
                await UnitOfWork.SignInManager.SignInAsync(user, false);
            }

            return result;
        }

        public async Task<SignInResult> AuthenticateUserAsync(UserLoginDTO userDTO)
        {
            var user = await UnitOfWork.UserManager.FindByEmailAsync(userDTO.Email);
            try
            {
                var result = await UnitOfWork.SignInManager.PasswordSignInAsync(user, userDTO.Password, userDTO.RememberMe, false);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task SignOutUserAsync()
        {
            await UnitOfWork.SignInManager.SignOutAsync();
        }

        public async Task<UserDTO> GetUserByEmailAsync(string Email)
        {
            var user = await UnitOfWork.UserManager.FindByEmailAsync(Email);

            return Mapper.Map<UserDTO>(user);
        }
        public async Task<UserDTO> GetUserByIdAsync(string Id)
        {
            var user = await UnitOfWork.UserManager.FindByIdAsync(Id);

            return Mapper.Map<UserDTO>(user);
        }
        public async Task<UserDTO> GetUserByUsernameAsync(string UserName)
        {
            var user = await UnitOfWork.UserManager.FindByNameAsync(UserName);

            return Mapper.Map<UserDTO>(user);
        }
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var userList = await UnitOfWork.UserManager.Users.ToListAsync();

            return Mapper.Map<IEnumerable<UserDTO>>(userList);
        }
        public async Task<UserUpdateDTO> GetUserForUpdateAsync(string UserId)
        {
            var user = await UnitOfWork.UserManager.FindByIdAsync(UserId);

            return Mapper.Map<UserUpdateDTO>(user);
        }

        public async Task<IdentityResult> UpdateUserAsync(string UserId, UserUpdateDTO userParam)
        {
            var user = await UnitOfWork.UserManager.FindByIdAsync(UserId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (!string.IsNullOrWhiteSpace(userParam.UserName) && userParam.UserName != user.UserName)
            {
                user.UserName = userParam.UserName;
                user.NormalizedUserName = UnitOfWork.UserManager.NormalizeName(user.UserName);
            }

            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
            {
                user.FirstName = userParam.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(userParam.LastName))
            {
                user.LastName = userParam.LastName;
            }

            if (!string.IsNullOrWhiteSpace(userParam.Email) && userParam.Email != user.Email)
            {
                user.Email = userParam.Email;
                user.NormalizedEmail = UnitOfWork.UserManager.NormalizeEmail(user.Email);
            }

            if (!string.IsNullOrWhiteSpace(userParam.PhotoPath) && userParam.PhotoPath != user.PhotoPath)
            {
                user.PhotoPath = userParam.PhotoPath;
            }

            if (!string.IsNullOrEmpty(userParam.NewPassword))
            {
                UnitOfWork.UserManager.PasswordHasher.HashPassword(user, userParam.NewPassword);
            }

            var result = await UnitOfWork.UserManager.UpdateAsync(user);

            return result;
        }

        public async Task DeleteUserAsync(string Id)
        {
            var user = await UnitOfWork.UserManager.FindByIdAsync(Id);

            if (user != null)
            {
                await UnitOfWork.UserManager.DeleteAsync(user);
            }
        }

        public async Task<bool> IsEmailUniqueAsync(string Email)
        {
            var user = await UnitOfWork.UserManager.FindByEmailAsync(Email);

            if (user == null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> IsUserNameUniqueAsync(string userName)
        {
            var user = await UnitOfWork.UserManager.FindByNameAsync(userName);

            if (user == null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> CheckUserPasswordAsync(UserDTO userDTO, string password)
        {
            var user = Mapper.Map<User>(userDTO);

            return await UnitOfWork.UserManager.CheckPasswordAsync(user, password);
        }
    }
}
