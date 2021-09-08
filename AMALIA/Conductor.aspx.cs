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
    public partial class Conductor : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Conductor";
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
                    B_NUEVO.Visible = ValidateUser();
                    LlenarGrilla();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "grid", "<script>javascript:Datatables();</script>", false);
            }
        }

        public bool ValidateUser()
        {
            string usuario = HttpContext.Current.User.Identity.Name;
            if (usuario == "lbelmar" || usuario == "festay" || usuario == "gestay" || usuario == "felipe")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LlenarGrilla()
        {
            G_PRINCIPAL.DataSource = FN_CONDUCTOR.LLENADT();
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
            if (T_NOMBRE.Text == "")
            {
                alert("Ingrese un nombre.", 0);
                T_NOMBRE.Focus();
            }
            else if (T_RUT.Text == "")
            {
                alert("Ingrese un Rut.", 0);
                T_RUT.Focus();
            }
            else
            {
                OBJ_CONDUCTOR objeto_mantenedor = new OBJ_CONDUCTOR();
                if (T_ID.Text != "")
                {
                    // EDITAR
                    objeto_mantenedor.ID_cONDUCTOR = int.Parse(T_ID.Text);
                    FN_CONDUCTOR.LLENAOBJETO(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        objeto_mantenedor.rut = T_RUT.Text;
                        objeto_mantenedor.nombre_completo = T_NOMBRE.Text;
                        objeto_mantenedor.telefono = T_TELEFONO.Text;
                        objeto_mantenedor.telefono2 = T_TELEFONO2.Text;
                        objeto_mantenedor.direccion = T_DIRECCION.Text;
                        if (T_FECHA_NACIMIENTO.Text != "")
                        {
                            objeto_mantenedor.fecha_nacimiento = DateTime.Parse(T_FECHA_NACIMIENTO.Text);
                        }
                        FN_CONDUCTOR.UPDATE(ref objeto_mantenedor);
                        if (objeto_mantenedor._respok)
                        {
                            alert(objeto_mantenedor_global + " modificado con éxito", 1);
                        }
                    }
                }
                else
                {
                    FN_CONDUCTOR.PREPARAOBJETO(ref objeto_mantenedor);
                    // NUEVO                
                    objeto_mantenedor.rut = T_RUT.Text;
                    objeto_mantenedor.nombre_completo = T_NOMBRE.Text;
                    objeto_mantenedor.telefono = T_TELEFONO.Text;
                    objeto_mantenedor.telefono2 = T_TELEFONO2.Text;
                    objeto_mantenedor.direccion = T_DIRECCION.Text;
                    if (T_FECHA_NACIMIENTO.Text != "")
                    {
                        objeto_mantenedor.fecha_nacimiento = DateTime.Parse(T_FECHA_NACIMIENTO.Text);
                    }
                    objeto_mantenedor.activo = "ACTIVO";
                    //      
                    FN_CONDUCTOR.INSERT(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        T_ID.Text = objeto_mantenedor.ID_cONDUCTOR.ToString();
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
                    if (ValidateUser())
                    {
                        int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                        COMPLETAR_DETALLE(id);
                        // MOSTRAR / OCULTAR PANEL
                        PANEL_PRINCIPAL.Visible = false;
                        PANEL_DETALLE1.Visible = true;
                    }
                    else
                    {
                        alert("Ud no tiene permisos para editar", 0);
                    }

                }
                //Borrar
                if (e.CommandName == "Borrar")
                {
                    int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    OBJ_CONDUCTOR tabla = new OBJ_CONDUCTOR();
                    tabla.ID_cONDUCTOR = id;
                    FN_CONDUCTOR.DELETE(ref tabla);
                    if (tabla._respok)
                    {
                        LlenarGrilla();
                    }
                }
                if (e.CommandName == "Cambiarestado")
                {
                    if (ValidateUser())
                    {
                        int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                        string estado = G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString();
                        OBJ_CONDUCTOR tabla = new OBJ_CONDUCTOR();
                        tabla.ID_cONDUCTOR = id;
                        FN_CONDUCTOR.LLENAOBJETO(ref tabla);
                        if (tabla._respok)
                        {
                            if (estado == "ACTIVO")
                            {
                                tabla.activo = "INACTIVO";
                            }
                            else
                            {
                                tabla.activo = "ACTIVO";
                            }
                            FN_CONDUCTOR.UPDATE(ref tabla);
                            if (tabla._respok)
                            {
                                LlenarGrilla();
                            }
                        }
                    }
                    else
                    {
                        alert("Ud no tiene permisos para cambiar estado al conductor", 0);
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
            OBJ_CONDUCTOR objeto_mantenedor = new OBJ_CONDUCTOR();

            objeto_mantenedor.ID_cONDUCTOR = id;
            FN_CONDUCTOR.LLENAOBJETO(ref objeto_mantenedor);
            if (objeto_mantenedor._respok)
            {
                LIMPIARCAMPOS();
                T_ID.Text = id.ToString();
                T_RUT.Text = objeto_mantenedor.rut;
                T_NOMBRE.Text = objeto_mantenedor.nombre_completo;
                T_TELEFONO.Text = objeto_mantenedor.telefono;
                T_TELEFONO2.Text = objeto_mantenedor.telefono2;
                T_DIRECCION.Text = objeto_mantenedor.direccion;
                T_FECHA_NACIMIENTO.Text = objeto_mantenedor.fecha_nacimiento.ToString("yyyy-MM-dd");
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