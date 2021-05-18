using BlazorUI.Models.AccountModels;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorUI.Interfaces.IServices;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using BlazorUI.Models;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorUI.Data;
using System;
using Microsoft.JSInterop;
using Microsoft.Extensions.Configuration;

namespace BlazorUI.Services
{
    public class AccountService : IAccountService
    {
        private HttpClient HttpClient { get; }
        private AuthenticationStateProvider StateProvider { get; }
        private IJSRuntime JSRuntime { get; }
        private IConfiguration Configuration { get; }


        public AccountService(
            HttpClient HttpClient,
            AuthenticationStateProvider StateProvider,
            IJSRuntime JSRuntime,
            IConfiguration Configuration)
        {
            this.StateProvider = StateProvider;
            this.HttpClient = HttpClient;
            this.JSRuntime = JSRuntime;
            this.Configuration = Configuration;
        }

        public async Task RegisterUserAsync(RegisterViewModel user)
        {
            string serializedUser = JsonSerializer.Serialize(user);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Account/Register")
            {
                Content = new StringContent(serializedUser)
            };

            requestMessage.Content.Headers.ContentType
                = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.SendAsync(requestMessage);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                using var responseBody = await response.Content.ReadAsStreamAsync();
                var ModelState = await JsonSerializer.DeserializeAsync<Dictionary<string, string[]>>(responseBody);

                throw new ARException(ModelState);
            }
        }

        public async Task AuthenticateUserAsync(UserLoginModel user)
        {
            string serializedUser = JsonSerializer.Serialize(user);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Account/Authenticate")
            {
                Content = new StringContent(serializedUser)
            };

            requestMessage.Content.Headers.ContentType
                = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.SendAsync(requestMessage);

            using var responseBody = await response.Content.ReadAsStreamAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                var ModelState = await JsonSerializer.DeserializeAsync<Dictionary<string, string[]>>(responseBody);
                throw new ARException(ModelState);
            }

            var tokenModel = await JsonSerializer.DeserializeAsync<TokenModel>(responseBody);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(tokenModel.token);

            var claimsIdentity = new ClaimsIdentity(
                jwtSecurityToken.Claims, CookieAuthenticationDefaults.AuthenticationScheme);

            ((CustomAuthenticationStateProvider)StateProvider).MarkUserAsAuthenticated(claimsIdentity);

            await SetUserClaimsAsync(tokenModel.token);
        }

        private async Task SetUserClaimsAsync(string token)
        {
            await JSRuntime.InvokeAsync<object>("setUserToken", Configuration["UserCookieInfoName"], token);
        }

        public async Task LogoutUserAsync()
        {
            ((CustomAuthenticationStateProvider)StateProvider).MarkUserAsLoggedOut();
            await JSRuntime.InvokeAsync<object>("romoveUserToken", Configuration["UserCookieInfoName"]);
        }
    }
}
