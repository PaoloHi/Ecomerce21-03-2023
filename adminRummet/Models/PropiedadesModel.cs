using System.ComponentModel.DataAnnotations;

namespace adminRummet.Models
{
    public class PropiedadesModel
    {

        // Se alamenaran los datos de la propiedad seleccionada del tablero
        public PropiedadesModelPropiedad? PropiedadSelect { get; set; }

        // Datos para la propiedad a editar
        public PropiedadModelDatosEditar? PropiedadEditar { get; set; }
        public List<PropiedadModelEditarCaracteristicasNum>? CaracteristicasNumPropiedadEditar { get; set; }
        public List<PropiedadesModelCaracteristicasCheck>? CaracteristicasCheckPropiedadEditar { get; set; }
        public List<PropiedadModelEditarImagenes>? ImagenesPropiedadEditar { get; set; }
        public List<PropiedadModelEditarDocumentos>? DocumentosPropiedadEditar { get; set; }

        public string? PropiedadGUID { get; set; }

        public string? PropiedadID { get; set; }
        public int? IDPropiedad { get; set; }
        //public List<PropiedadesModelIDpropiedad> IDpropiedadL { get; set; }
        // Datos de formulario parte 1 ( Generales )
        public string? Operacion { get; set; }
        public string? NombrePropiedad { get; set; }
        //Variable para el tipo de propiedad


        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public int TPropiedad { get; set; }

        public List<PropiedadesModelTipoPropiedad>? TPropiedadL { get; set; }
        public int? SubPropiedad { get; set; }
        public List<PropiedadesModelSubTipoPropiedad>? SubTipoPropiedadL { get; set; }
        //LISTA Y VARIABLE PARA LA MONEDA DEL COSTO DE PROPIEDAD Y PRECIO
        public int? Moneda { get; set; }
        public string? Precio { get; set; }
        public List<PropiedadesModelMoneda>? MonedaL { get; set; }
        public int? PeriodoRenta { get; set; }
        public string? MantenimientoPrecio { get; set; }
        public int? PeriodoMantenimiento { get; set; }
        public string? Descripcion { get; set; }

        //Que guarde en el git 
        // Datos de formulario parte 2 ( Direccion )
        public string? Calle { get; set; }
        public int? NoInt { get; set; }
        public int? NoExt { get; set; }
        public int? CP { get; set; }
        public string? Colonia { get; set; }
        public int? IDcolonia { get; set; }
        public string? Municipio { get; set; }
        //public int? Municipio { get; set; }
        public string? Estado { get; set; }
        //public int? Estado { get; set; }
        public string? Pais { get; set; }
        //public int? Pais { get; set; }
        public string? txtLat { get; set; }
        public string? txtLng { get; set; }
        public string? DireccionC { get; set; }


        // Datos de formulario parte 3 ( Superficie )
        //Variables de la construcción

        //Listas para el llenado de datos

        public int? Superficie { get; set; } // Terreno 
        public int? SuperficieUMedida { get; set; }

        public int? SuperficieContruccion { get; set; }
        public int SuperficieContruccionUMedida { get; set; }

        public int? Antiguedad { get; set; }

        public int? UsoSuelo { get; set; }
        public List<PropiedadesModelSuelo>? SueloL { get; set; }

        public int? UMedida { get; set; }
        public List<PropiedadesModelUMedida>? UMedidaL { get; set; }

        // Parte para guardado de Caracteristicas

        public int? Recamaras { get; set; }
        public int? Banos { get; set; }
        public int? MediosBanos { get; set; }
        public int? Estacionamiento { get; set; }

        //Variables de las caracteristicas
        //Lista de las caracteristicas Generales

        public List<PropiedadesModelCaracteristicasCheck>? CaracteristicasL { get; set; }

        public int? Bodegas { get; set; }
        public int? Closets { get; set; }
        public int? Elevadores { get; set; }



        // Parte para guardado de imagenes
        public List<IFormFile>? Files { get; set; }
        public IFormFile Predial { get; set; }
        public IFormFile Escritura { get; set; }
        public IFormFile Identificacion { get; set; }
        public IFormFile Regimen { get; set; }
        public IFormFile NoGravamen { get; set; }
        public IFormFile NoAdeudo { get; set; }

        public DateTime? FechaPublicacion { get; set; }  


        //Esta es la seccion del propietario 
        public Propietario PropietarioSelect { get; set; }


