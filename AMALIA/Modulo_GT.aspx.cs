using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AMALIAFW;
using System.Data;
using System.Web.Services;
using System.Web.UI.HtmlControls;

namespace AMALIA
{
    public partial class Modulo_GT : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "GT";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                    usuario.usuario = HttpContext.Current.User.Identity.Name;
                    FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
                    if (usuario.id_perfil == 2)
                    {
                        DIV_COMBUSTIBLE.Visible = false;
                        DIV_GASTOS.Visible = false;
                    }
                    if (usuario.id_perfil == 3)
                    {
                        DIV_OTZ.Visible = false;
                    }
                    if (usuario.usuario != "jbrantes" && usuario.usuario != "gestay" && usuario.usuario != "festay" && usuario.usuario != "cvargas")
                    {
                        CHK_ENTREGADO.Attributes.Add("onclick", "return false;");
                    }

                    LlenarCombos();

                    if (Request.QueryString["id_gt"] != null)
                    {
                        int id_gt = int.Parse(Request.QueryString["id_gt"].ToString());
                        COMPLETAR_DETALLE(id_gt);
                        PANEL_PRINCIPAL.Visible = false;
                        PANEL_DETALLE1.Visible = true;
                    }
                    else
                    {
                        G_PRINCIPAL.DataSource = FN_ENC_GT.LLENADTVISTATOP();
                        G_PRINCIPAL.DataBind();

                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "combopro", "<script>javascript:ComboPro();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "relojitofalse", "<script>javascript:relojito(false);</script>", false);
                arreglatablas();

            }
        }

        public void arreglatablas()
        {
            if (G_PRINCIPAL.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "modulogtprincipal", "<script>javascript:DT_PRINCIPAL();</script>", false);
            }
            if (G_OTZ.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "modulogtotz", "<script>javascript:DT_OTZ();</script>", false);
            }
            if (G_COMBUSTIBLE.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "modulogtcombustible", "<script>javascript:DT_COMBUSTIBLE();</script>", false);
            }
            if (G_GASTOGRAL.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "modulogtgastogral", "<script>javascript:DT_GASTOGRAL();</script>", false);
            }
        }

        #region PRINCIPAL GT
        public void LlenarGrilla(string filtro = "")
        {
            G_PRINCIPAL.DataSource = FN_ENC_GT.LLENADTVISTA(filtro);
            G_PRINCIPAL.DataBind();
            arreglatablas();
        }

        protected void B_FILTRAR_Click(object sender, EventArgs e)
        {
            string filtro = " where 1=1 ";
            if (FILTRA_FECHA_DESDE.Text != "")
            {
                filtro += " and fecha_creacion >= convert(date, '" + DateTime.Parse(FILTRA_FECHA_DESDE.Text).ToString("dd/MM/yyyy") + "', 103) ";
            }
            if (FILTRA_FECHA_HASTA.Text != "")
            {
                filtro += " and fecha_creacion <= convert(date, '" + DateTime.Parse(FILTRA_FECHA_HASTA.Text).ToString("dd/MM/yyyy") + "', 103) ";
            }
            if (FILTRA_GT_DESDE.Text != "")
            {
                filtro += " and num_correlativo >= " + FILTRA_GT_DESDE.Text;
            }
            if (FILTRA_GT_HASTA.Text != "")
            {
                filtro += " and num_correlativo <= " + FILTRA_GT_HASTA.Text;
            }
            LlenarGrilla(filtro);
        }

        public void LlenarCombos()
        {
            DBUtil db = new DBUtil();

            CB_CAMION.DataSource = FN_CAMION.LLENADT(" where activo = 'ACTIVO' order by PATENTE ");
            CB_CAMION.DataTextField = "PATENTE";
            CB_CAMION.DataValueField = "ID_CAMION";
            CB_CAMION.DataBind();
            CB_CAMION.Items.Insert(0, new ListItem("-- SELECCIONE --", "0"));

            CB_RAMPLA.DataSource = FN_RAMPLA.LLENADT(" where activo = 'ACTIVO' order by PATENTE ");
            CB_RAMPLA.DataTextField = "PATENTE";
            CB_RAMPLA.DataValueField = "ID_RAMPLA";
            CB_RAMPLA.DataBind();
            CB_RAMPLA.Items.Insert(0, new ListItem("-- SELECCIONE --", "0"));

            CB_CONDUCTOR.DataSource = FN_CONDUCTOR.LLENADT(" order by NOMBRE_COMPLETO ");
            CB_CONDUCTOR.DataTextField = "NOMBRE_COMPLETO";
            CB_CONDUCTOR.DataValueField = "ID_CONDUCTOR";
            CB_CONDUCTOR.DataBind();
            CB_CONDUCTOR.Items.Insert(0, new ListItem("-- SELECCIONE --", "0"));

            CB_CONDUCTOR2.DataSource = FN_CONDUCTOR.LLENADT(" order by NOMBRE_COMPLETO ");
            CB_CONDUCTOR2.DataTextField = "NOMBRE_COMPLETO";
            CB_CONDUCTOR2.DataValueField = "ID_CONDUCTOR";
            CB_CONDUCTOR2.DataBind();
            CB_CONDUCTOR2.Items.Insert(0, new ListItem("-- SELECCIONE --", "0"));

            CB_GASTO_TIPO.DataSource = db.consultar("select * from gasto_general_tipo order by id_tipo_gasto");
            CB_GASTO_TIPO.DataTextField = "NOMBRE_TIPO_GASTO";
            CB_GASTO_TIPO.DataValueField = "ID_TIPO_GASTO";
            CB_GASTO_TIPO.DataBind();
            CB_GASTO_TIPO.Items.Insert(0, new ListItem("-- SELECCIONE --", "0"));

            CB_OTZ_CLIENTE.DataSource = db.consultar("select ID_CLIENTE, CONCAT(razon_social, ' - ', nombre_cliente ) as 'nom_cliente' from cliente order by nombre_cliente");
            CB_OTZ_CLIENTE.DataTextField = "NOM_CLIENTE";
            CB_OTZ_CLIENTE.DataValueField = "ID_CLIENTE";
            CB_OTZ_CLIENTE.DataBind();
            CB_OTZ_CLIENTE.Items.Insert(0, new ListItem("-- SELECCIONE --", "0"));

            CB_CIUDAD_ORIGEN.DataSource = db.consultar("select * from ciudad order by nom_ciudad");
            CB_CIUDAD_ORIGEN.DataTextField = "NOM_CIUDAD";
            CB_CIUDAD_ORIGEN.DataValueField = "ID_CIUDAD";
            CB_CIUDAD_ORIGEN.DataBind();
            CB_CIUDAD_ORIGEN.Items.Insert(0, new ListItem("-- SELECCIONE --", "0"));

            CB_CIUDAD_DESTINO.DataSource = db.consultar("select * from ciudad order by nom_ciudad");
            CB_CIUDAD_DESTINO.DataTextField = "NOM_CIUDAD";
            CB_CIUDAD_DESTINO.DataValueField = "ID_CIUDAD";
            CB_CIUDAD_DESTINO.DataBind();
            CB_CIUDAD_DESTINO.Items.Insert(0, new ListItem("-- SELECCIONE --", "0"));

            CB_OBRA.DataSource = db.consultar("select id_obra, (cliente + ' - ' + obra) as 'obra' from obras order by cliente, obra");
            CB_OBRA.DataTextField = "obra";
            CB_OBRA.DataValueField = "id_obra";
            CB_OBRA.DataBind();
            CB_OBRA.Items.Insert(0, new ListItem("-- SELECCIONE --", "0"));

            CB_ESTACION_SERVICIO.DataSource = db.consultar("select ID_ESTACION, nombre_estacion from estaciones order by nombre_estacion");
            CB_ESTACION_SERVICIO.DataTextField = "nombre_estacion";
            CB_ESTACION_SERVICIO.DataValueField = "id_estacion";
            CB_ESTACION_SERVICIO.DataBind();
            CB_ESTACION_SERVICIO.Items.Insert(0, new ListItem("-- SELECCIONE --", "0"));
        }

        protected void B_NUEVO_Click(object sender, EventArgs e)
        {
            LIMPIARCAMPOS();
            PANEL_DETALLE1.Visible = true;
            PANEL_PRINCIPAL.Visible = false;

            // BUSCO EL CORRELATIVO
            DBUtil db = new DBUtil();
            int scalar = int.Parse(db.Scalar("select (ISNULL(MAX(gt),0) + 1) from correlativos").ToString());

            // CREO
            OBJ_ENC_GT nuevagt = new OBJ_ENC_GT();
            FN_ENC_GT.PREPARAOBJETO(ref nuevagt);
            if (nuevagt._respok)
            {
                nuevagt.num_correlativo = scalar;
                OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                usuario.usuario = HttpContext.Current.User.Identity.Name;
                FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
                nuevagt.creada_por = usuario.ID_USUARIO;
                nuevagt.fecha_creacion = DateTime.Now;
                FN_ENC_GT.INSERT(ref nuevagt);
                if (nuevagt._respok)
                {
                    T_ID.Text = nuevagt.ID_GT.ToString();
                    NUM_GT.Text = scalar.ToString();
                    db.Scalar("update correlativos set gt = " + scalar);
                }
            }
            COMPLETAR_DETALLE(nuevagt.ID_GT);
            //LlenarGrillaCombustible();
            //LlenarGrillaOTZ();
            //LlenarGrillaGastoGeneral();
        }

        protected void B_GUARDAR_Click(object sender, EventArgs e)
        {
            if (T_FECHA_INICIO.Text == "")
            {
                alert("Por favor ingrese una fecha de inicio.", 0);
            }
            else if (NUM_GT.Text == "")
            {
                alert("Por favor ingrese un numero para la GT.", 0);
            }
            //else if (CB_CONDUCTOR.SelectedValue == "0")
            //{
            //    alert("Por favor seleccione un conductor.", 0);
            //}
            else if (CB_CAMION.SelectedValue == "0")
            {
                alert("Por favor seleccione un camión.", 0);
            }
            else if (CB_RAMPLA.SelectedValue == "0")
            {
                alert("Por favor seleccione una rampla.", 0);
            }
            //else if (T_DINERO_ENTREGADO.Text == "")
            //{
            //    alert("Por favor ingrese el dinero entregado.", 0);
            //}
            else
            {
                try
                {
                    OBJ_ENC_GT objeto_mantenedor = new OBJ_ENC_GT();
                    if (T_ID.Text != "")
                    {
                        // EDITAR
                        objeto_mantenedor.ID_GT = int.Parse(T_ID.Text);
                        FN_ENC_GT.LLENAOBJETO(ref objeto_mantenedor);
                        if (objeto_mantenedor._respok)
                        {
                            objeto_mantenedor.num_correlativo = int.Parse(NUM_GT.Text);
                            objeto_mantenedor.id_conductor = int.Parse(CB_CONDUCTOR.SelectedValue);
                            objeto_mantenedor.id_conductor2 = int.Parse(CB_CONDUCTOR2.SelectedValue);
                            objeto_mantenedor.id_camion = int.Parse(CB_CAMION.SelectedValue);
                            objeto_mantenedor.tipo_camion = T_TIPO_CAMION.Text;
                            objeto_mantenedor.id_rampla = int.Parse(CB_RAMPLA.SelectedValue);
                            if (T_FECHA_INICIO.Text != "")
                            {
                                objeto_mantenedor.fecha_inicio = DateTime.Parse(T_FECHA_INICIO.Text);
                            }
                            if (T_FECHA_FINAL.Text != "")
                            {
                                objeto_mantenedor.fecha_termino = DateTime.Parse(T_FECHA_FINAL.Text);
                            }
                            //objeto_mantenedor.dinero_entregado = int.Parse(T_DINERO_ENTREGADO.Text);
                            //if (T_SOBREDEPOSITO.Text != "")
                            //{
                            //    objeto_mantenedor.sobre_deposito = int.Parse(T_SOBREDEPOSITO.Text);
                            //}
                            if (T_DINERO_DEVUELTO.Text != "")
                            {
                                objeto_mantenedor.dinero_devuelto = int.Parse(T_DINERO_DEVUELTO.Text);
                            }
                            if (T_KM_INICIAL.Text != "")
                            {
                                objeto_mantenedor.km_inicial = int.Parse(T_KM_INICIAL.Text);
                            }
                            if (T_KM_FINAL.Text != "")
                            {
                                objeto_mantenedor.km_final = int.Parse(T_KM_FINAL.Text);
                            }
                            objeto_mantenedor.observacion = T_OBSERVACION.Text;
                            objeto_mantenedor.id_estado = int.Parse(CB_ESTADO_GT.SelectedValue);
                            if (CHK_ENTREGADO.Checked)
                            {
                                objeto_mantenedor.entregado = "Entregado";
                            }
                            else
                            {
                                objeto_mantenedor.entregado = "Pendiente";
                            }
                            FN_ENC_GT.UPDATE(ref objeto_mantenedor);
                            if (objeto_mantenedor._respok)
                            {
                                totalesgt();
                                alert(objeto_mantenedor_global + " modificado con éxito", 1);
                            }
                        }
                    }
                    else
                    {
                        FN_ENC_GT.PREPARAOBJETO(ref objeto_mantenedor);
                        // NUEVO                
                        objeto_mantenedor.num_correlativo = int.Parse(NUM_GT.Text);
                        objeto_mantenedor.id_conductor = int.Parse(CB_CONDUCTOR.SelectedValue);
                        objeto_mantenedor.id_conductor2 = int.Parse(CB_CONDUCTOR2.SelectedValue);
                        objeto_mantenedor.id_camion = int.Parse(CB_CAMION.SelectedValue);
                        objeto_mantenedor.tipo_camion = T_TIPO_CAMION.Text;
                        objeto_mantenedor.id_rampla = int.Parse(CB_RAMPLA.SelectedValue);
                        if (T_FECHA_INICIO.Text != "")
                        {
                            objeto_mantenedor.fecha_inicio = DateTime.Parse(T_FECHA_INICIO.Text);
                        }
                        if (T_FECHA_FINAL.Text != "")
                        {
                            objeto_mantenedor.fecha_termino = DateTime.Parse(T_FECHA_FINAL.Text);
                        }
                        objeto_mantenedor.dinero_entregado = int.Parse(T_DINERO_ENTREGADO.Text);
                        //if (T_SOBREDEPOSITO.Text != "")
                        //{
                        objeto_mantenedor.sobre_deposito = 0;
                        //}
                        if (T_DINERO_DEVUELTO.Text != "")
                        {
                            objeto_mantenedor.dinero_devuelto = int.Parse(T_DINERO_DEVUELTO.Text);
                        }
                        if (T_KM_INICIAL.Text != "")
                        {
                            objeto_mantenedor.km_inicial = int.Parse(T_KM_INICIAL.Text);
                        }
                        if (T_KM_FINAL.Text != "")
                        {
                            objeto_mantenedor.km_final = int.Parse(T_KM_FINAL.Text);
                        }
                        objeto_mantenedor.observacion = T_OBSERVACION.Text;
                        objeto_mantenedor.id_estado = 1;
                        objeto_mantenedor.fecha_creacion = DateTime.Now;
                        objeto_mantenedor.id_estado = int.Parse(CB_ESTADO_GT.SelectedValue);
                        //
                        OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                        usuario.usuario = HttpContext.Current.User.Identity.Name;
                        FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
                        objeto_mantenedor.creada_por = usuario.ID_USUARIO;

                        if (CHK_ENTREGADO.Checked)
                        {
                            objeto_mantenedor.entregado = "Entregado";
                        }
                        else
                        {
                            objeto_mantenedor.entregado = "Pendiente";
                        }
                        //      
                        FN_ENC_GT.INSERT(ref objeto_mantenedor);
                        if (objeto_mantenedor._respok)
                        {
                            T_ID.Text = objeto_mantenedor.ID_GT.ToString();
                            totalesgt();
                            alert(objeto_mantenedor_global + " creado con éxito", 1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    alert("Error al guardar GT (detalle: " + ex.Message + ")", 0);
                }

                try
                {
                    LlenarGrillaCombustible();
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void B_VOLVER_Click(object sender, EventArgs e)
        {
            G_PRINCIPAL.DataSource = FN_ENC_GT.LLENADTVISTATOP();
            G_PRINCIPAL.DataBind();
            // MOSTRAR / OCULTAR PANEL
            PANEL_PRINCIPAL.Visible = true;
            PANEL_DETALLE1.Visible = false;
            arreglatablas();
        }

        protected void G_PRINCIPAL_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //editar
                if (e.CommandName == "Editar")
                {
                    int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    COMPLETAR_DETALLE(id);
                    // MOSTRAR / OCULTAR PANEL
                    PANEL_PRINCIPAL.Visible = false;
                    PANEL_DETALLE1.Visible = true;
                }
                //Borrar
                if (e.CommandName == "Borrar")
                {
                    string user = HttpContext.Current.User.Identity.Name;
                    if (user == "festay" || user == "felipe")
                    {
                        int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                        OBJ_ENC_GT tabla = new OBJ_ENC_GT();
                        tabla.ID_GT = id;
                        FN_ENC_GT.DELETE(ref tabla);
                        if (tabla._respok)
                        {
                            OBJ_ENC_OTZ otz = new OBJ_ENC_OTZ();
                            OBJ_GASTO_GENERAL gasto = new OBJ_GASTO_GENERAL();
                            OBJ_CARGA_COMBUSTIBLE combus = new OBJ_CARGA_COMBUSTIBLE();
                            gasto.id_gt = id;
                            combus.id_gt = id;
                            otz.id_gt = id;
                            FN_ENC_OTZ.DELETEWITHGT(ref otz);
                            FN_CARGA_COMBUSTIBLE.DELETEWITHGT(ref combus);
                            FN_GASTO_GENERAL.DELETEWITHGT(ref gasto);
                            LlenarGrilla();
                            alert("GT Eliminada con éxito", 1);
                        }
                    }
                    else
                    {
                        alert("Ud no tiene los permisos para eliminar GT", 0);
                    }
                }
                if (e.CommandName == "EstadoDineroEntregado")
                {
                    OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                    usuario.usuario = HttpContext.Current.User.Identity.Name;
                    FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
                    if (usuario.usuario == "jbrantes" || usuario.usuario == "gestay" || usuario.usuario == "festay" || usuario.usuario == "cvargas")
                    {
                        int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                        OBJ_ENC_GT tabla = new OBJ_ENC_GT();
                        tabla.ID_GT = id;
                        FN_ENC_GT.LLENAOBJETO(ref tabla);
                        if (tabla._respok)
                        {
                            string html_aux = "";
                            HtmlGenericControl spnHtml2 = (HtmlGenericControl)G_PRINCIPAL.Rows[Convert.ToInt32(e.CommandArgument)].Cells[14].FindControl("div_entregado");
                            if (tabla.entregado == "Entregado")
                            {
                                tabla.entregado = "Pendiente";
                                html_aux = "<span class='badge badge-danger'>Pendiente</span>";
                            }
                            else
                            {
                                tabla.entregado = "Entregado";
                                html_aux = "<span class='badge badge-success'>Entregado</span>";
                            }
                            FN_ENC_GT.UPDATE(ref tabla);
                            if (tabla._respok)
                            {
                                spnHtml2.InnerHtml = html_aux;
                            }
                        }
                    }
                    else
                    {
                        alert("Ud no tiene permisos para cambiar este estado", 0);
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void COMPLETAR_DETALLE(int id)
        {
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            OBJ_ENC_GT objeto_mantenedor = new OBJ_ENC_GT();

            objeto_mantenedor.ID_GT = id;
            FN_ENC_GT.LLENAOBJETO(ref objeto_mantenedor);
            if (objeto_mantenedor._respok)
            {
                LIMPIARCAMPOS();
                T_ID.Text = id.ToString();
                NUM_GT.Text = objeto_mantenedor.num_correlativo.ToString();
                CB_ESTADO_GT.SelectedValue = objeto_mantenedor.id_estado.ToString();
                CB_CONDUCTOR.SelectedValue = objeto_mantenedor.id_conductor.ToString();
                CB_CONDUCTOR2.SelectedValue = objeto_mantenedor.id_conductor2.ToString();
                CB_CAMION.SelectedValue = objeto_mantenedor.id_camion.ToString();
                T_TIPO_CAMION.Text = objeto_mantenedor.tipo_camion;
                CB_RAMPLA.SelectedValue = objeto_mantenedor.id_rampla.ToString();
                T_FECHA_INICIO.Text = objeto_mantenedor.fecha_inicio.ToString("yyyy-MM-dd");
                T_FECHA_FINAL.Text = objeto_mantenedor.fecha_termino.ToString("yyyy-MM-dd");
                T_FECHA_CREACION.Text = objeto_mantenedor.fecha_creacion.ToString("yyyy-MM-dd");
                T_DINERO_ENTREGADO.Text = objeto_mantenedor.dinero_entregado.ToString();
                //T_SOBREDEPOSITO.Text = objeto_mantenedor.sobre_deposito.ToString();
                T_DINERO_DEVUELTO.Text = objeto_mantenedor.dinero_devuelto.ToString();
                T_KM_INICIAL.Text = objeto_mantenedor.km_inicial.ToString();
                T_KM_FINAL.Text = objeto_mantenedor.km_final.ToString();
                T_OBSERVACION.Text = objeto_mantenedor.observacion;
                // NUEVO CALCULO SALDO
                int saldo_dinero_entregado_new = 0;
                if (objeto_mantenedor.ID_GT > 13155)
                {
                    saldo_dinero_entregado_new = Convert.ToInt32(db.Scalar(" select ISNULL(sum(valor),0) as 'valor'  from deposito_detalle where num_viaje = " + objeto_mantenedor.num_correlativo +
                                                          " and tipo in ('SALDO FONDO POR RENDIR',  'SALDO VIATICO') " +
                                                          " and estado = 'DEPOSITADO' ").ToString());
                }
                else
                {
                    saldo_dinero_entregado_new = int.Parse(db.Scalar("select (dinero_entregado - total_gastos - dinero_devuelto) from enc_gt where num_correlativo = " + objeto_mantenedor.num_correlativo).ToString());
                }

                if (saldo_dinero_entregado_new >= 0)
                {
                    T_SALDO_DEPOSITO.Text = saldo_dinero_entregado_new.ToString("#,##0");
                    T_SALDO_DEPOSITO.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    T_SALDO_DEPOSITO.Text = saldo_dinero_entregado_new.ToString("#,##0");
                    T_SALDO_DEPOSITO.ForeColor = System.Drawing.Color.Red;
                }
                if (objeto_mantenedor.entregado == "Entregado")
                {
                    CHK_ENTREGADO.Checked = true;
                }
                else if (objeto_mantenedor.entregado == "Pendiente")
                {
                    CHK_ENTREGADO.Checked = false;
                }
                // -- COMBUSTIBLE --
                LlenarGrillaCombustible();
                // -- GASTO GENERAL
                LlenarGrillaGastoGeneral();
                // -- OTZ
                LlenarGrillaOTZ();
            }
        }

        public void totalesgt()
        {
            try
            {
                DBUtil db = new DBUtil();
                int total_km = 0;
                int saldo_dinero = 0;
                int saldo_final = 0;

                // TOTAL KM
                try
                {
                    if (T_KM_INICIAL.Text != "" && T_KM_FINAL.Text != "")
                    {
                        total_km = int.Parse(T_KM_FINAL.Text) - int.Parse(T_KM_INICIAL.Text);
                        db.Scalar("update enc_gt set total_km = " + total_km + " where id_gt = " + T_ID.Text);
                    }
                }
                catch (Exception ex)
                {

                }

                // TOTAL SALDO DINERO
                try
                {
                    if (T_DINERO_ENTREGADO.Text != "")
                    {
                        int id_gt = Convert.ToInt32(T_ID.Text);                   
                        if (id_gt > 13155)
                        {
                            saldo_dinero = Convert.ToInt32(db.Scalar(" select ISNULL(sum(valor),0) as 'valor'  from deposito_detalle where num_viaje = " + NUM_GT.Text +
                                                                  " and tipo in ('SALDO FONDO POR RENDIR',  'SALDO VIATICO') " +
                                                                  " and estado = 'DEPOSITADO' ").ToString());
                        }
                        else
                        {
                            saldo_dinero = int.Parse(db.Scalar("select (dinero_entregado - total_gastos - dinero_devuelto) from enc_gt where num_correlativo = " + NUM_GT.Text).ToString());
                        }
                        
                        if (saldo_dinero >= 0)
                        {
                            T_SALDO_DEPOSITO.Text = saldo_dinero.ToString("#,##0");
                            T_SALDO_DEPOSITO.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            T_SALDO_DEPOSITO.Text = saldo_dinero.ToString("#,##0");
                            T_SALDO_DEPOSITO.ForeColor = System.Drawing.Color.Red;
                        }
                        db.Scalar("update enc_gt set saldo_dinero_entregado = " + saldo_dinero + " where id_gt = " + T_ID.Text);
                    }
                }
                catch (Exception ex)
                {

                }

                // SALDO TOTAL
                try
                {
                    saldo_final = int.Parse(db.Scalar("select (total_flete - total_gastos - total_precio_combustible) from enc_gt where id_gt = " + T_ID.Text).ToString());
                    db.Scalar("update enc_gt set saldo_total = " + saldo_final + " where id_gt = " + T_ID.Text);
                }
                catch (Exception ex)
                {

                }

                // RENDIMIENTO
                try
                {
                    // RENDIMIENTO
                    db.Scalar("update enc_gt set rendimiento = (select CONVERT(numeric(18,3), CONVERT(numeric(18,3),total_km)/CONVERT(numeric(18,3),total_litros)) from enc_gt where id_gt = " + T_ID.Text + ") where id_gt = " + T_ID.Text);
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region DETALLE DE OTZ

        public int correlativoOTZ()
        {
            try
            {
                string correlativo = "";

                DBUtil db = new DBUtil();
                int scalar = int.Parse(db.Scalar("select (ISNULL(MAX(otz),0) + 1) from correlativos").ToString());

                //string ano = DateTime.Now.ToString("yyyy");
                string ano = DateTime.Now.ToString("yy");

                if (scalar.ToString().Length == 1)
                {
                    correlativo = ano + "0000" + scalar.ToString();
                }
                else if (scalar.ToString().Length == 2)
                {
                    correlativo = ano + "000" + scalar.ToString();
                }
                else if (scalar.ToString().Length == 3)
                {
                    correlativo = ano + "00" + scalar.ToString();
                }
                else if (scalar.ToString().Length == 4)
                {
                    correlativo = ano + "0" + scalar.ToString();
                }
                else if (scalar.ToString().Length == 5)
                {
                    correlativo = ano + scalar.ToString();
                }
                int corr = int.Parse(correlativo);
                db.Scalar("update correlativos set otz = " + scalar);
                return corr;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        protected void B_AGREGAR_OTZ_Click(object sender, EventArgs e)
        {

            LimpiarCamposOTZ();
            TITULO_MODAL_OTZ.InnerHtml = "CREANDO NUEVA <b>OTZ</b>";
            UP_OTZ.Update();
            AbreModalOTZ();
        }

        public void AbreModalOTZ()
        {
            // ABRE MODAL
            ScriptManager.RegisterStartupScript(this, typeof(Page), "abremodalotz", "<script>javascript:OTZ();</script>", false);
        }

        protected void G_OTZ_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //editar
                if (e.CommandName == "Editar")
                {
                    int id = int.Parse((G_OTZ.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    COMPLETAR_OTZ(id);
                    // MOSTRAR / OCULTAR PANEL
                    UP_OTZ.Update();
                    TITULO_MODAL_OTZ.InnerHtml = "EDITANDO <b>OTZ</b>: " + T_OTZ_CORRELATIVO.Text;
                    AbreModalOTZ();
                }
                //Borrar
                if (e.CommandName == "Borrar")
                {
                    string user = HttpContext.Current.User.Identity.Name;
                    if (user == "festay" || user == "felipe")
                    {
                        int id = int.Parse((G_OTZ.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                        OBJ_ENC_OTZ tabla = new OBJ_ENC_OTZ();
                        tabla.ID_OTZ = id;
                        FN_ENC_OTZ.DELETE(ref tabla);
                        if (tabla._respok)
                        {
                            totalesotz();
                            LlenarGrillaOTZ();
                            alert("OTZ Eliminada con éxito", 1);
                        }
                    }
                    else
                    {
                        alert("Ud no tiene los permisos para eliminar OTZ's", 0);
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void LlenarGrillaOTZ()
        {
            G_OTZ.DataSource = FN_ENC_OTZ.LLENADTVISTA(" where id_gt = " + T_ID.Text + " ");
            G_OTZ.DataBind();
            UP_PRINCIPAL.Update();
            arreglatablas();
        }

        public void COMPLETAR_OTZ(int id)
        {
            OBJ_ENC_OTZ objeto_mantenedor = new OBJ_ENC_OTZ();
            objeto_mantenedor.ID_OTZ = id;
            FN_ENC_OTZ.LLENAOBJETO(ref objeto_mantenedor);

            if (objeto_mantenedor._respok)
            {
                T_ID_OTZ.Text = id.ToString();
                T_OTZ_CORRELATIVO.Text = objeto_mantenedor.correlativo_otz.ToString();
                CB_CIUDAD_ORIGEN.SelectedValue = objeto_mantenedor.origen.ToString();
                CB_CIUDAD_DESTINO.SelectedValue = objeto_mantenedor.destino.ToString();
                CB_OBRA.SelectedValue = objeto_mantenedor.id_obra.ToString();
                CB_OTZ_CLIENTE.SelectedValue = objeto_mantenedor.id_cliente.ToString();
                T_OTZ_GUIA.Text = objeto_mantenedor.guia;
                T_OTZ_FECHA_INICIO.Text = objeto_mantenedor.fecha_inicio.ToString("yyyy-MM-dd");
                T_OTZ_FECHA_FINAL.Text = objeto_mantenedor.fecha_final.ToString("yyyy-MM-dd");
                T_OTZ_VALOR.Text = objeto_mantenedor.valor_viaje.ToString();
                T_OTZ_ENTRADAS.Text = objeto_mantenedor.entradas.ToString();
                T_OTZ_ESTADIA.Text = objeto_mantenedor.estadia.ToString();
                T_OTZ_DOBLE_CONDUCTOR.Text = objeto_mantenedor.doble_conductor.ToString();
                T_OTZ_CARGA_DESCARGA.Text = objeto_mantenedor.carga_descarga.ToString();
                T_OTZ_FLETE_DE_TERCERO.Text = objeto_mantenedor.flete_de_tercero.ToString();
                T_OTZ_OTROS.Text = objeto_mantenedor.otros.ToString();
                T_OTZ_OTROS_DETALLE.Text = objeto_mantenedor.detalle_otros.ToString();

                T_OTZ_SOL_OC.Text = objeto_mantenedor.d_sol_oc;
                T_OTZ_OT.Text = objeto_mantenedor.d_ot;
                T_OTZ_EEPP.Text = objeto_mantenedor.d_eepp;
                T_OTZ_GASTO.Text = objeto_mantenedor.d_gasto;
                T_OTZ_D_OC.Text = objeto_mantenedor.d_oc;
                T_OTZ_HES.Text = objeto_mantenedor.d_hes;
                T_OTZ_FACTURA.Text = objeto_mantenedor.d_factura;
                T_OTZ_NOMBRE_TERCERO.Text = objeto_mantenedor.d_nombre_tercero;
                T_OTZ_EEPP_TERCERO.Text = objeto_mantenedor.d_eepp_tercero;
                T_OTZ_FACTURA_DE_TERCERO.Text = objeto_mantenedor.d_factura_tercero;
                T_OTZ_DIFERENCIA_FACTURA.Text = objeto_mantenedor.diferencia_factura.ToString();
                T_OTZ_OBSERVACION_FACTURA.Text = objeto_mantenedor.observacion_factura;

                UP_OTZ.Update();
            }

        }

        protected void B_GUARDAR_OTZ_Click(object sender, EventArgs e)
        {
            if (CB_CIUDAD_ORIGEN.SelectedValue == "0")
            {
                alert("Seleccione una ciudad de origen", 0);
                CB_CIUDAD_ORIGEN.Focus();
            }
            else if (CB_CIUDAD_DESTINO.SelectedValue == "0")
            {
                alert("Seleccione una ciudad de destino", 0);
                CB_CIUDAD_DESTINO.Focus();
            }
            else if (CB_OTZ_CLIENTE.SelectedValue == "0")
            {
                alert("Seleccione un cliente", 0);
                CB_OTZ_CLIENTE.Focus();
            }
            else if (T_OTZ_FECHA_INICIO.Text == "")
            {
                alert("Seleccione una fecha de inicio.", 0);
                T_OTZ_FECHA_INICIO.Focus();
            }
            else if (T_OTZ_FECHA_FINAL.Text == "")
            {
                alert("Seleccione una fecha de termino.", 0);
                T_OTZ_FECHA_FINAL.Focus();
            }
            else if (T_OTZ_VALOR.Text == "")
            {
                alert("Ingrese un valor para la OTZ.", 0);
                T_OTZ_VALOR.Focus();
            }
            else if (T_OTZ_CORRELATIVO.Text == "")
            {
                alert("Ingrese un correlativo para la OTZ.", 0);
                T_OTZ_CORRELATIVO.Focus();
            }
            else if (CB_CAMION.SelectedItem.Text == "Terceros" && T_OTZ_NOMBRE_TERCERO.Text == "")
            {
                alert("Si el servicio es de terceros, ingrese el nombre de tercero en la OTZ", 0);
                T_OTZ_NOMBRE_TERCERO.Focus();
            }
            else
            {
                if (T_ID_OTZ.Text != "")
                {
                    // EDITAR
                    OBJ_ENC_OTZ carga = new OBJ_ENC_OTZ();
                    carga.ID_OTZ = int.Parse(T_ID_OTZ.Text);
                    FN_ENC_OTZ.LLENAOBJETO(ref carga);
                    if (carga._respok)
                    {
                        carga.id_gt = int.Parse(T_ID.Text);
                        carga.correlativo_gt = int.Parse(NUM_GT.Text);
                        carga.correlativo_otz = int.Parse(T_OTZ_CORRELATIVO.Text);
                        carga.origen = CB_CIUDAD_ORIGEN.SelectedValue;
                        carga.destino = CB_CIUDAD_DESTINO.SelectedValue;
                        carga.id_obra = int.Parse(CB_OBRA.SelectedValue);
                        carga.id_cliente = int.Parse(CB_OTZ_CLIENTE.SelectedValue);
                        carga.fecha_inicio = DateTime.Parse(T_OTZ_FECHA_INICIO.Text);
                        carga.fecha_final = DateTime.Parse(T_OTZ_FECHA_FINAL.Text);
                        carga.valor_viaje = int.Parse(T_OTZ_VALOR.Text);
                        carga.guia = T_OTZ_GUIA.Text;
                        carga.d_sol_oc = T_OTZ_SOL_OC.Text;
                        carga.d_ot = T_OTZ_OT.Text;
                        carga.d_eepp = T_OTZ_EEPP.Text;
                        carga.d_gasto = T_OTZ_GASTO.Text;
                        carga.d_oc = T_OTZ_D_OC.Text;
                        carga.d_hes = T_OTZ_HES.Text;
                        carga.d_factura = T_OTZ_FACTURA.Text;
                        carga.d_nombre_tercero = T_OTZ_NOMBRE_TERCERO.Text;
                        carga.d_eepp_tercero = T_OTZ_EEPP_TERCERO.Text;
                        carga.d_factura_tercero = T_OTZ_FACTURA_DE_TERCERO.Text;
                        carga.observacion_factura = T_OTZ_OBSERVACION_FACTURA.Text;
                        if (T_OTZ_ESTADIA.Text != "")
                        {
                            carga.estadia = int.Parse(T_OTZ_ESTADIA.Text);
                        }
                        if (T_OTZ_ENTRADAS.Text != "")
                        {
                            carga.entradas = int.Parse(T_OTZ_ENTRADAS.Text);
                        }
                        if (T_OTZ_DOBLE_CONDUCTOR.Text != "")
                        {
                            carga.doble_conductor = int.Parse(T_OTZ_DOBLE_CONDUCTOR.Text);
                        }
                        if (T_OTZ_CARGA_DESCARGA.Text != "")
                        {
                            carga.carga_descarga = int.Parse(T_OTZ_CARGA_DESCARGA.Text);
                        }
                        if (T_OTZ_FLETE_DE_TERCERO.Text != "")
                        {
                            carga.flete_de_tercero = int.Parse(T_OTZ_FLETE_DE_TERCERO.Text);
                        }
                        if (T_OTZ_OTROS.Text != "")
                        {
                            carga.otros = int.Parse(T_OTZ_OTROS.Text);
                        }
                        if (T_OTZ_DIFERENCIA_FACTURA.Text != "")
                            carga.diferencia_factura = int.Parse(T_OTZ_DIFERENCIA_FACTURA.Text);
                        carga.detalle_otros = T_OTZ_OTROS_DETALLE.Text;

                        FN_ENC_OTZ.UPDATE(ref carga);
                        if (carga._respok)
                        {
                            totalesotz();
                            LlenarGrillaOTZ();
                            alert("OTZ modificado con éxito", 1);
                        }
                    }
                }
                else
                {
                    // NUEVO
                    OBJ_ENC_OTZ carga = new OBJ_ENC_OTZ();
                    FN_ENC_OTZ.PREPARAOBJETO(ref carga);

                    carga.id_gt = int.Parse(T_ID.Text);
                    carga.correlativo_gt = int.Parse(NUM_GT.Text);
                    carga.correlativo_otz = int.Parse(T_OTZ_CORRELATIVO.Text);
                    carga.origen = CB_CIUDAD_ORIGEN.SelectedValue;
                    carga.destino = CB_CIUDAD_DESTINO.SelectedValue;
                    carga.id_obra = int.Parse(CB_OBRA.SelectedValue);
                    carga.id_cliente = int.Parse(CB_OTZ_CLIENTE.SelectedValue);
                    carga.fecha_inicio = DateTime.Parse(T_OTZ_FECHA_INICIO.Text);
                    carga.fecha_final = DateTime.Parse(T_OTZ_FECHA_FINAL.Text);
                    carga.valor_viaje = int.Parse(T_OTZ_VALOR.Text);
                    carga.guia = T_OTZ_GUIA.Text;
                    carga.d_sol_oc = T_OTZ_SOL_OC.Text;
                    carga.d_ot = T_OTZ_OT.Text;
                    carga.d_eepp = T_OTZ_EEPP.Text;
                    carga.d_gasto = T_OTZ_GASTO.Text;
                    carga.d_oc = T_OTZ_D_OC.Text;
                    carga.d_hes = T_OTZ_HES.Text;
                    carga.d_factura = T_OTZ_FACTURA.Text;
                    carga.d_nombre_tercero = T_OTZ_NOMBRE_TERCERO.Text;
                    carga.d_eepp_tercero = T_OTZ_EEPP_TERCERO.Text;
                    carga.d_factura_tercero = T_OTZ_FACTURA_DE_TERCERO.Text;
                    carga.observacion_factura = T_OTZ_OBSERVACION_FACTURA.Text;
                    if (T_OTZ_ESTADIA.Text != "")
                    {
                        carga.estadia = int.Parse(T_OTZ_ESTADIA.Text);
                    }
                    if (T_OTZ_ENTRADAS.Text != "")
                    {
                        carga.entradas = int.Parse(T_OTZ_ENTRADAS.Text);
                    }
                    if (T_OTZ_DOBLE_CONDUCTOR.Text != "")
                    {
                        carga.doble_conductor = int.Parse(T_OTZ_DOBLE_CONDUCTOR.Text);
                    }
                    if (T_OTZ_CARGA_DESCARGA.Text != "")
                    {
                        carga.carga_descarga = int.Parse(T_OTZ_CARGA_DESCARGA.Text);
                    }
                    if (T_OTZ_FLETE_DE_TERCERO.Text != "")
                    {
                        carga.flete_de_tercero = int.Parse(T_OTZ_FLETE_DE_TERCERO.Text);
                    }
                    if (T_OTZ_OTROS.Text != "")
                    {
                        carga.otros = int.Parse(T_OTZ_OTROS.Text);
                    }
                    if (T_OTZ_DIFERENCIA_FACTURA.Text != "")
                        carga.diferencia_factura = int.Parse(T_OTZ_DIFERENCIA_FACTURA.Text);
                    carga.detalle_otros = T_OTZ_OTROS_DETALLE.Text;

                    FN_ENC_OTZ.INSERT(ref carga);
                    if (carga._respok)
                    {
                        totalesotz();
                        LlenarGrillaOTZ();
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrarmodalotz", "<script>javascript:CerrarModalOTZ();</script>", false);
                        alert("OTZ agregada con éxito", 1);

                    }
                    else
                    {
                        alert("Error al ingresar OTZ", 0);
                    }
                }
            }
        }

        public void LimpiarCamposOTZ()
        {
            int scalar = correlativoOTZ();
            T_OTZ_CORRELATIVO.Text = scalar.ToString();
            T_ID_OTZ.Text = string.Empty;
            CB_CIUDAD_ORIGEN.SelectedValue = "0";
            CB_CIUDAD_DESTINO.SelectedValue = "0";
            CB_OBRA.SelectedValue = "0";
            CB_OTZ_CLIENTE.SelectedValue = "0";
            T_OTZ_GUIA.Text = string.Empty;
            T_OTZ_FECHA_INICIO.Text = string.Empty;
            T_OTZ_FECHA_FINAL.Text = string.Empty;
            T_OTZ_VALOR.Text = string.Empty;
            T_OTZ_ENTRADAS.Text = string.Empty;
            T_OTZ_ESTADIA.Text = string.Empty;
            T_OTZ_DOBLE_CONDUCTOR.Text = string.Empty;
            T_OTZ_CARGA_DESCARGA.Text = string.Empty;
            T_OTZ_FLETE_DE_TERCERO.Text = string.Empty;
            T_OTZ_OTROS.Text = string.Empty;
            T_OTZ_OTROS_DETALLE.Text = string.Empty;
            T_OTZ_SOL_OC.Text = string.Empty;
            T_OTZ_OT.Text = string.Empty;
            T_OTZ_EEPP.Text = string.Empty;
            T_OTZ_GASTO.Text = string.Empty;
            T_OTZ_D_OC.Text = string.Empty;
            T_OTZ_HES.Text = string.Empty;
            T_OTZ_FACTURA.Text = string.Empty;
            T_OTZ_NOMBRE_TERCERO.Text = string.Empty;
            T_OTZ_FACTURA_DE_TERCERO.Text = string.Empty;
            T_OTZ_DIFERENCIA_FACTURA.Text = string.Empty;
            T_OTZ_OBSERVACION_FACTURA.Text = string.Empty;
        }

        public void totalesotz()
        {
            try
            {
                DBUtil db = new DBUtil();
                db.Scalar("update enc_gt set total_flete = (select ISNULL(SUM(valor_viaje + entradas + estadia + doble_conductor + carga_descarga + otros + diferencia_factura - flete_de_tercero),0) as 'suma' from enc_otz where id_gt = " + T_ID.Text + ") where id_gt = " + T_ID.Text);
                int saldo_final = int.Parse(db.Scalar("select (total_flete - total_gastos - total_precio_combustible) from enc_gt where id_gt = " + T_ID.Text).ToString());
                db.Scalar("update enc_gt set saldo_total = " + saldo_final + " where id_gt = " + T_ID.Text);
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region CARGA DE COMBUSTIBLE
        // ----------------- COMBUSTIBLE
        // ******************************************************************************************************
        // ******************************************************************************************************
        // ******************************************************************************************************
        // ******************************************************************************************************
        protected void G_COMBUSTIBLE_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //editar
                if (e.CommandName == "Editar")
                {
                    int id = int.Parse((G_COMBUSTIBLE.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    COMPLETAR_COMBUSTIBLE(id);
                    // MOSTRAR / OCULTAR PANEL
                    UP_COMBUSTIBLE.Update();
                    AbreModalCombustible();
                }
                //Borrar
                if (e.CommandName == "Borrar")
                {
                    int id = int.Parse((G_COMBUSTIBLE.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    OBJ_CARGA_COMBUSTIBLE tabla = new OBJ_CARGA_COMBUSTIBLE();
                    tabla.ID_CARGA = id;
                    FN_CARGA_COMBUSTIBLE.DELETE(ref tabla);
                    if (tabla._respok)
                    {
                        totalcombustible();
                        LlenarGrillaCombustible();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void GUARDAR_COMBUSTIBLE_Click(object sender, EventArgs e)
        {
            if (T_COMBUSTIBLE_FECHACARGA.Text == "")
            {
                alert("Ingrese una fecha de carga.", 0);
                T_COMBUSTIBLE_FECHACARGA.Focus();
            }
            else if (T_COMBUSTIBLE_GUIA.Text == "")
            {
                alert("Ingrese el numero de Guia.", 0);
                T_COMBUSTIBLE_GUIA.Focus();
            }
            else if (T_COMBUSTIBLE_KM.Text == "")
            {
                alert("Ingrese el Odometro (KM).", 0);
                T_COMBUSTIBLE_KM.Focus();
            }
            else if (T_COMBUSTIBLE_LITROS.Text == "")
            {
                alert("Ingrese los litros cargados.", 0);
                T_COMBUSTIBLE_LITROS.Focus();
            }
            else if (T_COMBUSTIBLE_PRECIO.Text == "")
            {
                alert("Ingrese el valor.", 0);
                T_COMBUSTIBLE_PRECIO.Focus();
            }
            else
            {
                if (T_ID_COMBUSTIBLE.Text != "")
                {
                    // EDITAR
                    OBJ_CARGA_COMBUSTIBLE carga = new OBJ_CARGA_COMBUSTIBLE();
                    carga.ID_CARGA = int.Parse(T_ID_COMBUSTIBLE.Text);
                    FN_CARGA_COMBUSTIBLE.LLENAOBJETO(ref carga);
                    if (carga._respok)
                    {
                        carga.id_gt = int.Parse(T_ID.Text);
                        carga.estacion = CB_ESTACION_SERVICIO.SelectedValue;
                        carga.fecha = DateTime.Parse(T_COMBUSTIBLE_FECHACARGA.Text);
                        carga.guia = T_COMBUSTIBLE_GUIA.Text;
                        carga.rollo = T_COMBUSTIBLE_ROLLO.Text;
                        carga.kilometraje = int.Parse(T_COMBUSTIBLE_KM.Text);
                        carga.litros_cargados = double.Parse(T_COMBUSTIBLE_LITROS.Text.Replace(".", ","));
                        carga.precio = int.Parse(T_COMBUSTIBLE_PRECIO.Text);

                        FN_CARGA_COMBUSTIBLE.UPDATE(ref carga);
                        if (carga._respok)
                        {
                            totalcombustible();
                            LlenarGrillaCombustible();
                            alert("Carga de combustible modificada con éxito", 1);
                        }
                    }
                }
                else
                {
                    // NUEVO
                    OBJ_CARGA_COMBUSTIBLE carga = new OBJ_CARGA_COMBUSTIBLE();
                    FN_CARGA_COMBUSTIBLE.PREPARAOBJETO(ref carga);

                    carga.id_gt = int.Parse(T_ID.Text);
                    carga.estacion = CB_ESTACION_SERVICIO.SelectedValue;
                    carga.fecha = DateTime.Parse(T_COMBUSTIBLE_FECHACARGA.Text);
                    carga.guia = T_COMBUSTIBLE_GUIA.Text;
                    carga.rollo = T_COMBUSTIBLE_ROLLO.Text;
                    carga.kilometraje = int.Parse(T_COMBUSTIBLE_KM.Text);
                    carga.litros_cargados = double.Parse(T_COMBUSTIBLE_LITROS.Text.Replace(".", ","));
                    carga.precio = int.Parse(T_COMBUSTIBLE_PRECIO.Text);


                    FN_CARGA_COMBUSTIBLE.INSERT(ref carga);
                    if (carga._respok)
                    {
                        totalcombustible();
                        LlenarGrillaCombustible();
                        LimpiarCamposCombustible();
                        alert("Carga de combustible agregada con éxito", 1);
                    }
                    else
                    {
                        alert("Error al ingresar carga de combustible", 0);
                    }
                }

            }
        }

        public void LlenarGrillaCombustible()
        {
            G_COMBUSTIBLE.DataSource = FN_CARGA_COMBUSTIBLE.LLENADTVISTA(" where id_gt = " + T_ID.Text + " ");
            G_COMBUSTIBLE.DataBind();
            UP_PRINCIPAL.Update();
            arreglatablas();
        }

        public void COMPLETAR_COMBUSTIBLE(int id)
        {
            OBJ_CARGA_COMBUSTIBLE objeto_mantenedor = new OBJ_CARGA_COMBUSTIBLE();
            objeto_mantenedor.ID_CARGA = id;
            FN_CARGA_COMBUSTIBLE.LLENAOBJETO(ref objeto_mantenedor);

            if (objeto_mantenedor._respok)
            {
                T_ID_COMBUSTIBLE.Text = id.ToString();
                CB_ESTACION_SERVICIO.SelectedValue = objeto_mantenedor.estacion.ToString();
                T_COMBUSTIBLE_FECHACARGA.Text = objeto_mantenedor.fecha.ToString("yyyy-MM-dd");
                T_COMBUSTIBLE_GUIA.Text = objeto_mantenedor.guia.ToString();
                T_COMBUSTIBLE_KM.Text = objeto_mantenedor.kilometraje.ToString();
                T_COMBUSTIBLE_ROLLO.Text = objeto_mantenedor.rollo.ToString();
                T_COMBUSTIBLE_LITROS.Text = objeto_mantenedor.litros_cargados.ToString().Replace(",", ".");
                T_COMBUSTIBLE_PRECIO.Text = objeto_mantenedor.precio.ToString();
                UP_COMBUSTIBLE.Update();
            }
        }

        public void LimpiarCamposCombustible()
        {
            T_ID_COMBUSTIBLE.Text = string.Empty;
            CB_ESTACION_SERVICIO.SelectedValue = "0";
            T_COMBUSTIBLE_FECHACARGA.Text = string.Empty;
            T_COMBUSTIBLE_GUIA.Text = string.Empty;
            T_COMBUSTIBLE_KM.Text = string.Empty;
            T_COMBUSTIBLE_ROLLO.Text = string.Empty;
            T_COMBUSTIBLE_LITROS.Text = string.Empty;
            T_COMBUSTIBLE_PRECIO.Text = string.Empty;
        }

        protected void AGREGAR_COMBUSTIBLE_Click(object sender, EventArgs e)
        {
            T_ID_COMBUSTIBLE.Text = "";
            UP_COMBUSTIBLE.Update();
            AbreModalCombustible();
        }

        public void AbreModalCombustible()
        {
            // ABRE MODAL
            ScriptManager.RegisterStartupScript(this, typeof(Page), "abremodalcombustible", "<script>javascript:CARGARCOMBUSTIBLE();</script>", false);
        }

        public void totalcombustible()
        {
            try
            {
                DBUtil db = new DBUtil();
                string sql = "";
                sql += " update enc_gt set " +
                        " total_precio_combustible = (select ISNULL(SUM(precio),0) as 'suma' from carga_combustible where id_gt = " + T_ID.Text + "), " +
                        " total_litros = (select ISNULL(SUM(litros_cargados),0) as 'suma' from carga_combustible where id_gt = " + T_ID.Text + ") " +
                        " where id_gt = " + T_ID.Text;
                db.Scalar(sql);

                int saldo_final = int.Parse(db.Scalar("select (total_flete - total_gastos - total_precio_combustible) from enc_gt where id_gt = " + T_ID.Text).ToString());
                db.Scalar("update enc_gt set saldo_total = " + saldo_final + " where id_gt = " + T_ID.Text);

                // RENDIMIENTO
                db.Scalar("update enc_gt set rendimiento = (select CONVERT(numeric(18,3), CONVERT(numeric(18,3),total_km)/CONVERT(numeric(18,3),total_litros)) from enc_gt where id_gt = " + T_ID.Text + ") where id_gt = " + T_ID.Text);
            }
            catch (Exception ex)
            {

            }
        }
        // ******************************************************************************************************
        // ******************************************************************************************************
        // ******************************************************************************************************
        // ******************************************************************************************************
        #endregion

        #region GASTOS GENERALES
        // ----------------- GASTO GENERAL

        protected void B_AGREGAR_GASTOGRAL_Click(object sender, EventArgs e)
        {
            T_ID_GASTO.Text = "";
            UP_GASTO_GENERAL.Update();
            AbreModalGasto();
        }

        protected void G_GASTOGRAL_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //editar
                if (e.CommandName == "Editar")
                {
                    int id = int.Parse((G_GASTOGRAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    COMPLETAR_GASTO_GENERAL(id);
                    // MOSTRAR / OCULTAR PANEL
                    UP_GASTO_GENERAL.Update();
                    AbreModalGasto();
                }
                //Borrar
                if (e.CommandName == "Borrar")
                {
                    int id = int.Parse((G_GASTOGRAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    OBJ_GASTO_GENERAL tabla = new OBJ_GASTO_GENERAL();
                    tabla.ID_GASTO = id;
                    FN_GASTO_GENERAL.DELETE(ref tabla);
                    if (tabla._respok)
                    {
                        totalesgastos();
                        LlenarGrillaGastoGeneral();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void COMPLETAR_GASTO_GENERAL(int id)
        {
            OBJ_GASTO_GENERAL objeto_mantenedor = new OBJ_GASTO_GENERAL();
            objeto_mantenedor.ID_GASTO = id;
            FN_GASTO_GENERAL.LLENAOBJETO(ref objeto_mantenedor);

            if (objeto_mantenedor._respok)
            {
                T_ID_GASTO.Text = id.ToString();
                T_GASTO_DETALLE.Text = objeto_mantenedor.detalle.ToString();
                CB_GASTO_TIPO.SelectedValue = objeto_mantenedor.tipo_gasto.ToString();
                T_GASTO_VALOR.Text = objeto_mantenedor.valor.ToString();
                UP_GASTO_GENERAL.Update();
            }
        }

        public void LimpiarCamposGG()
        {
            T_ID_GASTO.Text = string.Empty;
            T_GASTO_DETALLE.Text = string.Empty;
            CB_GASTO_TIPO.SelectedValue = "0";
            T_GASTO_VALOR.Text = string.Empty;
        }

        protected void B_GUARDAR_GASTOSGRAL_Click(object sender, EventArgs e)
        {
            if (CB_GASTO_TIPO.SelectedValue == "0")
            {
                alert("Seleccione un tipo de gasto.", 0);
                CB_GASTO_TIPO.Focus();
            }
            else if (T_GASTO_VALOR.Text == "")
            {
                alert("Ingrese el valor.", 0);
                T_GASTO_VALOR.Focus();
            }
            else
            {
                if (T_ID_GASTO.Text != "")
                {
                    // EDITAR
                    OBJ_GASTO_GENERAL carga = new OBJ_GASTO_GENERAL();
                    carga.ID_GASTO = int.Parse(T_ID_GASTO.Text);
                    FN_GASTO_GENERAL.LLENAOBJETO(ref carga);
                    if (carga._respok)
                    {
                        carga.id_gt = int.Parse(T_ID.Text);
                        carga.detalle = T_GASTO_DETALLE.Text;
                        carga.tipo_gasto = int.Parse(CB_GASTO_TIPO.SelectedValue);
                        carga.valor = int.Parse(T_GASTO_VALOR.Text);
                        FN_GASTO_GENERAL.UPDATE(ref carga);
                        if (carga._respok)
                        {
                            totalesgastos();
                            LlenarGrillaGastoGeneral();
                            alert("Gasto General modificado con éxito", 1);
                        }
                    }
                }
                else
                {
                    // NUEVO
                    OBJ_GASTO_GENERAL carga = new OBJ_GASTO_GENERAL();
                    FN_GASTO_GENERAL.PREPARAOBJETO(ref carga);

                    carga.id_gt = int.Parse(T_ID.Text);
                    carga.detalle = T_GASTO_DETALLE.Text;
                    carga.tipo_gasto = int.Parse(CB_GASTO_TIPO.SelectedValue);
                    carga.valor = int.Parse(T_GASTO_VALOR.Text);
                    FN_GASTO_GENERAL.INSERT(ref carga);
                    if (carga._respok)
                    {
                        totalesgastos();
                        LlenarGrillaGastoGeneral();
                        LimpiarCamposGG();
                        alert("Gasto General agregado con éxito", 1);
                    }
                    else
                    {
                        alert("Error al ingresar Gasto General", 0);
                    }
                }
            }
        }

        public void AbreModalGasto()
        {
            // ABRE MODAL
            ScriptManager.RegisterStartupScript(this, typeof(Page), "abremodalgasto", "<script>javascript:GASTOGENERAL();</script>", false);
        }

        public void LlenarGrillaGastoGeneral()
        {
            G_GASTOGRAL.DataSource = FN_GASTO_GENERAL.LLENADTVISTA(" where id_gt = " + T_ID.Text + " ");
            G_GASTOGRAL.DataBind();
            UP_PRINCIPAL.Update();
            arreglatablas();
        }

        public void totalesgastos()
        {
            try
            {
                DBUtil db = new DBUtil();
                db.Scalar("update enc_gt set total_gastos = (select ISNULL(SUM(valor),0) as 'suma' from gasto_general where id_gt = " + T_ID.Text + ") where id_gt = " + T_ID.Text);
                int saldo_final = int.Parse(db.Scalar("select (total_flete - total_gastos - total_precio_combustible) from enc_gt where id_gt = " + T_ID.Text).ToString());
                db.Scalar("update enc_gt set saldo_total = " + saldo_final + " where id_gt = " + T_ID.Text);
                int id_gt = Convert.ToInt32(T_ID.Text);
                int saldo_dinero = 0;
                if (id_gt > 13155)
                {
                    saldo_dinero = Convert.ToInt32(db.Scalar(" select ISNULL(sum(valor),0) as 'valor'  from deposito_detalle where num_viaje = " + NUM_GT.Text +
                                                          " and tipo in ('SALDO FONDO POR RENDIR',  'SALDO VIATICO') " +
                                                          " and estado = 'DEPOSITADO' ").ToString());
                }
                else
                {
                    saldo_dinero = int.Parse(db.Scalar("select (dinero_entregado - total_gastos - dinero_devuelto) from enc_gt where num_correlativo = " + NUM_GT.Text).ToString());
                }
                //= int.Parse(db.Scalar("select (dinero_entregado - total_gastos) from enc_gt where id_gt = " + T_ID.Text).ToString());
                db.Scalar("update enc_gt set saldo_dinero_entregado = " + saldo_dinero + " where id_gt = " + T_ID.Text);

                if (saldo_dinero >= 0)
                {
                    T_SALDO_DEPOSITO.Text = saldo_dinero.ToString("#,##0");
                    T_SALDO_DEPOSITO.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    T_SALDO_DEPOSITO.Text = saldo_dinero.ToString("#,##0");
                    T_SALDO_DEPOSITO.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region ---------------- NO CAMBIAR ---------------- 
        public void LIMPIARCAMPOS()
        {
            CleanControl(this.Controls);
            T_DINERO_ENTREGADO.Text = "0";
        }

        public void CleanControl(ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                if (control is TextBox)
                    ((TextBox)control).Text = string.Empty;
                else if (control is DropDownList)
                    ((DropDownList)control).ClearSelection();
                else if (control is RadioButtonList)
                    ((RadioButtonList)control).ClearSelection();
                else if (control is CheckBoxList)
                    ((CheckBoxList)control).ClearSelection();
                else if (control is RadioButton)
                    ((RadioButton)control).Checked = false;
                else if (control is CheckBox)
                    ((CheckBox)control).Checked = false;
                else if (control.HasControls())
                    CleanControl(control.Controls);
            }
        }

        protected void alert(string mensaje, int flag)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "mosnoti", "<script>javascript:MostrarNotificacion('" + mensaje + "', " + flag + ");</script>", false);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            MakeAccessible(G_PRINCIPAL);
            MakeAccessible(G_COMBUSTIBLE);
            MakeAccessible(G_GASTOGRAL);
            MakeAccessible(G_OTZ);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            Page.ClientScript.RegisterForEventValidation(this.UniqueID);
            base.Render(writer);
        }

        public static void MakeAccessible(GridView grid)
        {
            if (grid.Rows.Count <= 0) return;
            grid.UseAccessibleHeader = true;
            grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            grid.PagerStyle.CssClass = "GridPager";
            if (grid.ShowFooter)
                grid.FooterRow.TableSection = TableRowSection.TableFooter;
        }




        #endregion

        protected void G_PRINCIPAL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string saldo = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[1].ToString();
                string estado = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[2].ToString();
                string saldo_dinero = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[3].ToString();
                string entregado = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[4].ToString();
                // ESTADO
                HtmlGenericControl spnHtml = (HtmlGenericControl)e.Row.FindControl("div_estado");
                if (estado == "1")
                {
                    spnHtml.InnerHtml = "<span class='badge badge-primary'>Abierta</span>";
                }
                else if (estado == "2")
                {
                    spnHtml.InnerHtml = "<span class='badge badge-success'>Viaje Terminado</span>";
                }
                else
                {
                    spnHtml.InnerHtml = "<span class='badge badge-success'>Cerrada</span>";
                }
                // SALDO
                int saldo_ = int.Parse(saldo);
                if (saldo_ >= 0)
                {
                    e.Row.Cells[15].ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    e.Row.Cells[15].ForeColor = System.Drawing.Color.Red;
                }
                // SALDO DINERO
                int sald_dinero2 = int.Parse(saldo_dinero);
                if (sald_dinero2 >= 0)
                {
                    e.Row.Cells[13].ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    e.Row.Cells[13].ForeColor = System.Drawing.Color.Red;
                }
                HtmlGenericControl spnHtml2 = (HtmlGenericControl)e.Row.FindControl("div_entregado");
                if (entregado == "Entregado")
                {
                    spnHtml2.InnerHtml = "<span class='badge badge-success'>Entregado</span>";
                }
                else
                {
                    spnHtml2.InnerHtml = "<span class='badge badge-danger'>Pendiente</span>";
                }

            }
        }
    }
}