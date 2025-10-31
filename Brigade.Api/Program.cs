using Brigade.Domain.Enums;
using Brigade.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BrigadeDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("Brigade.Infrastructure"))
);

var app = builder.Build();

app.Run();