        public string? IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? apellidoPaterno { get; set; }
        public string? apellidoMaterno { get; set; }
        public string? email { get; set; }
        public string? telefono { get; set; }
        public string? UserName { get; set; }

        public string oColonia { get; set; }
        public List<Colonias> LColonias { get; set; }

    }

    //Clase destinada para enviar la información al tablero de propiedades
    public class PropiedadesModelTablero : PaginacionPropModel
    {
        /*******************************************/
        //Listado de propiedades por botón de tablero - PUBLICADAS
        public List<PropiedadesModelPropiedad>? PropiedadesL { get; set; }


        //Listado de propiedades por botón de tablero -  NO PUBLICADAS
        public List<PropiedadesModelPropiedad>? PropiedadesNL { get; set; }

        //Listado de propiedades por botón de tablero -  EN PROCESO DE ENTREGA
        public List<PropiedadesModelPropiedad>? PropiedadesPL { get; set; }

        //Listado de propiedades por botón de tablero -  CERRADAS
        public List<PropiedadesModelPropiedad>? PropiedadesCL { get; set; }


        /*******************************************************/
        //Para las imagenes
        /*******************************************************/

        //Listado de imagenes de propiedades por botón de tablero - PUBLICADAS
        public List<base64PropiedadesEcomm>? PropLImagenBase64 { get; set; }
        //Listado de imagenes de propiedades por botón de tablero -  NO PUBLICADAS
        public List<base64PropiedadesEcomm>? PropNLImagenBase64 { get; set; }
        //Listado de imagenes de propiedades por botón de tablero - EN PROCESO DE ENTREGA
        public List<base64PropiedadesEcomm>? PropPLImagenBase64 { get; set; }
        //Listado de imagenes de propiedades por botón de tablero - CERRADAS
        public List<base64PropiedadesEcomm>? PropCLImagenBase64 { get; set; }




    }

    public class PropiedadesModelTipoPropiedad
    {
        public int IDtipoP { get; set; }
        public string Tipo { get; set; }

    }
    //Para la lista del subtipo de propiedad
    public class PropiedadesModelSubTipoPropiedad

    {
        public int IDsubtipoP { get; set; }
        public int IDtipoP { get; set; }
        public string Subtipo { get; set; }

    }

    //Para traer los tipos de moneda 
    public class PropiedadesModelMoneda

    {
        public int IDMoneda { get; set; }
        public string Moneda { get; set; }
        public string ISO { get; set; }

    }

    public class PropiedadesModelCaracteristicasnumber
    {
        public int IDCaracteristica { get; set; }

        public int valorCaracteristicas { get; set; }
    }
    public class PropiedadesModelCaracteristicasCheck

    {
        public int IDTCaracteristica { get; set; }
        public string TipoCaract { get; set; }
        public int IDCaracteristica { get; set; }
        public string Caracteristica { get; set; }
        public bool isSelected { get; set; }

    }

    public class PropiedadesModelSuelo

    {
        public int IDSuelo { get; set; }
        public string Suelo { get; set; }

    }

    public class PropiedadesModelUMedida

    {
        public int IDUm { get; set; }
        public string UnidadMedida { get; set; }

    }

    //Modelo para mostrar la informacion en el tablero
    public class PropiedadesModelPropiedad
    {
        public int IDPropiedad { get; set; }
        public string? IDPropiedadG { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Operacion { get; set; }
        public string ISO { get; set; }
        public string Precio { get; set; }
        public string Direccion { get; set; }
        public string Superficie { get; set; }
        public string UM { get; set; }
        public string SuperficieC { get; set; }
        public string UMC { get; set; }
        public string Tipo { get; set; }
        public string Abrev { get; set; }
        public string Status { get; set; }
        public int? Vistas { get; set; }
        public int? Visitas { get; set; }
        public string FPublicacion { get; set; }

        public string StatusPropiedad { get; set; }


        public PropiedadesModelPropiedad? PropiedadSelect { get; set; }
        public List<PropiedadModelEditarCaracteristicasNum>? CaracteristicasNumPropiedadEditar { get; set; }

        public List<PropiedadModelEditarDocumentos> DocumentosDetalles { get; set; }

        public List<CaracteristicasD> ListaCaracteristicasD { get; set; }

    }

