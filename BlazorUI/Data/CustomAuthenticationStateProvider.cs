using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorUI.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private HttpContext HttpContext { get; }

        public CustomAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor)
        {
            HttpContext = httpContextAccessor.HttpContext;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (HttpContext.Request.Cookies.TryGetValue("UserToken", out string token) && !string.IsNullOrEmpty(token))
            {
                JwtSecurityToken jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    jwtSecurityToken.Claims, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                return Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            else
            {
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
                return Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
        }

        public void MarkUserAsAuthenticated(ClaimsIdentity identity)
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public void MarkUserAsLoggedOut()
        {
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}
