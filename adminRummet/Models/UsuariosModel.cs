namespace adminRummet.Models
{
    public class UsuariosModel
    {
        public int? IDusuario { get; set; }
        public int? IDrol { get; set; }

        //Lista de roles de usuario
        public List<RolesUsuario>? Roles { get; set; }
        public string? RolS { get; set; }

        public string? Nombre { get; set; }
        public string? ApellidoP { get; set; }
        public string? ApellidoM { get; set; }
        public string? Correo { get; set; }
        public string? Lada { get; set; }
        public string? Tel { get; set; }
        public string? Lada2 { get; set; }
        public string? Tel2 { get; set; }
        public string? status { get; set; }
        public string? Rfc { get; set; }
        public string? Curp { get; set; }
        public DateTime? FecCump { get; set; }
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

    public class RolesUsuario
    {
        public string Rol { get; set; }

    }
}
