namespace RealStateGestion.Models
{
    public class PlanesModel
    {
        public int? IDplanEcomm { get; set; }
        public string? nombrePlan { get; set; }
        public string? precioTotal { get; set; }

        //Para la edición de las categorías
        public int IDcatPlan { get; set; }
        public int IDPlan { get; set; }

        public string caractPlanEcomm { get; set; }

        public List<PlanesModelG>? planesG { get; set; }
        public List<PlanesModelCaract>? planesCaract { get; set; }

        public List<PlanesModelCaractG>? planesCaractG { get; set; }

        public List<PlanesModelCaractIndv>? planesCaractIndv { get; set; }

        public bool isSelected { get; set; }


        /*VARIABLES PRUEBA PARA CHECKBOX**/
        public int? IDplanEcommRel { get; set; }
        public int? IDcatCaractPlanEcommRel { get; set; }
        public int? orden { get; set; }
        public string? frontCaract { get; set; }

    }

    //Esta clase contiene los datos que vamos a utilizar de la lista de planes 
    public class PlanesModelG 
    {
        public int IDplanEcomm { get; set; }
        public string nombrePlan { get; set; }
        public string precioTotal { get; set; }
        public int propiedades { get; set; }
        public bool isSelected { get; set; }


    }

    //Esta clase contiene los datos que vamos a utilizar para listar las caracteristicas de los planes
    public class PlanesModelCaract
    {
        public int IDcatPlan { get; set; }
        public int IDPlan { get; set; }
        public string caractPlanEcomm { get; set; }

    }

    public class PlanesModelCaractG
    {
        public int IDcatPlan { get; set; }
        public int IDPlan { get; set; }

        public string caractPlanEcomm { get; set; }

        public bool isSelected { get; set; }
        public bool isnotSelected { get; set; }

    }
    public class PlanesModelCaractIndv
    {
        public int? IDplanEcommRel { get; set; }
        public int? IDcatCaractPlanEcommRel { get; set; }
        public int? orden { get; set; }
        public string? frontCaract { get; set; }

    }
}
