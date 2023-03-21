using EcommerceRealCVO.Models;
using System.Data;
using System.Data.SqlClient;

namespace EcommerceRealCVO.Datos.Center
{
    public class ContactoCenter
    {
        public bool Guardar(ContactoModel ocontacto)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar_Contacto_ecomm", conexion);
                    cmd.Parameters.AddWithValue("Nombre", ocontacto.Nombre);
                    cmd.Parameters.AddWithValue("APaterno", ocontacto.APaterno);
                    cmd.Parameters.AddWithValue("AMaterno", ocontacto.AMaterno);
                    cmd.Parameters.AddWithValue("Telefono", ocontacto.Telefono);
                    cmd.Parameters.AddWithValue("Correo", ocontacto.Email);
                    cmd.Parameters.AddWithValue("Mensaje", ocontacto.Mensaje);
                    cmd.Parameters.AddWithValue("TipoContacto", ocontacto.Tipo);
                    cmd.Parameters.AddWithValue("FechaContacto", ocontacto.FechaContacto);
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

        public bool LeadBrokerQC(ContactoModel ocontacto)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar_LeadB_ecomm", conexion);

                    if (ocontacto.FechaContacto == null)
                    {

                        cmd.Parameters.AddWithValue("FechaC", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("FechaC", ocontacto.FechaContacto);
                    }

                    if (ocontacto.Mensaje == null)
                    {

                        cmd.Parameters.AddWithValue("Mensaje", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("Mensaje", ocontacto.Mensaje);
                    }

                    cmd.Parameters.AddWithValue("Nombre", ocontacto.Nombre);
                    cmd.Parameters.AddWithValue("APP", ocontacto.APaterno);
                    cmd.Parameters.AddWithValue("APM", ocontacto.AMaterno);
                    cmd.Parameters.AddWithValue("NoTel", ocontacto.Telefono);
                    cmd.Parameters.AddWithValue("Correo", ocontacto.Email);
                   
                    cmd.Parameters.AddWithValue("TipoC", ocontacto.Tipo);
                    
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
    }
}
