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
    public partial class Usuarios : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Usuario";
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
            G_PRINCIPAL.DataSource = FN_USUARIOS.LLENADT();
            G_PRINCIPAL.DataBind();
        }

        protected void B_NUEVO_Click(object sender, EventArgs e)
        {
            LIMPIARCAMPOS();
            PANEL_DETALLE1.Visible = true;
            PANEL_PRINCIPAL.Visible = false;
            B_CAMBIAR_PASS.Visible = false;
            UP_PRINCIPAL.Update();
        }

        protected void B_GUARDAR_Click(object sender, EventArgs e)
        {

            if (T_NOMBRE.Text == "")
            {
                alert("Ingrese un Nombre.", 0);
                T_NOMBRE.Focus();
            }
            else if (T_USUARIO.Text == "")
            {
                alert("Ingrese un nombre de usuario.", 0);
                T_USUARIO.Focus();
            }
            else if ((T_PASS.Text == "" || T_PASS.Text.Length < 4) && T_ID.Text == "")
            {                
                alert("Ingrese una contraseña de al menos 4 caracteres.", 0);
                T_PASS.Focus();
            }
            else
            {
                OBJ_USUARIOS objeto_mantenedor = new OBJ_USUARIOS();
                if (T_ID.Text != "")
                {
                    // EDITAR
                    objeto_mantenedor.ID_USUARIO = int.Parse(T_ID.Text);
                    FN_USUARIOS.LLENAOBJETO(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        //objeto_mantenedor.usuario = T_USUARIO.Text;
                        objeto_mantenedor.nombre_completo = T_NOMBRE.Text;
                        objeto_mantenedor.correo = T_CORREO.Text;
                        objeto_mantenedor.direccion = T_DIRECCION.Text;
                        objeto_mantenedor.telefono = T_TELEFONO.Text;
                        objeto_mantenedor.id_perfil = int.Parse(CB_PERFIL.SelectedValue);
                        FN_USUARIOS.UPDATE(ref objeto_mantenedor);
                        if (objeto_mantenedor._respok)
                        {
                            alert(objeto_mantenedor_global + " modificado con éxito", 1);
                        }
                    }
                }
                else
                {
                    FN_USUARIOS.PREPARAOBJETO(ref objeto_mantenedor);
                    // NUEVO                
                    objeto_mantenedor.usuario = T_USUARIO.Text;
                    objeto_mantenedor.nombre_completo = T_NOMBRE.Text;
                    objeto_mantenedor.correo = T_CORREO.Text;
                    objeto_mantenedor.direccion = T_DIRECCION.Text;
                    objeto_mantenedor.pass = T_PASS.Text;
                    objeto_mantenedor.telefono = T_TELEFONO.Text;
                    objeto_mantenedor.id_perfil = int.Parse(CB_PERFIL.SelectedValue);
                    //      
                    FN_USUARIOS.INSERT(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        T_ID.Text = objeto_mantenedor.ID_USUARIO.ToString();
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
                    OBJ_USUARIOS tabla = new OBJ_USUARIOS();
                    tabla.ID_USUARIO = id;
                    FN_USUARIOS.DELETE(ref tabla);
                    if (tabla._respok)
                    {
                        LlenarGrilla();
                    }
                }
                if (e.CommandName == "Cambiarestado")
                {
                    int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    string estado = G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString();
                    OBJ_USUARIOS tabla = new OBJ_USUARIOS();
                    tabla.ID_USUARIO = id;
                    FN_USUARIOS.LLENAOBJETO(ref tabla);
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
                        FN_USUARIOS.UPDATE(ref tabla);
                        if (tabla._respok)
                        {
                            LlenarGrilla();
                        }
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
            OBJ_USUARIOS objeto_mantenedor = new OBJ_USUARIOS();

            objeto_mantenedor.ID_USUARIO = id;
            FN_USUARIOS.LLENAOBJETO(ref objeto_mantenedor);
            if (objeto_mantenedor._respok)
            {
                LIMPIARCAMPOS();
                T_ID.Text = id.ToString();
                T_NOMBRE.Text = objeto_mantenedor.nombre_completo;
                T_USUARIO.Text = objeto_mantenedor.usuario;
                T_CORREO.Text = objeto_mantenedor.correo;
                T_TELEFONO.Text = objeto_mantenedor.telefono;
                T_DIRECCION.Text = objeto_mantenedor.direccion;
                CB_PERFIL.SelectedValue = objeto_mantenedor.id_perfil.ToString();
            }
            B_CAMBIAR_PASS.Visible = true;
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

        protected void B_CAMBIAR_PASS_Click(object sender, EventArgs e)
        {
            if (T_PASS.Text != "")
            {
                OBJ_USUARIOS objeto_mantenedor = new OBJ_USUARIOS();
                if (T_ID.Text != "")
                {
                    // EDITAR
                    objeto_mantenedor.ID_USUARIO = int.Parse(T_ID.Text);
                    FN_USUARIOS.LLENAOBJETO(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        objeto_mantenedor.usuario = T_USUARIO.Text;
                        objeto_mantenedor.pass = T_PASS.Text;
                        FN_USUARIOS.CambiarContrasena(ref objeto_mantenedor);
                        if (objeto_mantenedor._respok)
                        {
                            alert(objeto_mantenedor_global + " modificado con éxito", 1);
                        }
                    }
                }
            }
            else
            {
                alert("Ingrese una contraseña", 0);
            }

        }
    }
}