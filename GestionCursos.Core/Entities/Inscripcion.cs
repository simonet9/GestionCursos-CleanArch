namespace GestionCursos.Core.Entities;

public record Inscripcion(int Id, int CursoId, int EstudianteId, DateTime FechaInscripcion);
