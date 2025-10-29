using System.ComponentModel.DataAnnotations;

namespace DemoWeb.Models
{
    public class ContactViewModel
    {
        [Display(Name = "Nombre")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Introduce un correo válido")]
        [Display(Name = "Correo electrónico")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "La consulta es obligatoria")]
        [Display(Name = "Consulta")]
        [DataType(DataType.MultilineText)]
        public string? Message { get; set; }
    }
}
