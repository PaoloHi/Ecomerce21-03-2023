using Microsoft.AspNetCore.Http;
namespace RealStateGestion.Models
{
    public class UsuariosModel
    {
        public int? IDusuario { get; set; }
        public int? IDrol { get; set; }
        public string? nombreUsuario { get; set; }
        public string? apellidoP { get; set; }
        public string? apellidoM { get; set; }
        public string? email { get; set; }
        public string? tel { get; set; }
        public string? status { get; set; }
        public string? rfc { get; set; }
        public string? curp { get; set; }

        public string? cel { get; set; }

        public string? fechaAlta { get; set; }
        //Listas de llenado de datos 

        /*
        public List<UsuariosModel>? datosG { get; set; }
        public List<UsuariosModel>? datosB { get; set; }
        public List<UsuariosModel>? datosA { get; set; }*/

        public string? clabe { get; set; }
        public int? banco { get; set; }

        public string? rolUsuario { get; set; }

        //Archivos

        //Variables para obtener archivos
        public IFormFile? imgPerfil { get; set; }
        public IFormFile? archivo1 { get; set; }
        public IFormFile? archivo2 { get; set; }
        public IFormFile? archivo3 { get; set; }
        public IFormFile? archivo4 { get; set; }

        /*******************************************************/
        /* VARIABLES PARA EL CONTENIDO DE LOS ARCHIVOS */
        /*******************************************************/

        //Nombre de los archivos
        public string? imgPerfilNombre { get; set; }

        //ruta de los archivos
        public string? imgPerfilRuta { get; set; }

        //Extensión
        public string? imgPerfiExt { get; set; }

        //Tamaño del archivo 
        public long? imgPerfiTam { get; set; }


    }
}
