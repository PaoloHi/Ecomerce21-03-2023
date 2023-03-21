using EcommerceRealCVO.Models;
using System.Data;
using System.Data.SqlClient;

namespace EcommerceRealCVO.Datos.Center
{
    public class OnboardingBroker
    {
        public bool RegistroUsuario(PlanesModel oLeadBrokerReg)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Registro_usuario_ecomm", conexion);
                    cmd.Parameters.AddWithValue("nombre", oLeadBrokerReg.nombreLeadBroker);
                    cmd.Parameters.AddWithValue("apellidoP", oLeadBrokerReg.apellidoPaterno);
                    cmd.Parameters.AddWithValue("apellidoM", oLeadBrokerReg.apellidoMaterno);
                    cmd.Parameters.AddWithValue("telefono", oLeadBrokerReg.telefono);
                    cmd.Parameters.AddWithValue("email", oLeadBrokerReg.email);
                    cmd.Parameters.AddWithValue("nombrePlan", oLeadBrokerReg.nombrePlan);
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
