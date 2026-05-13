using Application.UseCases;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;
using FluentValidation;
using Infrastructure.Repositories;
using WebApi.Models.Requests;
using WebApi.Models.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IAddOrderUseCase, AddOrderUseCase>();

builder.Services.AddTransient<IValidator<AddOrderRequest>, AddOrderRequestValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
