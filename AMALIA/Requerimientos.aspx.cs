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
    public partial class Requerimientos : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Requerimiento";
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
                ScriptManager.RegisterStartupScript(this, typeof(Page), "req_grid", "<script>javascript:Datatables();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "combopro", "<script>javascript:ComboPro();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "relojreq", "<script>javascript:relojito(false);</script>", false);
            }
        }
        public void LlenarGrilla()
        {
            G_PRINCIPAL.DataSource = FN_REQUERIMIENTOS.LLENADT();
            G_PRINCIPAL.DataBind();
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
                //Borrar
                if (e.CommandName == "Borrar")
                {
                    //int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    //OBJ_REQUERIMIENTOS tabla = new OBJ_REQUERIMIENTOS();
                    //tabla.id_requerimiento = id;
                    //FN_REQUERIMIENTOS.DELETE(ref tabla);
                    //if (tabla._respok)
                    //{
                    //    LlenarGrilla();
                    //}
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
            dt = FN_REQUERIMIENTOS.LLENADT(" where id_requerimiento = " + id);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["id_ot"].ToString() == "0")
                {
                    LIMPIARCAMPOS();
                    T_ID.Text = id.ToString();
                    DIV_FECHA.InnerHtml = DateTime.Parse(dt.Rows[0]["fecha"].ToString()).ToString("dd/MM/yyyy");
                    DIV_NOMBRE_CONDUCTOR.InnerHtml = dt.Rows[0]["nombre_completo"].ToString();
                    DIV_CAMION.InnerText = dt.Rows[0]["patente"].ToString();
                    DIV_RAMPLA.InnerText = dt.Rows[0]["patente_rampla"].ToString();
                    DIV_OBSERVACION.InnerText = dt.Rows[0]["descripcion"].ToString();

                    if (dt.Rows[0]["urgencia"].ToString() == "1")
                    {
                        DIV_PRIORIDAD_ICON.InnerHtml = "  <i class=' fa fa-eye col-green fa-2x'></i>";
                        DIV_PRIORIDAD.InnerHtml = "BAJA";
                    }
                    else if (dt.Rows[0]["urgencia"].ToString() == "2")
                    {
                        DIV_PRIORIDAD_ICON.InnerHtml = "  <i class=' fa fa-bell col-blue fa-2x'></i>";
                        DIV_PRIORIDAD.InnerHtml = "NORMAL";
                    }
                    else if (dt.Rows[0]["urgencia"].ToString() == "3")
                    {
                        DIV_PRIORIDAD_ICON.InnerHtml = "  <i class=' fa fa-exclamation col-orange fa-2x'></i>";
                        DIV_PRIORIDAD.InnerHtml = "ALTA";
                    }
                    else if (dt.Rows[0]["urgencia"].ToString() == "4")
                    {
                        DIV_PRIORIDAD_ICON.InnerHtml = "  <i class=' fa fa-exclamation-triangle col-red fa-2x'></i>";
                        DIV_PRIORIDAD.InnerHtml = "URGENTE";
                    }
                    UP_GASTO_GENERAL.Update();                 
                    AbreModalGasto();
                }
                else
                {
                    alert("Se generó orden de trabajo Nº " + dt.Rows[0]["id_ot"].ToString() + " para este requerimiento, no se puede editar o eliminar.", 0);
                }
            }
        }

        public void AbreModalGasto()
        {
            // ABRE MODAL
            ScriptManager.RegisterStartupScript(this, typeof(Page), "abremodalgasto", "<script>javascript:GASTOGENERAL();</script>", false);
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

        protected void G_PRINCIPAL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string ID_REQ = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[0].ToString();
                string PRIORIDAD = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[1].ToString();
                string ESTADO = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[2].ToString();
                string OT = G_PRINCIPAL.DataKeys[e.Row.RowIndex].Values[3].ToString();

                // ESTADO
                HtmlGenericControl html_prioridad = (HtmlGenericControl)e.Row.FindControl("div_prioridad");
                HtmlGenericControl html_estado = (HtmlGenericControl)e.Row.FindControl("div_estado");

                switch (PRIORIDAD)
                {
                    case "BAJA":
                   
                        html_prioridad.InnerHtml = "<span class='badge badge-block col-green'><i class=' fa fa-eye col-green'></i> BAJA</span>";
                        break;
                    case "NORMAL":
                   
                        html_prioridad.InnerHtml = "<span class='badge badge-block col-blue'><i class='fa fa-bell col-blue'></i> NORMAL</span>";
                        break;
                    case "ALTA":
                  
                        html_prioridad.InnerHtml = "<span class='badge badge-block col-orange'> <i class=' fa fa-exclamation col-orange '></i> ALTA</span>";
                        break;
                    case "URGENTE":
                        
                        html_prioridad.InnerHtml = "<span class='badge badge-block col-red'><i class='fa fa-exclamation-triangle col-red '></i> URGENTE</span>";
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }

                if (ESTADO.IndexOf("DE TRABAJO") > 0)
                {
                    html_estado.InnerHtml = "<a href='OrdenesdeTrabajo.aspx?id_ot=" + OT + "'><span class='badge badge-block badge-success'>" + ESTADO + "</span></a>";
                }
                else if (ESTADO == "NUEVO REQUERIMIENTO")
                {
                    html_estado.InnerHtml = "<span class='badge badge-block badge-danger'>" + ESTADO + "</span>";
                }
                else if (ESTADO == "EN ESPERA")
                {
                    html_estado.InnerHtml = "<span class='badge badge-block badge-warning'>" + ESTADO + "</span>";
                }
                else if (ESTADO == "RECHAZADO")
                {
                    html_estado.InnerHtml = "<span class='badge badge-block badge-danger'>" + ESTADO + "</span>";
                }
            }

        }


        protected void B_GENERAOT_Click(object sender, EventArgs e)
        {
            DBUtil db = new DBUtil();
            int nuevaot = 0;
            string num_ot = db.Scalar("select MAX(ID_OT) from MANT_OT").ToString();
            try
            {
                nuevaot = int.Parse(num_ot) + 1;
            }
            catch (Exception ex)
            {
                nuevaot = 1;
            }
          
            OBJ_REQUERIMIENTOS req = new OBJ_REQUERIMIENTOS();
            req.ID_REQUERIMIENTO = int.Parse(T_ID.Text);

            FN_REQUERIMIENTOS.LLENAOBJETO(ref req);
            if (req._respok)
            {
                // AQUI GENERAR LA OT Y CON EL SCOPE AGREGARLO AL REQUERIMIENTO
                OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                usuario.usuario = HttpContext.Current.User.Identity.Name;
                FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
                OBJ_OT ot = new OBJ_OT();
                ot.id_ot = nuevaot;
                ot.id_requerimiento = req.ID_REQUERIMIENTO;
                ot.id_camion = req.id_camion;
                ot.id_rampla = req.id_rampla;
                ot.descripcion = T_RESOLUCION.Text;
                ot.fecha_actualizacion = DateTime.Now;
                ot.fecha_creacion = DateTime.Now;
                ot.id_estado_ot = 1;
                ot.id_usuario = usuario.ID_USUARIO;
                ot.nom_estado_ot = "NUEVA";
                ot.resolucion = T_RESOLUCION.Text;
                ot.solicitante = req.solicitante;
                ot.urgencia = req.urgencia;

                FN_OT.INSERT(ref ot);
                if (ot._respok)
                {               
                    alert("Orden de trabajo Nº " + nuevaot + " creada con éxito.", 1);
                    req.id_ot = nuevaot;
                    req.estado = "ORDEN DE TRABAJO Nº " + nuevaot;
                    req.resolucion = T_RESOLUCION.Text;
                    FN_REQUERIMIENTOS.UPDATE(ref req);
                    db.Scalar(" insert into mant_ot_log (id_ot, fecha, observacion, estado_i, estado_f, id_usuario) values (" + nuevaot + ", getdate(), 'NUEVA OT', 1, 1, " + usuario.ID_USUARIO + "); ");
                    db.Scalar(" insert into mant_ot_log (id_ot, fecha, observacion, estado_i, estado_f, id_usuario) values (" + nuevaot + ", getdate(), 'REQ: " + req.descripcion + "', 1, 1, " + usuario.ID_USUARIO + "); ");
                    //FN_OT.INSERT_LOG(ref ot,"REQUERIMIENTO", "NUEVA");
                }
                else
                {
                    alert("Error al crear Orden de Trabajo", 0);
                }
                //
                LlenarGrilla();
                UP_PRINCIPAL.Update();
                CERRARMODAL();
            }
        }

        protected void B_RECHAZAR_Click(object sender, EventArgs e)
        {
            OBJ_REQUERIMIENTOS req = new OBJ_REQUERIMIENTOS();
            req.ID_REQUERIMIENTO = int.Parse(T_ID.Text);

            FN_REQUERIMIENTOS.LLENAOBJETO(ref req);
            if (req._respok)
            {
                req.estado = "EN ESPERA";
                req.resolucion = T_RESOLUCION.Text;
                FN_REQUERIMIENTOS.UPDATE(ref req);
                LlenarGrilla();
                UP_PRINCIPAL.Update();
                CERRARMODAL();
            }
        }

        protected void B_BORRAR_Click(object sender, EventArgs e)
        {
            OBJ_REQUERIMIENTOS req = new OBJ_REQUERIMIENTOS();
            req.ID_REQUERIMIENTO = int.Parse(T_ID.Text);

            FN_REQUERIMIENTOS.LLENAOBJETO(ref req);
            if (req._respok)
            {
                req.estado = "RECHAZADO";
                req.resolucion = T_RESOLUCION.Text;
                FN_REQUERIMIENTOS.UPDATE(ref req);
                LlenarGrilla();
                UP_PRINCIPAL.Update();
                CERRARMODAL();
            }
        }

        public void CERRARMODAL()
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "cerramodal", "<script>javascript:cerramodal();</script>", false);
        }
    }

}