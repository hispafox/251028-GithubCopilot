using System.ComponentModel.DataAnnotations;

namespace TareasAPI.DTOs;

public class ActualizarTareaDto 
{
    [StringLength(500, MinimumLength = 5, ErrorMessage = "La descripci�n debe tener entre 5 y 500 caracteres")]
    public string? Descripcion { get; set; }
    public DateTime? FechaLimite { get; set; }
    public bool? Completada { get; set; }
    public DateTime? FechaInicio { get; set; }
 
}

