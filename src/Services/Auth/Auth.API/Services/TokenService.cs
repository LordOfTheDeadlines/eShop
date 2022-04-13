using Auth.API.Configuration;
using Auth.API.Data.Entities;
using Auth.API.Data.Models.Responses;
using Auth.API.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Auth.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly AuthConfiguration _authConfig;

        public TokenService(IOptions<AuthConfiguration> authConfigOptions)
        {
            _authConfig = authConfigOptions.Value;
        }
        public string CreateToken(AppUser user)
        {
            var userData = new UserModel()
            {
                Id = user.Id,
                Username = user.Username,
                Roles = user.Roles?.Select(r => r.Name).ToArray(),
            };
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.UserData, JsonSerializer.Serialize(userData))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfig.AccessTokenSecret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken
            (
                _authConfig.Issuer,
                _authConfig.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(_authConfig.AccessTokenExpirationMinutes),
                credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
