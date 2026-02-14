using GestionCursos.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionCursos.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Estudiante> Estudiantes { get; set; }
    public DbSet<Inscripcion> Inscripciones { get; set; }
}
