using adminRummet.Datos.Center;
using adminRummet.Models;



namespace adminRummet.Tools
{
    public class HerramientasImagenes
    {
        //PROPIEDADES
        PropiedadesCenter _PropiedadesData = new PropiedadesCenter();

        public List<base64PropiedadesEcomm> ConversionB64Propiedades(string valor)
        {
            var oListaImagenesPropiedad = _PropiedadesData.ListarImagenesProp(valor);
            // Convercion de la Imagenes en Base64
            var oListaBase64 = new List<base64PropiedadesEcomm>();

            foreach (var datos in oListaImagenesPropiedad)
            {
                var path = System.IO.Directory.GetCurrentDirectory() + "\\..\\adminRummet\\ImagenesPropiedad\\" + datos.RutaImagenPropiedad.ToString();

                byte[] imageArray = System.IO.File.ReadAllBytes(path);
                string base64Image = Convert.ToBase64String(imageArray);
                string src64 = "data:image/jpg/jpeg/png;base64," + base64Image; // parte que se agregara en src=

                //txt64.Insert(i, src64); // se codigo base64 en la 

                oListaBase64.Add(new base64PropiedadesEcomm()
                {
                    IDpropiedadBase64 = Convert.ToInt32(datos.IDpropiedad),
                    RutaImagen = datos.RutaImagenPropiedad.ToString(),
                    txtBase64 = src64.ToString(),
                });
            }

            return oListaBase64;
        }


    }
}
