using GestionCursos.Core.Entities;

namespace GestionCursos.Core.Interfaces;

public interface IRepositorioCursos
{
    Task<Curso?> ObtenerPorIdAsync(int id);
    Task AgregarAsync(Curso curso);
    Task ActualizarAsync(Curso curso);
}
