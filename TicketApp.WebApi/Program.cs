using Microsoft.EntityFrameworkCore;
using TicketApp.Business.Services;
using TicketApp.Core.Entities;
using TicketApp.Core.Interfaces;
using TicketApp.DataAccess.Context;
using TicketApp.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Swagger nedir?
//Swagger, API'lerinizi belgelemek ve test etmek için kullanılan bir araçtır. API'nizin uç noktalarını, istek ve yanıt örneklerini görselleştirmenize ve test etmenize olanak tanır. Swagger, API'nizin kullanıcı dostu bir arayüzde belgelenmesini sağlar ve geliştiricilerin API'nizi daha kolay anlamasına yardımcı olur.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
