using GestionCursos.Application.Exceptions;
using GestionCursos.Application.UseCases;
using GestionCursos.Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Clean Architecture: Infrastructure Layer
builder.Services.AddInfrastructure("Data Source=cursos.db");

// Clean Architecture: Application Layer
builder.Services.AddScoped<InscribirEstudianteUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Endpoint: POST /inscripciones
app.MapPost("/inscripciones", async ([FromBody] InscripcionRequest request, [FromServices] InscribirEstudianteUseCase useCase) =>
{
    try
    {
        await useCase.EjecutarAsync(request.CursoId, request.EstudianteId);
        return Results.Ok();
    }
    catch (CursoLlenoException ex)
    {
        return Results.BadRequest(ex.Message);
    }
    catch (KeyNotFoundException ex)
    {
        return Results.NotFound(ex.Message);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
})
.WithName("InscribirEstudiante");

app.Run();

public record InscripcionRequest(int EstudianteId, int CursoId);
