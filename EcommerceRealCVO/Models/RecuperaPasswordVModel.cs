using System.ComponentModel.DataAnnotations;

namespace EcommerceRealCVO.Models
{
    public class RecuperaPasswordVModel
    {
        /*[Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }*/

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        public string Code { get; set; }
        public string UserId{ get; set; }

        public string ConfirmPassword { get; set; }
    }
}
