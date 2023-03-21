using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EcommerceRealCVO.Models
{

    public class PlanesModel
    {
        public int? IDplanEcomm { get; set; }
        public string? nombrePlan { get; set; }
        public string? precioTotal { get; set; }

        public List<PlanesModelG>? planesG { get; set; }
        public List<PlanesModelCaract>? planesCaract { get; set; }

        public string? nombreLeadBroker { get; set; }
        public string? apellidoPaterno { get; set; }
        public string? apellidoMaterno { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress]
        public string? email { get; set; }
        public string? telefono { get; set; }

        //Variables para el usuario en quiero saber más 

        public string? NombreQS { get; set; }
        public string? APaternoQS { get; set; }
        public string? AMaternoQS { get; set; }
        public string? TelefonoQS { get; set; }

        [EmailAddress]
        public string? EmailQS { get; set; }
        public string? MensajeQS { get; set; }
        public DateTime? FechaContactoQS { get; set; }



    }
    public class PlanesModelG : IdentityUser //Extensión de clase para Indentity
    {
        public int IDplanEcomm { get; set; }
        public string nombrePlan { get; set; }
        public string precioTotal { get; set; }
        public int propiedades { get; set; }

    }

    public class PlanesModelCaract
    {
        public int IDcatPlan { get; set; }
        public int IDPlan { get; set; }
        public string caractPlanEcomm { get; set; }

    }


}
