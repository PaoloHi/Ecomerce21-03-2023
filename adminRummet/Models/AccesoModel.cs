using System.ComponentModel.DataAnnotations;

namespace adminRummet.Models
{
    public class AccesoModel
    {
        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
