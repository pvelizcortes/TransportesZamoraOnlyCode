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
    public partial class Repuestos : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Repuesto";
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
                    LlenarCombos();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "repuestosaspx", "<script>javascript:Datatables();</script>", false);
            }
        }

        public void LlenarGrilla()
        {
            G_PRINCIPAL.DataSource = FN_MANT_PRODUCTO.LLENADT();
            G_PRINCIPAL.DataBind();
        }

        public void LlenarCombos()
        {
            DBUtil db = new DBUtil();
            CB_MARCA.DataSource = db.consultar("SELECT * FROM MANT_REPUESTOS_MARCAS");
            CB_MARCA.DataTextField = "NOMBRE_MARCA";
            CB_MARCA.DataValueField = "ID_MARCA";
            CB_MARCA.DataBind();

            CB_CATEGORIA.DataSource = db.consultar("SELECT * FROM MANT_PRODUCTO_CAT");
            CB_CATEGORIA.DataTextField = "NOMBRE_CATEGORIA";
            CB_CATEGORIA.DataValueField = "ID_PRODUCTO_CAT";
            CB_CATEGORIA.DataBind();

        }

        protected void B_NUEVO_Click(object sender, EventArgs e)
        {
            LIMPIARCAMPOS();
            PANEL_DETALLE1.Visible = true;
            PANEL_PRINCIPAL.Visible = false;
            UP_PRINCIPAL.Update();
            //DIV_STOCK.Visible = false;
            div_stock.Visible = true;
        }

        protected void B_GUARDAR_Click(object sender, EventArgs e)
        {
            if (T_NOMBRE.Text == "")
            {
                alert("Ingrese un nombre para el repuesto", 0);
                T_NOMBRE.Focus();
            }
            else if (T_CODIGO.Text == "")
            {
                alert("Ingrese un codigo para el repuesto", 0);
                T_CODIGO.Focus();
            }
            else if (T_SKU.Text == "")
            {
                alert("Ingrese un SKU para el repuesto", 0);
                T_SKU.Focus();
            }
            else if (T_STOCK.Text == "")
            {
                alert("Ignrese un stock inicial", 0);
                T_STOCK.Focus();
            }
            else
            {
                OBJ_MANT_PRODUCTO objeto_mantenedor = new OBJ_MANT_PRODUCTO();
                if (T_ID.Text != "")
                {
                    // EDITAR
                    objeto_mantenedor.ID_PRODUCTO = int.Parse(T_ID.Text);
                    FN_MANT_PRODUCTO.LLENAOBJETO(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        objeto_mantenedor.nom_producto = T_NOMBRE.Text;
                        objeto_mantenedor.cod_producto = T_CODIGO.Text;
                        objeto_mantenedor.sku = T_SKU.Text;
                        objeto_mantenedor.id_marca = int.Parse(CB_MARCA.SelectedValue);
                        objeto_mantenedor.cat_producto = int.Parse(CB_CATEGORIA.SelectedValue);

                        FN_MANT_PRODUCTO.UPDATE(ref objeto_mantenedor);
                        if (objeto_mantenedor._respok)
                        {
                            alert(objeto_mantenedor_global + " modificado con éxito", 1);
                        }
                    }
                }
                else
                {
                    FN_MANT_PRODUCTO.PREPARAOBJETO(ref objeto_mantenedor);
                    // NUEVO            
                    objeto_mantenedor.nom_producto = T_NOMBRE.Text;
                    objeto_mantenedor.cod_producto = T_CODIGO.Text;
                    objeto_mantenedor.sku = T_SKU.Text;
                    objeto_mantenedor.id_marca = int.Parse(CB_MARCA.SelectedValue);
                    objeto_mantenedor.cat_producto = int.Parse(CB_CATEGORIA.SelectedValue);
                    //      
                    FN_MANT_PRODUCTO.INSERT(ref objeto_mantenedor);
                    if (objeto_mantenedor._respok)
                    {
                        T_ID.Text = objeto_mantenedor.ID_PRODUCTO.ToString();
                        DBUtil db = new DBUtil();
                        db.Scalar("insert into mant_producto_stock (id_producto, cantidad) values (" + objeto_mantenedor.ID_PRODUCTO + ", " + T_STOCK.Text + ");");
                        OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                        usuario.usuario = HttpContext.Current.User.Identity.Name;
                        FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
                        db.Scalar("insert into mant_stock_log (entra_sale, id_producto, cantidad, precio_compra, id_ot, doc_compra, fecha, usuario, motivo) values (1, " + objeto_mantenedor.ID_PRODUCTO + ", " + T_STOCK.Text + ", -1, '-1', 'N/A', getdate(), " + usuario.ID_USUARIO + ", 'STOCK INICIAL'); ");
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
                    OBJ_MANT_PRODUCTO tabla = new OBJ_MANT_PRODUCTO();
                    tabla.ID_PRODUCTO = id;
                    FN_MANT_PRODUCTO.DELETE(ref tabla);
                    if (tabla._respok)
                    {
                        LlenarGrilla();
                    }
                }
                if (e.CommandName == "verstock")
                {
                    int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    string nombre = (G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString());
                    string marca = (G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[2].ToString());
                    ModalLogStock(id, nombre, marca);

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ModalLogStock(int id_repuesto, string nombre, string marca)
        {
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();

            dt = db.consultar("select * from V_REPUESTOS_LOG where id_producto = " + id_repuesto);
            G_LOG_STOCK.DataSource = dt;
            G_LOG_STOCK.DataBind();
            UP_GASTO_GENERAL.Update();
            stock_nombre_producto.InnerHtml = "Repuesto: " + nombre + " " + marca;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "abremodalgasto", "<script>javascript:GASTOGENERAL();</script>", false);
        }

        public void COMPLETAR_DETALLE(int id)
        {
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();
            OBJ_MANT_PRODUCTO objeto_mantenedor = new OBJ_MANT_PRODUCTO();

            objeto_mantenedor.ID_PRODUCTO = id;
            FN_MANT_PRODUCTO.LLENAOBJETO(ref objeto_mantenedor);
            if (objeto_mantenedor._respok)
            {
                LIMPIARCAMPOS();
                T_ID.Text = id.ToString();
                T_NOMBRE.Text = objeto_mantenedor.nom_producto;
                T_CODIGO.Text = objeto_mantenedor.cod_producto;
                T_SKU.Text = objeto_mantenedor.sku;
                CB_MARCA.SelectedValue = objeto_mantenedor.id_marca.ToString();
                CB_CATEGORIA.SelectedValue = objeto_mantenedor.cat_producto.ToString();
                // LLENAR LOG
                div_stock.Visible = false;
                //DIV_STOCK.Visible = true;

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
            MakeAccessible(G_LOG_STOCK);
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