using adminRummet.Models;
using System.Data;
using System.Data.SqlClient;

namespace adminRummet.Center.Admin
{
    public class UsuariosCenter
    {
        //Consola usuarios

        //Función para el listado de usuarios 
        public List<UsuariosModel> ListaUsuarios()
        {
            var oListaUsuarios = new List<UsuariosModel>();

            var cn = new Conexion();


            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_usuariosG_adm", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaUsuarios.Add(new UsuariosModel()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            Nombre = dr["nombre"].ToString(),
                            ApellidoP = dr["apellidoPaterno"].ToString(),
                            Correo = dr["email"].ToString(),
                            Tel = dr["telefono"].ToString(),
                            RolS = dr["Rol"].ToString(),

                        });
                    }

                }

                return oListaUsuarios;
            }

        }





        //Función para guardar el registro del usuario
        public bool Guardar(UsuariosModel oUsuarios)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar_usuario_adm", conexion);
                    cmd.Parameters.AddWithValue("nombre", oUsuarios.Nombre);
                    cmd.Parameters.AddWithValue("apellidoP", oUsuarios.ApellidoP);
                    cmd.Parameters.AddWithValue("apellidoM", oUsuarios.ApellidoM);
                    cmd.Parameters.AddWithValue("lada", oUsuarios.Lada);
                    cmd.Parameters.AddWithValue("telefono", oUsuarios.Tel);
                    cmd.Parameters.AddWithValue("ladaB", oUsuarios.Lada2);
                    cmd.Parameters.AddWithValue("telefonoB", oUsuarios.Tel2);
                    cmd.Parameters.AddWithValue("rfc", oUsuarios.Rfc);
                    cmd.Parameters.AddWithValue("curp", oUsuarios.Curp);
                    cmd.Parameters.AddWithValue("fecNac", oUsuarios.FecCump);
                    cmd.Parameters.AddWithValue("email", oUsuarios.Correo);
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

        //Función para traer los roles del usuario
        public List<RolesUsuario> ListarRoles()
        {

            var oListaRoles = new List<RolesUsuario>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_roles_adm", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaRoles.Add(new RolesUsuario()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            Rol = dr["Name"].ToString(),

                        });
                    }
                }
            }

            return oListaRoles;
        }

    }
}
