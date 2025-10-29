using System.ComponentModel.DataAnnotations;

namespace TareasAPI.DTOs;

public class ActualizarTareaDto : IValidatableObject
{
    [StringLength(500, MinimumLength = 5, ErrorMessage = "La descripción debe tener entre 5 y 500 caracteres")]
    public string? Descripcion { get; set; }
    public DateTime? FechaLimite { get; set; }
    public bool? Completada { get; set; }
    public DateTime? FechaInicio { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (FechaInicio.HasValue && FechaLimite.HasValue)
        {
            if (FechaInicio.Value >= FechaLimite.Value)
            {
                yield return new ValidationResult(
                    "La fecha de inicio debe ser anterior a la fecha límite",
                    new[] { nameof(FechaInicio), nameof(FechaLimite) });
            }
        }
    }
}

