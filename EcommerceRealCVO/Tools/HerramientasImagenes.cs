using EcommerceRealCVO.Datos.Center;
using EcommerceRealCVO.Models;

namespace RealStateGestion.Tools
{
    public class HerramientasImagenes
    {
        //PROPIEDADES
        PropiedadesCenter _PropiedadesData = new PropiedadesCenter();

        //El método recibe un valor que para el caso de las propiedades es usado en los tableros, según el estatus de la propiedad. 
        
      //  public List<base64PropiedadesEcomm> ConversionB64Propiedades(string valor)
      //  {
            //var oListaImagenesPropiedad = _PropiedadesData.ListarImagenesProp(valor);
            //// Convercion de la Imagenes en Base64
            //var oListaBase64 = new List<base64PropiedadesEcomm>();

            //foreach (var datos in oListaImagenesPropiedad)
            //{
            //    var path = System.IO.Directory.GetCurrentDirectory() + "\\..\\RealStateGestion\\ImagenesPropiedad\\" + datos.RutaImagenPropiedad.ToString();

            //    byte[] imageArray = System.IO.File.ReadAllBytes(path);
            //    string base64Image = Convert.ToBase64String(imageArray);
            //    string src64 = "data:image/jpg/jpeg/png;base64," + base64Image; // parte que se agregara en src=

            //    //txt64.Insert(i, src64); // se codigo base64 en la 

            //    oListaBase64.Add(new base64PropiedadesEcomm()
            //    {
            //        IDpropiedadBase64 = Convert.ToInt32(datos.IDpropiedad),
            //        RutaImagen = datos.RutaImagenPropiedad.ToString(),
            //        txtBase64 = src64.ToString(),
            //    });
            //}

            //return oListaBase64;
        //}

    }
}
