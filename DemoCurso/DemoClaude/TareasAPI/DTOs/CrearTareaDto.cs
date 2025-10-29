using System.ComponentModel.DataAnnotations;

namespace TareasAPI.DTOs;

public class CrearTareaDto
{
    [Required(ErrorMessage = "La descripción es obligatoria")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "La descripción debe tener entre 5 y 500 caracteres")]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha límite es obligatoria")]
    public DateTime FechaLimite { get; set; }
}
