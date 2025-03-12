using FurEverCarePlatform.Application.Services;
using FurEverCarePlatform.Domain.Entities;
using FurEverCarePlatform.Persistence.DatabaseContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Đảm bảo đọc file appsettings.json
IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


builder.Services.AddDbContext<PetDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PetDB")));

// Cấu hình Identity (giữ cookie-based authentication cho web)
builder.Services.AddIdentity<AppUser, AppRole>
//builder.Services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>
    (options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
})
.AddEntityFrameworkStores<PetDatabaseContext>()
.AddDefaultTokenProviders();

// Cấu hình JWT Authentication (cho API)
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secret = jwtSettings["SecretKey"];
if (string.IsNullOrEmpty(secret))
{
    throw new ArgumentException("JWT SecretKey is not configured in appsettings.json.");
}

var key = Encoding.ASCII.GetBytes(secret);

builder.Services.AddAuthentication(options =>
{
    // Giữ scheme mặc định cho Identity (cookie)
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    // Đặt JWT làm scheme mặc định cho API
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

// Cấu hình Authorization
builder.Services.AddAuthorization();

// Thêm các service khác
builder.Services.AddControllers();
builder.Services.AddScoped<JwtTokenService>();
// Cấu hình Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "FurEverCarePlatform API",
        Description = "API for FurEverCarePlatform with JWT Authentication"
    });

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter your JWT token in this field",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    c.AddSecurityDefinition("Bearer", jwtSecurityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    };

    c.AddSecurityRequirement(securityRequirement);
});

var app = builder.Build();

// Cấu hình pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();