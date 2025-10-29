using System.ComponentModel.DataAnnotations;

namespace TareasAPI.Models
{
    public class Tarea
    {
        public int Id { get; set; }

        [Required]
        public string Descripcion { get; set; } = null!;

        [Required]
        public DateTime FechaLimite { get; set; }

        public bool Completada { get; set; } = false;

        public DateTime FechaCreacion { get; set; }
    }
}
