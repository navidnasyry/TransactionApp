using Nest;
using TransactionAppProject.Classes;
using TransactionAppProject.ApplicationExceptions;
using TransactionAppProject.Configs;
using TransactionAppProject.Interfaces;
using TransactionAppProject.Models;



// Start WebApplication Setup
var builder = WebApplication.CreateBuilder(args);
Console.WriteLine(builder.Configuration);
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDependencyGroup();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();