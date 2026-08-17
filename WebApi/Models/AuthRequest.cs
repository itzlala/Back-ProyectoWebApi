using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class AuthRequest
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Contrasenia { get; set; }
    }
}
