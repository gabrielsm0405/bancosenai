using BancoSENAIAPI.Data; // Change
using Microsoft.EntityFrameworkCore; // Change
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Change
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)))); // Change

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Banco SENAI - Sistema Financeiro",
        Version = "v1",
        Description = "API Gest�o Financeira e Integra��o Clientes."
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        policy => policy.AllowAnyOrigin() // Permite a 'origin null' do seu arquivo local
                        .AllowAnyMethod() // Permite os verbos GET, POST, PUT, DELETE [2]
                        .AllowAnyHeader()); // Permite o envio de JSON no corpo da mensagem [3]
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PermitirTudo");

app.UseAuthorization();

app.MapControllers();

app.Run();
