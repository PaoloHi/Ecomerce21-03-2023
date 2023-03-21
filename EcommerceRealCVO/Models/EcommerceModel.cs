namespace EcommerceRealCVO.Models
{
    public class EcommerceModel
    {
        //Listado de tipo de propiedades
        public List<TipoPropiedad>? TPropiedadL { get; set; }
        public string? Tpropiedad { get; set; }
        public string? Accion { get; set; }
        public string? VariableJuego { get; set; }
        public string? UbiCarct { get; set; }
        public string? Colonia { get; set; }
        public string? CE { get; set; }
        public string? Municipio { get; set; }
        public List<PropiedadesDestacadas>? LPropiedadesDestacadas { get; set; }
        public List<PropiedadesExistentes>? LPropiedadesExistentes { get; set; }
        public List<base64PropiedadDestacada>? LImagenBase64 { get; set; }
        public List<UbicacionPropiedad>? Ubicaciones { get; set; }


    }

    //Para la lista del tipo de propiedad
    public class TipoPropiedad
    {
        public int IDtipoP { get; set; }
        public string Tipo { get; set; }

    }

    public class PropiedadesDestacadas
    {
        public int IDpropiedad { get; set; }
        public string IDpropiedadG { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string? RutaImagen { get; set; }
    }

    public class base64PropiedadDestacada
    {
        public int? IDpropiedadBase64 { get; set; }
        public string? RutaImagen { get; set; }
        public string? txtBase64 { get; set; }
    }

    public class base64PropiedadSeleccionada
    {
        public int? IDpropiedadBase64 { get; set; }
        public string? RutaImagen { get; set; }
        public string? txtBase64 { get; set; }
    }

    //Inventario
    public class PropiedadesExistentes
    {
        public string Tipo { get; set; }
        public string Cantidad { get; set; }

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
        public List<PropiedadesSugeridas>? PropiedadesSL { get; set; }
        public List<ImagenPropiedad>? ImgPropiedad { get; set; }
        public List<base64PropiedadDestacada>? LImagenBase64S { get; set; }
        public List<base64PropiedadSeleccionada>? LImagenBase64S2 { get; set; }
        public List<base64PropiedadDestacada>? LImagenBase64Sugerida { get; set; }
        public List<CaracteristicasPS>? CaracteristicasBas { get; set; }
        public List<CaracteristicasPS>? CaracteristicasBasA { get; set; }
        public List<CaracteristicasPS>? CaracteristicasGen { get; set; }
        public List<CaracteristicasPS>? CaracteristicasBasS { get; set; }


    }
    public class PropiedadesSugeridas
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

    }

    public class ImagenPropiedad
    {
        public int IDPropiedad { get; set; }
        public string RutaImagenPropiedad { get; set; }
    }

    //Método para almacenar Estado y Municipio una vez aplicado el filtro 
    public class UbicacionPropiedad
    {
        public string Municipio { get; set; }
        public string Estado { get; set; }
    }

    public class CaracteristicasPS
    {
        public int? IDPropiedad { get; set; }
        public string? Caracteristica { get; set; }
        public int? NoElementos { get; set; }
        public int? Seleccionado { get; set; }
        public string? TipoCaract { get; set; } 

    }
}
