using Microsoft.AspNetCore.Mvc;
using TareasAPI.DTOs;
using TareasAPI.Models;
using TareasAPI.Repositories;

namespace TareasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TareasController : ControllerBase
{
    private readonly ITareaRepository _repository;
    private readonly ILogger<TareasController> _logger;

    public TareasController(ITareaRepository repository, ILogger<TareasController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todas las tareas
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Tarea>>> ObtenerTodas()
    {
        var tareas = await _repository.ObtenerTodasAsync();
        return Ok(tareas);
    }

    /// <summary>
    /// Obtiene una tarea por su ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Tarea>> ObtenerPorId(int id)
    {
        var tarea = await _repository.ObtenerPorIdAsync(id);
        
        if (tarea == null)
        {
            _logger.LogWarning("Tarea con ID {Id} no encontrada", id);
            return NotFound(new { mensaje = $"No se encontr� la tarea con ID {id}" });
        }

        return Ok(tarea);
    }

    /// <summary>
    /// Obtiene tareas filtradas por estado
    /// </summary>
    [HttpGet("estado/{completada}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Tarea>>> ObtenerPorEstado(bool completada)
    {
        var tareas = await _repository.ObtenerPorEstadoAsync(completada);
        return Ok(tareas);
    }

    /// <summary>
    /// Crea una nueva tarea
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Tarea>> Crear([FromBody] CrearTareaDto tareaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Validar que la fecha l�mite no sea en el pasado
        if (tareaDto.FechaLimite < DateTime.UtcNow)
        {
            return BadRequest(new { mensaje = "La fecha l�mite no puede ser en el pasado" });
        }

        var tarea = new Tarea
        {
            Descripcion = tareaDto.Descripcion,
            FechaLimite = tareaDto.FechaLimite
        };

        var tareaCreada = await _repository.CrearAsync(tarea);
        _logger.LogInformation("Tarea creada con ID {Id}", tareaCreada.Id);

        return CreatedAtAction(nameof(ObtenerPorId), new { id = tareaCreada.Id }, tareaCreada);
    }

    /// <summary>
    /// Actualiza una tarea existente
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Tarea>> Actualizar(int id, [FromBody] ActualizarTareaDto tareaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var tareaExistente = await _repository.ObtenerPorIdAsync(id);
        if (tareaExistente == null)
        {
            return NotFound(new { mensaje = $"No se encontr� la tarea con ID {id}" });
        }

        // Actualizar solo los campos que se proporcionaron
        if (tareaDto.Descripcion != null)
            tareaExistente.Descripcion = tareaDto.Descripcion;

        if (tareaDto.FechaLimite.HasValue)
        {
            if (tareaDto.FechaLimite.Value < DateTime.UtcNow)
            {
                return BadRequest(new { mensaje = "La fecha l�mite no puede ser en el pasado" });
            }
            tareaExistente.FechaLimite = tareaDto.FechaLimite.Value;
        }

        if (tareaDto.Completada.HasValue)
            tareaExistente.Completada = tareaDto.Completada.Value;

        var tareaActualizada = await _repository.ActualizarAsync(id, tareaExistente);
        _logger.LogInformation("Tarea con ID {Id} actualizada", id);

        return Ok(tareaActualizada);
    }

    /// <summary>
    /// Elimina una tarea
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Eliminar(int id)
    {
        var eliminado = await _repository.EliminarAsync(id);
        
        if (!eliminado)
        {
            return NotFound(new { mensaje = $"No se encontr� la tarea con ID {id}" });
        }

        _logger.LogInformation("Tarea con ID {Id} eliminada", id);
        return NoContent();
    }

    /// <summary>
    /// Marca una tarea como completada
    /// </summary>
    [HttpPatch("{id}/completar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Tarea>> MarcarComoCompletada(int id)
    {
        var tarea = await _repository.ObtenerPorIdAsync(id);
        
        if (tarea == null)
        {
            return NotFound(new { mensaje = $"No se encontr� la tarea con ID {id}" });
        }

        tarea.Completada = true;
        var tareaActualizada = await _repository.ActualizarAsync(id, tarea);
        _logger.LogInformation("Tarea con ID {Id} marcada como completada", id);

        return Ok(tareaActualizada);
    }
}
