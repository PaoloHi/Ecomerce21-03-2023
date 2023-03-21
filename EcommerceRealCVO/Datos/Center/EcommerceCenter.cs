using System.Data;
using System.Data.SqlClient;
using EcommerceRealCVO.Models;

namespace EcommerceRealCVO.Datos.Center
{
    public class EcommerceCenter
    {
        //Este metodo funciona para listar los tipos de propiedadepropiedades dentro del buscador en la parte inicial del Ecommerce
        public List<TipoPropiedad> ListarTipoProp()
        {

            var oListaPropiedadesTipo = new List<TipoPropiedad>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Listar_TipoProp_ecomm", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaPropiedadesTipo.Add(new TipoPropiedad()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            IDtipoP = Convert.ToInt32(dr["IDTipo"]),
                            Tipo = dr["tipo"].ToString(),


                        });
                    }
                }
            }

            return oListaPropiedadesTipo;
        }

        //Este metodo funciona para listar las propiedades destacadas dentro de la parte principal del ecommerce
        public List<PropiedadesDestacadas> ListarPropDestacadas()
        {

            var oListaPropDestacadas = new List<PropiedadesDestacadas>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_propiedadesDest_ecomm", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaPropDestacadas.Add(new PropiedadesDestacadas()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            IDpropiedad = Convert.ToInt32(dr["IDPropiedad"]),
                            IDpropiedadG = dr["PropiedadID"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            Tipo = dr["Tipo"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                            RutaImagen = dr["RutaImagen"].ToString()

                        });
                    }
                }
            }

            return oListaPropDestacadas;
        }

        //Método para listar todas las propiedades existentes dentro del Ecommerce
        public List<PropiedadesExistentes> ListarPropExistentes()
        {

            var oListaPropExistentes = new List<PropiedadesExistentes>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_propiedadesExis_ecomm", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaPropExistentes.Add(new PropiedadesExistentes()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/


                            Tipo = dr["Tipo"].ToString(),
                            Cantidad = dr["TOTAL"].ToString(),

                        });
                    }
                }
            }

            return oListaPropExistentes;
        }

        //Método para traer la propiedad seleccionada para visualizar 
        public PropiedadSeleccionada ObtenerDatosProp(string IDPropiedad)
        {
            var oObtDatosProp = new PropiedadSeleccionada();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_propiedadSel_ecomm", conexion);
                cmd.Parameters.AddWithValue("IDPropiedad", IDPropiedad);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    SqlCommand cmdUpdate = new SqlCommand("sp_Conteo_propiedadVistas_ecomm", conexion);
                    cmdUpdate.Parameters.AddWithValue("IDPropiedad", IDPropiedad);
                    cmdUpdate.CommandType = CommandType.StoredProcedure;
                    cmdUpdate.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    string error = e.Message;
                }

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oObtDatosProp.IDPropiedad = dr["PropiedadID"].ToString();
                        oObtDatosProp.Titulo = dr["Nombre"].ToString();
                        oObtDatosProp.Descripcion = dr["descripcion"].ToString();
                        oObtDatosProp.ISO = dr["ISO"].ToString();
                        oObtDatosProp.Precio = dr["precio"].ToString();
                        oObtDatosProp.Direccion = dr["Direccion"].ToString();
                        oObtDatosProp.Calle = dr["Calle"].ToString();
                        oObtDatosProp.NoInt = dr["NoInterior"].ToString();
                        oObtDatosProp.NoExt = dr["NoExterior"].ToString();
                        oObtDatosProp.Colonia = dr["Colonia"].ToString();
                        oObtDatosProp.Municipio = dr["Municipio"].ToString();
                        oObtDatosProp.Estado = dr["Estado"].ToString();
                        oObtDatosProp.Pais = dr["Pais"].ToString();
                        oObtDatosProp.CP = dr["CP"].ToString();
                        oObtDatosProp.Superficie = dr["Superficie"].ToString();
                        oObtDatosProp.UM = dr["UM"].ToString();
                        oObtDatosProp.SuperficieC = dr["SuperficieC"].ToString();
                        oObtDatosProp.UMC = dr["UMC"].ToString();
                        oObtDatosProp.Tipo = dr["Tipo"].ToString();
                        oObtDatosProp.Abrev = dr["Abrev"].ToString();
                        oObtDatosProp.Latitud = dr["Latitud"].ToString();
                        oObtDatosProp.Longitud = dr["Longitud"].ToString();

                    }

                }
            }

            return oObtDatosProp;

        }

        //Método para obtener la lista de las imagenes de la propiedad seleccionada 
        public List<ImagenPropiedad> ListarImagenProp(string IDPropiedad)
        {

            var oListaImagenes = new List<ImagenPropiedad>();
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Obtener_propiedadSelImg_ecomm", conexion);
                    Console.WriteLine("Se supone que este es el ID PROPIEDAD -----" + IDPropiedad);
                    cmd.Parameters.AddWithValue("@IDPropiedad", IDPropiedad);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            oListaImagenes.Add(new ImagenPropiedad()
                            {
                                /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                                proviene de la base de datos*/
                                RutaImagenPropiedad = dr["RutaImg"].ToString()


                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {

                string error = e.Message;

            }



            return oListaImagenes;
        }

        //Método para obtener las características de la propiedad seleccionada num
        public List<CaracteristicasPS> ListarCaracteristicasB(string IDPropiedad, string TipoC)
        {

            var oListaCaracteristicas = new List<CaracteristicasPS>();
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Obtener_propiedadCaractSel_ecomm", conexion);
                    cmd.Parameters.AddWithValue("IDPropiedad", IDPropiedad);
                    cmd.Parameters.AddWithValue("Tipo", TipoC);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            oListaCaracteristicas.Add(new CaracteristicasPS()
                            {
                                /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                                proviene de la base de datos*/

                                IDPropiedad = dr["IDPropiedad"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IDPropiedad"]),
                                Caracteristica = dr["Caracteristica"].ToString(),
                                NoElementos = dr["NoElementos"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NoElementos"]),
                                Seleccionado = dr["Seleccionado"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Seleccionado"]),
                                TipoCaract = dr["TipoCarac"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
            return oListaCaracteristicas;
        }


        //Método para obtener las características de la propiedad seleccionada check
        
        //Método para traer las propiedades sugeridas una vez dentro de la propiedad
        public List<PropiedadesSugeridas> ListarPropiedadesS(string IDPropiedad)
        {
            var oListaPropiedades = new List<PropiedadesSugeridas>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_propiedadesSug_ecomm", conexion);
                cmd.Parameters.AddWithValue("IDPropiedad", IDPropiedad);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oListaPropiedades.Add(new PropiedadesSugeridas()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            IDPropiedad = Convert.ToInt32(dr["IDPropiedad"]),
                            IDPropiedadG = dr["PropiedadID"].ToString(),
                            Titulo = dr["Nombre"].ToString(),
                            ISO = dr["ISO"].ToString(),
                            Precio = dr["Precio"].ToString(),
                            Superficie = dr["Superficie"].ToString(),
                            Direccion = dr["Direccion"].ToString(),
                            Tipo = dr["Tipo"].ToString(),
                            UM = dr["Um"].ToString(),
                            Abrev = dr["Abrev"].ToString()
                        });
                    }
                }
            }

            return oListaPropiedades;
        }

        //Método para obtener la lista de las imagenes de las propiedades sugeridas 
        public List<ImagenPropiedad> ListarImagenPropS(List<int> IDPropiedades)
        {

            var oListaImagenes = new List<ImagenPropiedad>();
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Obtener_propiedadesSugimg_ecomm", conexion);

                    var dt = new DataTable();
                    dt.Columns.Add("IDPropiedad", typeof(int));

                    foreach (var IDPropiedadBD in IDPropiedades)
                    {
                        dt.Rows.Add(IDPropiedadBD);
                    }

                    cmd.Parameters.AddWithValue("@IDPropiedades", dt);


                    cmd.CommandType = CommandType.StoredProcedure;


                    using (var dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            oListaImagenes.Add(new ImagenPropiedad()
                            {
                                /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                                proviene de la base de datos*/
                                IDPropiedad = Convert.ToInt32(dr["IDPropiedad"]),
                                RutaImagenPropiedad = dr["RutaImg"].ToString()


                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {

                string error = e.Message;

            }

            return oListaImagenes;
        }

        /************************************ Comprobación de ubicación *****************************************/
        //Método para comprobar que sea una ubicación y una característica general
        public List<UbicacionPropiedad> ComprobacionUbi(string Colonia, string CE)
        {
            var obtUbicacion = new List<UbicacionPropiedad>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_ubicacion_ecomm", conexion);
                //Envío de Ciudad o Estado
                cmd.Parameters.AddWithValue("CE", CE);
                //Envío de colonia 
                cmd.Parameters.AddWithValue("Colonia", Colonia);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        obtUbicacion.Add(new UbicacionPropiedad()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/
                            Municipio = dr["Municipio"].ToString(),
                            Estado = dr["Estado"].ToString(),

                        });
                    }

                }
            }

            return obtUbicacion;
        }

        // Método para guardar la información de Más información de la propiedad 
       
    }
}
