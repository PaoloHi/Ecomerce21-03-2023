namespace EcommerceRealCVO.Models
{
    public class PropiedadesModel
    {
        public List<PropiedadesModelPropiedad>? PropiedadesGL { get; set; }
        public List<PaginacionModel2>? propiedadesList { get; set; }

        //Lista para el llendo de la información de las imagenes 
        public List<ImagenPropiedades>? ImagenesPropiedades { get; set; }

        //Lista para el mostrado de las imagenes dentro del apartado de propiedades
        public List<base64PropiedadesEcomm>? LImagenBase64 { get; set; }

        /****************** Variables para filtrado **********************************/

        //Variable para el tipo de propiedad
        public int? TPropiedadP { get; set; }
        //Lista de tipo de propiedades 
        public List<TipoPropiedadP>? TPropiedadL { get; set; }
        //Lista de las caracteristicas Generales
        public List<CaracteristicasCheck>? CaracteristicasGL { get; set; }
        //Lista de las caracteristicas Servicios
        public List<CaracteristicasCheck>? CaracteristicasSL { get; set; }
        //Lista de las caracteristicas Exteriores
        public List<CaracteristicasCheck>? CaracteristicasEL { get; set; }
        //Lista de las caracteristicas Ambientes
        public List<CaracteristicasCheck>? CaracteristicasAL { get; set; }
        //Operación de Venta o Renta
        public string? Operacion { get; set; }
        //Busqueda por ubicacion
        public string? BUbicacion { get; set; }
        //Busqueda por ubicacion
        public string? SuperficieCheck { get; set; }
        //Variable para la lista de la superficie
        public int? SuperficieUMedida { get; set; }
        //Lista para la superficie 
        public List<PropiedadesModelUMedida>? UMedidaL { get; set; }
        //Operación de Venta o Renta
        public string? Recamaras { get; set; }
        //Operación de Venta o Renta
        public string? Estacionamientos { get; set; }
        //Operación de Venta o Renta
        public string? Banos { get; set; }
        //Operación de Venta o Renta
        public string? MBanos { get; set; }
        //Operación de Venta o Renta
        public string? Bodegas { get; set; }
        //Operación de Venta o Renta
        //Operación de Venta o Renta
        public string? Closets { get; set; }
        public string? Elevadores { get; set; }
        public string? SDesde { get; set; }
        public string? SHasta { get; set; }
        //Para almacenar los ID de las propiedades en filtro 
        public List<PropiedadesModelFiltro>? LPropiedadesF { get; set; }

        //Lista para las caracteristicas de las propiedades 
        public List<CaracteristicasP>? LCaracteristicas { get; set; }

        //Lista para traer los valores introducidos en el filtro
        public List<ParametrosFiltro>? LParametrosFiltro { get; set; }

        public string? IDPropiedad { get; set; }
        public string? NombreMI { get; set; }
        public string? ApellidoPMI { get; set; }
        public string? ApellidoMMI { get; set; }
        public string? NumeroTMI { get; set; }
        public string? CorreoMI { get; set; }
        public int? TipoLI { get; set; }
        public DateTime? FechaMI { get; set; }
    }

    public class PropiedadesModelPropiedad
    {
        public int IDPropiedad { get; set; }
        public string IDPropiedadG { get; set; }
        public string Titulo { get; set; }
        public string ISO { get; set; }
        public string Precio { get; set; }
        public string Direccion { get; set; }
        public string Superficie { get; set; }
        public string UM { get; set; }
        public string Tipo { get; set; }
        public string Abrev { get; set; }

        public string Latitud { get; set; }
        public string Longitud { get; set; }
    }

    //Aquí dépositamos la información de las imagenes convertidas para las propiedades 
    public class base64PropiedadesEcomm
    {
        public int? IDpropiedadBase64 { get; set; }
        public string? RutaImagen { get; set; }
        public string? txtBase64 { get; set; }
    }

    //Este método es para el llenado de la información de las imagenes
    public class ImagenPropiedades
    {
        public int? IDpropiedad { get; set; }
        public string RutaImagenPropiedad { get; set; }
    }

    public class PaginacionModel2
    {
        public int PaginaActual { get; set; }
        public int TotalDeRegistros { get; set; }
        public int RegistrosPorPagina { get; set; }
        public RouteValueDictionary ValoresQueryString { get; set; }
    }

    //Para la lista de las características con check 
    public class CaracteristicasCheck

    {
        public int IDTCaracteristica { get; set; }
        public string TipoCaract { get; set; }
        public int IDCaracteristica { get; set; }
        public string Caracteristica { get; set; }
        public bool isSelected { get; set; }

    }

    public class TipoPropiedadP
    {
        public int IDtipoP { get; set; }
        public string Tipo { get; set; }

    }

    public class PropiedadesModelUMedida

    {
        public int IDUm { get; set; }
        public string UnidadMedida { get; set; }

    }

    public class PropiedadesModelFiltro

    {
        public int IDPropiedad { get; set; }


    }

    //Lista para los parametros de los filtros aplicados
    public class PropiedadesModelFiltroParam

    {
        public int IDPropiedad { get; set; }


    }

    //Lista para obtener las características de la propiedad 
    public class CaracteristicasP
    {
        public int? IDPropiedad { get; set; }
        public string? Caracteristica { get; set; }
        public int? NoElementos { get; set; }
        //
    }

    //Parametros para obtener las variables de busqueda en filtros sencillos dentro de la lista
    public class ParametrosFiltro
    {
        public string? Parametro { get; set; }
        public string? Valor { get; set; }
        public int? ValorT { get; set; }

    }

    //Variable para alamacenar el tipo de propiedad a filtrar
    public class ParametrosFiltroTipo
    {
        //Valor Tipo Propiedad
        public int? ValorTP { get; set; }

    }

    //Variables del formulario de más información 
    //public class MasInformacion
    //{
    //    public string? NombreMI { get; set; }
    //    public string? ApellidoPMI { get; set; }
    //    public string? ApellidoMMI { get; set; }
    //    public string? NumeroTMI { get; set; }
    //    public string? CorreoMI { get; set; }

    //}
}