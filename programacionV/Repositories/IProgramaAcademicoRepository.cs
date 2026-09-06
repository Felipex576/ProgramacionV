using programacionV.Models;

namespace programacionV.Repositories;

public interface IProgramaAcademicoRepository
{
    Task<IEnumerable<ProgramaAcademico>> GetAllAsync(bool includeEstudiantes = false);
    Task<ProgramaAcademico?> GetByIdAsync(int id, bool includeEstudiantes = true);
    Task<ProgramaAcademico> AddAsync(ProgramaAcademico programa);
    Task<bool> UpdateAsync(ProgramaAcademico programa);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> HasEstudiantesAsync(int id);
}
