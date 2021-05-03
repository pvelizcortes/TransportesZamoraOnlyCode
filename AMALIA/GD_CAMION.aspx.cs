using AMALIAFW;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
namespace AMALIA
{
    public partial class GD_CAMION : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Camión";
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
                    LlenarGrilla();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "grid", "<script>javascript:Datatables();</script>", false);
            }
        }
        public void LlenarGrilla()
        {
            G_PRINCIPAL.DataSource = FN_CAMION.LLENADT_VISTA();
            G_PRINCIPAL.DataBind();
        }
        protected void B_VOLVER_Click(object sender, EventArgs e)
        {
            LlenarGrilla();
            // MOSTRAR / OCULTAR PANEL
            PANEL_PRINCIPAL.Visible = true;
            PANEL_DETALLE1.Visible = false;
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
            }
            catch (Exception ex)
            {

            }
        }
        public void COMPLETAR_DETALLE(int id)
        {
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            OBJ_CAMION objeto_mantenedor = new OBJ_CAMION();
            DateTime fecha_hoy = DateTime.Now;

            objeto_mantenedor.ID_CAMION = id;

            FN_CAMION.LLENAOBJETO(ref objeto_mantenedor);
            if (objeto_mantenedor._respok)
            {
                LIMPIARCAMPOS();
                T_ID_CAMION.Text = objeto_mantenedor.ID_CAMION.ToString();
                T_PATENTE.Text = objeto_mantenedor.patente;
                T_MARCA.Text = objeto_mantenedor.marca;
                T_ANO.Text = objeto_mantenedor.ano.ToString();
                T_MODELO.Text = objeto_mantenedor.modelo;
                t_chasis.Text = objeto_mantenedor.num_chasis;
                t_motor.Text = objeto_mantenedor.num_motor;
                t_vin.Text = objeto_mantenedor.vin;


                OBJ_GD_CAMION gd_camion = new OBJ_GD_CAMION();
                gd_camion.ID_CAMION = objeto_mantenedor.ID_CAMION;
                FN_GD_CAMION.LLENAOBJETO(ref gd_camion);

                DataTable dt_mants = new DataTable();
                dt_mants = db.consultar("select top(1)* from GD_CAMION_MANT where fecha_mantencion = (select MAX(fecha_mantencion) from GD_CAMION_MANT where id_gd_camion = " + objeto_mantenedor.ID_CAMION.ToString() + ");");
                if (dt_mants.Rows.Count > 0)
                {
                    T_ULTIMA_MANTENCION.Text = dt_mants.Rows[0]["kilometraje_mantencion"].ToString();
                    T_FECHA_ULTIMA_MANTENCION.Text = dt_mants.Rows[0]["fecha_mantencion"].ToString();
                }
                else
                {
                    T_ULTIMA_MANTENCION.Text = "Sin información";
                    T_FECHA_ULTIMA_MANTENCION.Text = "Sin información";
                }

                // AGENDADOS
                DataTable dt_ag = new DataTable();
                dt_ag = db.consultar("select top(1)* from GD_MANT_AGENDADAS where fecha_mant = (select MAX(fecha_mant) from GD_MANT_AGENDADAS where id_gd_camion = " + objeto_mantenedor.ID_CAMION.ToString() + ");");
                if (dt_ag.Rows.Count > 0)
                {
                    T_PROXIMA_MANTENCION.Text = dt_ag.Rows[0]["fecha_mant"].ToString();
                }
                else
                {
                    T_PROXIMA_MANTENCION.Text = "Sin información";
                    T_MANTENCIONES_ATRASADAS.Text = "Sin información";
                }

                if (gd_camion._respok)
                {
                    try
                    {
                        T_F_REV_TECNICA.BorderColor = System.Drawing.Color.Green;
                        T_F_PERMISO.BorderColor = System.Drawing.Color.Green;
                        T_F_SEGURO.BorderColor = System.Drawing.Color.Green;
                        T_VENC_FAENA.BorderColor = System.Drawing.Color.Green;
                        T_KILOMETRAJE.BorderColor = System.Drawing.Color.Green;

                        T_REV_NOMBRE_BD.Text = gd_camion.doc_rev_tecnica_bd;
                        if (gd_camion.doc_rev_tecnica_real == "")
                        {
                            T_REV_NOMBRE_REAL.Text = "Aun no se carga el documento";
                        }
                        else
                        {
                            T_REV_NOMBRE_REAL.Text = gd_camion.doc_rev_tecnica_real;
                        }
                        CB_FAENA.SelectedValue = gd_camion.nombre_faena;
                        T_ID_GDCAMION.Text = gd_camion.ID_CAMION.ToString();

                        // REV TECNICA
                        T_F_REV_TECNICA.Text = gd_camion.venc_rev_tecnica.ToString("yyyy-MM-dd");
                        int daysDiff = ((TimeSpan)(DateTime.Parse(gd_camion.venc_rev_tecnica.ToString("dd/MM/yyyy")) - fecha_hoy)).Days;
                        LBL_F_REVTECNICA.Text = daysDiff.ToString() + " días restantes";
                        if (daysDiff >= 0)
                        {
                            LBL_F_REVTECNICA.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            LBL_F_REVTECNICA.ForeColor = System.Drawing.Color.Red;
                        }

                        // PERMISO
                        T_F_PERMISO.Text = gd_camion.venc_perm_circulacion.ToString("yyyy-MM-dd");
                        int daysDiff2 = ((TimeSpan)(DateTime.Parse(gd_camion.venc_perm_circulacion.ToString("dd/MM/yyyy")) - fecha_hoy)).Days;
                        LBL_F_PERMISO.Text = daysDiff2.ToString() + " días restantes";
                        if (daysDiff2 >= 0)
                        {
                            LBL_F_PERMISO.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            LBL_F_PERMISO.ForeColor = System.Drawing.Color.Red;
                        }
                        // SEGURO
                        T_F_SEGURO.Text = gd_camion.vec_seguro_obligatorio.ToString("yyyy-MM-dd");
                        int daysDiff3 = ((TimeSpan)(DateTime.Parse(gd_camion.vec_seguro_obligatorio.ToString("dd/MM/yyyy")) - fecha_hoy)).Days;
                        LBL_F_SEGURO.Text = daysDiff3.ToString() + " días restantes";
                        if (daysDiff3 >= 0)
                        {
                            LBL_F_SEGURO.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            LBL_F_SEGURO.ForeColor = System.Drawing.Color.Red;
                        }
                        // VENC FAENA
                        if (gd_camion.venc_faena == DateTime.Parse("01/01/1900"))
                        {
                            T_VENC_FAENA.Text = "";
                            LBL_VENC_FAENA.Text = "";
                        }
                        else
                        {
                            T_VENC_FAENA.Text = gd_camion.venc_faena.ToString("yyyy-MM-dd");
                            int daysDiff4 = ((TimeSpan)(DateTime.Parse(gd_camion.venc_faena.ToString("dd/MM/yyyy")) - fecha_hoy)).Days;
                            LBL_VENC_FAENA.Text = daysDiff4.ToString() + " días restantes";
                            if (daysDiff4 >= 0)
                            {
                                LBL_VENC_FAENA.ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                LBL_VENC_FAENA.ForeColor = System.Drawing.Color.Red;
                            }
                        }                  
                       
                        // KM
                        T_KILOMETRAJE.Text = gd_camion.kilometraje.ToString();
                        if (gd_camion.status == "OPERATIVO")
                        {
                            RB_ACTIVO.Checked = true;
                        }
                        else
                        {
                            RB_ACTIVO2.Checked = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        alert(ex.Message, 1);
                    }

                }
                else
                {
                    RB_ACTIVO.Checked = true;
                    T_F_REV_TECNICA.BorderColor = System.Drawing.Color.Red;
                    T_F_PERMISO.BorderColor = System.Drawing.Color.Red;
                    T_F_SEGURO.BorderColor = System.Drawing.Color.Red;
                    T_VENC_FAENA.BorderColor = System.Drawing.Color.Red;
                    T_KILOMETRAJE.BorderColor = System.Drawing.Color.Red;
                    T_REV_NOMBRE_REAL.Text = "Aun no se carga el documento";
                }
            }
        }
        #region ---------------- NO CAMBIAR ---------------- 
        public void LIMPIARCAMPOS()
        {
            LBL_F_PERMISO.Text = "";
            LBL_F_REVTECNICA.Text = "";
            LBL_F_SEGURO.Text = "";
            LBL_VENC_FAENA.Text = "";
            CleanControl(this.Controls);
        }
        public void CleanControl(ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = string.Empty;
                    ((TextBox)control).BorderColor = CB_FAENA.BorderColor;
                }
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
            MakeAccessible(G_MANTENCIONES);
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

        public void LLENAR_DOCS()
        {
            //DBUtil db = new DBUtil();
            //G_DOCUMENTOSPORVENCER.DataSource = db.consultar("select * from GD_CAMION_DOCS where id_gd_camion = " + T_ID.Text);
            //G_DOCUMENTOSPORVENCER.DataBind();
        }
        public void LLENAR_MANT()
        {
            DBUtil db = new DBUtil();
            G_MANTENCIONES.DataSource = db.consultar("select * from GD_CAMION_MANT where id_gd_camion = " + T_ID_CAMION.Text + " order by id_detalle_mant desc");
            G_MANTENCIONES.DataBind();
        }
        public void LLENAR_MANT4()
        {
            DBUtil db = new DBUtil();
            G_AG_MANTENCIONES.DataSource = db.consultar("select * from GD_MANT_AGENDADAS where id_gd_camion = " + T_ID_CAMION.Text + " order by fecha_mant desc");
            G_AG_MANTENCIONES.DataBind();
        }


        //protected void G_DOCUMENTOSPORVENCER_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        //editar
        //        if (e.CommandName == "Subir")
        //        {
        //            int id = int.Parse((G_DOCUMENTOSPORVENCER.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
        //            OBJ_GD_CAMION_DOCS obj_doc = new OBJ_GD_CAMION_DOCS();
        //            obj_doc.ID_DETALLE_DOCUMENTO = id;
        //            FN_GD_CAMION_DOCS.LLENAOBJETO(ref obj_doc);
        //            if (obj_doc._respok)
        //            {
        //                T_DET_DOC.Text = obj_doc.ID_DETALLE_DOCUMENTO.ToString();
        //                T_NOMBRE_DOCUMENTO.Text = obj_doc.nombre_documento;
        //                T_FECHA_DOCUMENTO.Text = obj_doc.fecha_documento.ToString("yyyy-MM-dd");
        //                T_FECHA_VENC_DOCUMENTO.Text = obj_doc.fecha_vencimiento.ToString("yyyy-MM-dd");
        //                T_DIAS_ANTICIPACION.Text = obj_doc.dias_anticipacion.ToString();
        //                //T_DOC_ACTUAL1.Text = obj_doc.nombre_archivo_bd;
        //                div_subir_documento.Visible = true;
        //                UP_DOCUMENTO.Update();
        //                AbreModalGasto();
        //            }
        //            else
        //            {
        //                alert("No se encontró el registro.", 0);
        //            }
        //        }
        //        //editar
        //        if (e.CommandName == "Borrar")
        //        {
        //            int id = int.Parse((G_DOCUMENTOSPORVENCER.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
        //            int id_camion = int.Parse((G_DOCUMENTOSPORVENCER.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString()));

        //            OBJ_GD_CAMION_DOCS doc = new OBJ_GD_CAMION_DOCS();
        //            doc.ID_DETALLE_DOCUMENTO = id;
        //            FN_GD_CAMION_DOCS.LLENAOBJETO(ref doc);
        //            if (doc._respok)
        //            {
        //                string ServerPath = HttpContext.Current.Server.MapPath("~").ToString();
        //                string subPath = ServerPath + "/Documentos/Camiones/DOCS/" + id_camion;
        //                if (File.Exists(Path.Combine(subPath, doc.nombre_archivo_bd)))
        //                {
        //                    File.Delete(Path.Combine(subPath, doc.nombre_archivo_bd));
        //                    FN_GD_CAMION_DOCS.DELETE(ref doc);
        //                    if (doc._respok)
        //                    {
        //                        alert("Eliminado con éxito", 1);
        //                        LLENAR_DOCS();
        //                    }
        //                }
        //                else
        //                {
        //                    FN_GD_CAMION_DOCS.DELETE(ref doc);
        //                    if (doc._respok)
        //                    {
        //                        alert("Eliminado con éxito", 1);
        //                        LLENAR_DOCS();
        //                    }
        //                }
        //            }




        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        protected void B_GUARDAR_Click(object sender, EventArgs e)
        {
            OBJ_GD_CAMION gd_camion = new OBJ_GD_CAMION();
            if (T_KILOMETRAJE.Text == "")
            {
                alert("Ingrese un kilometraje para poder guardar", 0);
                T_KILOMETRAJE.BorderColor = System.Drawing.Color.Red;
                T_KILOMETRAJE.Focus();

            }
            else if (T_F_REV_TECNICA.Text == "")
            {
                T_KILOMETRAJE.BorderColor = System.Drawing.Color.Green;
                alert("Ingrese el vencimiento de la revisión técnica para poder guardar", 0);
                T_F_REV_TECNICA.BorderColor = System.Drawing.Color.Red;
                T_F_REV_TECNICA.Focus();
            }
            else if (T_F_PERMISO.Text == "")
            {
                T_F_REV_TECNICA.BorderColor = System.Drawing.Color.Green;
                alert("Ingrese el vencimiento del permiso de circulación para poder guardar", 0);
                T_F_PERMISO.BorderColor = System.Drawing.Color.Red;
                T_F_PERMISO.Focus();
            }
            else if (T_F_SEGURO.Text == "")
            {
                T_F_PERMISO.BorderColor = System.Drawing.Color.Green;
                alert("Ingrese el vencimiento del seguro obligatorio para poder guardar", 0);
                T_F_SEGURO.BorderColor = System.Drawing.Color.Red;
                T_F_SEGURO.Focus();
            }        
            else
            {
                T_F_REV_TECNICA.BorderColor = System.Drawing.Color.Green;
                T_F_PERMISO.BorderColor = System.Drawing.Color.Green;
                T_F_SEGURO.BorderColor = System.Drawing.Color.Green;
                T_VENC_FAENA.BorderColor = System.Drawing.Color.Green;
                T_KILOMETRAJE.BorderColor = System.Drawing.Color.Green;

                if (T_ID_GDCAMION.Text != "")
                {
                    gd_camion.ID_CAMION = int.Parse(T_ID_GDCAMION.Text);
                    FN_GD_CAMION.LLENAOBJETO(ref gd_camion);
                    if (gd_camion._respok)
                    {
                        if (RB_ACTIVO.Checked)
                        {
                            gd_camion.status = "OPERATIVO";
                        }
                        else
                        {
                            gd_camion.status = "FUERA DE SERVICIO";
                        }
                        gd_camion.venc_rev_tecnica = DateTime.Parse(T_F_REV_TECNICA.Text);
                        gd_camion.venc_perm_circulacion = DateTime.Parse(T_F_PERMISO.Text);
                        gd_camion.vec_seguro_obligatorio = DateTime.Parse(T_F_SEGURO.Text);
                        gd_camion.fecha_actualizacion = DateTime.Now;
                        gd_camion.kilometraje = int.Parse(T_KILOMETRAJE.Text);
                        gd_camion.nombre_faena = CB_FAENA.SelectedItem.Text;
                        if (T_VENC_FAENA.Text == "")
                        {
                            gd_camion.venc_faena = DateTime.Parse("01/01/1900");
                        }
                        else
                        {
                            gd_camion.venc_faena = DateTime.Parse(T_VENC_FAENA.Text);
                        }                       
                        gd_camion.doc_rev_tecnica_bd = T_REV_NOMBRE_BD.Text;
                        gd_camion.doc_rev_tecnica_real = T_REV_NOMBRE_REAL.Text;
                        FN_GD_CAMION.UPDATE(ref gd_camion);
                        if (gd_camion._respok)
                        {
                            alert("Modificado con éxito", 1);
                        }
                    }
                }
                else
                {
                    gd_camion.ID_CAMION = int.Parse(T_ID_CAMION.Text);
                    if (RB_ACTIVO.Checked)
                    {
                        gd_camion.status = "OPERATIVO";
                    }
                    else
                    {
                        gd_camion.status = "FUERA DE SERVICIO";
                    }
                    gd_camion.venc_rev_tecnica = DateTime.Parse(T_F_REV_TECNICA.Text);
                    gd_camion.venc_perm_circulacion = DateTime.Parse(T_F_PERMISO.Text);
                    gd_camion.vec_seguro_obligatorio = DateTime.Parse(T_F_SEGURO.Text);
                    gd_camion.fecha_actualizacion = DateTime.Now;
                    gd_camion.kilometraje = int.Parse(T_KILOMETRAJE.Text);
                    gd_camion.nombre_faena = CB_FAENA.SelectedItem.Text;
                    if (T_VENC_FAENA.Text == "")
                    {
                        gd_camion.venc_faena = DateTime.Parse("01/01/1900");
                    }
                    else
                    {
                        gd_camion.venc_faena = DateTime.Parse(T_VENC_FAENA.Text);
                    }
                    gd_camion.doc_rev_tecnica_bd = T_REV_NOMBRE_BD.Text;
                    gd_camion.doc_rev_tecnica_real = T_REV_NOMBRE_REAL.Text;
                    FN_GD_CAMION.INSERT(ref gd_camion);
                    if (gd_camion._respok)
                    {
                        T_ID_GDCAMION.Text = gd_camion.ID_CAMION.ToString();
                        alert("Creado con éxito", 1);
                    }
                }
            }
        }

        protected void B_AGREGAR_MANTENCION_Click(object sender, EventArgs e)
        {
            LimpiarModal2();
            AbreModalGasto2();
        }

        public void AbreModalGasto2()
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "abremodalgasto2", "<script>javascript:GASTOGENERAL2();</script>", false);
        }

        public void LimpiarModal2()
        {
            T_ID_MANTENCION.Text = "";
            T_KILOMETRAJE_MANTENCION.Text = "";
            T_FECHA_MANTENCION.Text = "";
            T_NOMBRE_ARCHIVO_BD.Text = "";
            T_NOMBRE_ARCHIVO_REAL.Text = "";
            T_OBSERVACION_MANTENCION.Text = "";
            UP_MANTENCION.Update();
        }

        protected void B_GUARDAR_MANTENCION_Click(object sender, EventArgs e)
        {
            if (T_FECHA_MANTENCION.Text == "" || T_KILOMETRAJE_MANTENCION.Text == "")
            {
                alert("Ingrese los datos obligatorios", 0);
            }
            else
            {
                if (T_ID_MANTENCION.Text != "")
                {
                    // UPDATE
                    OBJ_GD_CAMION_MANT det_doc = new OBJ_GD_CAMION_MANT();
                    det_doc.ID_DETALLE_MANT = int.Parse(T_ID_MANTENCION.Text);
                    FN_GD_CAMION_MANT.LLENAOBJETO(ref det_doc);
                    if (det_doc._respok)
                    {
                        det_doc.mantecion_asociada = CB_MANT_ASOCIADA.SelectedValue;
                        det_doc.tipo_mantecion = CB_TIPO_MANTENCION.SelectedValue;
                        det_doc.kilometraje_mantencion = int.Parse(T_KILOMETRAJE_MANTENCION.Text);
                        det_doc.mantencion_tipo = CB_TIPO_MANTENCION_2.SelectedValue;
                        det_doc.fecha_mantencion = DateTime.Parse(T_FECHA_MANTENCION.Text);
                        det_doc.observacion = T_OBSERVACION_MANTENCION.Text;
                        det_doc.fecha_actualizacion = DateTime.Now;
                        det_doc.nombre_archivo_bd = T_NOMBRE_ARCHIVO_BD.Text;
                        det_doc.nombre_archivo_real = T_NOMBRE_ARCHIVO_REAL.Text;

                        FN_GD_CAMION_MANT.UPDATE(ref det_doc);
                        if (det_doc._respok)
                        {
                            alert("Modificado con éxito", 1);
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "ascs33ad", "<script>javascript:CerrarModalMant();</script>", false);

                        }
                    }
                }
                else
                {
                    // NUEVO
                    OBJ_GD_CAMION_MANT det_doc = new OBJ_GD_CAMION_MANT();
                    det_doc.id_gd_camion = int.Parse(T_ID_CAMION.Text);

                    det_doc.mantecion_asociada = CB_MANT_ASOCIADA.SelectedValue;
                    det_doc.tipo_mantecion = CB_TIPO_MANTENCION.SelectedValue;
                    det_doc.kilometraje_mantencion = int.Parse(T_KILOMETRAJE_MANTENCION.Text);
                    det_doc.mantencion_tipo = CB_TIPO_MANTENCION_2.SelectedValue;
                    det_doc.fecha_mantencion = DateTime.Parse(T_FECHA_MANTENCION.Text);
                    det_doc.observacion = T_OBSERVACION_MANTENCION.Text;
                    det_doc.fecha_actualizacion = DateTime.Now;
                    det_doc.estado = "Realizada";

                    det_doc.aviso_dias_anticipacion = 0;
                    det_doc.nombre_archivo_bd = T_NOMBRE_ARCHIVO_BD.Text;
                    det_doc.nombre_archivo_real = T_NOMBRE_ARCHIVO_REAL.Text;
                    FN_GD_CAMION_MANT.INSERT(ref det_doc);
                    if (det_doc._respok)
                    {
                        //T_DOC_ACTUAL2.Text = det_doc.nombre_archivo_bd;
                        T_ID_MANTENCION.Text = det_doc.ID_DETALLE_MANT.ToString();
                        alert("Creado con éxito", 1);
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "ascsad", "<script>javascript:CerrarModalMant();</script>", false);
                        DBUtil db = new DBUtil();
                        DataTable dt_mants = new DataTable();
                        dt_mants = db.consultar("select top(1)* from GD_CAMION_MANT where fecha_mantencion = (select MAX(fecha_mantencion) from GD_CAMION_MANT where id_gd_camion = " + det_doc.id_gd_camion + ");");
                        if (dt_mants.Rows.Count > 0)
                        {
                            T_ULTIMA_MANTENCION.Text = dt_mants.Rows[0]["kilometraje_mantencion"].ToString();
                            T_FECHA_ULTIMA_MANTENCION.Text = dt_mants.Rows[0]["fecha_mantencion"].ToString();
                        }
                        else
                        {
                            T_ULTIMA_MANTENCION.Text = "Sin información";
                            T_FECHA_ULTIMA_MANTENCION.Text = "Sin información";
                        }
                        T_PROXIMA_MANTENCION.Text = "n/a";
                        T_MANTENCIONES_ATRASADAS.Text = "n/a";
                        UP_PRINCIPAL.Update();
                    }
                }
            }
        }

        protected void G_MANTENCIONES_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //editar
                if (e.CommandName == "Verarchivo")
                {

                    int id_camion = int.Parse((G_MANTENCIONES.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString()));
                    string arhivo = (G_MANTENCIONES.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[2].ToString());
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "abrearchivo2", "<script>javascript:VerArchivo2(\"" + arhivo + "\"," + id_camion + ");</script>", false);

                    //VerArchivo2
                }
                //editar
                if (e.CommandName == "Borrar")
                {
                    int id = int.Parse((G_MANTENCIONES.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    int id_camion = int.Parse((G_MANTENCIONES.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString()));

                    OBJ_GD_CAMION_MANT doc = new OBJ_GD_CAMION_MANT();
                    doc.ID_DETALLE_MANT = id;
                    FN_GD_CAMION_MANT.LLENAOBJETO(ref doc);
                    if (doc._respok)
                    {
                        string ServerPath = HttpContext.Current.Server.MapPath("~").ToString();
                        string subPath = ServerPath + "/Documentos/Camiones/MANT/" + id_camion;
                        if (File.Exists(Path.Combine(subPath, doc.nombre_archivo_bd)))
                        {
                            // If file found, delete it    
                            File.Delete(Path.Combine(subPath, doc.nombre_archivo_bd));
                            FN_GD_CAMION_MANT.DELETE(ref doc);
                            if (doc._respok)
                            {
                                alert("Eliminado con éxito", 1);
                                LLENAR_MANT();
                            }
                        }
                        else
                        {
                            FN_GD_CAMION_MANT.DELETE(ref doc);
                            if (doc._respok)
                            {
                                alert("Eliminado con éxito", 1);
                                LLENAR_MANT();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void B_ACTUALIZAR_DOCS_Click(object sender, EventArgs e)
        {
            LLENAR_DOCS();
            UP_PRINCIPAL.Update();
        }

        protected void B_ACTUALIZAR_MANT_Click(object sender, EventArgs e)
        {
            LLENAR_MANT();
            UP_PRINCIPAL.Update();
        }

        protected void B_LISTAR_MANTENCIONES_Click(object sender, EventArgs e)
        {
            LBL_MANT_PATENTE.InnerHtml = " <h4>Detalle Mantenciones Camion: " + T_PATENTE.Text + " <i class='fa fa-cogs text-purple'></i></h4>";
            LLENAR_MANT();
            UP_MANTENCION_LISTADO.Update();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "fdgh123", "<script>javascript:ABREMODALMANTECIONES();</script>", false);
        }

        public void AbreModalGasto3()
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "abremodalgasto3", "<script>javascript:GASTOGENERAL3();</script>", false);
        }

        public void AbreModalGasto4()
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "abremodalgasto4", "<script>javascript:GASTOGENERAL4();</script>", false);
        }

        protected void G_PRINCIPAL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DBUtil db = new DBUtil();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string id_camion = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[0].ToString();
                string status = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[1].ToString();
                string diasag = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[2].ToString();
                string atrasadas = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[3].ToString();

                // ESTADO
                HtmlGenericControl spnHtml = (HtmlGenericControl)e.Row.FindControl("div_estado");
                if (status == "OPERATIVO")
                {
                    spnHtml.InnerHtml = "<span class='badge badge-success'>OPERATIVO</span>";
                }
                else if (status == "FUERA DE SERVICIO")
                {
                    spnHtml.InnerHtml = "<span class='badge badge-danger'>FUERA DE SERVICIO</span>";
                }
                // ESTADO 1
                HtmlGenericControl spnHtml1 = (HtmlGenericControl)e.Row.FindControl("div_estado1");
                try
                {
                    if (int.Parse(diasag) < 0)
                    {
                        spnHtml1.InnerHtml = "<span class='text-danger'>" + diasag + " días atraso</span>";
                    }
                    else
                    {
                        spnHtml1.InnerHtml = "<span class='text-success'>faltan " + diasag + " días</span>";
                    }
                }
                catch (Exception ex)
                {
                    spnHtml1.InnerHtml = "<span class='text-primary'>sin información</span>";
                }

                // ESTADO 2
                HtmlGenericControl spnHtml2 = (HtmlGenericControl)e.Row.FindControl("div_estado2");
                try
                {
                    if (int.Parse(atrasadas) > 0)
                    {
                        spnHtml2.InnerHtml = "<span class='text-danger'>" + atrasadas + " atrasadas</span>";
                    }
                    else
                    {
                        spnHtml2.InnerHtml = "<span class='text-success'>" + atrasadas + " atrasadas</span>";
                    }
                }
                catch (Exception ex)
                {
                    spnHtml2.InnerHtml = "<span class='text-primary'>sin información</span>";
                }
            }
        }

        protected void B_AGENDAR_MANTENCION_Click(object sender, EventArgs e)
        {
            LimpiarModal3();
            AbreModalGasto3();
        }
        public void LimpiarModal3()
        {
            T_AG_NOMBRE.Text = "";
            T_AG_FECHA.Text = "";
            T_AG_DIAS_ANTERIORIDAD.Text = "";
            T_AG_OBSERVACION.Text = "";

            UP_AGENDAR.Update();
        }

        protected void B_LISTAR_AGENDADAS_Click(object sender, EventArgs e)
        {
            LBL_MANT_PATENTE4.InnerHtml = " <h4>Detalle Mantenciones Agendadas: " + T_PATENTE.Text + " <i class='fa fa-cogs text-purple'></i></h4>";
            LLENAR_MANT4();
            UP_LISTA_AGENDADOS.Update();
            AbreModalGasto4();
        }

        protected void B_GUARDAR_AG_Click(object sender, EventArgs e)
        {
            if (T_AG_NOMBRE.Text == "" || T_AG_FECHA.Text == "" || T_AG_DIAS_ANTERIORIDAD.Text == "" || CB_PRIORIDAD_MANT.SelectedValue == "-1")
            {
                alert("Complete los campos", 0);
            }
            else
            {
                OBJ_GD_MANT_AGENDADAS ag = new OBJ_GD_MANT_AGENDADAS();
                ag.id_gd_camion = int.Parse(T_ID_CAMION.Text);
                ag.nombre_mant_pendiente = T_AG_NOMBRE.Text;
                ag.fecha_mant = DateTime.Parse(T_AG_FECHA.Text);
                ag.prioridad = CB_PRIORIDAD_MANT.SelectedItem.Value;
                ag.aviso_dias_anticipacion = int.Parse(T_AG_DIAS_ANTERIORIDAD.Text);
                ag.observacion = T_AG_OBSERVACION.Text;
                FN_GD_MANT_AGENDADAS.INSERT(ref ag);
                if (ag._respok)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "ascs33ad", "<script>javascript:CerrarModalMant3();</script>", false);
                    alert("Creado con éxito", 1);
                }
            }


        }


        protected void G_AG_MANTENCIONES_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Borrar")
            {
                int id = int.Parse((G_AG_MANTENCIONES.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                OBJ_GD_MANT_AGENDADAS doc = new OBJ_GD_MANT_AGENDADAS();
                doc.ID_MANT_PENDIENTE = id;

                FN_GD_MANT_AGENDADAS.DELETE(ref doc);
                if (doc._respok)
                {
                    alert("Eliminado con éxito", 1);
                    LLENAR_MANT4();
                }
            }
        }
    }
}