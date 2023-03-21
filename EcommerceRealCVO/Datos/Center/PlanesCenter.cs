using EcommerceRealCVO.Models;
using System.Data;
using System.Data.SqlClient;

namespace EcommerceRealCVO.Datos.Center
{
    public class PlanesCenter
    {
        //Creación de la primera lista para traer los nombres y precios de los planes para la vista del ecommerce
        public List<PlanesModelG> Listar()
        {

            var oListaPlanes = new List<PlanesModelG>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                var sqlQuery = "SELECT IDplanEcomm, nombrePlan, noPropiedades, precioTotal from PlanesEcomm";
                SqlCommand cmd = new SqlCommand(sqlQuery, conexion);
                cmd.CommandType = CommandType.Text;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaPlanes.Add(new PlanesModelG()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            IDplanEcomm = Convert.ToInt32(dr["IDplanEcomm"]),
                            nombrePlan = dr["nombrePlan"].ToString(),
                            precioTotal = dr["precioTotal"].ToString(),
                            propiedades = Convert.ToInt32(dr["noPropiedades"]),
                        });
                    }
                }
            }

            return oListaPlanes;
        }

        //Obtención de caracteristicas de planes 

        public List<PlanesModelCaract> Listar2()
        {

            var oListaPlanesCaract = new List<PlanesModelCaract>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                var sqlQuery = "SELECT  " +
                    "a.IDplanEcommRel AS IDplan, " +
                    "a.IDcatCaractPlanEcommRel AS IDcatPlan, " +
                    "b.nombrePlan AS nombrePlan, " +
                    "b.precioTotal AS precio, " +
                    "c.nombreCaracteristica AS caracteristica, " +
                    "a.orden AS orden, " +
                    "a.caractFront as Front " +
                    "FROM Planes_CarctEcomm a JOIN PlanesEcomm b on(b.IDplanEcomm = a.IDplanEcommRel) " +
                    "JOIN Cat_CaractPlanesEcomm c on(c.IDcatCaractPlanEcomm = a.IDcatCaractPlanEcommRel) " +
                    "WHERE a.caractFront = 'S' ORDER BY a.IDplanEcommRel, a.orden; ";

                SqlCommand cmd = new SqlCommand(sqlQuery, conexion);
                cmd.CommandType = CommandType.Text;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaPlanesCaract.Add(new PlanesModelCaract()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            IDcatPlan = Convert.ToInt32(dr["IDcatPlan"]),
                            caractPlanEcomm = dr["caracteristica"].ToString(),
                            IDPlan= Convert.ToInt32(dr["IDplan"]),

                        });
                    }
                }
            }

            return oListaPlanesCaract;
        }
    }



}
