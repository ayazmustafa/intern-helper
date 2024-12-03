using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StajTakipSistemi.Authentication;
using StajTakipSistemi.Authentication.PasswordHasher;
using StajTakipSistemi.Authentication.TokenGenerator;
using StajTakipSistemi.Behaviours;
using StajTakipSistemi.Database;
using StajTakipSistemi.Middleware;
using StajTakipSistemi.Repositories.HistoryRepository;
using StajTakipSistemi.Repositories.InternshipFormRepository;
using StajTakipSistemi.Repositories.RoleRepository;
using StajTakipSistemi.Repositories.UserRepository;
using StajTakipSistemi.Repositories.UserRoleRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=asd123123;Database=StajTakipSistemi"));
builder.Services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());
// Database

// JWT
var jwtSettings = new JwtSettings();
builder.Configuration.Bind(JwtSettings.Section, jwtSettings);

builder.Services.AddSingleton(Options.Create(jwtSettings));
builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

builder.Services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings.Secret)),
    });
builder.Services.AddAuthorization();
// JWT

// Metiatr
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblyContaining(typeof(Program));

    options.AddOpenBehavior(typeof(ValidationBehavior<,>));
    options.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
});
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));
//Metiatr


// Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IInternshipFormRepository, InternshipFormRepository>();
builder.Services.AddScoped<IHistoryRepository, HistoryRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
// Repository

// User Context
builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
// User Context


// exception handler
builder.Services.AddExceptionHandler<ExceptionToProblemDetailsHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddHttpContextAccessor();
// exception handler

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();


app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();


app.Run();