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
    public partial class Hallazgo : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Hallazgo";
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
                filtro += " and fecha_envio >= convert(date, '" + DateTime.Parse(FILTRA_FECHA_DESDE.Text).ToString("dd/MM/yyyy") + "', 103) ";
            }
            if (FILTRA_FECHA_HASTA.Text != "")
            {
                filtro += " and fecha_envio <= convert(date, '" + DateTime.Parse(FILTRA_FECHA_HASTA.Text).ToString("dd/MM/yyyy") + "', 103) ";
            }
            //if (CB_USUARIOS.SelectedValue != "-1")
            //{
            //    filtro += " and usuario = '" + CB_USUARIOS.SelectedValue + "'";
            //}

            DataTable dt = new DataTable();
            dt = FN_HALLAZGO.LLENADT(filtro);
            G_PRINCIPAL.DataSource = dt;
            G_PRINCIPAL.DataBind();
        }


        public void CargarCombos()
        {
            // Llenar combo camión
            //DBUtil db = new DBUtil();

            //CB_USUARIOS.DataSource = db.consultar("select id_usuario, nombre_completo from usuarios order by nombre_completo");
            //CB_USUARIOS.DataTextField = "nombre_completo";
            //CB_USUARIOS.DataValueField = "id_usuario";
            //CB_USUARIOS.DataBind();
            //CB_USUARIOS.Items.Insert(0, new ListItem("-- SELECCIONE --", "-1"));

            //OBJ_USUARIOS usuario = new OBJ_USUARIOS();
            //usuario.usuario = HttpContext.Current.User.Identity.Name;
            //FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);

            //try
            //{
            //    CB_USUARIOS.SelectedValue = usuario.ID_USUARIO.ToString();
            //}
            //catch (Exception ex)
            //{
            //    CB_USUARIOS.SelectedValue = "-1";
            //}
        }

        protected void B_NUEVO_Click(object sender, EventArgs e)
        {
            LIMPIARCAMPOS();
            //DIVAUTORIZADAS.Visible = false;   
            //Automatico  
            tFechaEnvio.Text = DateTime.Now.ToString("yyyy-MM-dd");
            tNombreRemitente.Text = HttpContext.Current.User.Identity.Name;
            tNumReferencia.Text = FN_HALLAZGO.getCorrelativo().ToString();
            tNombreDetecta.Text = HttpContext.Current.User.Identity.Name;

            PANEL_ENC.Visible = true;
            PANEL_PRINCIPAL.Visible = false;
            DIV_ADJUNTAR.Visible = false;
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
             
            }
            catch (Exception ex)
            {

            }
        }

        public void COMPLETAR_DETALLE(int id)
        {
            LIMPIARCAMPOS();
            OBJ_HALLAZGO fact = new OBJ_HALLAZGO();
            fact.ID_HALLAZGO = id;

            FN_HALLAZGO.LLENAOBJETO(ref fact);
            if (fact._respok)
            {
                T_ID.Text = id.ToString();
                tNumReferencia.Text = fact.num_referencia.ToString();
                tFechaEnvio.Text = fact.fecha_envio.ToString("yyyy-MM-dd");
                tNombreRemitente.Text = fact.nombre_remitente;
                CBAREA.SelectedValue = fact.area;
                //tLugar.Text = fact.lugar;
                CBDETECCION.SelectedValue = fact.deteccion;
                CBTIPO.SelectedValue = fact.tipo;
                CBORIGEN.SelectedValue = fact.origen;
                CBDESTINO.SelectedValue = fact.destino;
                tNombreDetecta.Text = fact.nombre_deteccion;
                //tCargo.Text = fact.cargo;
                tDeteccionHallazgo.Text = fact.deteccion_hallazgo;
                tDocReferencia.Text = fact.doc_referencia;
                tAccionInmediata.Text = fact.accion_inmediata;
                tEmpInvolucrada.Text = fact.empresainvolucrada;

                // AUTORIZACIONES
                //DIVAUTORIZADAS.Visible = true;
                LlenarGrillaAdjuntos(id);
                
                //LBLAUTMZ.Text = fact.aprobado_mz;
                //LBLAUTFZ.Text = fact.aprobado_fz;
                //LBLOBSERVACION.Text = fact.obs_aprobacion;
                //T_COPYCLIPBOARD.Text = "https://app.transporteszamora.com/aut_oc.aspx?idoc=" + fact.ID_OC;
                PANEL_ENC.Visible = true;
                PANEL_PRINCIPAL.Visible = false;
                DIV_ADJUNTAR.Visible = true;
            }
        }

        #region ---------------- NO CAMBIAR ---------------- 
        public void LIMPIARCAMPOS()
        {
            CleanControl(this.Controls);
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


        protected void B_GUARDAROC_Click(object sender, EventArgs e)
        {
            DBUtil db = new DBUtil();
            if (tNombreDetecta.Text == "")
            {
                alert("Ingrese nombre de quien detecta.", 0);
            }
            //else if (T_PLAZOENTREGA.Text == "")
            //{
            //    alert("Ingrese fecha de plazo de entrega.", 0);
            //}
            //else if (T_SOLICITANTE.Text == "")
            //{
            //    alert("Ingrese el solicitante.", 0);
            //}
            //else if (T_DESTINO.Text == "")
            //{
            //    alert("Ingrese el destino.", 0);
            //}
            else
            {
                OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                usuario.usuario = HttpContext.Current.User.Identity.Name;
                FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);

                OBJ_HALLAZGO fac = new OBJ_HALLAZGO();
                FN_HALLAZGO.PREPARAOBJETO(ref fac);
                long correlativo = FN_HALLAZGO.getCorrelativo();
                if (T_ID.Text == "")
                {
                    // NUEVO   
                    fac.num_referencia = correlativo;
                    fac.fecha_envio = DateTime.Parse(tFechaEnvio.Text);
                    fac.nombre_remitente = tNombreRemitente.Text;
                    fac.area = CBAREA.SelectedValue;
                    //fac.lugar = tLugar.Text;
                    fac.deteccion = CBDETECCION.SelectedValue;
                    fac.tipo = CBTIPO.SelectedValue;
                    fac.origen = CBORIGEN.SelectedValue;
                    fac.destino = CBDESTINO.SelectedValue;
                    fac.nombre_deteccion = tNombreDetecta.Text;
                    //fac.cargo = tCargo.Text;
                    fac.deteccion_hallazgo = tDeteccionHallazgo.Text;
                    fac.doc_referencia = tDocReferencia.Text;
                    fac.accion_inmediata = tAccionInmediata.Text;
                    fac.estado = "NUEVO HALLAZGO";
                    fac.usuario = usuario.usuario;
                    fac.fecha = DateTime.Now;
                    fac.empresainvolucrada = tEmpInvolucrada.Text;


                    FN_HALLAZGO.INSERT(ref fac);
                    if (fac._respok)
                    {
                        T_ID.Text = fac.ID_HALLAZGO.ToString();
                        LlenarGrilla();
                        alert("Guardado con éxito", 1);
                        // AUTORIZACION
                        //DIVAUTORIZADAS.Visible = true;
                        //LBLAUTMZ.Text = fac.aprobado_mz;
                        //LBLAUTFZ.Text = fac.aprobado_fz;
                        //LBLOBSERVACION.Text = fac.obs_aprobacion;
                        //T_COPYCLIPBOARD.Text = "https://app.transporteszamora.com/aut_oc.aspx?idoc=" + fac.ID_OC;
                        //
                        DIV_ADJUNTAR.Visible = true;
                    }
                    else
                    {
                        alert("Problemas al guardar el documento", 0);
                    }
                }
                else
                {
                    fac.ID_HALLAZGO = int.Parse(T_ID.Text);
                    FN_HALLAZGO.LLENAOBJETO(ref fac);
                    if (fac._respok)
                    {
                        fac.num_referencia = long.Parse(tNumReferencia.Text);
                        fac.fecha_envio = DateTime.Parse(tFechaEnvio.Text);
                        fac.nombre_remitente = tNombreRemitente.Text;
                        fac.area = CBAREA.SelectedValue;
                        //fac.lugar = tLugar.Text;
                        fac.deteccion = CBDETECCION.SelectedValue;
                        fac.tipo = CBTIPO.SelectedValue;
                        fac.origen = CBORIGEN.SelectedValue;
                        fac.destino = CBDESTINO.SelectedValue;
                        fac.nombre_deteccion = tNombreDetecta.Text;
                        //fac.cargo = tCargo.Text;
                        fac.deteccion_hallazgo = tDeteccionHallazgo.Text;
                        fac.doc_referencia = tDocReferencia.Text;
                        fac.accion_inmediata = tAccionInmediata.Text;
                        //fac.estado = "NUEVO HALLAZGO";
                        fac.usuario = usuario.usuario;
                        fac.fecha = DateTime.Now;
                        fac.empresainvolucrada = tEmpInvolucrada.Text;
                        // MODIFICAR
                        FN_HALLAZGO.UPDATE(ref fac);
                        if (fac._respok)
                        {
                            LlenarGrilla();
                            alert("Modificado con éxito", 1);
                        }
                        else
                        {
                            alert("Problemas al modificar el documento", 0);
                        }
                    }
                }
            }

        }

        protected void B_VOLVER_Click(object sender, EventArgs e)
        {
            PANEL_PRINCIPAL.Visible = true;
            PANEL_ENC.Visible = false;
        }

        protected void B_FILTRAR_Click(object sender, EventArgs e)
        {
            LlenarGrilla();
        }

        protected void B_BORRAHALLAZGO_Click(object sender, EventArgs e)
        {
            OBJ_USUARIOS usuario = new OBJ_USUARIOS();
            usuario.usuario = HttpContext.Current.User.Identity.Name;
            FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);

            //if (usuario.usuario == "mzapata")
            //{
            int id = int.Parse(T_ID.Text);
            OBJ_HALLAZGO fact = new OBJ_HALLAZGO();
            fact.ID_HALLAZGO = id;
            FN_HALLAZGO.DELETE(ref fact);
            if (fact._respok)
            {
                DBUtil db = new DBUtil();
                db.Scalar("delete from HALLAZGOS_ADJUNTOS where ID_HALLAZGO = " + id);
                alert("Hallazgo eliminado con éxito", 0);
                LlenarGrilla();
                PANEL_PRINCIPAL.Visible = true;
                PANEL_ENC.Visible = false;
            }
            //}
            //else
            //{
            //    alert("Solo Mauricio Zapata puede eliminar ordenes de compra", 0);
            //}
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

                    if (usuario.usuario == "mzapata" || usuario.usuario == "festay" || usuario.usuario == "gestay")
                    {
                        int id = int.Parse((G_ADJUNTOS.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                        OBJ_HALLAZGOS_ADJUNTOS fact = new OBJ_HALLAZGOS_ADJUNTOS();
                        fact.ID_DOC = id;
                        FN_HALLAZGOS_ADJUNTOS.LLENAOBJETO(ref fact);
                        if (fact._respok)
                        {
                            FN_HALLAZGOS_ADJUNTOS.DELETE(ref fact);
                            if (fact._respok)
                            {
                                try
                                {
                                    string ServerPath = HttpContext.Current.Server.MapPath("~").ToString();
                                    System.IO.File.Delete(ServerPath + "Documentos/Hallazgos/" + fact.id_hallazgo + "/" + fact.nom_archivo);
                                }
                                catch (System.IO.IOException ex)
                                {

                                }
                                LlenarGrillaAdjuntos(fact.id_hallazgo);
                                alert("Archivo Adjunto eliminado con éxito", 0);
                            }
                        }
                    }
                    else
                    {
                        alert("Solo Mauricio Zapata o Francisca Estay pueden eliminar documentos adjuntos", 0);
                    }
                }
                if (e.CommandName == "veradjunto")
                {
                    int id = int.Parse((G_ADJUNTOS.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    OBJ_HALLAZGOS_ADJUNTOS fact = new OBJ_HALLAZGOS_ADJUNTOS();
                    fact.ID_DOC = id;
                    FN_HALLAZGOS_ADJUNTOS.LLENAOBJETO(ref fact);
                    if (fact._respok)
                    {
                        string url = "Documentos/Hallazgos/" + fact.id_hallazgo + "/" + fact.nom_archivo;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "abrirdocadjunto", "<script>javascript:VerArchivo('" + url + "');</script>", false);
                    }
                }
            }
            catch (Exception ex)
            {
                alert("No se pudo eliminar el adjunto", 0);
            }
        }

        public void LlenarGrillaAdjuntos(int id_hallazgo)
        {
            try
            {
                G_ADJUNTOS.DataSource = FN_HALLAZGOS_ADJUNTOS.LLENADT(" where id_hallazgo = " + id_hallazgo);
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
    }
}