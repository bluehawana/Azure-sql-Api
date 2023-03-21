using AutoApiProject.Data;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var keyVaultEndpoint = new Uri(builder.Configuration["VaultKey"]);
var secretClient = new SecretClient(keyVaultEndpoint, new DefaultAzureCredential());

KeyVaultSecret kvs = secretClient.GetSecret("autodemon");
builder.Services.AddDbContext<AdventureWorks2019Context>(o => o.UseSqlServer(kvs.Value));

//var secbuilder.Services.AddDbContext<AdventureWorks2019Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorks2019ContextConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();
//new commnet 
 
app.UseHttpsRedirection();

app.MapGet("api/person", async ([FromServices] AdventureWorks2019Context db) =>
{ return await db.Person.ToListAsync(); });

app.Run();
  