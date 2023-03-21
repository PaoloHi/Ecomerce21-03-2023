using System.ComponentModel.DataAnnotations;

namespace EcommerceRealCVO.Models
{
    public class OlvidoPasswordVModel
    {
        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }

    }
}
