using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Commons.Services
{
    public class AuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<(string AccessToken, string RefreshToken)> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);
            Console.WriteLine($"User found: {user?.UserName}"); // Debug
            if (user != null )
            {
                var passwordValid = await _userManager.CheckPasswordAsync(user, password);
                Console.WriteLine($"Password valid: {passwordValid}");
                if (passwordValid)
                {
                    var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "PET OWNER")
                };

                    // Tạo access token
                    var accessKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
                    var accessCreds = new SigningCredentials(accessKey, SecurityAlgorithms.HmacSha256);

                    var accessToken = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(45), // Access token hết hạn sau 15 phút
                        signingCredentials: accessCreds);

                    var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);

                    // Tạo refresh token
                    var refreshTokenString = GenerateRefreshTokenAsJwt(user.Id);

                    return (accessTokenString, refreshTokenString);
                }
            }
            return (null, null); // Đăng nhập thất bại
        }

        public async Task<(bool Succeeded, string AccessToken, string RefreshToken, string[] Errors)> RegisterAsync(RegisterModel model)
        {
            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                UserName = model.Username,
                Email = model.Email,
                Name = model.Name,
                CreationDate = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Gán vai trò mặc định
                await _userManager.AddToRoleAsync(user, "PET OWNER");

                // Tạo access token
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, "PET OWNER")
                };

                var accessKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
                var accessCreds = new SigningCredentials(accessKey, SecurityAlgorithms.HmacSha256);

                var accessToken = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(15),
                    signingCredentials: accessCreds);

                var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);

                // Tạo refresh token
                var refreshTokenString = GenerateRefreshTokenAsJwt(user.Id);

                return (true, accessTokenString, refreshTokenString, null);
            }

            return (false, null, null, result.Errors.Select(e => e.Description).ToArray());
        }

        private string GenerateRefreshTokenAsJwt(Guid userId)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim("type", "refresh") // Đánh dấu đây là refresh token
            };

            var refreshKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:RefreshSecretKey"]));
            var refreshCreds = new SigningCredentials(refreshKey, SecurityAlgorithms.HmacSha256);

            var refreshToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7), // Refresh token hết hạn sau 7 ngày
                signingCredentials: refreshCreds);

            return new JwtSecurityTokenHandler().WriteToken(refreshToken);
        }

        public async Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var refreshKey = Encoding.UTF8.GetBytes(_configuration["Jwt:RefreshSecretKey"]);

            try
            {
                // Xác thực refresh token
                var principal = tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(refreshKey),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // Không cho phép lệch thời gian
                }, out SecurityToken validatedToken);

                // Kiểm tra loại token
                if (principal.FindFirst("type")?.Value != "refresh")
                    return (null, null);

                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                    return (null, null);

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return (null, null);

                // Tạo access token mới
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "User")
                };

                var accessKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
                var creds = new SigningCredentials(accessKey, SecurityAlgorithms.HmacSha256);

                var newAccessToken = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(15),
                    signingCredentials: creds);

                var newAccessTokenString = new JwtSecurityTokenHandler().WriteToken(newAccessToken);

                // Tạo refresh token mới (tùy chọn để tăng bảo mật)
                var newRefreshTokenString = GenerateRefreshTokenAsJwt(user.Id);

                return (newAccessTokenString, newRefreshTokenString);
            }
            catch
            {
                return (null, null); // Refresh token không hợp lệ
            }
        }
    }
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
