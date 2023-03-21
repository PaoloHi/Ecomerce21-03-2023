using adminRummet.Center;
using adminRummet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace adminRummet.Datos.Center
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
                        if (Convert.IsDBNull(dr["Vistas"]))
                        {
                            vistas = 0;
                            //whatever you need done with the count in here...
                        }

                        else
                        {
                            vistas = Convert.ToInt32(dr["Vistas"]);
                        }

                        if (Convert.IsDBNull(dr["Visitas"]))
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
                            UMC = dr["UMC"].ToString(),
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


                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oObtDatosProp.IDPropiedad = dr["IDPropiedad"].ToString();
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
                        oObtDatosProp.StatusPropiedad = dr["StatusPropiedad"].ToString();

                    }

                }
            }

            return oObtDatosProp;

        }


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

        // Este metodo es para listar las colonias que tenemos relacionadas con el IDColonia


        public List<Colonias> ListarColonias(string CodigoPostal) { 
            
            var oListaCol = new List<Colonias>();
            
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_obtenerColonias_CP", conexion);
                cmd.Parameters.AddWithValue("CodigoPostal",CodigoPostal);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oListaCol.Add(new Colonias ()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            IDColonia = Convert.ToInt32(dr["IDColonia"]),
                            Colonia = dr["Colonia"].ToString(),
                        }); ;
                    }
                }
            }

            return oListaCol;

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
                    string rummet = "Rummet";
                    string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@1234567890";
                    //string numeros = "1234567890";
                    int longitud = caracteres.Length;
                    //int longitudNums = numeros.Length;
                    char letra;
                    int longitudPropiedadID = 6;
                    string AleatorioPropiedadID = string.Empty;
                    for (int i = 0; i < longitudPropiedadID; i++)
                    {
                        letra = caracteres[rdn.Next(longitud)];
                        AleatorioPropiedadID += letra.ToString();

                    }
                    AleatorioPropiedadID = rummet + "-" + AleatorioPropiedadID; 
                    cmd.Parameters.AddWithValue("PropiedadID", AleatorioPropiedadID);
                    cmd.Parameters.AddWithValue("nombre", oPropiedad.NombrePropiedad);
                    cmd.Parameters.AddWithValue("descripcion", oPropiedad.Descripcion);
                    cmd.Parameters.AddWithValue("IDoperacionPropRel", oPropiedad.Operacion);
                    cmd.Parameters.AddWithValue("IDtipoProRel", oPropiedad.TPropiedad);
                    cmd.Parameters.AddWithValue("IDsubtipoPropRel", oPropiedad.SubPropiedad);
                    cmd.Parameters.AddWithValue("IDmonedaRel", oPropiedad.Moneda);
                    cmd.Parameters.AddWithValue("precio", oPropiedad.Precio);
                    cmd.Parameters.AddWithValue("IDperiodoRentaPropRel", oPropiedad.PeriodoRenta);
                    if (oPropiedad.MantenimientoPrecio == null)
                    {
                        cmd.Parameters.AddWithValue("mantenimiento", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("mantenimiento", oPropiedad.MantenimientoPrecio);
                    }

                    if (oPropiedad.PeriodoMantenimiento == null)
                    {
                        cmd.Parameters.AddWithValue("IDperiodoMtoPropRel", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("IDperiodoMtoPropRel", oPropiedad.PeriodoMantenimiento);
                    }




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
                    gUbicacion.Parameters.AddWithValue("IDcoloniaRel", oPropiedad.IDcolonia);
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
                    //gPublicacion.Parameters.AddWithValue("fechaPublicacion", oPropiedad.FechaPublicacion);

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

                    var pathCarpetaProp = System.IO.Path.Combine(pathCarpetaIMG, AleatorioPropiedadID);

                    System.IO.Directory.CreateDirectory(pathCarpetaProp);

                    // Obteniendo imagenes para guardarlas

                    if (oPropiedad.Files.Count > 0)
                    {
                        foreach (var file in oPropiedad.Files)
                        {
                            // Carpeta en donde se debera de guardar la imagenes
                            string path = Path.Combine(Directory.GetCurrentDirectory(), pathCarpetaIMG, AleatorioPropiedadID);

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

                    // Guardando Documentos

                    // Guardado documento Predial

                    var pathCarpetaDoc = System.IO.Path.Combine("DocumentosPropiedad");

                    var pathCarpetaPropDoc = System.IO.Path.Combine(pathCarpetaDoc, AleatorioPropiedadID);

                    System.IO.Directory.CreateDirectory(pathCarpetaPropDoc);

                    string pathDoc = Path.Combine(Directory.GetCurrentDirectory(), pathCarpetaDoc, AleatorioPropiedadID);

                    // Guardado documento Predial
                    if (oPropiedad.Predial != null)
                    {
                        //Obteniendo los Documentos
                        var Predial = oPropiedad.Predial;

                        //Generando nombre aleatroio
                        Guid gPredial = Guid.NewGuid();

                        string nDocFilePredial = gPredial + Path.GetExtension(Predial.FileName);

                        // Generando ruta para el guardado del documento
                        string filePredialWithPath = Path.Combine(pathDoc, nDocFilePredial);


                        // Guardado documentos en carpeta
                        Predial.CopyTo(new FileStream(filePredialWithPath, FileMode.Create));


                        // datos a mandar a la base de datos
                        string rutaPredialProp = /*"DocumentosPropiedad/" +*/ AleatorioPropiedadID + "/" + nDocFilePredial;
                        string extPredialProp = Path.GetExtension(Predial.FileName);
                        FileInfo sizePredialProp = new FileInfo(filePredialWithPath);


                        // Guardado de info de doc Predial
                        SqlCommand gPredialProp = new SqlCommand("sp_Guardar_docPropiedad_adm", conexion);

                        gPredialProp.Parameters.AddWithValue("IDpropiedadRealEcommRel", oValorIDpropiedad.IDPropiedad);
                        gPredialProp.Parameters.AddWithValue("IDtipoArchivoRel", 5);
                        gPredialProp.Parameters.AddWithValue("fileNombre", gPredial);
                        gPredialProp.Parameters.AddWithValue("fileSize", sizePredialProp.Length);
                        gPredialProp.Parameters.AddWithValue("extension", extPredialProp);
                        gPredialProp.Parameters.AddWithValue("filePath", rutaPredialProp);

                        gPredialProp.CommandType = CommandType.StoredProcedure;
                        gPredialProp.ExecuteNonQuery();
                    }
              

                    // Guardado documento Escritura
                    if (oPropiedad.Escritura != null)
                    {
                        //Obteniendo los Documentos
                        var Escritura = oPropiedad.Escritura;

                        //Generando nombre aleatorio
                        Guid gEscritura = Guid.NewGuid();

                        string nDocFileEscritura = gEscritura + Path.GetExtension(Escritura.FileName);

                        // Generando ruta para el guardado del documento
                        string fileEscrituraWithPath = Path.Combine(pathDoc, nDocFileEscritura);

                        // Guardando documento en carpeta
                        Escritura.CopyTo(new FileStream(fileEscrituraWithPath, FileMode.Create));

                        // datos a mandar a la base de datos
                        string rutaEscrituraProp = /*"DocumentosPropiedad/" +*/ AleatorioPropiedadID + "/" + nDocFileEscritura;
                        string extEscrituraProp = Path.GetExtension(Escritura.FileName);
                        FileInfo sizeEscrituraProp = new FileInfo(fileEscrituraWithPath);

                        // Guardado de info de doc Escritura
                        SqlCommand gEscrituraProp = new SqlCommand("sp_Guardar_docPropiedad_adm", conexion);

                        gEscrituraProp.Parameters.AddWithValue("IDpropiedadRealEcommRel", oValorIDpropiedad.IDPropiedad);
                        gEscrituraProp.Parameters.AddWithValue("IDtipoArchivoRel", 9);
                        gEscrituraProp.Parameters.AddWithValue("fileNombre", gEscritura);
                        gEscrituraProp.Parameters.AddWithValue("fileSize", sizeEscrituraProp.Length);
                        gEscrituraProp.Parameters.AddWithValue("extension", extEscrituraProp);
                        gEscrituraProp.Parameters.AddWithValue("filePath", rutaEscrituraProp);

                        gEscrituraProp.CommandType = CommandType.StoredProcedure;
                        gEscrituraProp.ExecuteNonQuery();
                    }
        
                    // Guardado documento Identificacion 
                    if (oPropiedad.Identificacion != null)
                    {
                        // Obteniendo documentos
                        var Identificacion = oPropiedad.Identificacion;

                        // Generando nombre aleatorio
                        Guid gIdentificacion = Guid.NewGuid();

                        string nDocFileIdentificacion = gIdentificacion + Path.GetExtension(Identificacion.FileName);

                        // Generando ruta para el guardado del documento
                        string fileIdentificacionWithPath = Path.Combine(pathDoc, nDocFileIdentificacion);

                        // Guardado documentos en carpeta
                        Identificacion.CopyTo(new FileStream(fileIdentificacionWithPath, FileMode.Create));


                        // datos a mandar a la base de datos

                        string rutaIdentificacionProp = /*"DocumentosPropiedad/" +*/ AleatorioPropiedadID + "/" + nDocFileIdentificacion;
                        string extIdentificacionProp = Path.GetExtension(Identificacion.FileName);
                        FileInfo sizeIdentificacionProp = new FileInfo(fileIdentificacionWithPath);

                        // Guardado de la infromacion de iamgenes en la base de datos



                        // Guarado de info de doc Identificacion
                        SqlCommand gIdentificacionProp = new SqlCommand("sp_Guardar_docPropiedad_adm", conexion);

                        gIdentificacionProp.Parameters.AddWithValue("IDpropiedadRealEcommRel", oValorIDpropiedad.IDPropiedad);
                        gIdentificacionProp.Parameters.AddWithValue("IDtipoArchivoRel", 4);
                        gIdentificacionProp.Parameters.AddWithValue("fileNombre", gIdentificacion);
                        gIdentificacionProp.Parameters.AddWithValue("fileSize", sizeIdentificacionProp.Length);
                        gIdentificacionProp.Parameters.AddWithValue("extension", extIdentificacionProp);
                        gIdentificacionProp.Parameters.AddWithValue("filePath", rutaIdentificacionProp);

                        gIdentificacionProp.CommandType = CommandType.StoredProcedure;
                        gIdentificacionProp.ExecuteNonQuery();

                    }

                    // Guardado documento Regimen 

                    if (oPropiedad.Regimen != null)
                    {
                        // Obteniendo documentos
                        var Regimen = oPropiedad.Regimen;

                        // Generando nombre aleatorio
                        Guid gRegimen = Guid.NewGuid();

                        string nDocFileRegimen = gRegimen + Path.GetExtension(Regimen.FileName);

                        // Generando ruta para el guardado del documento
                        string fileRegimenWithPath = Path.Combine(pathDoc, nDocFileRegimen);

                        // Guardado documentos en carpeta
                        Regimen.CopyTo(new FileStream(fileRegimenWithPath, FileMode.Create));


                        // datos a mandar a la base de datos

                        string rutaRegimenProp = /*"DocumentosPropiedad/" +*/ AleatorioPropiedadID + "/" + nDocFileRegimen;
                        string extRegimenProp = Path.GetExtension(Regimen.FileName);
                        FileInfo sizeRegimenProp = new FileInfo(fileRegimenWithPath);

                        // Guardado de la infromacion de iamgenes en la base de datos



                        // Guarado de info de doc Regimen
                        SqlCommand gRegimenProp = new SqlCommand("sp_Guardar_docPropiedad_adm", conexion);

                        gRegimenProp.Parameters.AddWithValue("IDpropiedadRealEcommRel", oValorIDpropiedad.IDPropiedad);
                        gRegimenProp.Parameters.AddWithValue("IDtipoArchivoRel", 10);
                        gRegimenProp.Parameters.AddWithValue("fileNombre", gRegimen);
                        gRegimenProp.Parameters.AddWithValue("fileSize", sizeRegimenProp.Length);
                        gRegimenProp.Parameters.AddWithValue("extension", extRegimenProp);
                        gRegimenProp.Parameters.AddWithValue("filePath", rutaRegimenProp);

                        gRegimenProp.CommandType = CommandType.StoredProcedure;
                        gRegimenProp.ExecuteNonQuery();

                    }

                    // Guardado documento No Gravamen
                    if (oPropiedad.NoGravamen != null)
                    {
                        // Obteniendo documentos
                        var NoGravamen = oPropiedad.NoGravamen;

                        // Generando nombre aleatorio
                        Guid gNoGravamen = Guid.NewGuid();

                        string nDocFileNoGravamen = gNoGravamen + Path.GetExtension(NoGravamen.FileName);

                        // Generando ruta para el guardado del documento
                        string fileNoGravamenWithPath = Path.Combine(pathDoc, nDocFileNoGravamen);

                        // Guardado documentos en carpeta
                        NoGravamen.CopyTo(new FileStream(fileNoGravamenWithPath, FileMode.Create));


                        // datos a mandar a la base de datos

                        string rutaNoGravamenProp = /*"DocumentosPropiedad/" +*/ AleatorioPropiedadID + "/" + nDocFileNoGravamen;
                        string extNoGravamenProp = Path.GetExtension(NoGravamen.FileName);
                        FileInfo sizeNoGravamenProp = new FileInfo(fileNoGravamenWithPath);

                        // Guardado de la infromacion de iamgenes en la base de datos



                        // Guarado de info de doc NoGravamen
                        SqlCommand gNoGravamenProp = new SqlCommand("sp_Guardar_docPropiedad_adm", conexion);

                        gNoGravamenProp.Parameters.AddWithValue("IDpropiedadRealEcommRel", oValorIDpropiedad.IDPropiedad);
                        gNoGravamenProp.Parameters.AddWithValue("IDtipoArchivoRel", 11);
                        gNoGravamenProp.Parameters.AddWithValue("fileNombre", gNoGravamen);
                        gNoGravamenProp.Parameters.AddWithValue("fileSize", sizeNoGravamenProp.Length);
                        gNoGravamenProp.Parameters.AddWithValue("extension", extNoGravamenProp);
                        gNoGravamenProp.Parameters.AddWithValue("filePath", rutaNoGravamenProp);

                        gNoGravamenProp.CommandType = CommandType.StoredProcedure;
                        gNoGravamenProp.ExecuteNonQuery();

                    }

                    // Guardado documento NoAdeudo 
                    if (oPropiedad.NoAdeudo != null)
                    {
                        // Obteniendo documentos
                        var NoAdeudo = oPropiedad.NoAdeudo;

                        // Generando nombre aleatorio
                        Guid gNoAdeudo = Guid.NewGuid();

                        string nDocFileNoAdeudo = gNoAdeudo + Path.GetExtension(NoAdeudo.FileName);

                        // Generando ruta para el guardado del documento
                        string fileNoAdeudoWithPath = Path.Combine(pathDoc, nDocFileNoAdeudo);

                        // Guardado documentos en carpeta
                        NoAdeudo.CopyTo(new FileStream(fileNoAdeudoWithPath, FileMode.Create));


                        // datos a mandar a la base de datos

                        string rutaNoAdeudoProp = /*"DocumentosPropiedad/" +*/ AleatorioPropiedadID + "/" + nDocFileNoAdeudo;
                        string extNoAdeudoProp = Path.GetExtension(NoAdeudo.FileName);
                        FileInfo sizeNoAdeudoProp = new FileInfo(fileNoAdeudoWithPath);

                        // Guardado de la infromacion de iamgenes en la base de datos



                        // Guarado de info de doc NoAdeudo
                        SqlCommand gNoAdeudoProp = new SqlCommand("sp_Guardar_docPropiedad_adm", conexion);

                        gNoAdeudoProp.Parameters.AddWithValue("IDpropiedadRealEcommRel", oValorIDpropiedad.IDPropiedad);
                        gNoAdeudoProp.Parameters.AddWithValue("IDtipoArchivoRel", 12);
                        gNoAdeudoProp.Parameters.AddWithValue("fileNombre", gNoAdeudo);
                        gNoAdeudoProp.Parameters.AddWithValue("fileSize", sizeNoAdeudoProp.Length);
                        gNoAdeudoProp.Parameters.AddWithValue("extension", extNoAdeudoProp);
                        gNoAdeudoProp.Parameters.AddWithValue("filePath", rutaNoAdeudoProp);

                        gNoAdeudoProp.CommandType = CommandType.StoredProcedure;
                        gNoAdeudoProp.ExecuteNonQuery();

                    }
                


                    //Guardado de los datos del propietario no verificado 
                    SqlCommand gPropietario = new SqlCommand("sp_Guardar_Propietario_NP", conexion);

                        gPropietario.Parameters.AddWithValue("email", oPropiedad.email);
                        gPropietario.Parameters.AddWithValue("nombre", oPropiedad.Nombre);
                        gPropietario.Parameters.AddWithValue("a_paterno", oPropiedad.apellidoPaterno);
                        gPropietario.Parameters.AddWithValue("a_materno", oPropiedad.apellidoMaterno);
                        gPropietario.Parameters.AddWithValue("telefono", oPropiedad.telefono);

                        gPropietario.CommandType = CommandType.StoredProcedure;
                        gPropietario.ExecuteNonQuery();




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

        public List<PropiedadModelEditarCaracteristicasNum> ListarCaractPropEditarNum(string PropiedadID)
        {
            var oListaCaract = new List<PropiedadModelEditarCaracteristicasNum>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_caractPropiedadEditarNum_adm", conexion);
                cmd.Parameters.AddWithValue("PropiedadID", PropiedadID);

                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {

                        oListaCaract.Add(new PropiedadModelEditarCaracteristicasNum()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/



                            IDTCaracteristica = Convert.ToInt32(dr["IDTipoCaractProp"]),
                            TipoCaract = dr["tipoCaract"].ToString(),
                            IDCaracteristica = Convert.ToInt32(dr["IDCaractProp"]),
                            Caracteristica = dr["Caracteristica"].ToString(),
                            //Orden = Convert.ToInt32(dr["orden"]),

                            numeroElementos = dr["numeroElementos"] == DBNull.Value ? 0 : Convert.ToInt32(dr["numeroElementos"]),


                        }); ;
                    }
                }
            }

            return oListaCaract;
        }

        public PropiedadesModelPropiedad ObtenerDatosPropiedadSelect(string PropiedadID)
        {
            var oPropiedad = new PropiedadesModelPropiedad();
            var vistas = 0;
            var visitas = 0;

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_datosPropiedad_tab", conexion);
                cmd.Parameters.AddWithValue("PropiedadID", PropiedadID);
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

                        oPropiedad.IDPropiedadG = dr["PropiedadID"].ToString();
                        oPropiedad.Operacion = dr["Operacion"].ToString();
                        oPropiedad.Titulo = dr["Nombre"].ToString();
                        oPropiedad.Abrev = dr["Abrev"].ToString();
                        oPropiedad.ISO = dr["ISO"].ToString();
                        oPropiedad.Precio = dr["precio"].ToString();
                        oPropiedad.Superficie = dr["Superficie"].ToString();
                        oPropiedad.UM = dr["UM"].ToString();
                        oPropiedad.SuperficieC = dr["SuperficieC"].ToString();
                        oPropiedad.UMC = dr["UMC"].ToString();
                        oPropiedad.Vistas = visitas;
                        oPropiedad.Visitas = vistas;
                        oPropiedad.FPublicacion = dr["FPublicacion"].ToString();
                        oPropiedad.StatusPropiedad = dr["StatusPropiedad"].ToString();

                    }
                }
            }

            return oPropiedad;
        }

        public List<PropiedadesModelCaracteristicasCheck> ListarCaractPropEditarCheck(string PropiedadID)
        {

            var oListaCaract = new List<PropiedadesModelCaracteristicasCheck>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_caractPropiedadEditar_adm", conexion);
                cmd.Parameters.AddWithValue("PropiedadID", PropiedadID);

                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        bool valorCheck;
                        if (Convert.ToInt32(dr["isTrue"]) == 1)
                        {
                            valorCheck = true;
                        }
                        else
                        {
                            valorCheck = false;
                        }

                        oListaCaract.Add(new PropiedadesModelCaracteristicasCheck()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            IDTCaracteristica = Convert.ToInt32(dr["IDTipoCaractProp"]),
                            TipoCaract = dr["tipoCaract"].ToString(),
                            IDCaracteristica = Convert.ToInt32(dr["IDCaractProp"]),
                            Caracteristica = dr["Caracteristica"].ToString(),
                            //Orden = Convert.ToInt32(dr["orden"]),

                            isSelected = valorCheck

                        }); ;
                    }
                }
            }

            return oListaCaract;
        }

        public List<PropiedadModelEditarImagenes> ListarImgPropEditar(string PropiedadID)
        {
            var oListaImg = new List<PropiedadModelEditarImagenes>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_imgPropEditar_adm", conexion);
                cmd.Parameters.AddWithValue("PropiedadID", PropiedadID);

                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    int numImg = 0;
                    while (dr.Read())
                    {
                        //string path = Path.Combine(Directory.GetCurrentDirectory(), dr["imgPath"].ToString());

                        var path = System.IO.Directory.GetCurrentDirectory() + "\\ImagenesPropiedad\\" + dr["imgPath"].ToString();
                        byte[] imageArray = System.IO.File.ReadAllBytes(path);
                        string base64Image = Convert.ToBase64String(imageArray);
                        string src64 = "data:image/jpg/jpeg/png;base64," + base64Image;

                        numImg = numImg + 1;


                        oListaImg.Add(new PropiedadModelEditarImagenes()
                        {

                            IDimgPropiedad = Convert.ToInt32(dr["IDimgPropiedadesRealEcommint"]),
                            IDpropiedad = Convert.ToInt32(dr["IDpropiedadRealEcommRel"]),
                            PropiedadID = dr["PropiedadID"].ToString(),
                            numImg = Convert.ToInt32(numImg),
                            imgName = dr["imgName"].ToString(),
                            extension = dr["extension"].ToString(),
                            imgPath = path,
                            txt64 = src64.ToString()


                        }); ;
                    }
                }
            }

            return oListaImg;
        }

        public List<PropiedadModelEditarDocumentos> ListarDocumentosPropEditar(string PropiedadID)
        {
            var oListaDoc = new List<PropiedadModelEditarDocumentos>();

            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_docPropEditar_adm", conexion);
                cmd.Parameters.AddWithValue("PropiedadID", PropiedadID);

                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        //string path = Path.Combine(Directory.GetCurrentDirectory(), dr["imgPath"].ToString());

                        var path = System.IO.Directory.GetCurrentDirectory() + "\\DocumentosPropiedad\\" + dr["filePath"].ToString();
                        byte[] imageArray = System.IO.File.ReadAllBytes(path);
                        string base64Image = Convert.ToBase64String(imageArray);
                        //string src64 = "data:image/jpg/jpeg/png;base64," + base64Image;
                        string src64 = "";



                        if (dr["extension"].ToString() == ".jpg")
                        {
                            src64 = "data:image/jpg;base64," + base64Image;
                        }
                        if (dr["extension"].ToString() == ".jpeg")
                        {
                            src64 = "data:image/jpeg;base64," + base64Image;
                        }
                        if (dr["extension"].ToString() == ".png")
                        {
                            src64 = "data:image/png;base64," + base64Image;
                        }
                        if (dr["extension"].ToString() == ".pdf")
                        {
                            src64 = "data:application/pdf;base64," + base64Image;
                        }

                        Console.Write(src64);

                        oListaDoc.Add(new PropiedadModelEditarDocumentos()
                        {

                            IDdocPropiedad = Convert.ToInt32(dr["IDdocPropiedad"]),
                            IDtipoArchivo = Convert.ToInt32(dr["IDtipoArchivoRel"]),
                            TipoArchivo = dr["archivo"].ToString(),
                            IDpropiedad = Convert.ToInt32(dr["IDpropiedadReal"]),
                            PropiedadID = dr["PropiedadID"].ToString(),
                            fileNombre = dr["fileNombre"].ToString(),
                            extension = dr["extension"].ToString(),
                            filePath = path,
                            txt64 = src64.ToString(),
                            IDStatusDoc = Convert.ToInt32(dr["IDstatusDocPropRel"])


                        }); ;
                    }
                }
            }

            return oListaDoc;
        }

        public PropiedadModelDatosEditar ObtenerPropiedadEditar(string PropiedadID)
        {
            var oPropiedad = new PropiedadModelDatosEditar();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener_datosPropiedadEditar_adm", conexion);

                cmd.Parameters.AddWithValue("PropiedadID", PropiedadID);

                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oPropiedad.Operacion = Convert.ToInt32(dr["IDoperacionPropRel"]);
                        oPropiedad.NombrePropiedad = dr["nombre"].ToString();
                        oPropiedad.TipoPropiedad = Convert.ToInt32(dr["IDtipoPropRel"]);


                        if (dr["IDsubtipoPropRel"] != DBNull.Value)
                        {
                            oPropiedad.subTipoPropiedad = Convert.ToInt32(dr["IDsubtipoPropRel"]);
                        }

                        oPropiedad.Moneda = Convert.ToInt32(dr["IDmonedaRel"]);
                        oPropiedad.Precio = dr["precio"].ToString();

                        if (dr["IDperiodoRentaPropRel"] != DBNull.Value)
                        {
                            oPropiedad.periodoRenta = Convert.ToInt32(dr["IDperiodoRentaPropRel"]);
                        }

                        oPropiedad.Mantenimiento = dr["mantenimiento"].ToString();

                        if (dr["IDperiodoMtoPropRel"] != DBNull.Value)
                        {
                            oPropiedad.periodoMantenimiento = Convert.ToInt32(dr["IDperiodoMtoPropRel"]);
                        }

                        oPropiedad.Descripcion = dr["descripcion"].ToString();

                        oPropiedad.Calle = dr["calle"].ToString();

                        if (dr["no_interior"] != DBNull.Value)
                        {
                            oPropiedad.NoInt = Convert.ToInt32(dr["no_interior"]);
                        }

                        oPropiedad.NoExt = Convert.ToInt32(dr["no_exterior"]);
                        oPropiedad.CP = Convert.ToInt32(dr["codigoPostal"]);
                        oPropiedad.Colonia = dr["colonia"].ToString();
                        oPropiedad.Municipio = dr["municipio"].ToString();
                        oPropiedad.Estado = dr["estado"].ToString();
                        oPropiedad.Pais = dr["pais"].ToString();
                        oPropiedad.DireccionC = dr["direccion_completa"].ToString();
                        oPropiedad.txtLat = dr["latitud"].ToString();
                        oPropiedad.txtLng = dr["longitud"].ToString();

                        oPropiedad.Superficie = Convert.ToInt32(dr["terreno"]);
                        oPropiedad.SuperficieUMedida = Convert.ToInt32(dr["IDuMTerrenoPropRel"]);
                        oPropiedad.txtSuperficieContruccionUMedida = dr["UmTerreno"].ToString();
                        oPropiedad.SuperficieContruccion = Convert.ToInt32(dr["Construccion"]);
                        oPropiedad.SuperficieContruccionUMedida = Convert.ToInt32(dr["IDuMTConstruidaRel"]);
                        oPropiedad.txtSuperficieContruccionUMedida = dr["UmConstruccion"].ToString();
                        oPropiedad.Antiguedad = Convert.ToInt32(dr["Antiguedad"]);
                        oPropiedad.UsoSuelo = Convert.ToInt32(dr["IDusoSueloPropRel"]);
                    }
                }
            }

            return oPropiedad;

        }

        public bool EliminarImgPropiedad(int IDimgPropiedad)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();

                    // Obtener Datos de la img a eliminar y obtener la ruta
                    SqlCommand oRutaImg = new SqlCommand("sp_Obtener_DatosIDimg_adm", conexion);

                    oRutaImg.Parameters.AddWithValue("IDimg", IDimgPropiedad);

                    oRutaImg.CommandType = CommandType.StoredProcedure;

                    var oValorRutaImg = new base64PropiedadesEcomm();

                    using (var RutaImg = oRutaImg.ExecuteReader())
                    {
                        while (RutaImg.Read())
                        {
                            oValorRutaImg.RutaImagen = RutaImg["imgPath"].ToString();
                        }
                    }

                    var path = System.IO.Directory.GetCurrentDirectory() + "\\ImagenesPropiedad\\" + oValorRutaImg.RutaImagen;

                    //Console.WriteLine(path);
                    File.Delete(path);

                    SqlCommand cmd = new SqlCommand("sp_Eliminar_imgPropiedadRealEcomm_adm", conexion);

                    cmd.Parameters.AddWithValue("IDImgPropiedad", IDimgPropiedad);

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

        public bool EliminarDocPropiedad(int IDdocPropiedad)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();

                    // Obtener Datos del documento a eliminar y obtener la ruta
                    SqlCommand oRutaDoc = new SqlCommand("sp_Obtener_DatosIDdoc_adm", conexion);

                    oRutaDoc.Parameters.AddWithValue("IDdoc", IDdocPropiedad);

                    oRutaDoc.CommandType = CommandType.StoredProcedure;

                    var oValorRutaDoc = new PropiedadModelEditarDocumentos();

                    using (var RutaDoc = oRutaDoc.ExecuteReader())
                    {
                        while (RutaDoc.Read())
                        {
                            oValorRutaDoc.filePath = RutaDoc["filePath"].ToString();
                        }
                    }

                    var path = System.IO.Directory.GetCurrentDirectory() + "\\DocumentosPropiedad\\" + oValorRutaDoc.filePath;

                    //Console.WriteLine(path);
                    File.Delete(path);



                    SqlCommand cmd = new SqlCommand("sp_Eliminar_docPropiedadRealEcomm_adm", conexion);

                    cmd.Parameters.AddWithValue("IDdocPropiedad", IDdocPropiedad);

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

        public bool GuardarEditarPropiedad(PropiedadesModel oPropiedad)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConnSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Editar_PropiedadRealEcomm_adm", conexion);

                    cmd.Parameters.AddWithValue("PropiedadID", oPropiedad.PropiedadGUID);
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

                    // Obtener ID de la propiedad con el dato de PropiedadID

                    SqlCommand oIdpropiedad = new SqlCommand("sp_Obtener_idPropiedadRealEcomm", conexion);

                    oIdpropiedad.Parameters.AddWithValue("PropiedadID", oPropiedad.PropiedadGUID);

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

                    // Edicion de ubicacion de la propiedad
                    SqlCommand gUbicacion = new SqlCommand("sp_Editar_UbicacionProp_adm", conexion);

                    gUbicacion.Parameters.AddWithValue("IDpropiedadRealEcommRel", oValorIDpropiedad.IDPropiedad);
                    gUbicacion.Parameters.AddWithValue("calle", oPropiedad.Calle);
                    gUbicacion.Parameters.AddWithValue("IDcoloniaRel", oPropiedad.IDcolonia);
                    gUbicacion.Parameters.AddWithValue("no_interior", oPropiedad.NoInt);
                    gUbicacion.Parameters.AddWithValue("no_exterior", oPropiedad.NoExt);
                    gUbicacion.Parameters.AddWithValue("latitud", oPropiedad.txtLat);
                    gUbicacion.Parameters.AddWithValue("longitud", oPropiedad.txtLng);
                    gUbicacion.Parameters.AddWithValue("direccion_completa", oPropiedad.DireccionC);


                    gUbicacion.CommandType = CommandType.StoredProcedure;
                    gUbicacion.ExecuteNonQuery();

                    // Edicion de Construccion de la Propiedad

                    SqlCommand gConstruccion = new SqlCommand("sp_Editar_ConstruccionProp_adm", conexion);

                    gConstruccion.Parameters.AddWithValue("IDpropiedadRealEcommRel", oValorIDpropiedad.IDPropiedad);
                    gConstruccion.Parameters.AddWithValue("antiguedad", oPropiedad.Antiguedad);
                    gConstruccion.Parameters.AddWithValue("IDusoSueloPropRel", oPropiedad.UsoSuelo);
                    gConstruccion.Parameters.AddWithValue("superficie", oPropiedad.Superficie);
                    gConstruccion.Parameters.AddWithValue("IDuMTerrenoPropRel", oPropiedad.SuperficieUMedida);
                    gConstruccion.Parameters.AddWithValue("superficieConstruida", oPropiedad.SuperficieContruccion);
                    gConstruccion.Parameters.AddWithValue("IDuMTConstruidaRel", oPropiedad.SuperficieContruccionUMedida);

                    gConstruccion.CommandType = CommandType.StoredProcedure;
                    gConstruccion.ExecuteNonQuery();


                    // Edicion de las caracteristicas de la propiedad

                    SqlCommand gCaracteristicasRecamaras = new SqlCommand("sp_Editar_AmenidadesProp_adm", conexion);

                    gCaracteristicasRecamaras.Parameters.AddWithValue("IDpropiedadRealEcomm", oValorIDpropiedad.IDPropiedad);
                    gCaracteristicasRecamaras.Parameters.AddWithValue("IDCaractProp", 1);
                    gCaracteristicasRecamaras.Parameters.AddWithValue("numeroElementos", oPropiedad.Recamaras);

                    gCaracteristicasRecamaras.CommandType = CommandType.StoredProcedure;
                    gCaracteristicasRecamaras.ExecuteNonQuery();


                    SqlCommand gCaracteristicasEstacionamiento = new SqlCommand("sp_Editar_AmenidadesProp_adm", conexion);

                    gCaracteristicasEstacionamiento.Parameters.AddWithValue("IDpropiedadRealEcomm", oValorIDpropiedad.IDPropiedad);
                    gCaracteristicasEstacionamiento.Parameters.AddWithValue("IDCaractProp", 2);
                    gCaracteristicasEstacionamiento.Parameters.AddWithValue("numeroElementos", oPropiedad.Estacionamiento);

                    gCaracteristicasEstacionamiento.CommandType = CommandType.StoredProcedure;
                    gCaracteristicasEstacionamiento.ExecuteNonQuery();

                    SqlCommand gCaracteristicasBanos = new SqlCommand("sp_Editar_AmenidadesProp_adm", conexion);

                    gCaracteristicasBanos.Parameters.AddWithValue("IDpropiedadRealEcomm", oValorIDpropiedad.IDPropiedad);
                    gCaracteristicasBanos.Parameters.AddWithValue("IDCaractProp", 3);
                    gCaracteristicasBanos.Parameters.AddWithValue("numeroElementos", oPropiedad.Banos);

                    gCaracteristicasBanos.CommandType = CommandType.StoredProcedure;
                    gCaracteristicasBanos.ExecuteNonQuery();

                    SqlCommand gCaracteristicasMBanos = new SqlCommand("sp_Editar_AmenidadesProp_adm", conexion);

                    gCaracteristicasMBanos.Parameters.AddWithValue("IDpropiedadRealEcomm", oValorIDpropiedad.IDPropiedad);
                    gCaracteristicasMBanos.Parameters.AddWithValue("IDCaractProp", 4);
                    gCaracteristicasMBanos.Parameters.AddWithValue("numeroElementos", oPropiedad.MediosBanos);

                    gCaracteristicasMBanos.CommandType = CommandType.StoredProcedure;
                    gCaracteristicasMBanos.ExecuteNonQuery();


                    foreach (var caracteristica in oPropiedad.CaracteristicasCheckPropiedadEditar)
                    {
                        SqlCommand gCaracteristicas = new SqlCommand("sp_Editar_AmenidadesProp_adm", conexion);

                        gCaracteristicas.Parameters.AddWithValue("IDpropiedadRealEcomm", oValorIDpropiedad.IDPropiedad);
                        gCaracteristicas.Parameters.AddWithValue("IDCaractProp", caracteristica.IDCaracteristica);
                        gCaracteristicas.Parameters.AddWithValue("isTrue", caracteristica.isSelected);

                        gCaracteristicas.CommandType = CommandType.StoredProcedure;
                        gCaracteristicas.ExecuteNonQuery();
                    }

                    SqlCommand gCaracteristicasBodegas = new SqlCommand("sp_Editar_AmenidadesProp_adm", conexion);

                    gCaracteristicasBodegas.Parameters.AddWithValue("IDpropiedadRealEcomm", oValorIDpropiedad.IDPropiedad);
                    gCaracteristicasBodegas.Parameters.AddWithValue("IDCaractProp", 27);
                    gCaracteristicasBodegas.Parameters.AddWithValue("numeroElementos", oPropiedad.Bodegas);

                    gCaracteristicasBodegas.CommandType = CommandType.StoredProcedure;
                    gCaracteristicasBodegas.ExecuteNonQuery();

                    SqlCommand gCaracteristicasClosets = new SqlCommand("sp_Editar_AmenidadesProp_adm", conexion);

                    gCaracteristicasClosets.Parameters.AddWithValue("IDpropiedadRealEcomm", oValorIDpropiedad.IDPropiedad);
                    gCaracteristicasClosets.Parameters.AddWithValue("IDCaractProp", 28);
                    gCaracteristicasClosets.Parameters.AddWithValue("numeroElementos", oPropiedad.Closets);

                    gCaracteristicasClosets.CommandType = CommandType.StoredProcedure;
                    gCaracteristicasClosets.ExecuteNonQuery();

                    SqlCommand gCaracteristicasElevadores = new SqlCommand("sp_Editar_AmenidadesProp_adm", conexion);

                    gCaracteristicasElevadores.Parameters.AddWithValue("IDpropiedadRealEcomm", oValorIDpropiedad.IDPropiedad);
                    gCaracteristicasElevadores.Parameters.AddWithValue("IDCaractProp", 29);
                    gCaracteristicasElevadores.Parameters.AddWithValue("numeroElementos", oPropiedad.Elevadores);

                    gCaracteristicasElevadores.CommandType = CommandType.StoredProcedure;
                    gCaracteristicasElevadores.ExecuteNonQuery();


                    // Guardado de imagenes y documentos

                    var pathCarpetaIMG = System.IO.Path.Combine("ImagenesPropiedad");

                    var pathCarpetaProp = System.IO.Path.Combine(pathCarpetaIMG, oPropiedad.PropiedadGUID);

                    System.IO.Directory.CreateDirectory(pathCarpetaProp);

                    // Obteniendo imagenes para guardarlas
                    if (oPropiedad.Files != null)
                    {

                        if (oPropiedad.Files.Count > 0)
                        {
                            foreach (var file in oPropiedad.Files)
                            {
                                // Carpeta en donde se debera de guardar la imagenes
                                string path = Path.Combine(Directory.GetCurrentDirectory(), pathCarpetaIMG, oPropiedad.PropiedadGUID);

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
                                string rutaImgProp = /*"ImagenesPropiedad/" +*/ oPropiedad.PropiedadGUID + "/" + nImgFileNombre;
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

                    if (oPropiedad.Predial != null)
                    {
                        // Guardando Documentos

                        // Guardado documento Predial

                        var pathCarpetaDoc = System.IO.Path.Combine("DocumentosPropiedad");

                        var pathCarpetaPropDoc = System.IO.Path.Combine(pathCarpetaDoc, oPropiedad.PropiedadGUID);

                        System.IO.Directory.CreateDirectory(pathCarpetaPropDoc);

                        string pathDoc = Path.Combine(Directory.GetCurrentDirectory(), pathCarpetaDoc, oPropiedad.PropiedadGUID);

                        // Obteniendo documentos
                        var Predial = oPropiedad.Predial;

                        // Generando nombre aleatorio
                        Guid gPredial = Guid.NewGuid();

                        string nDocFilePredial = gPredial + Path.GetExtension(Predial.FileName);

                        // Generando ruta para el guardado del documento
                        string filePredialWithPath = Path.Combine(pathDoc, nDocFilePredial);

                        // Guardado documentos en carpeta
                        Predial.CopyTo(new FileStream(filePredialWithPath, FileMode.Create));

                        // datos a mandar a la base de datos
                        string rutaPredialProp = /*"DocumentosPropiedad/" +*/ oPropiedad.PropiedadGUID + "/" + nDocFilePredial;
                        string extPredialProp = Path.GetExtension(Predial.FileName);
                        FileInfo sizePredialProp = new FileInfo(filePredialWithPath);

                        // Guardado de la infromacion de iamgenes en la base de datos

                        // Guardado de info de doc Predial
                        SqlCommand gPredialProp = new SqlCommand("sp_Guardar_docPropiedad_adm", conexion);

                        gPredialProp.Parameters.AddWithValue("IDpropiedadRealEcommRel", oValorIDpropiedad.IDPropiedad);
                        gPredialProp.Parameters.AddWithValue("IDtipoArchivoRel", 5);
                        gPredialProp.Parameters.AddWithValue("fileNombre", gPredial);
                        gPredialProp.Parameters.AddWithValue("fileSize", sizePredialProp.Length);
                        gPredialProp.Parameters.AddWithValue("extension", extPredialProp);
                        gPredialProp.Parameters.AddWithValue("filePath", rutaPredialProp);

                        gPredialProp.CommandType = CommandType.StoredProcedure;
                        gPredialProp.ExecuteNonQuery();
                    }

                    if (oPropiedad.Escritura != null)
                    {
                        // Guardando Documentos

                        // Guardado documento Escritura

                        var pathCarpetaDocEscritura = System.IO.Path.Combine("DocumentosPropiedad");

                        var pathCarpetaPropDocEscritura = System.IO.Path.Combine(pathCarpetaDocEscritura, oPropiedad.PropiedadGUID);

                        System.IO.Directory.CreateDirectory(pathCarpetaPropDocEscritura);

                        string pathDoc = Path.Combine(Directory.GetCurrentDirectory(), pathCarpetaDocEscritura, oPropiedad.PropiedadGUID);

                        // Obteniendo documentos
                        var Escritura = oPropiedad.Escritura;

                        // Generando nombre aleatorio
                        Guid gEscritura = Guid.NewGuid();

                        string nDocFileEscritura = gEscritura + Path.GetExtension(Escritura.FileName);

                        // Generando ruta para el guardado del documento
                        string fileEscrituraWithPath = Path.Combine(pathDoc, nDocFileEscritura);

                        // Guardado documentos en carpeta
                        Escritura.CopyTo(new FileStream(fileEscrituraWithPath, FileMode.Create));

                        // datos a mandar a la base de 
                        string rutaEscrituraProp = /*"DocumentosPropiedad/" + */oPropiedad.PropiedadGUID + "/" + nDocFileEscritura;
                        string extEscrituraProp = Path.GetExtension(Escritura.FileName);
                        FileInfo sizeEscrituraProp = new FileInfo(fileEscrituraWithPath);

                        // Guardado de info de doc Escritura
                        SqlCommand gEscrituraProp = new SqlCommand("sp_Guardar_docPropiedad_adm", conexion);

                        gEscrituraProp.Parameters.AddWithValue("IDpropiedadRealEcommRel", oValorIDpropiedad.IDPropiedad);
                        gEscrituraProp.Parameters.AddWithValue("IDtipoArchivoRel", 6);
                        gEscrituraProp.Parameters.AddWithValue("fileNombre", gEscritura);
                        gEscrituraProp.Parameters.AddWithValue("fileSize", sizeEscrituraProp.Length);
                        gEscrituraProp.Parameters.AddWithValue("extension", extEscrituraProp);
                        gEscrituraProp.Parameters.AddWithValue("filePath", rutaEscrituraProp);

                        gEscrituraProp.CommandType = CommandType.StoredProcedure;
                        gEscrituraProp.ExecuteNonQuery();

                    }

                    if (oPropiedad.Identificacion != null)
                    {
                        // Guardando Documentos

                        // Guardado documento Predial

                        var pathCarpetaDoc = System.IO.Path.Combine("DocumentosPropiedad");

                        var pathCarpetaPropDoc = System.IO.Path.Combine(pathCarpetaDoc, oPropiedad.PropiedadGUID);

                        System.IO.Directory.CreateDirectory(pathCarpetaPropDoc);

                        string pathDoc = Path.Combine(Directory.GetCurrentDirectory(), pathCarpetaDoc, oPropiedad.PropiedadGUID);

                        // Obteniendo documentos
                        var Identificacion = oPropiedad.Identificacion;

                        // Generando nombre aleatorio
                        Guid gIdentificacion = Guid.NewGuid();

                        string nDocFileIdentificacion = gIdentificacion + Path.GetExtension(Identificacion.FileName);

                        // Generando ruta para el guardado del documento
                        string fileIdentificacionWithPath = Path.Combine(pathDoc, nDocFileIdentificacion);

                        // Guardado documentos en carpeta
                        Identificacion.CopyTo(new FileStream(fileIdentificacionWithPath, FileMode.Create));


                        // datos a mandar a la base de datos
                        string rutaIdentificacionProp = /*"DocumentosPropiedad/" + */oPropiedad.PropiedadGUID + "/" + nDocFileIdentificacion;
                        string extIdentificacionProp = Path.GetExtension(Identificacion.FileName);
                        FileInfo sizeIdentificacionProp = new FileInfo(fileIdentificacionWithPath);

                        // Guardado de la infromacion de iamgenes en la base de datos

                        // Guarado de info de doc Identificacion
                        SqlCommand gIdentificacionProp = new SqlCommand("sp_Guardar_docPropiedad_adm", conexion);

                        gIdentificacionProp.Parameters.AddWithValue("IDpropiedadRealEcommRel", oValorIDpropiedad.IDPropiedad);
                        gIdentificacionProp.Parameters.AddWithValue("IDtipoArchivoRel", 4);
                        gIdentificacionProp.Parameters.AddWithValue("fileNombre", gIdentificacion);
                        gIdentificacionProp.Parameters.AddWithValue("fileSize", sizeIdentificacionProp.Length);
                        gIdentificacionProp.Parameters.AddWithValue("extension", extIdentificacionProp);
                        gIdentificacionProp.Parameters.AddWithValue("filePath", rutaIdentificacionProp);

                        gIdentificacionProp.CommandType = CommandType.StoredProcedure;
                        gIdentificacionProp.ExecuteNonQuery();

                    }



                    //Guardado de datos de publicación 
                    SqlCommand gPublicacion = new SqlCommand("sp_GuardarEditar_PublicacionProp", conexion);

                    gPublicacion.Parameters.AddWithValue("IDpropiedadRealEcomm", oValorIDpropiedad.IDPropiedad);
                  //  gPublicacion.Parameters.AddWithValue("fechaPublicacion", oPropiedad.FechaPublicacion);

                    gPublicacion.CommandType = CommandType.StoredProcedure;
                    gPublicacion.ExecuteNonQuery();

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

        //metodo? para el controller de las caracteristicas de las propiedades

        public List<CaracteristicasD> LCaracteristicasD(string PropiedadID)
        {
            var oListaCaracter = new List<CaracteristicasD>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_obtener_caractD_adm", conexion);
                cmd.Parameters.AddWithValue("IDPropiedad", PropiedadID);

                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {

                        oListaCaracter.Add(new CaracteristicasD()
                        {
                            /*Lado izquiero es la variable del modelo, lado derecho es la variable que
                            proviene de la base de datos*/

                            PropiedadID = dr["PropiedadID"].ToString(),
                            Caracteristicas = dr["Caracteristica"].ToString(),
                            NoElementos = dr["NoElementos"].ToString(),
                            //Orden = Convert.ToInt32(dr["orden"]),

                        }); ;
                    }
                }
            }

            return oListaCaracter;
        }

        public PropiedadSeleccionada ObtenerMunicipio(string CODIGOPOSTAL)
        {
            var oMunicipio = new PropiedadSeleccionada();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_obtenerMunicipio", conexion);
                cmd.Parameters.AddWithValue("codigoPostal", CODIGOPOSTAL);
                cmd.CommandType = CommandType.StoredProcedure;


                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oMunicipio.Municipio = dr["municipio"].ToString();

                    }

                }
            }

            return oMunicipio;

        }


        public Propietario Verificar(string correo)
        {
            var oVerifPropietario = new Propietario();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConnSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_VerificarCorreo_propietario", conexion);
                cmd.Parameters.AddWithValue("correo", correo);
                cmd.CommandType = CommandType.StoredProcedure;


                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oVerifPropietario.Nombre = dr["nombre"].ToString();
                        oVerifPropietario.email = dr["email"].ToString();
                        oVerifPropietario.apellidoMaterno = dr["apellidoMaterno"].ToString() ;
                        oVerifPropietario.apellidoPaterno = dr["apellidoPaterno"].ToString();
                        oVerifPropietario.telefono = dr["telefono"].ToString();
                    }

                }
            }

            return oVerifPropietario;
        }
    }
}




