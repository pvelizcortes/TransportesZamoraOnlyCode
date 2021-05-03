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
    public partial class Checklists : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Checklist";
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
                    if (Request.QueryString["id"] != null)
                    {
                        FILTRA_FECHA_DESDE.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                        int id_completado = Convert.ToInt32(Request.QueryString["id"]);
                        COMPLETAR_DETALLE(id_completado);
                    }
                    else
                    {
                        LlenarGrillaInicio();
                    }
                  
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
                filtro += " and fecha >= convert(date, '" + DateTime.Parse(FILTRA_FECHA_DESDE.Text).ToString("dd/MM/yyyy") + "', 103) ";
            }
            if (FILTRA_FECHA_HASTA.Text != "")
            {
                filtro += " and fecha <= convert(date, '" + DateTime.Parse(FILTRA_FECHA_HASTA.Text).ToString("dd/MM/yyyy") + "', 103) ";
            }
            if (CB_ESTADOS.SelectedValue != "-1")
            {
                filtro += " and estado = '" + CB_ESTADOS.SelectedValue + "'";
            }

            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            dt = db.consultar("select * from [V_CHECKLISTS] " + filtro + " order by fecha desc;");
            G_PRINCIPAL.DataSource = dt;
            G_PRINCIPAL.DataBind();
        }


        protected void B_NUEVO_Click(object sender, EventArgs e)
        {
            LIMPIARCAMPOS();
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
            DBUtil db = new DBUtil();
            LIMPIARCAMPOS();
            OBJ_CHECKLISTS_COMPLETADOS fact = new OBJ_CHECKLISTS_COMPLETADOS();
            fact.ID_CHECKLIST_COMPLETADO = id;

            FN_CHECKLISTS_COMPLETADOS.LLENAOBJETO(ref fact);
            if (fact._respok)
            {
                imagenes_div.InnerHtml = "";
                T_IDCHECKLISTCOMPLETADO.Text = id.ToString();

                G_RESPUESTAS.DataSource =  db.consultar("select * from V_CHECKLISTS_RESPUESTAS WHERE ID_CHECKLIST_COMPLETADO = " + fact.ID_CHECKLIST_COMPLETADO);
                G_RESPUESTAS.DataBind();

                G_ENCABEZADO.DataSource = FN_CHECKLISTS_COMPLETADOS.LLENADT(" where id_checklist_completado = " + fact.ID_CHECKLIST_COMPLETADO.ToString());
                G_ENCABEZADO.DataBind();
          

                DataTable dtImagenes = new DataTable();
                dtImagenes = FN_CHECKLISTS_IMAGENES.LLENADT(" where id_checklist_completado = " + fact.ID_CHECKLIST_COMPLETADO);            
                if (dtImagenes.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtImagenes.Rows)
                    {
                        try
                        {
                            imagenes_div.InnerHtml += "<a href='Checklist/" + fact.ID_CHECKLIST_COMPLETADO + "/" + dr["nombreGuardado"].ToString() + "' target='_blank'><img src='Checklist/" + fact.ID_CHECKLIST_COMPLETADO + "/" + dr["nombreGuardado"].ToString() + "' style='width:20%;padding:10px;'></a>";  
                        }
                        catch (Exception ex)
                        {

                        }
                    }                    
                }

                PANEL_ENC.Visible = true;          
                PANEL_PRINCIPAL.Visible = false;    
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


        protected void B_VOLVER_Click(object sender, EventArgs e)
        {
            PANEL_PRINCIPAL.Visible = true;
            PANEL_ENC.Visible = false;
        }

        protected void B_FILTRAR_Click(object sender, EventArgs e)
        {
            LlenarGrilla();
        }

        protected void B_ELIMINAR_OC_Click(object sender, EventArgs e)
        {
            int id = int.Parse(T_IDCHECKLISTCOMPLETADO.Text);
            OBJ_CHECKLISTS_COMPLETADOS fact = new OBJ_CHECKLISTS_COMPLETADOS();
            fact.ID_CHECKLIST_COMPLETADO = id;
            FN_CHECKLISTS_COMPLETADOS.DELETE(ref fact);
            if (fact._respok)
            {
                DBUtil db = new DBUtil();
                db.Scalar("delete from CHECKLISTS_RESPUESTAS where ID_CHECKLIST_COMPLETADO = " + id);
                alert("Checklist eliminado con éxito", 0);
                LlenarGrilla();
                PANEL_PRINCIPAL.Visible = true;
                PANEL_ENC.Visible = false;
            }
        }

        protected void bAprobar_Click(object sender, EventArgs e)
        {
            OBJ_USUARIOS usuario = new OBJ_USUARIOS();
            usuario.usuario = HttpContext.Current.User.Identity.Name;
            FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);

            int id = int.Parse(T_IDCHECKLISTCOMPLETADO.Text);
            OBJ_CHECKLISTS_COMPLETADOS fact = new OBJ_CHECKLISTS_COMPLETADOS();
            fact.ID_CHECKLIST_COMPLETADO = id;
            FN_CHECKLISTS_COMPLETADOS.LLENAOBJETO(ref fact);
            if (fact._respok)
            {
                fact.estado = "APROBADO";
                fact.observacion_aprobacion = t_observacion.Text;
                fact.usuario_aprobacion = usuario.usuario;
                fact.fecha_aprobacion = DateTime.Now;
                FN_CHECKLISTS_COMPLETADOS.UPDATE(ref fact);
                if (fact._respok)
                {
                    LlenarGrilla();
                    alert("Checklist aprobado con éxito", 1);
                    PANEL_PRINCIPAL.Visible = true;
                    PANEL_ENC.Visible = false;
                }
            }
        }

        protected void bRechazar_Click(object sender, EventArgs e)
        {
            OBJ_USUARIOS usuario = new OBJ_USUARIOS();
            usuario.usuario = HttpContext.Current.User.Identity.Name;
            FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);

            int id = int.Parse(T_IDCHECKLISTCOMPLETADO.Text);
            OBJ_CHECKLISTS_COMPLETADOS fact = new OBJ_CHECKLISTS_COMPLETADOS();
            fact.ID_CHECKLIST_COMPLETADO = id;
            FN_CHECKLISTS_COMPLETADOS.LLENAOBJETO(ref fact);
            if (fact._respok)
            {
                fact.estado = "RECHAZADO";
                fact.observacion_aprobacion = t_observacion.Text;
                fact.usuario_aprobacion = usuario.usuario;
                fact.fecha_aprobacion = DateTime.Now;
                FN_CHECKLISTS_COMPLETADOS.UPDATE(ref fact);
                if (fact._respok)
                {
                    LlenarGrilla();
                    alert("Checklist rechazado con éxito", 0);
                    PANEL_PRINCIPAL.Visible = true;
                    PANEL_ENC.Visible = false;
                }
            }
        }

        protected void bVerPDF_Click(object sender, EventArgs e)
        {

        }

        protected void G_PRINCIPAL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              
                string estado = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[2].ToString();
                string fecha = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[3].ToString();
                // ESTADO
                HtmlGenericControl spnHtml = (HtmlGenericControl)e.Row.FindControl("div_estado");
                if (estado == "NUEVO" || estado == "EDITADO")
                {
                    spnHtml.InnerHtml = "<span class='badge badge-primary'>" + estado + "</span>";
                }
                else if (estado == "APROBADO")
                {
                    spnHtml.InnerHtml = "<span class='badge badge-success'>APROBADO</span>";
                }
                else
                {
                    spnHtml.InnerHtml = "<span class='badge badge-danger'>RECHAZADO</span>";
                }
                // FECHA
                HtmlGenericControl spnHtml2 = (HtmlGenericControl)e.Row.FindControl("div_fecha");
                if (fecha == "1/1/1900 00:00:00")
                {
                    spnHtml2.InnerHtml = "N/A";
                }               
                else
                {
                    spnHtml2.InnerHtml = Convert.ToDateTime(fecha).ToString("dd/MM/yyyy HH:mm");
                }
            }
        }
    }
}