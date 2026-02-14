using GestionCursos.Application.Exceptions;
using GestionCursos.Core.Entities;
using GestionCursos.Core.Interfaces;

namespace GestionCursos.Application.UseCases;

public class InscribirEstudianteUseCase(IRepositorioCursos repositorio)
{
    public async Task EjecutarAsync(int cursoId, int estudianteId)
    {
        var curso = await repositorio.ObtenerPorIdAsync(cursoId);
        if (curso is null)
        {
            throw new KeyNotFoundException($"Curso {cursoId} no encontrado.");
        }

        if (curso.Inscripciones.Count >= curso.CapacidadMaxima)
        {
            throw new CursoLlenoException();
        }

        var inscripcion = new Inscripcion(0, cursoId, estudianteId, DateTime.Now);
        
        // Since Curso is an immutable record with a ReadOnlyCollection, 
        // we create a new list and use the 'with' expression to create a new version of the course.
        // However, IReadOnlyCollection doesn't have an easy "append" that returns a collection compatible with the property type unless we cast/toList.
        // We need to create a new list including the new item.
        var nuevasInscripciones = curso.Inscripciones.Append(inscripcion).ToList();
        
        var cursoActualizado = curso with { Inscripciones = nuevasInscripciones };

        await repositorio.ActualizarAsync(cursoActualizado);
    }
}
