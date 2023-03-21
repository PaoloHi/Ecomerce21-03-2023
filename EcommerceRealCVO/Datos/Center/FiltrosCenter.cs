using EcommerceRealCVO.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace EcommerceRealCVO.Datos.Center
{
    public class FiltrosCenter
    {
        //Este metodo funciona para listar los tipos de propiedades dentro del buscador en la parte inicial del Ecommerce
        public int ObtenerTipo(string? Tipo)
        {

            var TipoID =0;

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_tipoPropiedad_ecomm", conexion);
                cmd.Parameters.AddWithValue("tipo", Tipo);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        TipoID = Convert.ToInt32(dr["IDTipo"]);
                    }
                }
            }

            return TipoID;
        }
    }
}
