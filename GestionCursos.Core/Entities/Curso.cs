namespace GestionCursos.Core.Entities;

public record Curso(int Id, string Nombre, int CapacidadMaxima)
{
    public IReadOnlyCollection<Inscripcion> Inscripciones { get; init; } = [];
}
