using AutoMapper;
using EscNet.IoC.Cryptography;
using Localize.API.Token;
using Localize.API.ViewModels;
using Localize.Application.DTO;
using Localize.Application.Interfaces;
using Localize.Application.Services;
using Localize.Domain.Entities;
using Localize.Infra.Context;
using Localize.Infra.Interfaces;
using Localize.Infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API",
        Version = "v1",
        Description = "API teste técnico Localize",
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "utilize Bearer <TOKEN>",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
});

// Add services to the container.
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();

builder.Services.AddRijndaelCryptography(builder.Configuration["Cryptography:Key"]);
builder.Services.AddDbContext<LocalizeContext>(p => p.UseSqlServer(builder.Configuration.GetConnectionString("Localize")));

#region Jwt

var secretKey = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

#endregion

var autoMapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Usuario, UsuarioDTO>().ReverseMap();
    cfg.CreateMap<CriarUsuarioViewModel, UsuarioDTO>().ReverseMap();
    cfg.CreateMap<AtualizarUsuarioViewModel, UsuarioDTO>().ReverseMap();
});

builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

var app = builder.Build();

// Configure the HTTP request pipeline.
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
