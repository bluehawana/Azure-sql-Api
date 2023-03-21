using AutoApiProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AdventureWorks2019Context>(o => o.UseSqlServer(connString));

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();
//new commnet 

app.UseHttpsRedirection();

app.MapGet("api/person", async ([FromServices] AdventureWorks2019Context db) =>
{ return await db.Person.ToListAsync(); });

app.Run();
  