using EcommerceRealCVO.Datos.Center;
using EcommerceRealCVO.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceRealCVO.Tools
{
    public class Filtros
    {
        //FILTROS
        FiltrosCenter _FiltrosData = new FiltrosCenter();

        public List<ParametrosFiltro> ParametrosFiltro(string? Ac, string? Tipo, string? Gen, string? Propiedad)
        {
            //Lista para añadir los parametros 
            var oListaParametros = new List<ParametrosFiltro>();

            //Variable para comparar si es ubicación o dato random 
            string tUbiCarct = "";

            //Para la acción 
            //Validaciones
            if (Ac == "en-venta")
            {
                Ac = "Venta";
            }
            else if (Ac == "en-renta")
            {
                Ac = "Renta";
            }

            oListaParametros.Add(new ParametrosFiltro
            {
                Parametro = "Ac",
                Valor = Ac

            });

            //Para el tipo 
            if(Tipo==null || Tipo=="")
            {
                oListaParametros.Add(new ParametrosFiltro
                {
                    Parametro = "Tipo",
                    Valor = " ",
                    ValorT = 0

                });
            }
            else
            {
                var IDTipo = _FiltrosData.ObtenerTipo(Tipo);

                oListaParametros.Add(new ParametrosFiltro
                {
                    Parametro = "Tipo",
                    Valor = " ",
                    ValorT = IDTipo

                });
            }
           

            //Para el general
            if (Gen != null)
            {
                string ubiCaract = Gen.Extract(4);
                string s2 = "con-";
                bool b1 = ubiCaract.Contains(s2);

                string s3 = "en-";
                bool b2 = ubiCaract.Contains(s3);

                if (b1)
                {
                    //Cuando es una característica general
                    tUbiCarct = "CG";
                }
                else if (b2)
                {
                    //Cuando es una ubicación
                    tUbiCarct = "UG";

                }
                //Quitando prefijos para saber si es una ubicación o caracteristica
                string s1 = "this is something";
                s1 = s1.Remove(0, 1);
                if (tUbiCarct == "CG")
                {
                    //Retiramos prejo "con"
                    Gen = Gen.Remove(0, 4);
                    Console.WriteLine("Gen sin con " + Gen);
                }
                else
                {
                    //Retiramos prefijo "en"
                    Gen = Gen.Remove(0, 3);
                }

                //Quitando guiones
                Gen = Gen.Replace('-', ' ');
                Console.WriteLine("este es GEN FINAL " + Gen);

                oListaParametros.Add(new ParametrosFiltro
                {
                    Parametro = "Gen",
                    Valor = Gen,
                    ValorT =0

                });
            }

            return oListaParametros;
        }


     
    }
}
