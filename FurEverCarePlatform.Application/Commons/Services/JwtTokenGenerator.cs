using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FurEverCarePlatform.Application.Commons.Services
{
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly RedisTokenService _redisTokenService;

        public JwtTokenGenerator(
            IConfiguration configuration,
            UserManager<AppUser> userManager,
            RedisTokenService redisTokenService
        )
        {
            _configuration = configuration;
            _userManager = userManager;
            _redisTokenService = redisTokenService;
        }

        public async Task<(string AccessToken, DateTime ExpiresAt)> GenerateAccessTokenAsync(
            AppUser user
        )
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // Add roles as claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Add custom user claims
            claims.AddRange(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiryMinutes = Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"]);
            var expiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiresAt,
                signingCredentials: creds
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
        }

        public async Task<string> GenerateRefreshTokenAsync(string userId)
        {
            // Generate a random token
            var randomBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            var refreshToken = Convert.ToBase64String(randomBytes);

            // Save to Redis
            await _redisTokenService.SaveRefreshTokenAsync(userId, refreshToken);

            return refreshToken;
        }

        public async Task<(bool IsValid, string UserId)> ValidateRefreshTokenAsync(string token)
        {
            return await _redisTokenService.ValidateRefreshTokenAsync(token);
        }

        public async Task RevokeUserTokensAsync(string userId)
        {
            await _redisTokenService.RevokeUserTokensAsync(userId);
        }
    }
}
