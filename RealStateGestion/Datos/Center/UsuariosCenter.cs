using RealStateGestion.Models;
using System.Data;
using System.Data.SqlClient;

namespace RealStateGestion.Datos.Center
{
    public class UsuariosCenter
    {
        //Función para el listado de usuarios 
        public List<UsuariosModel> ListaUsuarios()
        {

            var oListaUsuarios = new List<UsuariosModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                var sqlQuery = "SELECT  " +
                    "IDusuarioRealEcomm, " +
                    "nombre, " +
                    "apellidoPaterno, " +
                    "apellidoMaterno, " +
                    "email, " +
                    "telefono, " +
                    "IDRolRel, statusReg " +
                    "FROM UsuariosRealEcomm WHERE statusReg <> 'Eliminado' ";

                SqlCommand cmd = new SqlCommand(sqlQuery, conexion);
                cmd.CommandType = CommandType.Text;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaUsuarios.Add(new UsuariosModel()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            IDusuario = Convert.ToInt32(dr["IDusuarioRealEcomm"]),
                            nombreUsuario = dr["nombre"].ToString(),
                            apellidoP = dr["apellidoPaterno"].ToString(),
                            apellidoM = dr["apellidoMaterno"].ToString(),
                            email = dr["email"].ToString(),
                            tel = dr["telefono"].ToString(),
                            IDrol = Convert.ToInt32(dr["IDRolRel"]),
                            status = dr["statusReg"].ToString(),
                        });
                    }
                }
            }

            return oListaUsuarios;
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
                    cmd.Parameters.AddWithValue("nombre", oUsuarios.nombreUsuario);
                    cmd.Parameters.AddWithValue("apellidoP", oUsuarios.apellidoP);
                    cmd.Parameters.AddWithValue("apellidoM", oUsuarios.apellidoM);
                    cmd.Parameters.AddWithValue("email", oUsuarios.email);
                    cmd.Parameters.AddWithValue("telefono", oUsuarios.tel);
                    cmd.Parameters.AddWithValue("celular", oUsuarios.cel);
                    cmd.Parameters.AddWithValue("rfc", oUsuarios.rfc);
                    cmd.Parameters.AddWithValue("curp", oUsuarios.curp);
                    cmd.Parameters.AddWithValue("rol", oUsuarios.IDrol);
                    cmd.Parameters.AddWithValue("IDBanco", oUsuarios.banco);
                    cmd.Parameters.AddWithValue("clabe", oUsuarios.clabe);
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

        //Función para guardar el registro del usuario img
        public bool GuardarIMG(UsuariosModel oUsuarios)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar_imgusuario_adm", conexion);
                    cmd.Parameters.AddWithValue("email", oUsuarios.email);
                    cmd.Parameters.AddWithValue("imgName", oUsuarios.imgPerfilNombre);
                    cmd.Parameters.AddWithValue("imgExt", oUsuarios.imgPerfiExt);
                    cmd.Parameters.AddWithValue("imgPath", oUsuarios.imgPerfilRuta);
                    cmd.Parameters.AddWithValue("imgSize", oUsuarios.imgPerfiTam);
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

        //Función para guardar los archivos de los usuarios 
        public bool GuardarDOC(string[] datosDoc)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar_docusuario_adm", conexion);
                    cmd.Parameters.AddWithValue("email", datosDoc[0]);
                    cmd.Parameters.AddWithValue("docName", datosDoc[1]);
                    cmd.Parameters.AddWithValue("docSize", datosDoc[2]);
                    cmd.Parameters.AddWithValue("docExt", datosDoc[3]);
                    cmd.Parameters.AddWithValue("docPath", datosDoc[4]);
                    cmd.Parameters.AddWithValue("doc", datosDoc[5]);
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

        //Función para obtener datos directos del usuario 
        public UsuariosModel ObtenerCarpeta(string correo)
        {
            bool rpta;
            var oObtUsers = new UsuariosModel();

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Obtener_usuario_carpeta", conexion);
                    cmd.Parameters.AddWithValue("correo", correo);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            oObtUsers.IDusuario = Convert.ToInt32(dr["IDusuarioRealEcomm"]);
                            oObtUsers.fechaAlta = dr["fechaAlt"].ToString();

                        }

                    }
                }

                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;

            }

                        return oObtUsers;

        }

        //Función para cargar la lista de roles
        //Función para cargar la lista de zonas
        //Función para cargar la lista de bancos 

        //Este método funciona al actualizar el registro obtener la información de usuario -DATOS G
        public UsuariosModel ObtenerG(int IDUser)
        {

            var oObtUsers = new UsuariosModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_usuario_adm", conexion);
                cmd.Parameters.AddWithValue("IDusuarioEcomm", IDUser);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oObtUsers.IDusuario = Convert.ToInt32(dr["IDusuarioRealEcomm"]);
                        oObtUsers.nombreUsuario = dr["nombre"].ToString();
                        oObtUsers.apellidoP = dr["apellidoPaterno"].ToString();
                        oObtUsers.apellidoM = dr["apellidoMaterno"].ToString();
                        oObtUsers.email = dr["email"].ToString();
                        oObtUsers.tel = dr["telefono"].ToString();
                        oObtUsers.IDrol = Convert.ToInt32(dr["IDRolRel"]);
                        oObtUsers.status = dr["statusReg"].ToString();
                        oObtUsers.rfc = dr["rfc"].ToString();
                        oObtUsers.curp = dr["curp"].ToString();
                        oObtUsers.cel = dr["celular"].ToString();
                    }
                }

            }

            return oObtUsers;
        }

        //DATOS B
        public UsuariosModel ObtenerB(int IDUser)
        {
            var oObtUsers = new UsuariosModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_movimiento_adm", conexion);
                cmd.Parameters.AddWithValue("IDusuarioEcomm", IDUser);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oObtUsers.IDusuario = Convert.ToInt32(dr["IDusuarioRealEcomm"]);
                        oObtUsers.clabe = dr["clabe"].ToString();
                        oObtUsers.banco = Convert.ToInt32(dr["banco"]);

                    }
                }
            }

            return oObtUsers;
        }


        //Este método es para verificar si el usuario existe dentro de la tabla de usuarios general
        public bool ValidacionUsuario(string correo)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Verificar_Usuario_adm", conexion);
                    cmd.Parameters.AddWithValue("correo", correo);
                    cmd.CommandType = CommandType.StoredProcedure;
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


        public UsuariosModel ObtenerImg(int IDUser)
        {
            var oObtUsers = new UsuariosModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_imgusuario_adm", conexion);
                cmd.Parameters.AddWithValue("IDusuarioEcomm", IDUser);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oObtUsers.IDusuario = Convert.ToInt32(dr["IDusuarioRealEcommRel"]);
                        oObtUsers.imgPerfilRuta = dr["imgPath"].ToString();

                    }
                }
            }

            return oObtUsers;
        }

        //Actualización de usuario 
        public bool Actualizacion(UsuariosModel usuarios)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Editar_usuario_adm", conexion);
                    cmd.Parameters.AddWithValue("IDusuario", usuarios.IDusuario);
                    cmd.Parameters.AddWithValue("nombre", usuarios.nombreUsuario);
                    cmd.Parameters.AddWithValue("apellidoP", usuarios.apellidoP);
                    cmd.Parameters.AddWithValue("apellidoM", usuarios.apellidoM);
                    cmd.Parameters.AddWithValue("email", usuarios.email);
                    cmd.Parameters.AddWithValue("telefono", usuarios.tel);
                    cmd.Parameters.AddWithValue("celular", usuarios.cel);
                    cmd.Parameters.AddWithValue("rfc", usuarios.rfc);
                    cmd.Parameters.AddWithValue("curp", usuarios.curp);
                    cmd.Parameters.AddWithValue("rol", usuarios.IDrol);
                    cmd.Parameters.AddWithValue("clabe", usuarios.clabe);
                    cmd.Parameters.AddWithValue("IDBanco", usuarios.banco);

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

        public bool Eliminar(int IDUsuario)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Eliminar_togg_usuario", conexion);
                    cmd.Parameters.AddWithValue("IDusuarioRealEcomm", IDUsuario);
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

        //Inactivar usuarios
        public bool ChangeStatus(int IDUsuario, string status)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Cambiar_togg_usuario", conexion);
                    cmd.Parameters.AddWithValue("IDusuarioRealEcomm", IDUsuario);
                    cmd.Parameters.AddWithValue("StatusReg", status);
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


