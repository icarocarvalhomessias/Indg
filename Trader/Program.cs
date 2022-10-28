using Microsoft.EntityFrameworkCore;

using Trader.Api.DataAcess;
using Trader.Api.Middleware;
using Trader.Api.Repositories.Interfaces;
using Trader.Api.Repositories.Repositories;
using Trader.Api.Service.Interfaces;
using Trader.Api.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApiDbContext>(opt => opt.UseSqlServer(connection));
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemTransferRepository, ItemTransferRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IItemTransferService, ItemTransferService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
  
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
