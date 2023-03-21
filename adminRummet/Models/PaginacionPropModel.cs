namespace adminRummet.Models
{
    public class PaginacionPropModel
    {

        public int PaginaActualPropPublicada { get; set; }
        public int PaginaActualPropNoPublicada { get; set; }
        public int PaginaActualPropProceso { get; set; }
        public int PaginaActualPropCerrada { get; set; }

        public int TotalDeRegistrosPropPublicada { get; set; }
        public int TotalDeRegistrosPropNoPublicada { get; set; }
        public int TotalDeRegistrosPropProceso { get; set; }
        public int TotalDeRegistrosPropCerrada { get; set; }

        public int RegistrosPorPaginaPropPublicada { get; set; }
        public int RegistrosPorPaginaPropNoPublicada { get; set; }
        public int RegistrosPorPaginaPropProceso { get; set; }
        public int RegistrosPorPaginaPropCerrada { get; set; }

        public string txtIdPropPublicadaAct { get; set; }
        public string txtIdPropNoPublicadaAct { get; set; }
        public string txtIdPropProcesoAct { get; set; }
        public string txtIdPropCerradasAct { get; set; }

        public RouteValueDictionary ValoresQueryString { get; set; }


    }
}
