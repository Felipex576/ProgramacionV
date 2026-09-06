using Microsoft.AspNetCore.Mvc;
using programacionV.Models;
using programacionV.Repositories;

namespace programacionV.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudianteController : ControllerBase
{
    private readonly IEstudianteRepository _estudianteRepository;
    private readonly IProgramaAcademicoRepository _programaRepository;

    public EstudianteController(
        IEstudianteRepository estudianteRepository,
        IProgramaAcademicoRepository programaRepository)
    {
        _estudianteRepository = estudianteRepository;
        _programaRepository = programaRepository;
    }

    // GET: api/Estudiante
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Estudiante>>> GetEstudiantes([FromQuery] int? programaId = null)
    {
        var estudiantes = await _estudianteRepository.GetAllAsync(programaId);
        return Ok(estudiantes);
    }

    // GET: api/Estudiante/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Estudiante>> GetEstudiante(int id)
    {
        var estudiante = await _estudianteRepository.GetByIdAsync(id);

        if (estudiante == null)
        {
            return NotFound(new { mensaje = $"Estudiante con ID {id} no encontrado." });
        }

        return Ok(estudiante);
    }

    // GET: api/Estudiante/telefono/3001234567
    [HttpGet("telefono/{telefono}")]
    public async Task<ActionResult<Estudiante>> GetEstudianteByTelefono(string telefono)
    {
        var estudiante = await _estudianteRepository.GetByTelefonoAsync(telefono);

        if (estudiante == null)
        {
            return NotFound(new { mensaje = $"Estudiante con teléfono '{telefono}' no encontrado." });
        }

        return Ok(estudiante);
    }

    // POST: api/Estudiante
    [HttpPost]
    public async Task<ActionResult<Estudiante>> CreateEstudiante(Estudiante estudiante)
    {
        // Validar que el programa académico exista
        var programaExiste = await _programaRepository.ExistsAsync(estudiante.ProgramaAcademicoId);
        if (!programaExiste)
        {
            return BadRequest(new { mensaje = $"El programa académico con ID {estudiante.ProgramaAcademicoId} no existe." });
        }

        var nuevoEstudiante = await _estudianteRepository.AddAsync(estudiante);
        return CreatedAtAction(nameof(GetEstudiante), new { id = nuevoEstudiante.Id }, nuevoEstudiante);
    }

    // PUT: api/Estudiante/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEstudiante(int id, Estudiante estudiante)
    {
        if (id != estudiante.Id)
        {
            return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del cuerpo de la petición." });
        }

        var existe = await _estudianteRepository.ExistsAsync(id);
        if (!existe)
        {
            return NotFound(new { mensaje = $"Estudiante con ID {id} no encontrado." });
        }

        var programaExiste = await _programaRepository.ExistsAsync(estudiante.ProgramaAcademicoId);
        if (!programaExiste)
        {
            return BadRequest(new { mensaje = $"El programa académico con ID {estudiante.ProgramaAcademicoId} no existe." });
        }

        await _estudianteRepository.UpdateAsync(estudiante);
        return NoContent();
    }

    // DELETE: api/Estudiante/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEstudiante(int id)
    {
        var existe = await _estudianteRepository.ExistsAsync(id);
        if (!existe)
        {
            return NotFound(new { mensaje = $"Estudiante con ID {id} no encontrado." });
        }

        await _estudianteRepository.DeleteAsync(id);
        return NoContent();
    }
}
