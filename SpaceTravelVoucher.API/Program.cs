using Microsoft.EntityFrameworkCore;
using SpaceTravelVoucher.API.Authentication;
using SpaceTravelVoucher.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=TESTSPACE;Integrated Security=True;TrustServerCertificate=True;";

builder.Services.AddDbContext<TESTSPACEContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ApiKeyAuthMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
