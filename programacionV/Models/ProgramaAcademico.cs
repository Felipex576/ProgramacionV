using System.Text.Json.Serialization;

namespace programacionV.Models;

public class ProgramaAcademico
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Codigo { get; set; }
    public string? Facultad { get; set; }

    // Relación 1 a muchos: Un programa académico tiene muchos estudiantes
    [JsonIgnore]
    public ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
}
