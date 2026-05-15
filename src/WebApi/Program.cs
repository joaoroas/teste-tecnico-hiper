using Application.Models.Validations;
using Application.UseCases;
using Domain.Interfaces.Messaging;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;
using Domain.Models.Requests;
using FluentValidation;
using Infrastructure.DbContext;
using Infrastructure.Interfaces;
using Infrastructure.Messaging.Configuration;
using Infrastructure.Messaging.Producers;
using Infrastructure.Repositories;
using Infrastructure.Settings;
using System.Text.Json.Serialization;
using WebApi.Worker.Consumers;
using WebApi.Worker.Interfaces;
using WebApi.Worker.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHostedService<OrdersConsumer>();

builder.Services.AddSingleton<IDbContext, DbContext>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IOrderProducer, OrderProducer>();
builder.Services.AddScoped<IOrderConsumerService, OrderConsumerService>();


builder.Services.AddSingleton<IRabbitMqConfiguration, RabbitMqConfiguration>();

builder.Services.AddScoped<IAddOrderUseCase, AddOrderUseCase>();
builder.Services.AddScoped<IGetOrderUseCase, GetOrderUseCase>();
builder.Services.AddScoped<IUpdateOrderUseCase, UpdateOrderUseCase>();
builder.Services.AddScoped<IDeleteOrderUseCase, DeleteOrderUseCase>();

builder.Services.AddTransient<IValidator<AddOrderRequest>, AddOrderRequestValidator>();
builder.Services.AddTransient<IValidator<UpdateOrderRequest>, UpdateOrderRequestValidator>();

builder.Services.AddOptions<RabbitMqSettings>().BindConfiguration("RabbitMq");

builder.Services.AddControllers();

builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition
                       = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVue",
        policy => policy.WithOrigins("http://localhost:8080")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});


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

app.UseCors("AllowVue");

app.UseAuthorization();

app.MapControllers();

app.Run();
