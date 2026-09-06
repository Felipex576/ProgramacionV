using Microsoft.EntityFrameworkCore;
using programacionV.Data;
using programacionV.Models;

namespace programacionV.Repositories;

public class EstudianteRepository : IEstudianteRepository
{
    private readonly AppDbContext _context;

    public EstudianteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Estudiante>> GetAllAsync(int? programaId = null)
    {
        IQueryable<Estudiante> query = _context.Estudiantes.Include(e => e.ProgramaAcademico);

        if (programaId.HasValue)
        {
            query = query.Where(e => e.ProgramaAcademicoId == programaId.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<Estudiante?> GetByIdAsync(int id)
    {
        return await _context.Estudiantes
            .Include(e => e.ProgramaAcademico)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Estudiante?> GetByTelefonoAsync(string telefono)
    {
        return await _context.Estudiantes
            .Include(e => e.ProgramaAcademico)
            .FirstOrDefaultAsync(e => e.Telefono == telefono);
    }

    public async Task<Estudiante> AddAsync(Estudiante estudiante)
    {
        estudiante.ProgramaAcademico = null; // Evitar que EF intente re-insertar o mutar la entidad navegación
        _context.Estudiantes.Add(estudiante);
        await _context.SaveChangesAsync();

        // Cargar los datos del programa académico para la respuesta
        await _context.Entry(estudiante).Reference(e => e.ProgramaAcademico).LoadAsync();
        return estudiante;
    }

    public async Task<bool> UpdateAsync(Estudiante estudiante)
    {
        estudiante.ProgramaAcademico = null;
        _context.Entry(estudiante).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ExistsAsync(estudiante.Id))
            {
                return false;
            }
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);
        if (estudiante == null)
        {
            return false;
        }

        _context.Estudiantes.Remove(estudiante);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Estudiantes.AnyAsync(e => e.Id == id);
    }
}
