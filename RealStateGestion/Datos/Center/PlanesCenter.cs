using RealStateGestion.Models;
using System.Data;
using System.Data.SqlClient;

namespace RealStateGestion.Datos.Center
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
                SqlCommand cmd = new SqlCommand("sp_Obtener_plan_adm", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

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
                            isSelected = true,
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
                SqlCommand cmd = new SqlCommand("sp_Obtener_plan_caract_adm", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

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
                            IDPlan = Convert.ToInt32(dr["IDplan"]),
                        });
                    }
                }
            }

            return oListaPlanesCaract;
        }

        //Posee todas las caracteristicas
        public List<PlanesModelCaractG> ListarCaractG()
        {

            var oListaPlanesCaract = new List<PlanesModelCaractG>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_plan_caractG_adm", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaPlanesCaract.Add(new PlanesModelCaractG()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            IDcatPlan = Convert.ToInt32(dr["IDcatPlan"]),
                            caractPlanEcomm = dr["caracteristica"].ToString(),
                            isSelected=true,
                            isnotSelected=false
                       

                        });
                    }
                }
            }

            return oListaPlanesCaract;
        }

        //Obtener las caracteristicas seleccionadas Lista 
        public List<PlanesModelCaractIndv> CaractSelect(int IDPlan)
        {

            var oListaCaractSelect = new List<PlanesModelCaractIndv>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_caractxplan_adm", conexion);
                cmd.Parameters.AddWithValue("IDPlan", IDPlan);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaCaractSelect.Add(new PlanesModelCaractIndv()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            IDplanEcommRel = Convert.ToInt32(dr["IDplanEcommRel"]),
                            IDcatCaractPlanEcommRel = Convert.ToInt32(dr["IDcatCaractPlanEcommRel"]),
                            orden = Convert.ToInt32(dr["orden"]),
                            frontCaract = dr["caractFront"].ToString()
                        });
                    }
                }
            }

            return oListaCaractSelect;
        }

        //Obtener caracteristicas seleccionadas

        public PlanesModel ObtenerCG(int IDPlan)
        {

            var oObtPlan = new PlanesModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_usuario_adm", conexion);
                cmd.Parameters.AddWithValue("IDplanRealEcomm", IDPlan);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {

                        oObtPlan.IDplanEcommRel = Convert.ToInt32(dr["IDplanEcommRel"]);

                        oObtPlan.IDcatCaractPlanEcommRel = Convert.ToInt32(dr["IDcatCaractPlanEcommRel"]);
                        oObtPlan.orden = Convert.ToInt32(dr["IDcatPlan"]);
                        oObtPlan.frontCaract = dr["front"].ToString();

                    }
                }
            }
            return oObtPlan;
        }

    }
}
