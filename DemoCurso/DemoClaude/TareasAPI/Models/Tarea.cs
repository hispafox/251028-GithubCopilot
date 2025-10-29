namespace TareasAPI.Models;

public class Tarea
{
    public int Id { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaLimite { get; set; }
    public bool Completada { get; set; } = false;
    public DateTime FechaInicio { get; set; } = DateTime.UtcNow;
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}
