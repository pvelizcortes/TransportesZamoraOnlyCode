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
    public partial class Cliente : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Cliente";
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
                if (G_PRINCIPAL.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "table_principal", "<script>javascript:Datatables();</script>", false);
                }
            }
        }

        public void LlenarGrilla()
        {
            G_PRINCIPAL.DataSource = FN_CLIENTE.LLENADT();
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
            else if (T_RAZON_SOCIAL.Text == "")
            {
                alert("Ingrese la razón Social.", 0);
                T_RAZON_SOCIAL.Focus();
            }
            else if (T_RUT.Text == "")
            {
                alert("Ingrese el rut del cliente.", 0);
                T_RUT.Focus();
            }
            else
            {
                OBJ_CLIENTE objeto_mantenedor = new OBJ_CLIENTE();
                if (T_ID.Text != "")
                {
                    // EDITAR
                    objeto_mantenedor.ID_CLIENTE = int.Parse(T_ID.Text);
                    FN_CLIENTE.LLENAOBJETO(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        objeto_mantenedor.nombre_cliente = T_NOMBRE.Text;
                        objeto_mantenedor.razon_social = T_RAZON_SOCIAL.Text;
                        objeto_mantenedor.correo = T_CORREO.Text;
                        objeto_mantenedor.direccion = T_DIRECCION.Text;
                        objeto_mantenedor.telefono = T_TELEFONO.Text;
                        objeto_mantenedor.telefono2 = T_TELEFONO2.Text;
                        objeto_mantenedor.nombre_encargado2 = T_ENCARGADO2.Text;
                        objeto_mantenedor.nombre_encargado = T_ENCARGADO.Text;
                        objeto_mantenedor.rut_cliente = T_RUT.Text;

                        FN_CLIENTE.UPDATE(ref objeto_mantenedor);
                        if (objeto_mantenedor._respok)
                        {
                            alert(objeto_mantenedor_global + " modificado con éxito", 1);
                        }
                    }
                }
                else
                {
                    FN_CLIENTE.PREPARAOBJETO(ref objeto_mantenedor);
                    // NUEVO               

                    objeto_mantenedor.nombre_cliente = T_NOMBRE.Text;
                    objeto_mantenedor.razon_social = T_RAZON_SOCIAL.Text;
                    objeto_mantenedor.correo = T_CORREO.Text;
                    objeto_mantenedor.direccion = T_DIRECCION.Text;
                    objeto_mantenedor.telefono = T_TELEFONO.Text;
                    objeto_mantenedor.telefono2 = T_TELEFONO2.Text;
                    objeto_mantenedor.nombre_encargado2 = T_ENCARGADO2.Text;
                    objeto_mantenedor.nombre_encargado = T_ENCARGADO.Text;
                    objeto_mantenedor.rut_cliente = T_RUT.Text;
                    //      
                    FN_CLIENTE.INSERT(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        T_ID.Text = objeto_mantenedor.ID_CLIENTE.ToString();
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
                    OBJ_CLIENTE tabla = new OBJ_CLIENTE();
                    tabla.ID_CLIENTE = id;
                    FN_CLIENTE.DELETE(ref tabla);
                    if (tabla._respok)
                    {
                        LlenarGrilla();
                    }
                }
                if (e.CommandName == "Cambiarestado")
                {
                    int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    string estado = G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString();
                    OBJ_CLIENTE tabla = new OBJ_CLIENTE();
                    tabla.ID_CLIENTE = id;
                    FN_CLIENTE.LLENAOBJETO(ref tabla);
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
                        FN_CLIENTE.UPDATE(ref tabla);
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
            OBJ_CLIENTE objeto_mantenedor = new OBJ_CLIENTE();

            objeto_mantenedor.ID_CLIENTE = id;
            FN_CLIENTE.LLENAOBJETO(ref objeto_mantenedor);
            if (objeto_mantenedor._respok)
            {
                LIMPIARCAMPOS();
                T_ID.Text = id.ToString();
                T_NOMBRE.Text = objeto_mantenedor.nombre_cliente;
                T_RAZON_SOCIAL.Text = objeto_mantenedor.razon_social;
                T_DIRECCION.Text = objeto_mantenedor.direccion;
                T_CORREO.Text = objeto_mantenedor.correo;
                T_TELEFONO.Text = objeto_mantenedor.telefono;
                T_TELEFONO2.Text = objeto_mantenedor.telefono2;
                T_ENCARGADO.Text = objeto_mantenedor.nombre_encargado;
                T_ENCARGADO2.Text = objeto_mantenedor.nombre_encargado2;
                T_RUT.Text = objeto_mantenedor.rut_cliente;
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