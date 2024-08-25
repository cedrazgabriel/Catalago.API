using Catalago.API.Context;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Catalago.API.Midlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
options.JsonSerializerOptions
.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Configura o FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

if (mySqlConnection == null)
{
    throw new Exception("String de conexão não definida no appsettings.json");
}

Console.WriteLine(mySqlConnection);

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseMySql(mySqlConnection,
                 ServerVersion.AutoDetect(mySqlConnection)));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ValidationMiddleware>(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