    //Para traer los datos de las imagenes y colocarlo dentro de las listas
    //Aquí dépositamos la información de las imagenes convertidas para las propiedades 
    public class base64PropiedadesEcomm
    {
        public int? IDpropiedadBase64 { get; set; }
        public string? RutaImagen { get; set; }
        public string? txtBase64 { get; set; }
    }

    // Datos para la propiedad a editar
    public class PropiedadModelEditarCaracteristicasNum
    {
        public int IDTCaracteristica { get; set; }
        public string TipoCaract { get; set; }
        public int IDCaracteristica { get; set; }
        public string Caracteristica { get; set; }
        public int numeroElementos { get; set; }
    }

    public class PropiedadModelEditarImagenes
    {
        public int? IDimgPropiedad { get; set; }
        public int? IDpropiedad { get; set; }
        public string? PropiedadID { get; set; }
        public int? numImg { get; set; }
        public string? imgName { get; set; }
        public string? extension { get; set; }
        public string? imgPath { get; set; }
        public string? txt64 { get; set; }
        public string? estado { get; set; }
    }

    public class PropiedadModelEditarDocumentos
    {
        public int? IDdocPropiedad { get; set; }
        public int? IDtipoArchivo { get; set; }
        public string? TipoArchivo { get; set; }
        public int? IDpropiedad { get; set; }
        public string? PropiedadID { get; set; }
        public string? fileNombre { get; set; }
        public string? extension { get; set; }
        public string? filePath { get; set; }
        public string? txt64 { get; set; }
        public int IDStatusDoc { get; set; }

    }

    public class PropiedadModelDatosEditar
    {
        public int? Operacion { get; set; }
        public string? NombrePropiedad { get; set; }
        public int? TipoPropiedad { get; set; }
        public int? subTipoPropiedad { get; set; }
        public int? Moneda { get; set; }
        public string? Precio { get; set; }
        public int? periodoRenta { get; set; }
        public string? Mantenimiento { get; set; }
        public int? periodoMantenimiento { get; set; }
        public string? Descripcion { get; set; }

        public string? Calle { get; set; }
        public int? NoInt { get; set; }
        public int? NoExt { get; set; }
        public int? CP { get; set; }
        public string? Colonia { get; set; }
        public int? IDcolonia { get; set; }
        public string? Municipio { get; set; }
        //public int? Municipio { get; set; }
        public string? Estado { get; set; }
        //public int? Estado { get; set; }
        public string? Pais { get; set; }
        //public int? Pais { get; set; }
        public string? txtLat { get; set; }
        public string? txtLng { get; set; }
        public string? DireccionC { get; set; }


        public int? Superficie { get; set; } // Terreno
        public int? SuperficieUMedida { get; set; }
        public string? txtSuperficieUMedida { get; set; }

        public int? SuperficieContruccion { get; set; }
        public int SuperficieContruccionUMedida { get; set; }
        public string? txtSuperficieContruccionUMedida { get; set; }

        public int? Antiguedad { get; set; }

        public int? UsoSuelo { get; set; }
    }

    public class ImagenPropiedades
    {
        public int? IDpropiedad { get; set; }
        public string RutaImagenPropiedad { get; set; }
    }

    public class PropiedadSeleccionada
    {
        public string? IDPropiedad { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? ISO { get; set; }
        public string? Precio { get; set; }
        public string? Direccion { get; set; }
        public string? Calle { get; set; }
        public string? NoInt { get; set; }
        public string? NoExt { get; set; }
        public string? Colonia { get; set; }
        public string? Municipio { get; set; }
        public string? Estado { get; set; }
        public string? Pais { get; set; }
        public string? CP { get; set; }
        public string? Superficie { get; set; }
        public string? UM { get; set; }
        public string? SuperficieC { get; set; }
        public string? UMC { get; set; }
        public string? Tipo { get; set; }
        public string? Abrev { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public string? StatusPropiedad { get; set; }  

    }


    //esta fue todo un exito 
    public class CaracteristicasD
    {
        public string PropiedadID { get; set; }
        public string Caracteristicas { get; set; }
        public string NoElementos { get; set; }
    }


    public class Propietario { 
        public string? IdUsuario { get; set; }
        public string? Nombre { get; set;  }
        public string? apellidoPaterno { get; set; }
        public string? apellidoMaterno { get; set;}
        public string? email { get; set; }   
        public string? telefono { get; set; }
        public string? UserName { get; set; }
    }

   

    public class Colonias
    {
        public int IDColonia { get; set; }
        public string Colonia { get; set; }

    }
}

