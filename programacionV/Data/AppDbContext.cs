using Microsoft.EntityFrameworkCore;
using programacionV.Models;

namespace programacionV.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ProgramaAcademico> ProgramasAcademicos => Set<ProgramaAcademico>();
    public DbSet<Estudiante> Estudiantes => Set<Estudiante>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración explícita de la relación 1 a muchos:
        // 1 ProgramaAcademico tiene muchos Estudiantes
        modelBuilder.Entity<ProgramaAcademico>()
            .HasMany(p => p.Estudiantes)
            .WithOne(e => e.ProgramaAcademico)
            .HasForeignKey(e => e.ProgramaAcademicoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
