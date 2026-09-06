using Microsoft.EntityFrameworkCore;
using programacionV.Data;
using programacionV.Models;

namespace programacionV.Repositories;

public class ProgramaAcademicoRepository : IProgramaAcademicoRepository
{
    private readonly AppDbContext _context;

    public ProgramaAcademicoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProgramaAcademico>> GetAllAsync(bool includeEstudiantes = false)
    {
        if (includeEstudiantes)
        {
            return await _context.ProgramasAcademicos
                .Include(p => p.Estudiantes)
                .ToListAsync();
        }

        return await _context.ProgramasAcademicos.ToListAsync();
    }

    public async Task<ProgramaAcademico?> GetByIdAsync(int id, bool includeEstudiantes = true)
    {
        if (includeEstudiantes)
        {
            return await _context.ProgramasAcademicos
                .Include(p => p.Estudiantes)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        return await _context.ProgramasAcademicos.FindAsync(id);
    }

    public async Task<ProgramaAcademico> AddAsync(ProgramaAcademico programa)
    {
        _context.ProgramasAcademicos.Add(programa);
        await _context.SaveChangesAsync();
        return programa;
    }

    public async Task<bool> UpdateAsync(ProgramaAcademico programa)
    {
        _context.Entry(programa).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ExistsAsync(programa.Id))
            {
                return false;
            }
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var programa = await _context.ProgramasAcademicos.FindAsync(id);
        if (programa == null)
        {
            return false;
        }

        _context.ProgramasAcademicos.Remove(programa);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.ProgramasAcademicos.AnyAsync(p => p.Id == id);
    }

    public async Task<bool> HasEstudiantesAsync(int id)
    {
        return await _context.Estudiantes.AnyAsync(e => e.ProgramaAcademicoId == id);
    }
}
