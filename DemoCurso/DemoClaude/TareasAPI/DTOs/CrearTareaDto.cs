using System.ComponentModel.DataAnnotations;

namespace TareasAPI.DTOs;

public class CrearTareaDto
{
    [Required(ErrorMessage = "La descripción es obligatoria")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "La descripción debe tener entre 5 y 500 caracteres")]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha límite es obligatoria")]
    [FechaInicioAntesDeFechaLimite("FechaLimite")]
    public DateTime FechaLimite { get; set; }

    [Required(ErrorMessage = "La fecha de inicio es obligatoria")]

    public DateTime FechaInicio { get; set; }
}


// Crear un atributo de validación personalizado llamado FechaInicioAntesDeFechaLimiteAttribute
public class FechaInicioAntesDeFechaLimiteAttribute : ValidationAttribute
{
    private readonly string _fechaLimitePropertyName;
    public FechaInicioAntesDeFechaLimiteAttribute(string fechaLimitePropertyName)
    {
        _fechaLimitePropertyName = fechaLimitePropertyName;
        ErrorMessage = "La fecha de inicio debe ser anterior a la fecha límite";
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var fechaInicio = (DateTime?)value;
        var fechaLimiteProperty = validationContext.ObjectType.GetProperty(_fechaLimitePropertyName);
        if (fechaLimiteProperty == null)
        {
            return new ValidationResult($"No se encontró la propiedad {_fechaLimitePropertyName}");
        }
        var fechaLimite = (DateTime?)fechaLimiteProperty.GetValue(validationContext.ObjectInstance);
        if (fechaInicio is not null && fechaLimite is not null)
        {
            if (fechaInicio >= fechaLimite)
            {
                return new ValidationResult(ErrorMessage);
            }
        }
        return ValidationResult.Success;
    }
}