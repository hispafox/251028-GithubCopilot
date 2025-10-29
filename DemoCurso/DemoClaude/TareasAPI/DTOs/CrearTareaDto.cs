using System.ComponentModel.DataAnnotations;
using TareasAPI.Validation;

namespace TareasAPI.DTOs;

[StartBeforeEnd(nameof(FechaInicio), nameof(FechaLimite))]
public class CrearTareaDto
{
    [Required(ErrorMessage = "La descripci�n es obligatoria")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "La descripci�n debe tener entre 5 y 500 caracteres")]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha l�mite es obligatoria")]
    public DateTime? FechaLimite { get; set; }

    [Required(ErrorMessage = "La fecha de inicio es obligatoria")]

    public DateTime? FechaInicio { get; set; }
}


