using FluentAssertions;
using GestionCursos.Application.Exceptions;
using GestionCursos.Application.UseCases;
using GestionCursos.Core.Entities;
using GestionCursos.Core.Interfaces;
using NSubstitute;

namespace GestionCursos.Tests;

public class InscribirEstudianteUseCaseTests
{
    [Fact]
    public async Task Inscribir_DeberiaLanzarExcepcion_CuandoCursoEstaLeno()
    {
        // Arrange
        var repositorio = Substitute.For<IRepositorioCursos>();
        var useCase = new InscribirEstudianteUseCase(repositorio);
        
        var cursoId = 1;
        var estudianteId = 100;
        
        // Curso con capacidad 2 y 2 inscripciones
        var curso = new Curso(cursoId, "Matemáticas", 2)
        {
            Inscripciones = new List<Inscripcion>
            {
                new(1, cursoId, 101, DateTime.Now),
                new(2, cursoId, 102, DateTime.Now)
            }
        };

        repositorio.ObtenerPorIdAsync(cursoId).Returns(curso);

        // Act
        var act = async () => await useCase.EjecutarAsync(cursoId, estudianteId);

        // Assert
        await act.Should().ThrowAsync<CursoLlenoException>()
            .WithMessage("El curso ha alcanzado su capacidad máxima.");
    }
}
