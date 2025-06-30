using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurEverCarePlatform.Application.Commons.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class AuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly JwtTokenGenerator _jwtTokenGenerator;
    private readonly UserService _userService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        JwtTokenGenerator jwtTokenGenerator,
        UserService userService,
        ILogger<AuthService> logger
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _userService = userService;
        _logger = logger;
    }

    public async Task<(bool Success, string Message, LoginResponseDto Response)> LoginAsync(
        string emailOrUserNameOrPhone,
        string password
    )
    {
        AppUser? user = null;
        
        if (emailOrUserNameOrPhone.Contains("@"))
        {
            user = await _userManager.FindByEmailAsync(emailOrUserNameOrPhone);
        }
        else if (emailOrUserNameOrPhone.All(c => char.IsDigit(c) || c == '+' || c == '-' || c == ' ' || c == '(' || c == ')'))
        {
            user = await _userManager.Users.Where(u => u.PhoneNumber == emailOrUserNameOrPhone).FirstOrDefaultAsync();
        }
        else
        {
            user = await _userManager.FindByNameAsync(emailOrUserNameOrPhone);
        }
        
        if (user == null)
        {
            return (false, "User does not exist", null);
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, true);

        if (signInResult.Succeeded)
        {
            // Update last login date
            await _userManager.UpdateAsync(user);

            // Generate tokens
            var (accessToken, expiresAt) = await _jwtTokenGenerator.GenerateAccessTokenAsync(user);
            var refreshToken = await _jwtTokenGenerator.GenerateRefreshTokenAsync(
                user.Id.ToString()
            );

            // Get user details
            var userDetails = await _userService.GetUserDetailsAsync(user.Id.ToString());

            var response = new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                User = new UserDto { Id = user.Id, Name = $"{user.Name}".Trim() },
            };

            return (true, "Login successful", response);
        }

        if (signInResult.IsLockedOut)
        {
            return (false, "Account is locked out", null);
        }

        return (false, "Invalid credentials", null);
    }

    public async Task<(bool Success, string Message)> RegisterAsync(RegisterRequestDto request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return (false, "User with this email already exists");
        }

        var user = new AppUser
        {
            UserName = request.Email,
            Email = request.Email,
            Name = request.Name,
            PhoneNumber = request.PhoneNumber,
            EmailConfirmed = false, // For simplicity; in production, implement email confirmation
        };

        var createResult = await _userManager.CreateAsync(user, request.Password);

        if (!createResult.Succeeded)
        {
            var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
            return (false, $"User creation failed: {errors}");
        }

        // Add user to role
        var roleResult = await _userManager.AddToRoleAsync(user, "Customer");

        if (!roleResult.Succeeded)
        {
            var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
            _logger.LogWarning("Role assignment failed: {Errors}", errors);
        }

        // Generate tokens
        var (accessToken, expiresAt) = await _jwtTokenGenerator.GenerateAccessTokenAsync(user);
        var refreshToken = await _jwtTokenGenerator.GenerateRefreshTokenAsync(user.Id.ToString());

        return (true, "Registration successful");
    }

    public async Task<LoginResponseDto> RefreshTokenAsync(string refreshToken)
    {
        var (isValid, userId) = await _jwtTokenGenerator.ValidateRefreshTokenAsync(refreshToken);

        if (!isValid || string.IsNullOrEmpty(userId))
        {
            return null;
        }

        var user = await _userManager.FindByIdAsync(userId);

        // Generate new tokens
        var (accessToken, expiresAt) = await _jwtTokenGenerator.GenerateAccessTokenAsync(user);
        var newRefreshToken = await _jwtTokenGenerator.GenerateRefreshTokenAsync(
            user.Id.ToString()
        );

        var response = new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken,
            User = new UserDto { Id = user.Id, Name = $"{user.Name}".Trim() },
        };

        return null;
    }
}

public class LoginResponseDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public UserDto User { get; set; }
}

public class UserDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Avatar { get; set; }
}

public class RegisterRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
}
