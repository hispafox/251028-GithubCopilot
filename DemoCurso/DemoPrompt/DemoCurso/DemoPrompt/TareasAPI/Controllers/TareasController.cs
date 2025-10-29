using Microsoft.AspNetCore.Mvc;
using TareasAPI.DTOs;
using TareasAPI.Models;
using TareasAPI.Repositories;

namespace TareasAPI.Controllers
{
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
                _logger.LogWarning("Tarea no encontrada. Id={Id}", id);
                return NotFound(new { mensaje = $"No se encontró la tarea con ID {id}" });
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

            if (tareaDto.FechaLimite < DateTime.UtcNow)
            {
                return BadRequest(new { mensaje = "La fecha límite no puede ser en el pasado" });
            }

            var tarea = new Tarea
            {
                Descripcion = tareaDto.Descripcion,
                FechaLimite = tareaDto.FechaLimite,
                Completada = false
            };

            var created = await _repository.CrearAsync(tarea);
            _logger.LogInformation("Tarea creada. Id={Id}", created.Id);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = created.Id }, created);
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

            var existing = await _repository.ObtenerPorIdAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("Intento de actualizar tarea no encontrada. Id={Id}", id);
                return NotFound(new { mensaje = $"No se encontró la tarea con ID {id}" });
            }

            if (tareaDto.FechaLimite.HasValue && tareaDto.FechaLimite.Value < DateTime.UtcNow)
            {
                return BadRequest(new { mensaje = "La fecha límite no puede ser en el pasado" });
            }

            // Solo actualizar campos no nulos
            existing.Descripcion = tareaDto.Descripcion ?? existing.Descripcion;
            existing.FechaLimite = tareaDto.FechaLimite ?? existing.FechaLimite;
            existing.Completada = tareaDto.Completada ?? existing.Completada;

            var updated = await _repository.ActualizarAsync(id, existing);
            _logger.LogInformation("Tarea actualizada. Id={Id}", id);
            return Ok(updated);
        }

        /// <summary>
        /// Marca una tarea como completada
        /// </summary>
        [HttpPatch("{id}/completar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Tarea>> MarcarComoCompletada(int id)
        {
            var existing = await _repository.ObtenerPorIdAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("Intento de marcar completada tarea no encontrada. Id={Id}", id);
                return NotFound(new { mensaje = $"No se encontró la tarea con ID {id}" });
            }

            existing.Completada = true;
            var updated = await _repository.ActualizarAsync(id, existing);
            _logger.LogInformation("Tarea marcada como completada. Id={Id}", id);
            return Ok(updated);
        }

        /// <summary>
        /// Elimina una tarea
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Eliminar(int id)
        {
            var deleted = await _repository.EliminarAsync(id);
            if (!deleted)
            {
                _logger.LogWarning("Intento de eliminar tarea no encontrada. Id={Id}", id);
                return NotFound(new { mensaje = $"No se encontró la tarea con ID {id}" });
            }

            _logger.LogInformation("Tarea eliminada. Id={Id}", id);
            return NoContent();
        }
    }
}
