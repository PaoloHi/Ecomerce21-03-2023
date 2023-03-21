using RealStateGestion.Models;
using System.Data;
using System.Data.SqlClient;

namespace RealStateGestion.Datos.Center
{

    public class PropiedadesCenter
    {

        //Este metodo es para listar las propiedades que están publicadas ya dentro del ecommerce
        public List<PropiedadesModelPropiedad> ListarPropiedades(string Estatus)
        {
            var oListaPropiedades = new List<PropiedadesModelPropiedad>();
            var vistas = 0;
            var visitas = 0;

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_propiedades_tab", conexion);
                cmd.Parameters.AddWithValue("Estatus", Estatus);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        if (Convert.ToInt32(dr["Vistas"]) == null)
                        {
                            vistas = 0;
                        }
                        else
                        {
                            vistas = Convert.ToInt32(dr["Vistas"]);
                        }

                        if (Convert.ToInt32(dr["Visitas"]) == null)
                        {
                            visitas = 0;
                        }
                        else
                        {
                            visitas = Convert.ToInt32(dr["Visitas"]);
                        }
                        oListaPropiedades.Add(new PropiedadesModelPropiedad()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            IDPropiedad = Convert.ToInt32(dr["IDPropiedad"]),
                            IDPropiedadG = dr["PropiedadID"].ToString(),
                            Titulo = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                            ISO = dr["ISO"].ToString(),
                            Precio = dr["Precio"].ToString(),
                            Superficie = dr["Superficie"].ToString(),
                            Direccion = dr["Direccion"].ToString(),
                            UM = dr["Um"].ToString(),
                            Abrev = dr["Abrev"].ToString(),
                            Status = dr["StatusPropiedad"].ToString(),
                            Visitas = visitas,
                            Vistas = vistas,
                            FPublicacion = dr["FPublicacion"].ToString(),
                        });
                    }
                }
            }

            return oListaPropiedades;
        }

        //Este metodo es para listar las propiedades que están publicadas ya dentro del ecommerce
        public List<ImagenPropiedades> ListarImagenesProp(string Estatus)
        {

            var oListaImagenes = new List<ImagenPropiedades>();
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Obtener_propiedadesImgtab_adm", conexion);
                    cmd.Parameters.AddWithValue("Estatus", Estatus);
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


        //


        //Este metodo es para listar las propiedades que no están publicadas dentro del ecommerce 

        public List<PropiedadesModelTipoPropiedad> ListarTipoProp()
        {

            var oListaPropiedadesTipo = new List<PropiedadesModelTipoPropiedad>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Listar_TipoProp_adm", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaPropiedadesTipo.Add(new PropiedadesModelTipoPropiedad()
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

        public List<PropiedadesModelSubTipoPropiedad> ListarSubtipoProp(int IDTipo)
        {

            var oListaPropiedadesSubtipo = new List<PropiedadesModelSubTipoPropiedad>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Listar_SubtipoProp_adm", conexion);
                cmd.Parameters.AddWithValue("IDTipo", IDTipo);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaPropiedadesSubtipo.Add(new PropiedadesModelSubTipoPropiedad()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            IDtipoP = Convert.ToInt32(dr["IDTipo"]),
                            IDsubtipoP = Convert.ToInt32(dr["IDSubtipo"]),
                            Subtipo = dr["Subtipo"].ToString(),


                        });
                    }
                }
            }

            return oListaPropiedadesSubtipo;
        }

        public List<PropiedadesModelMoneda> ListarMoneda()
        {

            var oListaMoneda = new List<PropiedadesModelMoneda>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Listar_Moneda_adm", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaMoneda.Add(new PropiedadesModelMoneda()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            IDMoneda = Convert.ToInt32(dr["IDmoneda"]),
                            Moneda = dr["moneda"].ToString(),
                            ISO = dr["iso"].ToString(),

                        });
                    }
                }
            }

            return oListaMoneda;
        }

        //Lista de las caracteristicas de las propiedades CHECK 

        public List<PropiedadesModelCaracteristicasCheck> ListarCarct()
        {

            var oListaCaract = new List<PropiedadesModelCaracteristicasCheck>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Listar_CaractCheck_adm", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaCaract.Add(new PropiedadesModelCaracteristicasCheck()
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

        public List<PropiedadesModelSuelo> ListarSuelo()
        {

            var oListaCaract = new List<PropiedadesModelSuelo>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Listar_UsoSuelo_adm", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaCaract.Add(new PropiedadesModelSuelo()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            IDSuelo = Convert.ToInt32(dr["IDSuelo"]),
                            Suelo = dr["Suelo"].ToString(),


                        }); ;
                    }
                }
            }

            return oListaCaract;
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

        //Función de guardado de propiedad
        public bool GuardarPropiedad(PropiedadesModel oPropiedad)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar_PropiedadRealEcomm", conexion);

                    // Generacion de PropiedadID aleatorio
                    Random rdn = new Random();
                    string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@";
                    int longitud = caracteres.Length;
                    char letra;
                    int longitudPropiedadID = 20;
                    string AleatorioPropiedadID = string.Empty;
                    for (int i = 0; i < longitudPropiedadID; i++)
                    {
                        letra = caracteres[rdn.Next(longitud)];
                        AleatorioPropiedadID += letra.ToString();
                    }

                    cmd.Parameters.AddWithValue("PropiedadID", AleatorioPropiedadID);
                    cmd.Parameters.AddWithValue("nombre", oPropiedad.NombrePropiedad);
                    cmd.Parameters.AddWithValue("descripcion", oPropiedad.Descripcion);
                    cmd.Parameters.AddWithValue("IDoperacionPropRel", oPropiedad.Operacion);
                    cmd.Parameters.AddWithValue("IDtipoProRel", oPropiedad.TPropiedad);
                    cmd.Parameters.AddWithValue("IDsubtipoPropRel", oPropiedad.SubPropiedad);
                    cmd.Parameters.AddWithValue("IDmonedaRel", oPropiedad.Moneda);
                    cmd.Parameters.AddWithValue("precio", oPropiedad.Precio);
                    cmd.Parameters.AddWithValue("IDperiodoRentaPropRel", oPropiedad.PeriodoRenta);
                    cmd.Parameters.AddWithValue("mantenimiento", oPropiedad.MantenimientoPrecio);
                    cmd.Parameters.AddWithValue("IDperiodoMtoPropRel", oPropiedad.PeriodoMantenimiento);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();


                    // Obtener ID de la propiedad con el dato de PropiedadID (texto random)

                    SqlCommand oIdpropiedad = new SqlCommand("sp_Obtener_idPropiedadRealEcomm", conexion);

                    oIdpropiedad.Parameters.AddWithValue("PropiedadID", AleatorioPropiedadID);

                    oIdpropiedad.CommandType = CommandType.StoredProcedure;

                    var oValorIDpropiedad = new PropiedadesModel();

                    using (var id = oIdpropiedad.ExecuteReader())
                    {
                        while (id.Read())
                        {
                            oValorIDpropiedad.IDPropiedad = Convert.ToInt32(id["IDpropiedadRealEcomm"]);
                        }
                    }


                    // Obtener ID de la colonia con los datos ingresados
                    SqlCommand oIdcolonia = new SqlCommand("sp_Obtener_idcoloniaUbicacion", conexion);

                    oIdcolonia.Parameters.AddWithValue("estado", oPropiedad.Estado);
                    oIdcolonia.Parameters.AddWithValue("codigoPostal", oPropiedad.CP);
                    oIdcolonia.Parameters.AddWithValue("municipio", oPropiedad.Municipio);
                    oIdcolonia.Parameters.AddWithValue("colonia", oPropiedad.Colonia);


                    oIdcolonia.CommandType = CommandType.StoredProcedure;

                    var oValorIDcolonia = new PropiedadesModel();

                    using (var id = oIdcolonia.ExecuteReader())
                    {
                        while (id.Read())
                        {
                            oValorIDcolonia.IDcolonia = Convert.ToInt32(id["IDcolonia"]);
                        }
                    }

                    // Guardar ubicacion de la propiedad
                    SqlCommand gUbicacion = new SqlCommand("sp_Guardar_UbicacionProp", conexion);

                    gUbicacion.Parameters.AddWithValue("IDpropiedadRealEcommRel", oValorIDpropiedad.IDPropiedad);
                    gUbicacion.Parameters.AddWithValue("calle", oPropiedad.Calle);
                    gUbicacion.Parameters.AddWithValue("IDcoloniaRel", 1007);
                    gUbicacion.Parameters.AddWithValue("no_interior", oPropiedad.NoInt);
                    gUbicacion.Parameters.AddWithValue("no_exterior", oPropiedad.NoExt);
                    gUbicacion.Parameters.AddWithValue("latitud", oPropiedad.txtLat);
                    gUbicacion.Parameters.AddWithValue("longitud", oPropiedad.txtLng);
                    gUbicacion.Parameters.AddWithValue("direccion_completa", oPropiedad.DireccionC);

                    gUbicacion.CommandType = CommandType.StoredProcedure;
                    gUbicacion.ExecuteNonQuery();

                    //Guardado de datos de publicación 
                    SqlCommand gPublicacion = new SqlCommand("sp_Guardar_PublicacionProp", conexion);

                    gPublicacion.Parameters.AddWithValue("IDpropiedadRealEcomm", oValorIDpropiedad.IDPropiedad);
                    gPublicacion.CommandType = CommandType.StoredProcedure;
                    gPublicacion.ExecuteNonQuery();


                    //Guardado de Construccion

                    SqlCommand gConstruccion = new SqlCommand("sp_Guardar_construccionProp", conexion);

                    gConstruccion.Parameters.AddWithValue("IDpropiedadRealEcommRel", oValorIDpropiedad.IDPropiedad);
                    gConstruccion.Parameters.AddWithValue("antiguedad", oPropiedad.Antiguedad);
                    gConstruccion.Parameters.AddWithValue("IDusoSueloPropRel", oPropiedad.UsoSuelo);
                    gConstruccion.Parameters.AddWithValue("superficie", oPropiedad.Superficie);
                    gConstruccion.Parameters.AddWithValue("IDuMTerrenoPropRel", oPropiedad.SuperficieUMedida);
                    gConstruccion.Parameters.AddWithValue("superficieConstruida", oPropiedad.SuperficieContruccion);
                    gConstruccion.Parameters.AddWithValue("IDuMTConstruidaRel", oPropiedad.SuperficieContruccionUMedida);

                    gConstruccion.CommandType = CommandType.StoredProcedure;
                    gConstruccion.ExecuteNonQuery();

                    //Guardado de Caracteristicas

                    SqlCommand gCaracteristicasRecamaras = new SqlCommand("sp_Guardar_amenidadesProp", conexion);

                    gCaracteristicasRecamaras.Parameters.AddWithValue("IDpropiedadRealEcomm", oValorIDpropiedad.IDPropiedad);
                    gCaracteristicasRecamaras.Parameters.AddWithValue("IDCaractProp", 1);
                    gCaracteristicasRecamaras.Parameters.AddWithValue("numeroElementos", oPropiedad.Recamaras);

                    gCaracteristicasRecamaras.CommandType = CommandType.StoredProcedure;
                    gCaracteristicasRecamaras.ExecuteNonQuery();


                    SqlCommand gCaracteristicasEstacionamiento = new SqlCommand("sp_Guardar_amenidadesProp", conexion);

                    gCaracteristicasEstacionamiento.Parameters.AddWithValue("IDpropiedadRealEcomm", oValorIDpropiedad.IDPropiedad);
                    gCaracteristicasEstacionamiento.Parameters.AddWithValue("IDCaractProp", 2);
                    gCaracteristicasEstacionamiento.Parameters.AddWithValue("numeroElementos", oPropiedad.Estacionamiento);

                    gCaracteristicasEstacionamiento.CommandType = CommandType.StoredProcedure;
                    gCaracteristicasEstacionamiento.ExecuteNonQuery();

                    SqlCommand gCaracteristicasBanos = new SqlCommand("sp_Guardar_amenidadesProp", conexion);

                    gCaracteristicasBanos.Parameters.AddWithValue("IDpropiedadRealEcomm", oValorIDpropiedad.IDPropiedad);
                    gCaracteristicasBanos.Parameters.AddWithValue("IDCaractProp", 3);
                    gCaracteristicasBanos.Parameters.AddWithValue("numeroElementos", oPropiedad.Banos);

                    gCaracteristicasBanos.CommandType = CommandType.StoredProcedure;
                    gCaracteristicasBanos.ExecuteNonQuery();

                    SqlCommand gCaracteristicasMBanos = new SqlCommand("sp_Guardar_amenidadesProp", conexion);

                    gCaracteristicasMBanos.Parameters.AddWithValue("IDpropiedadRealEcomm", oValorIDpropiedad.IDPropiedad);
                    gCaracteristicasMBanos.Parameters.AddWithValue("IDCaractProp", 4);
                    gCaracteristicasMBanos.Parameters.AddWithValue("numeroElementos", oPropiedad.MediosBanos);

                    gCaracteristicasMBanos.CommandType = CommandType.StoredProcedure;
                    gCaracteristicasMBanos.ExecuteNonQuery();


                    foreach (var caracteristica in oPropiedad.CaracteristicasL)
                    {
                        SqlCommand gCaracteristicas = new SqlCommand("sp_Guardar_amenidadesProp", conexion);

                        gCaracteristicas.Parameters.AddWithValue("IDpropiedadRealEcomm", oValorIDpropiedad.IDPropiedad);
                        gCaracteristicas.Parameters.AddWithValue("IDCaractProp", caracteristica.IDCaracteristica);
                        gCaracteristicas.Parameters.AddWithValue("isTrue", caracteristica.isSelected);

                        gCaracteristicas.CommandType = CommandType.StoredProcedure;
                        gCaracteristicas.ExecuteNonQuery();
                    }

                    SqlCommand gCaracteristicasBodegas = new SqlCommand("sp_Guardar_amenidadesProp", conexion);

                    gCaracteristicasBodegas.Parameters.AddWithValue("IDpropiedadRealEcomm", oValorIDpropiedad.IDPropiedad);
                    gCaracteristicasBodegas.Parameters.AddWithValue("IDCaractProp", 27);
                    gCaracteristicasBodegas.Parameters.AddWithValue("numeroElementos", oPropiedad.Bodegas);

                    gCaracteristicasBodegas.CommandType = CommandType.StoredProcedure;
                    gCaracteristicasBodegas.ExecuteNonQuery();

                    SqlCommand gCaracteristicasClosets = new SqlCommand("sp_Guardar_amenidadesProp", conexion);

                    gCaracteristicasClosets.Parameters.AddWithValue("IDpropiedadRealEcomm", oValorIDpropiedad.IDPropiedad);
                    gCaracteristicasClosets.Parameters.AddWithValue("IDCaractProp", 28);
                    gCaracteristicasClosets.Parameters.AddWithValue("numeroElementos", oPropiedad.Closets);

                    gCaracteristicasClosets.CommandType = CommandType.StoredProcedure;
                    gCaracteristicasClosets.ExecuteNonQuery();

                    SqlCommand gCaracteristicasElevadores = new SqlCommand("sp_Guardar_amenidadesProp", conexion);

                    gCaracteristicasElevadores.Parameters.AddWithValue("IDpropiedadRealEcomm", oValorIDpropiedad.IDPropiedad);
                    gCaracteristicasElevadores.Parameters.AddWithValue("IDCaractProp", 29);
                    gCaracteristicasElevadores.Parameters.AddWithValue("numeroElementos", oPropiedad.Elevadores);

                    gCaracteristicasElevadores.CommandType = CommandType.StoredProcedure;
                    gCaracteristicasElevadores.ExecuteNonQuery();


                    // Guardado de imagenes y documentos

                    var pathCarpetaIMG = System.IO.Path.Combine("ImagenesPropiedad");

                    var pathCarpetaProp = System.IO.Path.Combine(/*pathCarpetaIMG,*/ AleatorioPropiedadID);

                    System.IO.Directory.CreateDirectory(pathCarpetaProp);

                    // Obteniendo imagenes para guardarlas

                    if (oPropiedad.Files.Count > 0)
                    {
                        foreach (var file in oPropiedad.Files)
                        {
                            // Carpeta en donde se debera de guardar la imagenes
                            string path = Path.Combine(Directory.GetCurrentDirectory(), /*pathCarpetaIMG,*/ AleatorioPropiedadID);

                            //string nombreImg = AleatorioPropiedadID;

                            Guid g = Guid.NewGuid();

                            //Asignacion de nombre a las imagenes
                            string nImgFileNombre = g + Path.GetExtension(file.FileName);

                            string fileNameWithPath = Path.Combine(path, nImgFileNombre);

                            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }

                            // datos a mandar a la base de datos
                            string rutaImgProp = AleatorioPropiedadID + "/" + nImgFileNombre;
                            string extImgProp = Path.GetExtension(file.FileName);
                            FileInfo sizeImgProp = new FileInfo(fileNameWithPath);

                            // Guardado de la infromacion de iamgenes en la base de datos

                            SqlCommand gImgProp = new SqlCommand("sp_Guardar_imgProp", conexion);

                            gImgProp.Parameters.AddWithValue("IDpropiedadRealEcommRel", oValorIDpropiedad.IDPropiedad);
                            gImgProp.Parameters.AddWithValue("imgName", g);
                            gImgProp.Parameters.AddWithValue("imgSize", sizeImgProp.Length);
                            gImgProp.Parameters.AddWithValue("extension", extImgProp);
                            gImgProp.Parameters.AddWithValue("imgPath", rutaImgProp);

                            gImgProp.CommandType = CommandType.StoredProcedure;
                            gImgProp.ExecuteNonQuery();
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

            //if( rpta == true)
            //{
            //    //var oPrevisualizacion = new PropiedadesModel();

            //    //oPrevisualizacion.NombrePropiedad = oPropiedad.NombrePropiedad;


            //    return rpta;
            //} else
            //{
            //    return ;
            //}

            return rpta;
        }


    }

}

