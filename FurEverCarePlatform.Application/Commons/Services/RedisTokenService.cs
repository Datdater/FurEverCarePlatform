using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace FurEverCarePlatform.Application.Commons.Services
{
    public class RedisTokenService
    {
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _configuration;

        public RedisTokenService(IDistributedCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _configuration = configuration;
        }

        public async Task SaveRefreshTokenAsync(string userId, string refreshToken)
        {
            var tokenInfo = new RefreshTokenInfo
            {
                UserId = userId,
                Token = refreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(
                    Convert.ToDouble(_configuration["Jwt:RefreshTokenExpiryInDays"])
                ),
                CreatedAt = DateTime.UtcNow,
                IsRevoked = false,
            };

            var cacheKey = $"RefreshToken:{refreshToken}";
            var userTokensKey = $"UserRefreshTokens:{userId}";

            // Store token info
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddDays(
                    Convert.ToDouble(_configuration["Jwt:RefreshTokenExpiryInDays"])
                ),
            };

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(tokenInfo), options);

            // Get existing user tokens (if any)
            var userTokensJson = await _cache.GetStringAsync(userTokensKey);
            var userTokens = string.IsNullOrEmpty(userTokensJson)
                ? new UserRefreshTokens { UserId = userId, Tokens = new List<string>() }
                : JsonSerializer.Deserialize<UserRefreshTokens>(userTokensJson);

            // Add new token and save
            userTokens.Tokens.Add(refreshToken);
            await _cache.SetStringAsync(
                userTokensKey,
                JsonSerializer.Serialize(userTokens),
                options
            );
        }

        public async Task<(bool IsValid, string UserId)> ValidateRefreshTokenAsync(string token)
        {
            var cacheKey = $"RefreshToken:{token}";
            var jsonToken = await _cache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(jsonToken))
                return (false, null);

            var tokenInfo = JsonSerializer.Deserialize<RefreshTokenInfo>(jsonToken);

            if (tokenInfo.IsRevoked || tokenInfo.ExpiryDate < DateTime.UtcNow)
                return (false, null);

            // Remove the used token
            await _cache.RemoveAsync(cacheKey);

            // Update user tokens list by removing this token
            var userTokensKey = $"UserRefreshTokens:{tokenInfo.UserId}";
            var userTokensJson = await _cache.GetStringAsync(userTokensKey);

            if (!string.IsNullOrEmpty(userTokensJson))
            {
                var userTokens = JsonSerializer.Deserialize<UserRefreshTokens>(userTokensJson);
                userTokens.Tokens.Remove(token);

                // Save updated tokens list
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.UtcNow.AddDays(
                        Convert.ToDouble(_configuration["Jwt:RefreshTokenExpiryInDays"])
                    ),
                };
                await _cache.SetStringAsync(
                    userTokensKey,
                    JsonSerializer.Serialize(userTokens),
                    options
                );
            }

            return (true, tokenInfo.UserId);
        }

        public async Task RevokeUserTokensAsync(string userId)
        {
            var userTokensKey = $"UserRefreshTokens:{userId}";
            var userTokensJson = await _cache.GetStringAsync(userTokensKey);

            if (string.IsNullOrEmpty(userTokensJson))
                return;

            var userTokens = JsonSerializer.Deserialize<UserRefreshTokens>(userTokensJson);

            // Remove all tokens for this user
            foreach (var token in userTokens.Tokens)
            {
                await _cache.RemoveAsync($"RefreshToken:{token}");
            }

            // Remove the user tokens list
            await _cache.RemoveAsync(userTokensKey);
        }
    }

    public class RefreshTokenInfo
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRevoked { get; set; }
    }

    public class UserRefreshTokens
    {
        public string UserId { get; set; }
        public List<string> Tokens { get; set; } = new();
    }
}
