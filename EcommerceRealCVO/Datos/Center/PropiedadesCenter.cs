using EcommerceRealCVO.Models;
using System.Data;
using System.Data.SqlClient;

namespace EcommerceRealCVO.Datos.Center
{
    public class PropiedadesCenter
    {

        /************************************PROPIEDADES SIN FILTRO*****************************************/
        //Este método es para listar todas las propiedades dentro de Ecommerce sin que exista un filtro
        public List<PropiedadesModelPropiedad> ListarPropiedades()
        {

            var oListaPropiedades = new List<PropiedadesModelPropiedad>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_propiedades_ecomm", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaPropiedades.Add(new PropiedadesModelPropiedad()
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
                            Abrev = dr["Abrev"].ToString(),
                            Latitud = dr["Latitud"].ToString(),
                            Longitud = dr["Longitud"].ToString()
                        });
                    }
                }
            }

            return oListaPropiedades;
        }

        //Método para obtener las características de todas las propiedades
        public List<CaracteristicasP> ListarCaracteristicas()
        {

            var oListaPropiedades = new List<CaracteristicasP>();
            var cn = new Conexion();
            var elementos = 0;

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_caractP_ecomm", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {

                        oListaPropiedades.Add(new CaracteristicasP()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            IDPropiedad = Convert.ToInt32(dr["IDPropiedad"]),
                            Caracteristica = dr["Caracteristica"].ToString(),
                            NoElementos = Convert.ToInt32(dr["NoElementos"])

                        });
                    }
                }
            }

            return oListaPropiedades;
        }

        //Método para obtener la lista de las imagenes todas las propiedades
        public List<ImagenPropiedades> ListarImagenesProp()
        {

            var oListaImagenes = new List<ImagenPropiedades>();
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Obtener_propiedadesimg_ecomm", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            oListaImagenes.Add(new ImagenPropiedades()
                            {
                                /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                                proviene de la base de datos*/

                                IDpropiedad = Convert.ToInt32(dr["IDPropiedad"]),
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

        /************************************PROPIEDADES CON FILTRO SENCILLO*****************************************/

        //Este método es para listas las propiedades que tienen filtro de la página principal 
        public List<PropiedadesModelPropiedad> ListarPropiedadesB(List<ParametrosFiltro>? parametrosFiltros)
        {
            var Operacion = "";
            var General = "";
            int Tipo = 0;

            foreach (var datos in parametrosFiltros)
            {

                if (datos.Parametro == "Ac")
                {
                    Operacion = datos.Valor;
                }
                if (datos.Parametro == "Tipo")
                {
                    Tipo = (int)datos.ValorT;
                }
                if (datos.Parametro == "Gen")
                {
                    General = datos.Valor;
                }
            }
            /*if (General == null)
            {
                General = "";
            }

            if (Tipo == null)
            {
                Tipo = 0;
            }*/
            var oListaPropiedades = new List<PropiedadesModelPropiedad>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_propiedadesB_ecomm", conexion);
                cmd.Parameters.AddWithValue("Accion", Operacion);
                cmd.Parameters.AddWithValue("Tipo", Tipo);
                cmd.Parameters.AddWithValue("General", General);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaPropiedades.Add(new PropiedadesModelPropiedad()
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
                            Abrev = dr["Abrev"].ToString(),
                            Latitud = dr["Latitud"].ToString(),
                            Longitud = dr["Longitud"].ToString()
                        });
                    }
                }
            }

            return oListaPropiedades;
        }

        //Método para obtener las características de todas las propiedades
        public List<CaracteristicasP> ListarCaracteristicasB(List<ParametrosFiltro>? parametrosFiltros)
        {

            var oListaCaracteristicas = new List<CaracteristicasP>();
            var Operacion = "";
            var General = "";
            int Tipo = 0;

            foreach (var datos in parametrosFiltros)
            {

                if (datos.Parametro == "Ac")
                {
                    Operacion = datos.Valor;
                }
                if (datos.Parametro == "Tipo")
                {
                    Tipo = (int)datos.ValorT;
                }
                if (datos.Parametro == "Gen")
                {
                    General = datos.Valor;
                }
            }

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_caractB_ecomm", conexion);
                cmd.Parameters.AddWithValue("Accion", Operacion);
                cmd.Parameters.AddWithValue("Tipo", Tipo);
                cmd.Parameters.AddWithValue("General", General);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaCaracteristicas.Add(new CaracteristicasP()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            IDPropiedad = Convert.ToInt32(dr["IDPropiedad"]),
                            Caracteristica = dr["Caracteristica"].ToString(),
                            NoElementos = Convert.ToInt32(dr["NoElementos"])
                        });
                    }
                }
            }

            return oListaCaracteristicas;
        }


        //Método para obtener la lista de las imagenes de las propiedades con el filtro inicial
        public List<ImagenPropiedades> ListarImagenesPropB(List<ParametrosFiltro>? parametrosFiltros)
        {
            var oListaImagenes = new List<ImagenPropiedades>();
            var Operacion = "";
            var General = "";
            int Tipo = 0;

            foreach (var datos in parametrosFiltros)
            {

                if (datos.Parametro == "Ac")
                {
                    Operacion = datos.Valor;
                }
                if (datos.Parametro == "Tipo")
                {
                    Tipo = (int)datos.ValorT;
                }
                if (datos.Parametro == "Gen")
                {
                    General = datos.Valor;
                }
            }


            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Obtener_propiedadesimgB_ecomm", conexion);
                    cmd.Parameters.AddWithValue("Accion", Operacion);
                    cmd.Parameters.AddWithValue("Tipo", Tipo);
                    cmd.Parameters.AddWithValue("General", General);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oListaImagenes.Add(new ImagenPropiedades()
                            {
                                /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                                proviene de la base de datos*/

                                IDpropiedad = Convert.ToInt32(dr["IDPropiedad"]),
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


        /************************************PROPIEDADES CON FILTRO COMBINADO*****************************************/
        //Este método es para listar todas las propiedades dentro de Ecommerce con filtro compuesto desde la pantalla de propiedades
        public List<PropiedadesModelPropiedad> ListarPropiedadesFC(List<int> IDPropiedades)
        {

            var oListaPropiedades = new List<PropiedadesModelPropiedad>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_propiedadesFiltro_ecomm", conexion);

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
                        oListaPropiedades.Add(new PropiedadesModelPropiedad()
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
                            Abrev = dr["Abrev"].ToString(),
                            Latitud = dr["Latitud"].ToString(),
                            Longitud = dr["Longitud"].ToString()
                        });
                    }
                }
            }
            return oListaPropiedades;
        }

        //Método para obtener la lista de las imagenes todas las propiedades con filtro compuesto desde la pantalla de propiedades
        public List<ImagenPropiedades> ListarImagenesPropFC(List<int> IDPropiedades)
        {
            var oListaImagenes = new List<ImagenPropiedades>();
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Obtener_propiedadesimgFiltro_ecomm", conexion);
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
                            oListaImagenes.Add(new ImagenPropiedades()
                            {
                                /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                                proviene de la base de datos*/

                                IDpropiedad = Convert.ToInt32(dr["IDPropiedad"]),
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

        /************************************TIPOS*****************************************/
        //Este método es para listar las propiedades cuando sea aplicado sólo el filtro de TIPO
        public List<PropiedadesModelPropiedad> ListarPropiedadesTipo(string? Tipo)
        {

            var oListaPropiedades = new List<PropiedadesModelPropiedad>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_propiedadesInv_ecomm", conexion);
                cmd.Parameters.AddWithValue("Tipo", Tipo);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaPropiedades.Add(new PropiedadesModelPropiedad()
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

        //Método para obtener la lista de las imagenes todas las propiedades
        public List<ImagenPropiedades> ListarImagenesPropTipo(string? Tipo)
        {

            var oListaImagenes = new List<ImagenPropiedades>();
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Obtener_propiedadInvImg_ecomm", conexion);
                    cmd.Parameters.AddWithValue("Tipo", Tipo);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            oListaImagenes.Add(new ImagenPropiedades()
                            {
                                /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                                proviene de la base de datos*/

                                IDpropiedad = Convert.ToInt32(dr["IDPropiedad"]),
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

        //Método para traer los check de las características para el filtrado
        public List<CaracteristicasCheck> ListarCarct(string? Clasificacion)
        {

            var oListaCaract = new List<CaracteristicasCheck>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Listar_CaractCheck_ecom", conexion);
                cmd.Parameters.AddWithValue("Clasificacion", Clasificacion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaCaract.Add(new CaracteristicasCheck()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            IDTCaracteristica = Convert.ToInt32(dr["IDTipoCaract"]),
                            TipoCaract = dr["TipoC"].ToString(),
                            IDCaracteristica = Convert.ToInt32(dr["IDCaracteristica"]),
                            Caracteristica = dr["Caracteristica"].ToString(),
                            //Orden = Convert.ToInt32(dr["orden"]),
                            isSelected = false

                        }); ;
                    }
                }
            }

            return oListaCaract;
        }

        /****************************FILTROS DE ECOMMERCE*****************************************/
        public List<TipoPropiedadP> ListarTipoProp()
        {

            var oListaPropiedadesTipo = new List<TipoPropiedadP>();

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
                        oListaPropiedadesTipo.Add(new TipoPropiedadP()
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

        public List<PropiedadesModelUMedida> ListarUMTerreno()
        {

            var oListaUM = new List<PropiedadesModelUMedida>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Listar_UMTerreno_adm", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaUM.Add(new PropiedadesModelUMedida()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            IDUm = Convert.ToInt32(dr["IDUm"]),
                            UnidadMedida = dr["UnidadM"].ToString(),

                        }); ;
                    }
                }
            }

            return oListaUM;
        }

        //Método para traer las propiedades sugeridas una vez dentro de la propiedad
        public List<PropiedadesModelFiltro> LPropiedadesF(PropiedadesModel oPropiedadL, List<int>? amenidadesSelec)
        {
            var oListaPropiedades = new List<PropiedadesModelFiltro>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Listar_propiedadesFiltro_ecomm", conexion);
                var dt = new DataTable();
                dt.Columns.Add("IDPropiedad", typeof(int));

                foreach (var IDPropiedadBD in amenidadesSelec)
                {
                    dt.Rows.Add(IDPropiedadBD);
                }

                //cmd.Parameters.AddWithValue("@IDAmenidades", dt);
                cmd.Parameters.AddWithValue("Operacion", oPropiedadL.Operacion);
                //cmd.Parameters.AddWithValue("Ubicacion", oPropiedadL.BUbicacion);
                //cmd.Parameters.AddWithValue("TPropiedad", oPropiedadL.TPropiedadP);
                //cmd.Parameters.AddWithValue("Superficie", oPropiedadL.SuperficieCheck);
                //cmd.Parameters.AddWithValue("DesdeS", oPropiedadL.SDesde);
                //cmd.Parameters.AddWithValue("HastaS", oPropiedadL.SHasta);
                //cmd.Parameters.AddWithValue("Recamaras", oPropiedadL.Recamaras);
                //cmd.Parameters.AddWithValue("Estacionamientos", oPropiedadL.Estacionamientos);
                //cmd.Parameters.AddWithValue("Banos", oPropiedadL.Banos);
                //cmd.Parameters.AddWithValue("MBanos", oPropiedadL.MBanos);
                //cmd.Parameters.AddWithValue("Bodegas", oPropiedadL.Bodegas);
                //cmd.Parameters.AddWithValue("Closets", oPropiedadL.Closets);
                //cmd.Parameters.AddWithValue("Elevadores", oPropiedadL.Elevadores);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oListaPropiedades.Add(new PropiedadesModelFiltro()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            IDPropiedad = Convert.ToInt32(dr["IDPropiedad"]),
                        });
                    }
                }
            }

            return oListaPropiedades;
        }


        /****************************LEADS ****************************************/
        public bool LeadInteresadoMI(PropiedadesModel oMI)
        {
           string fecha;
          
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar_LeadI_ecomm", conexion);
                    if (oMI.FechaMI == null)
                    {
                       
                        cmd.Parameters.AddWithValue("FechaC", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("FechaC", oMI.FechaMI);
                    }
                    cmd.Parameters.AddWithValue("Nombre", oMI.NombreMI);
                    cmd.Parameters.AddWithValue("APP", oMI.ApellidoPMI);
                    cmd.Parameters.AddWithValue("APM", oMI.ApellidoMMI);
                    cmd.Parameters.AddWithValue("NoTel", oMI.NumeroTMI);
                    cmd.Parameters.AddWithValue("Correo", oMI.CorreoMI);
                    cmd.Parameters.AddWithValue("IDPropiedad", oMI.IDPropiedad);
                    
                    cmd.Parameters.AddWithValue("TipoC", oMI.TipoLI);

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

        public bool LeadInteresadoAV(PropiedadesModel oMI)
        {
            
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar_LeadI_ecomm", conexion);
                    cmd.Parameters.AddWithValue("Nombre", oMI.NombreMI);
                    cmd.Parameters.AddWithValue("APP", oMI.ApellidoPMI);
                    cmd.Parameters.AddWithValue("APM", oMI.ApellidoMMI);
                    cmd.Parameters.AddWithValue("NoTel", oMI.NumeroTMI);
                    cmd.Parameters.AddWithValue("Correo", oMI.CorreoMI);
                    cmd.Parameters.AddWithValue("IDPropiedad", oMI.IDPropiedad);
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
