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
    public partial class Proveedores_OC : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Proveedor";
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
                    if (usuario.usuario != "mzapata" && usuario.usuario != "leonel" && usuario.usuario != "festay" && usuario.usuario != "gestay" && usuario.usuario != "gestay" && usuario.usuario != "fduran" && usuario.usuario != "cmartinez" && usuario.usuario != "lbelmar")
                    {
                        Response.Redirect("index.aspx");
                    }
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
            G_PRINCIPAL.DataSource = FN_OC_PROVEEDORES.LLENADT();
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
                alert("Ingrese el rut del proveedor.", 0);
                T_RUT.Focus();
            }
            else if (T_RAZONSOCIAL.Text == "")
            {
                alert("Ingrese la razón social del proveedor.", 0);
                T_RAZONSOCIAL.Focus();
            }
            else
            {
                OBJ_OC_PROVEEDORES objeto_mantenedor = new OBJ_OC_PROVEEDORES();
                if (T_ID.Text != "")
                {
                    // EDITAR
                    objeto_mantenedor.ID_OC_PROVEEDOR = int.Parse(T_ID.Text);
                    FN_OC_PROVEEDORES.LLENAOBJETO(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        objeto_mantenedor.nombre_corto = T_NOMBRE.Text;
                        objeto_mantenedor.razon_social = T_RAZONSOCIAL.Text;
                        objeto_mantenedor.comuna = T_COMUNA.Text;
                        objeto_mantenedor.ciudad = T_CIUDAD.Text;
                        objeto_mantenedor.direccion = T_DIRECCION.Text;
                        objeto_mantenedor.contacto = T_CONTACTO.Text;
                        objeto_mantenedor.fono = T_FONO.Text;
                        objeto_mantenedor.banco = T_BANCO.Text;
                        objeto_mantenedor.tipo_cuenta = CB_TIPO_CUENTA.SelectedValue;
                        objeto_mantenedor.rut_cuenta = T_RUT_CUENTA.Text;
                        objeto_mantenedor.num_cuenta = T_NUMCUENTA.Text;
                        objeto_mantenedor.email = T_EMAIL.Text;

                        FN_OC_PROVEEDORES.UPDATE(ref objeto_mantenedor);
                        if (objeto_mantenedor._respok)
                        {
                            alert(objeto_mantenedor_global + " modificado con éxito", 1);
                        }
                    }
                }
                else
                {
                    FN_OC_PROVEEDORES.PREPARAOBJETO(ref objeto_mantenedor);
                    // NUEVO 
                    objeto_mantenedor.rut = T_RUT.Text;
                    objeto_mantenedor.nombre_corto = T_NOMBRE.Text;
                    objeto_mantenedor.razon_social = T_RAZONSOCIAL.Text;
                    objeto_mantenedor.comuna = T_COMUNA.Text;
                    objeto_mantenedor.ciudad = T_CIUDAD.Text;
                    objeto_mantenedor.direccion = T_DIRECCION.Text;
                    objeto_mantenedor.contacto = T_CONTACTO.Text;
                    objeto_mantenedor.fono = T_FONO.Text;
                    objeto_mantenedor.banco = T_BANCO.Text;
                    objeto_mantenedor.tipo_cuenta = CB_TIPO_CUENTA.SelectedValue;
                    objeto_mantenedor.rut_cuenta = T_RUT_CUENTA.Text;
                    objeto_mantenedor.num_cuenta = T_NUMCUENTA.Text;
                    objeto_mantenedor.email = T_EMAIL.Text;
                    //      
                    FN_OC_PROVEEDORES.INSERT(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        T_ID.Text = objeto_mantenedor.ID_OC_PROVEEDOR.ToString();
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
                    OBJ_OC_PROVEEDORES tabla = new OBJ_OC_PROVEEDORES();
                    tabla.ID_OC_PROVEEDOR = id;
                    FN_OC_PROVEEDORES.DELETE(ref tabla);
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
            OBJ_OC_PROVEEDORES objeto_mantenedor = new OBJ_OC_PROVEEDORES();
            objeto_mantenedor.ID_OC_PROVEEDOR = id;
            FN_OC_PROVEEDORES.LLENAOBJETO(ref objeto_mantenedor);
            if (objeto_mantenedor._respok)
            {
                LIMPIARCAMPOS();
                T_ID.Text = id.ToString();
                T_NOMBRE.Text = objeto_mantenedor.nombre_corto;
                T_RUT.Text = objeto_mantenedor.rut;
                T_RAZONSOCIAL.Text = objeto_mantenedor.razon_social;
                T_COMUNA.Text = objeto_mantenedor.comuna;
                T_CIUDAD.Text = objeto_mantenedor.ciudad;
                T_DIRECCION.Text = objeto_mantenedor.direccion;
                T_CONTACTO.Text = objeto_mantenedor.contacto;
                T_FONO.Text = objeto_mantenedor.fono;
                T_BANCO.Text = objeto_mantenedor.banco;
                CB_TIPO_CUENTA.SelectedValue = objeto_mantenedor.tipo_cuenta;
                T_RUT_CUENTA.Text = objeto_mantenedor.rut_cuenta;
                T_NUMCUENTA.Text = objeto_mantenedor.num_cuenta;
                T_EMAIL.Text = objeto_mantenedor.email;
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