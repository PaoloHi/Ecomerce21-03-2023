using EcommerceRealCVO.Models;
using System.Data;
using System.Data.SqlClient;

namespace EcommerceRealCVO.Datos.Center
{
    public class IntegracionCenter
    {
        //Obtener el registro insertado 
        int lead;
        public int IDLead(PropiedadesModel oMI)
        {
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Obtener_Lead_ecomm", conexion);
                    cmd.Parameters.AddWithValue("Correo", oMI.CorreoMI);
                    cmd.Parameters.AddWithValue("IDPropiedad", oMI.IDPropiedad);
                    cmd.Parameters.AddWithValue("TipoLead", oMI.TipoLI);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lead = Convert.ToInt32(dr["IDLead"]);
                        }
                    } 
                   
                }
                return lead;

            }
            catch (Exception e)
            {

                string error = e.Message;
                return 0;
            }

        }

        public bool IntegracionEntrada(BitrixLIModel integracionB)
        {
            if(integracionB.leadId== null && integracionB.id != null)
            {
                integracionB.leadId = integracionB.id;
            }

            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar_APIIS_ecomm", conexion);
                    cmd.Parameters.AddWithValue("Proveedor", integracionB.Proveedor);
                    cmd.Parameters.AddWithValue("IDRummet", integracionB.IDRummet);
                    cmd.Parameters.AddWithValue("IDintegra", integracionB.leadId);
                    cmd.Parameters.AddWithValue("Accion", integracionB.Accion);
                    cmd.Parameters.AddWithValue("Mensaje", integracionB.msg);
                    cmd.Parameters.AddWithValue("Tipo", integracionB.Tipo);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    
                }
                rpta = true;
                

            }
            catch (Exception e)
            {

                string error = e.Message;
                rpta = false;
            }
            return rpta;
        }

        public int IDLeadB(ContactoModel oMI)
        {
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Obtener_Lead_ecomm", conexion);
             
                    cmd.Parameters.AddWithValue("Correo", oMI.Email);
                    cmd.Parameters.AddWithValue("IDPropiedad", DBNull.Value);
                    cmd.Parameters.AddWithValue("TipoLead", oMI.Tipo);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lead = Convert.ToInt32(dr["IDLead"]);
                        }
                    }

                }
                return lead;

            }
            catch (Exception e)
            {

                string error = e.Message;
                return 0;
            }

        }

    }
}
