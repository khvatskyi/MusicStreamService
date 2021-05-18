using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MusicStreamServiceApp.API.Interface;
using MusicStreamServiceApp.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MusicStreamServiceApp.API.Service
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateJwtToken(UserDTO user)
        {
            var claims = new List<Claim>
            {
                new Claim("id", user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfiguration:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JWTConfiguration:JwtExpireDays"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWTConfiguration:JwtIssuer"],
                audience: configuration["JWTConfiguration:JwtAudience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
