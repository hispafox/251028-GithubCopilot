using TareasAPI.Models;

namespace TareasAPI.Repositories
{
    public class TareaRepository : ITareaRepository
    {
        private readonly List<Tarea> _tareas = new();
        private int _nextId = 1;

        public TareaRepository()
        {
            var now = DateTime.UtcNow;
            _tareas.Add(new Tarea
            {
                Id = _nextId++,
                Descripcion = "Completar documentación del proyecto",
                FechaLimite = now.AddDays(5),
                Completada = false,
                FechaCreacion = now
            });
            _tareas.Add(new Tarea
            {
                Id = _nextId++,
                Descripcion = "Revisar código del equipo",
                FechaLimite = now.AddDays(2),
                Completada = false,
                FechaCreacion = now
            });
            _tareas.Add(new Tarea
            {
                Id = _nextId++,
                Descripcion = "Preparar presentación para cliente",
                FechaLimite = now.AddDays(7),
                Completada = true,
                FechaCreacion = now
            });
        }

        public Task<Tarea> CrearAsync(Tarea tarea)
        {
            tarea.Id = _nextId++;
            tarea.FechaCreacion = DateTime.UtcNow;
            _tareas.Add(tarea);
            return Task.FromResult(tarea);
        }

        public Task<bool> EliminarAsync(int id)
        {
            var tarea = _tareas.FirstOrDefault(t => t.Id == id);
            if (tarea == null) return Task.FromResult(false);
            _tareas.Remove(tarea);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<Tarea>> ObtenerTodasAsync()
        {
            return Task.FromResult<IEnumerable<Tarea>>(_tareas);
        }

        public Task<Tarea?> ObtenerPorIdAsync(int id)
        {
            var tarea = _tareas.FirstOrDefault(t => t.Id == id);
            return Task.FromResult<Tarea?>(tarea);
        }

        public Task<IEnumerable<Tarea>> ObtenerPorEstadoAsync(bool completada)
        {
            var result = _tareas.Where(t => t.Completada == completada);
            return Task.FromResult<IEnumerable<Tarea>>(result);
        }

        public Task<Tarea?> ActualizarAsync(int id, Tarea tarea)
        {
            var existing = _tareas.FirstOrDefault(t => t.Id == id);
            if (existing == null) return Task.FromResult<Tarea?>(null);

            // Update fields
            existing.Descripcion = tarea.Descripcion;
            existing.FechaLimite = tarea.FechaLimite;
            existing.Completada = tarea.Completada;

            return Task.FromResult<Tarea?>(existing);
        }
    }
}
