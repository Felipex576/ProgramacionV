using programacionV.Models;

namespace programacionV.Repositories;

public interface IEstudianteRepository
{
    Task<IEnumerable<Estudiante>> GetAllAsync(int? programaId = null);
    Task<Estudiante?> GetByIdAsync(int id);
    Task<Estudiante?> GetByTelefonoAsync(string telefono);
    Task<Estudiante> AddAsync(Estudiante estudiante);
    Task<bool> UpdateAsync(Estudiante estudiante);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
