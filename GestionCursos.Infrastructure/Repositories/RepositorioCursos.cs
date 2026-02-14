using GestionCursos.Core.Entities;
using GestionCursos.Core.Interfaces;
using GestionCursos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionCursos.Infrastructure.Repositories;

public class RepositorioCursos(AppDbContext context) : IRepositorioCursos
{
    public async Task<Curso?> ObtenerPorIdAsync(int id)
    {
        return await context.Cursos
            .Include(c => c.Inscripciones)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AgregarAsync(Curso curso)
    {
        await context.Cursos.AddAsync(curso);
        await context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Curso curso)
    {
        context.Cursos.Update(curso);
        await context.SaveChangesAsync();
    }
}
