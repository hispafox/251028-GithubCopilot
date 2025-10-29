using TareasAPI.Models;

namespace TareasAPI.Repositories;

public class TareaRepository : ITareaRepository
{
    private readonly List<Tarea> _tareas = new();
    private int _nextId = 1;

    public TareaRepository()
    {
        // Datos de ejemplo
        _tareas.Add(new Tarea
        {
            Id = _nextId++,
            Descripcion = "Completar documentación del proyecto",
            FechaInicio = DateTime.UtcNow,
            FechaLimite = DateTime.UtcNow.AddDays(5),
            Completada = false
        });

        _tareas.Add(new Tarea
        {
            Id = _nextId++,
            Descripcion = "Revisar código del equipo",
            FechaInicio = DateTime.UtcNow,
            FechaLimite = DateTime.UtcNow.AddDays(2),
            Completada = false
        });

        _tareas.Add(new Tarea
        {
            Id = _nextId++,
            Descripcion = "Preparar presentación para cliente",
            FechaInicio = DateTime.UtcNow,
            FechaLimite = DateTime.UtcNow.AddDays(7),
            Completada = true
        });
    }

    public Task<IEnumerable<Tarea>> ObtenerTodasAsync()
    {
        return Task.FromResult(_tareas.AsEnumerable());
    }

    public Task<Tarea?> ObtenerPorIdAsync(int id)
    {
        var tarea = _tareas.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(tarea);
    }

    public Task<Tarea> CrearAsync(Tarea tarea)
    {
        tarea.Id = _nextId++;
        tarea.FechaCreacion = DateTime.UtcNow;
        tarea.FechaInicio = tarea.FechaInicio == default ? DateTime.UtcNow : tarea.FechaInicio;
        _tareas.Add(tarea);
        return Task.FromResult(tarea);
    }

    public Task<Tarea?> ActualizarAsync(int id, Tarea tareaActualizada)
    {
        var tarea = _tareas.FirstOrDefault(t => t.Id == id);
        if (tarea == null)
            return Task.FromResult<Tarea?>(null);

        tarea.Descripcion = tareaActualizada.Descripcion;
        tarea.FechaLimite = tareaActualizada.FechaLimite;
        tarea.Completada = tareaActualizada.Completada;
        // Solo actualizar FechaInicio si se proporciona un valor válido
        if (tareaActualizada.FechaInicio != default)
        {
            tarea.FechaInicio = tareaActualizada.FechaInicio;
        }

        return Task.FromResult<Tarea?>(tarea);
    }

    public Task<bool> EliminarAsync(int id)
    {
        var tarea = _tareas.FirstOrDefault(t => t.Id == id);
        if (tarea == null)
            return Task.FromResult(false);

        _tareas.Remove(tarea);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<Tarea>> ObtenerPorEstadoAsync(bool completada)
    {
        var tareas = _tareas.Where(t => t.Completada == completada);
        return Task.FromResult(tareas.AsEnumerable());
    }
}
