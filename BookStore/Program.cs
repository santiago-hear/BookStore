using BookStore.Model.Interfaces;
using BookStore.Model.Repositories;
using BookStore.Model.Repositories.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//var OracleConnection = builder.Configuration.GetConnectionString("OracleConnection");
//builder.Services.AddDbContext<BookStoreOracleContext>(options => options.UseOracle(OracleConnection));
var SQLServerConnection = builder.Configuration.GetConnectionString("SqlServerConnection");
builder.Services.AddDbContext<BookStoreSqlServerContext>( options => options.UseSqlServer(SQLServerConnection));

builder.Services.AddTransient<IAutorRepository, AutorRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();

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
