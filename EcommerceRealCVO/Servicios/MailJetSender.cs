using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace EcommerceRealCVO.Servicios
{
    public class MailJetSender : IEmailSender //Extensión a propiedad de correo propia de .NET 6
    {
        //Creación de variables públicas y privadas para usar MailJet
        private readonly IConfiguration _configuration;
        public MailJetConfi _mailJetConfi;

        public MailJetSender(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //Remplazo de función 
            //throw new NotImplementedException();
            //Código de Email Jet 

            _mailJetConfi = _configuration.GetSection("MailJet").Get<MailJetConfi>();

            //Mandamos a llamar las variables del Modelo creado para MailJet a traves de la variable creada en la parte superior
            MailjetClient client = new MailjetClient(_mailJetConfi.ApiKey, _mailJetConfi.SecretKey)
            {
                Version = ApiVersion.V3_1,
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
             .Property(Send.Messages, new JArray {
     new JObject {
      {
       "From",
       new JObject {
        {"Email", "testecommercecvo@proton.me"},
        {"Name", "Test Ecommerce CVO "}
       }
      }, {
       "To",
       new JArray {
        new JObject {
         {
          "Email",
          //Cambio de variable a email -> Obtener el email del usuario 
          email
         }, {
          "Name",
          "Test Ecommerce CVO"
         }
        }
       }
      }, {
       "Subject",
      //Cambio al paramétro subject 
      subject
      }, {
       "HTMLPart",
       htmlMessage
      }
     }
             });
           /* MailjetResponse response =*/  await client.PostAsync(request);
            
            /* if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(response.GetData());
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }*/
        }
    }
}
    
