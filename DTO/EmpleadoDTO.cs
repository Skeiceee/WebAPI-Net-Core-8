using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.DTO
{
    public class EmpleadoDTO
    {
        [Required(ErrorMessage = "Obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Obligatorio")]
        [MaxLength(4, ErrorMessage = "Máximo 4 digitos")]
        public string CodEmpleado { get; set; } = string.Empty;

        [Required(ErrorMessage = "Obligatorio")]
        [EmailAddress(ErrorMessage = "Formato incorrecto")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Obligatorio")]
        [Range(16,100, ErrorMessage = "La edad debe estar entre 16 a 100")]
        public int Edad { get; set; }
    }
}