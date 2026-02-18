using FluentValidation.AspNetCore;
using MasPatas.API.Middleware;
using MasPatas.Application.DependencyInjection;
using MasPatas.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Register all application and infrastructure services.
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services
    .AddControllers()
    .AddFluentValidation(fv =>
    {
        // Enables FluentValidation to run automatically for DTOs.
        fv.AutomaticValidationEnabled = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global error handling for consistent API responses.
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
