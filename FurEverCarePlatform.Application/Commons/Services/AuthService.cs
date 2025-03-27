using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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

        public async Task<(string AccessToken, string RefreshToken, AppUserDto User)> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);
            if (user == null)
                user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == username);

            if (user != null)
            {
                var passwordValid = await _userManager.CheckPasswordAsync(user, password);
                if (passwordValid)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(
                            ClaimTypes.Role,
                            (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "Store Owner"
                        ),
                    };

                    // Tạo access token
                    var accessKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])
                    );
                    var accessCreds = new SigningCredentials(
                        accessKey,
                        SecurityAlgorithms.HmacSha256
                    );

                    var accessToken = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(45),
                        signingCredentials: accessCreds
                    );

                    var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);

                    // Tạo refresh token
                    var refreshTokenString = GenerateRefreshTokenAsJwt(user.Id);

                    // Tạo AppUserDto
                    var userDto = new AppUserDto
                    {
                        Id = user.Id,
                        Name = user.Name,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email
                    };

                    return (accessTokenString, refreshTokenString, userDto);
                }
            }
            return (null, null, null); // Đăng nhập thất bại
        }

        public async Task<(bool Succeeded, string AccessToken, string RefreshToken, AppUserDto User, string[] Errors)> RegisterAsync(RegisterModel model)
        {
            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                Name = model.Name,
                UserName = model.Name,
                PhoneNumber = model.Phone,
                CreationDate = DateTime.UtcNow,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Store Owner");

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Store Owner"),
                };

                var accessKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])
                );
                var accessCreds = new SigningCredentials(accessKey, SecurityAlgorithms.HmacSha256);

                var accessToken = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(15),
                    signingCredentials: accessCreds
                );

                var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);

                var refreshTokenString = GenerateRefreshTokenAsJwt(user.Id);

                var userDto = new AppUserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email
                };

                return (true, accessTokenString, refreshTokenString, userDto, null);
            }

            return (false, null, null, null, result.Errors.Select(e => e.Description).ToArray());
        }

        private string GenerateRefreshTokenAsJwt(Guid userId)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim("type", "refresh")
            };

            var refreshKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:RefreshSecretKey"])
            );
            var refreshCreds = new SigningCredentials(refreshKey, SecurityAlgorithms.HmacSha256);

            var refreshToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: refreshCreds
            );

            return new JwtSecurityTokenHandler().WriteToken(refreshToken);
        }

        public async Task<(string AccessToken, string RefreshToken, AppUserDto User)> RefreshTokenAsync(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var refreshKey = Encoding.UTF8.GetBytes(_configuration["Jwt:RefreshSecretKey"]);

            try
            {
                var principal = tokenHandler.ValidateToken(
                    refreshToken,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(refreshKey),
                        ValidateIssuer = true,
                        ValidIssuer = _configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = _configuration["Jwt:Audience"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    },
                    out SecurityToken validatedToken
                );

                if (principal.FindFirst("type")?.Value != "refresh")
                    return (null, null, null);

                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                    return (null, null, null);

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return (null, null, null);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(
                        ClaimTypes.Role,
                        (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "User"
                    ),
                };

                var accessKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])
                );
                var creds = new SigningCredentials(accessKey, SecurityAlgorithms.HmacSha256);

                var newAccessToken = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(15),
                    signingCredentials: creds
                );

                var newAccessTokenString = new JwtSecurityTokenHandler().WriteToken(newAccessToken);

                var newRefreshTokenString = GenerateRefreshTokenAsJwt(user.Id);

                var userDto = new AppUserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email
                };

                return (newAccessTokenString, newRefreshTokenString, userDto);
            }
            catch
            {
                return (null, null, null);
            }
        }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(
            100,
            MinimumLength = 6,
            ErrorMessage = "Password must be at least 6 characters long"
        )]
        public string Password { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
    }

    public class LoginModel
    {
        public string EmailorPhone { get; set; }
        public string Password { get; set; }
    }
}
