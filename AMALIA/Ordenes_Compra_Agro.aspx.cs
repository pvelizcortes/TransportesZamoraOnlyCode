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
    public partial class Ordenes_Compra_Agro : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Compra";
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
                    if (usuario.usuario != "mzapata" && usuario.usuario != "felipe" && usuario.usuario != "festay" && usuario.usuario != "gestay")
                    {
                        Response.Redirect("index.aspx");
                    }
                    CargarCombos();
                    LlenarGrillaInicio();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "gridoc", "<script>javascript:Datatables();</script>", false);
            }
        }

        public void LlenarGrillaInicio()
        {
            FILTRA_FECHA_DESDE.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            LlenarGrilla();
        }

        public void LlenarGrilla()
        {
            string filtro = " where 1=1 ";
            if (FILTRA_FECHA_DESDE.Text != "")
            {
                filtro += " and fecha_oc >= convert(date, '" + DateTime.Parse(FILTRA_FECHA_DESDE.Text).ToString("dd/MM/yyyy") + "', 103) ";
            }
            if (FILTRA_FECHA_HASTA.Text != "")
            {
                filtro += " and fecha_oc <= convert(date, '" + DateTime.Parse(FILTRA_FECHA_HASTA.Text).ToString("dd/MM/yyyy") + "', 103) ";
            }
            if (FILTRA_GT_DESDE.Text != "")
            {
                filtro += " and correlativo_oc >= " + FILTRA_GT_DESDE.Text;
            }
            if (FILTRA_GT_HASTA.Text != "")
            {
                filtro += " and correlativo_oc <= " + FILTRA_GT_HASTA.Text;
            }
            if (CB_USUARIOS.SelectedValue != "-1")
            {
                filtro += " and usuario = '" + CB_USUARIOS.SelectedValue + "'";
            }
            if (FILTRA_FACTURA.Text != "")
            {
                filtro += " and num_factura = '" + FILTRA_FACTURA.Text + "'";
            }
            //
            if (CB_FILTRO_AUT_FZ.SelectedValue != "-1")
            {
                filtro += " and aprobado_fz = '" + CB_FILTRO_AUT_FZ.SelectedValue + "'";
            }
            if (CB_FILTRO_AUT_MZ.SelectedValue != "-1")
            {
                filtro += " and aprobado_mz = '" + CB_FILTRO_AUT_MZ.SelectedValue + "'";
            }
            if (CB_FILTRO_CANCELADA.SelectedValue != "-1")
            {
                filtro += " and cancelada = '" + CB_FILTRO_CANCELADA.SelectedValue + "'";
            }
            if (CB_FILTRO_FACTURADA.SelectedValue != "-1")
            {
                filtro += " and facturada = '" + CB_FILTRO_FACTURADA.SelectedValue + "'";
            }
            if (CB_FILTRO_SIS_FACT.SelectedValue != "-1")
            {
                filtro += " and sistfact = '" + CB_FILTRO_SIS_FACT.SelectedValue + "'";
            }
            if (CB_FILTRO_ESTADO_FACT.SelectedValue != "-1")
            {
                filtro += " and estado = '" + CB_FILTRO_ESTADO_FACT.SelectedValue + "'";
            }

            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            dt = db.consultar("select * from [V_PV_ORDENES_COMPRA_DETALLE] " + filtro + " order by correlativo_oc desc;");
            G_PRINCIPAL.DataSource = dt;
            G_PRINCIPAL.DataBind();

        }
        public void CargarCombos()
        {
            // Llenar combo camión
            DBUtil db = new DBUtil();

            CB_PROVEEDOR.DataSource = db.consultar("select id_oc_proveedor, CONCAT(rut,' - ', nombre_corto) as 'nombre' from oc_proveedores order by nombre_corto");
            CB_PROVEEDOR.DataTextField = "nombre";
            CB_PROVEEDOR.DataValueField = "id_oc_proveedor";
            CB_PROVEEDOR.DataBind();
            CB_PROVEEDOR.Items.Insert(0, new ListItem("-- SELECCIONE --", "-1"));
            CB_PROVEEDOR.SelectedValue = "-1";

            DataTable dt_estados = db.consultar("select id_estado, nombre_estado from oc_estados order by nombre_estado");
            CB_ESTADO_OC.DataSource = dt_estados;
            CB_ESTADO_OC.DataTextField = "nombre_estado";
            CB_ESTADO_OC.DataValueField = "nombre_estado";
            CB_ESTADO_OC.DataBind();

            CB_FILTRO_ESTADO_FACT.DataSource = dt_estados;
            CB_FILTRO_ESTADO_FACT.DataTextField = "nombre_estado";
            CB_FILTRO_ESTADO_FACT.DataValueField = "nombre_estado";
            CB_FILTRO_ESTADO_FACT.DataBind();
            CB_FILTRO_ESTADO_FACT.Items.Insert(0, new ListItem("-- SELECCIONE --", "-1"));
            CB_FILTRO_ESTADO_FACT.SelectedValue = "-1";

            CB_USUARIOS.DataSource = db.consultar("select id_usuario, nombre_completo from usuarios order by nombre_completo");
            CB_USUARIOS.DataTextField = "nombre_completo";
            CB_USUARIOS.DataValueField = "id_usuario";
            CB_USUARIOS.DataBind();
            CB_USUARIOS.Items.Insert(0, new ListItem("-- SELECCIONE --", "-1"));

            OBJ_USUARIOS usuario = new OBJ_USUARIOS();
            usuario.usuario = HttpContext.Current.User.Identity.Name;
            FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);

            try
            {
                CB_USUARIOS.SelectedValue = usuario.ID_USUARIO.ToString();
            }
            catch (Exception ex)
            {
                CB_USUARIOS.SelectedValue = "-1";
            }
        }

        protected void B_NUEVO_Click(object sender, EventArgs e)
        {
            LIMPIARCAMPOS();
            DIVAUTORIZADAS.Visible = false;
            OBJ_USUARIOS usuario = new OBJ_USUARIOS();
            usuario.usuario = HttpContext.Current.User.Identity.Name;
            FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);

            T_SOLICITANTE.Text = usuario.nombre_completo;
            PANEL_ENC.Visible = true;
            PANEL_DET.Visible = false;
            PANEL_PRINCIPAL.Visible = false;
            DIV_DETALLE.Visible = false;
        }

        public void LlenarGrillaDocs(int id_oc)
        {
            try
            {
                G_DETALLE_DOCS.DataSource = FN_OCPV_DETALLE.LLENADT(" where id_oc = " + id_oc);
                G_DETALLE_DOCS.DataBind();
            }
            catch (Exception ex)
            {

            }
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
                }
                //editar
                if (e.CommandName == "EditarGlosa")
                {
                    int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString()));
                    int correlativo = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[2].ToString()));
                    COMPLETAR_DETALLE_OC(id, correlativo);
                }
                //Borrar
                //if (e.CommandName == "Borrar")
                //{
                //    int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                //    OBJ_OCPV_ENC fact = new OBJ_OCPV_ENC();
                //    fact.ID_OC = id;
                //    FN_OCPV_ENC.DELETE(ref fact);
                //    if (fact._respok)
                //    {
                //        DBUtil db = new DBUtil();
                //        db.Scalar("delete from OCPV_DETALLE where ID_OC = " + id);
                //        alert("Documento de compra eliminado con éxito", 0);
                //        LlenarGrilla();
                //    }
                //}
            }
            catch (Exception ex)
            {

            }
        }
        public void COMPLETAR_DETALLE_OC(int id, int correlativo_oc)
        {
            LIMPIARCAMPOS();
            OBJ_OCPV_DETALLE fact = new OBJ_OCPV_DETALLE();
            fact.ID_OC_DET = id;

            FN_OCPV_DETALLE.LLENAOBJETO(ref fact);
            if (fact._respok)
            {
                // Permisos de cambio de estados solo 
                OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                usuario.usuario = HttpContext.Current.User.Identity.Name;
                FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);


                CB_ESTADO_OC.Enabled = true;
                CB_CANCELADA.Enabled = true;
                CB_FACTURADA.Enabled = true;
                T_DET_NUM_FACTURA.Enabled = true;
                CB_SIST_FACTURADA.Enabled = true;
                CB_AUT_ZAMORA.Enabled = true;
                CB_AUT_ZAPATA.Enabled = true;

                T_ID_DETALLE.Text = id.ToString();
                T_ID_OC_DETALLE.Text = fact.id_oc.ToString();
                T_DET_OBSERVACION.Text = fact.observacion;
                CB_ESTADO_OC.SelectedValue = fact.estado.ToString();
                CB_CANCELADA.SelectedValue = fact.cancelada;
                //CB_AUTORIZADA.SelectedValue = fact.autorizada;
                T_DET_NUM_FACTURA.Text = fact.num_factura;
                LBL_DETALLE.Text = "Detalle/Glosa de OC Nº : " + correlativo_oc;
                CB_FACTURADA.SelectedValue = fact.facturada;
                CB_SIST_FACTURADA.SelectedValue = fact.sistFact;

                OBJ_OCPV_ENC enc = new OBJ_OCPV_ENC();
                enc.ID_OC = fact.id_oc;
                FN_OCPV_ENC.LLENAOBJETO(ref enc);
                if (enc._respok)
                {
                    CB_AUT_ZAMORA.SelectedValue = enc.aprobado_fz;
                    CB_AUT_ZAPATA.SelectedValue = enc.aprobado_mz;
                }

                GG_DET.DataSource = FN_OCPV_DETALLE.LLENADT(" where id_oc_det = " + id);
                GG_DET.DataBind();

                PANEL_ENC.Visible = false;
                PANEL_DET.Visible = true;
                PANEL_PRINCIPAL.Visible = false;

            }
        }
        public void COMPLETAR_DETALLE(int id)
        {
            LIMPIARCAMPOS();
            OBJ_OCPV_ENC fact = new OBJ_OCPV_ENC();
            fact.ID_OC = id;

            FN_OCPV_ENC.LLENAOBJETO(ref fact);
            if (fact._respok)
            {
                T_ID.Text = id.ToString();
                CB_PROVEEDOR.SelectedValue = fact.id_proveedor.ToString();
                T_FECHA_EMISION.Text = fact.fecha_oc.ToString("yyyy-MM-dd");
                T_PLAZOENTREGA.Text = fact.plazo_entrega.ToString("yyyy-MM-dd");
                T_SOLICITANTE.Text = fact.solicitante;
                T_DESTINO.Text = fact.destino;
                T_CONTACTO.Text = fact.contacto;
                T_EMAIL.Text = fact.email;
                T_CLASE.Text = fact.clase;
                T_OBSERVACION.Text = fact.observacion;

                LB_NETO.Text = "$ " + fact.neto.ToString("#,##0");
                LB_IVA.Text = "$ " + fact.iva.ToString("#,##0");
                LB_TOTAL.Text = "$ " + fact.total.ToString("#,##0");

                LBL_ESTADO.Text = "Orden de Compra Nº: " + fact.correlativo_oc;

                // AUTORIZACIONES
                DIVAUTORIZADAS.Visible = true;
                LBLAUTMZ.Text = fact.aprobado_mz;
                LBLAUTFZ.Text = fact.aprobado_fz;
                LBLOBSERVACION.Text = fact.obs_aprobacion;
                T_COPYCLIPBOARD.Text = "https://app.transporteszamora.com/aut_oc.aspx?idoc=" + fact.ID_OC;

                LlenarGrillaDocs(id);
                LlenarGrillaAdjuntos(id);
                PANEL_ENC.Visible = true;
                PANEL_DET.Visible = false;
                PANEL_PRINCIPAL.Visible = false;
                DIV_DETALLE.Visible = true;



            }
        }

        #region ---------------- NO CAMBIAR ---------------- 
        public void LIMPIARCAMPOS()
        {
            CleanControl(this.Controls);
            LB_NETO.Text = "";
            LB_IVA.Text = "";
            LB_TOTAL.Text = "";
        }

        public void CleanControl(ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                if (control is TextBox)
                {
                    if (control.ID == "T_FECHA_EMISION")
                    {
                        ((TextBox)control).Text = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    else if (control.ID == "TD_UM")
                    {
                        ((TextBox)control).Text = "C/U";
                    }
                    else if (control.ID == "FILTRA_FECHA_DESDE" || control.ID == "FILTRA_FECHA_HASTA" || control.ID == "FILTRA_GT_DESDE" || control.ID == "FILTRA_GT_HASTA")
                    {

                    }
                    else
                    {
                        ((TextBox)control).Text = string.Empty;
                    }
                }
                else if (control is DropDownList)
                    if (control.ID == "CB_USUARIOS")
                    {

                    }
                    else
                    {
                        ((DropDownList)control).ClearSelection();
                    }
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


        public void AbrirModal()
        {
            // ABRE MODAL
            ScriptManager.RegisterStartupScript(this, typeof(Page), "abrirmodal", "<script>javascript:ABREMODAL();</script>", false);
        }

        public void AbrirModalDetalle()
        {
            // ABRE MODAL
            ScriptManager.RegisterStartupScript(this, typeof(Page), "abrirmodaldetalle", "<script>javascript:ABREMODALDETALLE();</script>", false);
        }

        protected void G_DETALLE_DOCS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Borrar")
            {
                int id = int.Parse((G_DETALLE_DOCS.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                int id_oc = int.Parse((G_DETALLE_DOCS.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString()));
                OBJ_OCPV_DETALLE det = new OBJ_OCPV_DETALLE();
                det.ID_OC_DET = id;
                FN_OCPV_DETALLE.DELETE(ref det);
                if (det._respok)
                {
                    // Actualizar Totales
                    OBJ_OCPV_ENC objeto = new OBJ_OCPV_ENC();
                    objeto.ID_OC = id_oc;
                    FN_OCPV_ENC.GetTotales(ref objeto);
                    LB_NETO.Text = "$ " + objeto.neto.ToString("#,##0");
                    LB_IVA.Text = "$ " + objeto.iva.ToString("#,##0");
                    LB_TOTAL.Text = "$ " + objeto.total.ToString("#,##0");
                    //
                    LlenarGrillaDocs(id_oc);
                    LlenarGrilla();
                    alert("Eliminado con éxito", 0);
                }
            }
        }

        protected void B_GUARDAROC_Click(object sender, EventArgs e)
        {
            DBUtil db = new DBUtil();
            if (CB_PROVEEDOR.SelectedValue == "-1")
            {
                alert("Seleccione un Proveedor.", 0);
            }
            else if (T_FECHA_EMISION.Text == "")
            {
                alert("Ingrese fecha de emisión.", 0);
            }
            else if (T_PLAZOENTREGA.Text == "")
            {
                alert("Ingrese fecha de plazo de entrega.", 0);
            }
            else if (T_SOLICITANTE.Text == "")
            {
                alert("Ingrese el solicitante.", 0);
            }
            else if (T_DESTINO.Text == "")
            {
                alert("Ingrese el destino.", 0);
            }
            else
            {
                OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                usuario.usuario = HttpContext.Current.User.Identity.Name;
                FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);

                OBJ_OCPV_ENC fac = new OBJ_OCPV_ENC();
                FN_OCPV_ENC.PREPARAOBJETO(ref fac);
                int correlativo = FN_OCPV_ENC.GetCorrelativo();
                if (T_ID.Text == "")
                {
                    // NUEVO
                    fac.correlativo_oc = correlativo;
                    fac.id_proveedor = int.Parse(CB_PROVEEDOR.SelectedValue);
                    fac.fecha_oc = DateTime.Parse(T_FECHA_EMISION.Text);
                    fac.plazo_entrega = DateTime.Parse(T_PLAZOENTREGA.Text);
                    fac.solicitante = T_SOLICITANTE.Text;
                    fac.destino = T_DESTINO.Text;
                    fac.contacto = T_CONTACTO.Text;
                    fac.email = T_EMAIL.Text;
                    fac.clase = T_CLASE.Text;
                    fac.usuario = usuario.ID_USUARIO;
                    fac.fecha_creacion = DateTime.Now;
                    fac.observacion = T_OBSERVACION.Text;

                    FN_OCPV_ENC.INSERT(ref fac);
                    if (fac._respok)
                    {
                        LBL_ESTADO.Text = "Orden de Compra Nº: " + fac.correlativo_oc;
                        T_ID.Text = fac.ID_OC.ToString();
                        LlenarGrillaDocs(fac.ID_OC);
                        LlenarGrillaAdjuntos(fac.ID_OC);
                        LlenarGrilla();
                        alert("Guardado con éxito", 1);
                        DIV_DETALLE.Visible = true;
                        // AUTORIZACION
                        DIVAUTORIZADAS.Visible = true;
                        LBLAUTMZ.Text = fac.aprobado_mz;
                        LBLAUTFZ.Text = fac.aprobado_fz;
                        LBLOBSERVACION.Text = fac.obs_aprobacion;
                        T_COPYCLIPBOARD.Text = "https://app.transporteszamora.com/aut_oc.aspx?idoc=" + fac.ID_OC;
                        //

                        db.Scalar("update correlativos set oc_agro = " + correlativo);
                        LBL_ESTADO.Text = "Orden de Compra Nº : " + correlativo;
                    }
                    else
                    {
                        alert("Problemas al guardar el documento", 0);
                    }
                }
                else
                {
                    fac.ID_OC = int.Parse(T_ID.Text);
                    FN_OCPV_ENC.LLENAOBJETO(ref fac);
                    if (fac._respok)
                    {
                        fac.id_proveedor = int.Parse(CB_PROVEEDOR.SelectedValue);
                        fac.fecha_oc = DateTime.Parse(T_FECHA_EMISION.Text);
                        fac.plazo_entrega = DateTime.Parse(T_PLAZOENTREGA.Text);
                        //fac.solicitante = T_SOLICITANTE.Text;
                        fac.destino = T_DESTINO.Text;
                        fac.contacto = T_CONTACTO.Text;
                        fac.email = T_EMAIL.Text;
                        fac.clase = T_CLASE.Text;
                        //fac.usuario = usuario.ID_USUARIO;
                        fac.fecha_creacion = DateTime.Now;
                        fac.observacion = T_OBSERVACION.Text;
                        // MODIFICAR
                        FN_OCPV_ENC.UPDATE(ref fac);
                        if (fac._respok)
                        {
                            LlenarGrillaDocs(fac.ID_OC);
                            LlenarGrillaAdjuntos(fac.ID_OC);
                            LlenarGrilla();
                            alert("Modificado con éxito", 1);

                            DIV_DETALLE.Visible = true;
                        }
                        else
                        {
                            alert("Problemas al modificar el documento", 0);
                        }
                    }
                }
            }

        }


        protected void B_GUARDAR_DETALLE_Click(object sender, EventArgs e)
        {
            OBJ_OCPV_DETALLE fac = new OBJ_OCPV_DETALLE();
            fac.ID_OC_DET = int.Parse(T_ID_DETALLE.Text);
            FN_OCPV_DETALLE.LLENAOBJETO(ref fac);
            if (fac._respok)
            {
                fac.observacion = T_DET_OBSERVACION.Text;
                fac.estado = CB_ESTADO_OC.SelectedValue;
                //fac.autorizada = CB_AUTORIZADA.SelectedValue;        
                fac.cancelada = CB_CANCELADA.SelectedValue;
                fac.num_factura = T_DET_NUM_FACTURA.Text;
                fac.observacion = T_DET_OBSERVACION.Text;
                fac.facturada = CB_FACTURADA.SelectedValue;
                fac.sistFact = CB_SIST_FACTURADA.SelectedValue;


                // MODIFICAR
                FN_OCPV_DETALLE.UPDATE(ref fac);
                if (fac._respok)
                {
                    OBJ_OCPV_ENC enc = new OBJ_OCPV_ENC();
                    enc.ID_OC = fac.id_oc;
                    FN_OCPV_ENC.LLENAOBJETO(ref enc);
                    if (enc._respok)
                    {
                        enc.aprobado_mz = CB_AUT_ZAPATA.SelectedValue;
                        enc.aprobado_fz = CB_AUT_ZAMORA.SelectedValue;
                        FN_OCPV_ENC.UPDATE(ref enc);
                    }

                    LlenarGrilla();
                    alert("Modificado con éxito", 1);

                }
                else
                {
                    alert("Problemas al modificar el detalle", 0);
                }
            }
        }

        protected void B_AGREGAR_DETALLE_Click(object sender, EventArgs e)
        {
            try
            {
                if (TD_LI.Text == "" || TD_CANT.Text == "" || TD_UM.Text == ""
                    || TD_GLOSA.Text.Trim() == "" || TD_PRECIOUNITARIO.Text.Trim() == "")
                {
                    alert("Favor complete todos los campos de la glosa", 0);
                }
                else if (T_ID.Text != "")
                {
                    OBJ_OCPV_DETALLE objeto = new OBJ_OCPV_DETALLE();
                    FN_OCPV_DETALLE.PREPARAOBJETO(ref objeto);
                    objeto.id_oc = int.Parse(T_ID.Text);
                    objeto.li = int.Parse(TD_LI.Text);
                    objeto.cant = int.Parse(TD_CANT.Text);
                    objeto.um = TD_UM.Text;
                    objeto.glosa = TD_GLOSA.Text;
                    objeto.unitario = int.Parse(TD_PRECIOUNITARIO.Text);
                    objeto.neto = objeto.cant * objeto.unitario;
                    objeto.iva = float.Parse((objeto.neto * 0.19).ToString());
                    objeto.total = objeto.neto + objeto.iva;
                    objeto.observacion = T_DET_OBSERVACION.Text;


                    objeto.autorizada = "NO";
                    objeto.cancelada = CB_CANCELADA.SelectedValue;
                    objeto.facturada = CB_FACTURADA.SelectedValue;
                    objeto.num_factura = T_DET_NUM_FACTURA.Text;
                    objeto.sistFact = "";
                    objeto.estado = CB_ESTADO_OC.SelectedValue;

                    objeto.observacion = T_DET_OBSERVACION.Text;

                    objeto.sistFact = CB_SIST_FACTURADA.SelectedValue;

                    FN_OCPV_DETALLE.INSERT(ref objeto);
                    if (objeto._respok)
                    {
                        // Actualizar Totales
                        OBJ_OCPV_ENC objEnc = new OBJ_OCPV_ENC();
                        objEnc.ID_OC = int.Parse(T_ID.Text);
                        FN_OCPV_ENC.GetTotales(ref objEnc);
                        if (objEnc._respok)
                        {
                            alert("Detalle agregado con éxito", 1);
                            LB_NETO.Text = "$ " + objEnc.neto.ToString("#,##0");
                            LB_IVA.Text = "$ " + objEnc.iva.ToString("#,##0");
                            LB_TOTAL.Text = "$ " + objEnc.total.ToString("#,##0");
                            LlenarGrillaDocs(objeto.id_oc);
                            LlenarGrilla();

                        }
                        else
                        {
                            alert("Error al agregar detalle:" + objEnc._respdet, 0);
                        }
                    }
                }
                else
                {
                    alert("Genere primero la OC para agregar detalle.", 0);
                }
            }
            catch (Exception ex)
            {
                alert("Revise los valores ingresados: " + ex.Message, 0);
            }

        }

        protected void B_VOLVER_Click(object sender, EventArgs e)
        {
            PANEL_PRINCIPAL.Visible = true;
            PANEL_DET.Visible = false;
            PANEL_ENC.Visible = false;
        }

        protected void B_FILTRAR_Click(object sender, EventArgs e)
        {
            LlenarGrilla();
        }

        protected void G_ADJUNTOS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //Borrar
                if (e.CommandName == "Borrar")
                {

                    OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                    usuario.usuario = HttpContext.Current.User.Identity.Name;
                    FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);

                    if (usuario.usuario == "mzapata")
                    {
                        int id = int.Parse((G_ADJUNTOS.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                        OBJ_OCPV_ADJUNTOS fact = new OBJ_OCPV_ADJUNTOS();
                        fact.ID_DOC = id;
                        FN_OCPV_ADJUNTOS.LLENAOBJETO(ref fact);
                        if (fact._respok)
                        {
                            FN_OCPV_ADJUNTOS.DELETE(ref fact);
                            if (fact._respok)
                            {
                                try
                                {
                                    string ServerPath = HttpContext.Current.Server.MapPath("~").ToString();
                                    System.IO.File.Delete(ServerPath + "Documentos/OC/" + fact.id_oc + "/" + fact.nom_archivo);
                                }
                                catch (System.IO.IOException ex)
                                {

                                }
                                LlenarGrillaAdjuntos(fact.id_oc);
                                alert("Archivo Adjunto eliminado con éxito", 0);
                            }
                        }
                    }
                    else
                    {
                        alert("Solo Mauricio Zapata puede eliminar documentos adjuntos", 0);
                    }
                }
                if (e.CommandName == "veradjunto")
                {
                    int id = int.Parse((G_ADJUNTOS.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    OBJ_OCPV_ADJUNTOS fact = new OBJ_OCPV_ADJUNTOS();
                    fact.ID_DOC = id;
                    FN_OCPV_ADJUNTOS.LLENAOBJETO(ref fact);
                    if (fact._respok)
                    {
                        string url = "Documentos/OC/" + fact.id_oc + "/" + fact.nom_archivo;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "abrirdocadjunto", "<script>javascript:VerArchivo('" + url + "');</script>", false);
                    }
                }
            }
            catch (Exception ex)
            {
                alert("No se pudo eliminar el adjunto", 0);
            }
        }

        public void LlenarGrillaAdjuntos(int id_oc)
        {
            try
            {
                G_ADJUNTOS.DataSource = FN_OCPV_ADJUNTOS.LLENADT(" where id_oc = " + id_oc);
                G_ADJUNTOS.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void B_CARGARADJUNTOS_Click(object sender, EventArgs e)
        {
            int id = int.Parse(T_ID.Text);
            LlenarGrillaAdjuntos(id);
        }

        protected void B_ELIMINAR_OC_Click(object sender, EventArgs e)
        {
            OBJ_USUARIOS usuario = new OBJ_USUARIOS();
            usuario.usuario = HttpContext.Current.User.Identity.Name;
            FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);

            if (usuario.usuario == "mzapata")
            {
                int id = int.Parse(T_ID.Text);
                OBJ_OCPV_ENC fact = new OBJ_OCPV_ENC();
                fact.ID_OC = id;
                FN_OCPV_ENC.DELETE(ref fact);
                if (fact._respok)
                {
                    DBUtil db = new DBUtil();
                    db.Scalar("delete from OCPV_DETALLE where ID_OC = " + id);
                    db.Scalar("delete from OCPV_ADJUNTOS where ID_OC = " + id);
                    alert("Orden de Compra eliminada con éxito", 0);
                    LlenarGrilla();
                    PANEL_PRINCIPAL.Visible = true;
                    PANEL_DET.Visible = false;
                    PANEL_ENC.Visible = false;
                }
            }
            else
            {
                alert("Solo Mauricio Zapata puede eliminar ordenes de compra", 0);
            }
        }
    }
}