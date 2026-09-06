using Microsoft.AspNetCore.Mvc;
using programacionV.Models;
using programacionV.Repositories;

namespace programacionV.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgramaAcademicoController : ControllerBase
{
    private readonly IProgramaAcademicoRepository _repository;

    public ProgramaAcademicoController(IProgramaAcademicoRepository repository)
    {
        _repository = repository;
    }

    // GET: api/ProgramaAcademico
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProgramaAcademico>>> GetProgramas([FromQuery] bool includeEstudiantes = false)
    {
        var programas = await _repository.GetAllAsync(includeEstudiantes);
        return Ok(programas);
    }

    // GET: api/ProgramaAcademico/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProgramaAcademico>> GetPrograma(int id)
    {
        var programa = await _repository.GetByIdAsync(id);

        if (programa == null)
        {
            return NotFound(new { mensaje = $"Programa académico con ID {id} no encontrado." });
        }

        return Ok(programa);
    }

    // POST: api/ProgramaAcademico
    [HttpPost]
    public async Task<ActionResult<ProgramaAcademico>> CreatePrograma(ProgramaAcademico programa)
    {
        var nuevoPrograma = await _repository.AddAsync(programa);
        return CreatedAtAction(nameof(GetPrograma), new { id = nuevoPrograma.Id }, nuevoPrograma);
    }

    // PUT: api/ProgramaAcademico/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePrograma(int id, ProgramaAcademico programa)
    {
        if (id != programa.Id)
        {
            return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del cuerpo de la petición." });
        }

        var existe = await _repository.ExistsAsync(id);
        if (!existe)
        {
            return NotFound(new { mensaje = $"Programa académico con ID {id} no encontrado." });
        }

        await _repository.UpdateAsync(programa);
        return NoContent();
    }

    // DELETE: api/ProgramaAcademico/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePrograma(int id)
    {
        var existe = await _repository.ExistsAsync(id);
        if (!existe)
        {
            return NotFound(new { mensaje = $"Programa académico con ID {id} no encontrado." });
        }

        var tieneEstudiantes = await _repository.HasEstudiantesAsync(id);
        if (tieneEstudiantes)
        {
            return BadRequest(new { mensaje = "No se puede eliminar el programa académico porque tiene estudiantes asociados." });
        }

        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
