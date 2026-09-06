namespace programacionV.Models;

public class Estudiante
{
    public int Id { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string? Documento { get; set; }
    public string? Email { get; set; }

    // Clave foránea de la relación 1 a muchos
    public int ProgramaAcademicoId { get; set; }

    // Propiedad de navegación
    public ProgramaAcademico? ProgramaAcademico { get; set; }
}
