using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AMALIAFW;
using System.Data;
using System.Web.Services;


namespace AMALIA
{
    public partial class Obra : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Obra";
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
            G_PRINCIPAL.DataSource = FN_OBRAS.LLENADT();
            G_PRINCIPAL.DataBind();
        }

        protected void B_NUEVO_Click(object sender, EventArgs e)
        {
            LIMPIARCAMPOS();
            PANEL_DETALLE1.Visible = true;
            PANEL_PRINCIPAL.Visible = false;
            UP_PRINCIPAL.Update();
        }

        protected void B_GUARDAR_Click(object sender, EventArgs e)
        {
            if (T_CLIENTE.Text == "")
            {
                alert("Ingrese un nombre para el cliente.", 0);
                T_CLIENTE.Focus();
            }
            else if (T_OBRA.Text == "")
            {
                alert("Ingrese un nombre para la obra.", 0);
                T_OBRA.Focus();
            }
            else
            {
                OBJ_OBRAS objeto_mantenedor = new OBJ_OBRAS();
                if (T_ID.Text != "")
                {
                    // EDITAR
                    objeto_mantenedor.ID_OBRA = int.Parse(T_ID.Text);
                    FN_OBRAS.LLENAOBJETO(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        objeto_mantenedor.cliente = T_CLIENTE.Text;
                        objeto_mantenedor.obra = T_OBRA.Text;

                        FN_OBRAS.UPDATE(ref objeto_mantenedor);
                        if (objeto_mantenedor._respok)
                        {
                            alert(objeto_mantenedor_global + " modificado con éxito", 1);
                        }
                    }
                }
                else
                {
                    FN_OBRAS.PREPARAOBJETO(ref objeto_mantenedor);
                    // NUEVO                
                    objeto_mantenedor.cliente = T_CLIENTE.Text;
                    objeto_mantenedor.obra = T_OBRA.Text;
                    //      
                    FN_OBRAS.INSERT(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        T_ID.Text = objeto_mantenedor.ID_OBRA.ToString();
                        alert(objeto_mantenedor_global + " creado con éxito", 1);
                    }
                }
            }
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
                //Borrar
                if (e.CommandName == "Borrar")
                {
                    int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    OBJ_OBRAS tabla = new OBJ_OBRAS();
                    tabla.ID_OBRA = id;
                    FN_OBRAS.DELETE(ref tabla);
                    if (tabla._respok)
                    {
                        LlenarGrilla();
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
            OBJ_OBRAS objeto_mantenedor = new OBJ_OBRAS();

            objeto_mantenedor.ID_OBRA = id;
            FN_OBRAS.LLENAOBJETO(ref objeto_mantenedor);
            if (objeto_mantenedor._respok)
            {
                LIMPIARCAMPOS();
                T_ID.Text = id.ToString();
                T_CLIENTE.Text = objeto_mantenedor.cliente;
                T_OBRA.Text = objeto_mantenedor.obra;
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
    }
}