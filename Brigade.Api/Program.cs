using Brigade.Application.User.Command.AuthUser.Validator;
using Brigade.Application.User.Command.RegisterUser.Validators;
using Brigade.Application.User.Services;
using Brigade.Domain.Repositories;
using Brigade.Domain.Services;
using Brigade.Infrastructure.Data;
using Brigade.Infrastructure.Repositories;
using Brigade.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BrigadeDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("Brigade.Infrastructure"))
);

#region Регистрация Unit of Work 

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#endregion

#region Регистрация репозиториев 

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ICompanyProfileRepository, CompanyProfileRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();

#endregion

#region Регистрация сервисов 

builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();
builder.Services.AddScoped<IJwtService, JwtService>();

#endregion

#region Регистрация Application Services 

builder.Services.AddScoped<UserRegistrationService>();
builder.Services.AddScoped<UserAuthService>();

#endregion

#region Регистрация FluentValidation валидаторов 

builder.Services.AddScoped<RegisterUserCommandValidator>();
builder.Services.AddScoped<AuthUserCommandValidator>();

#endregion

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(); 

var app = builder.Build();

if (app.Environment.IsDevelopment()) 
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();