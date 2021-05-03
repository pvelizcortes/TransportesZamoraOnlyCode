using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AMALIAFW;

namespace AMALIA
{
    public partial class Stock_Repuestos : System.Web.UI.Page
    {
        public static string objeto_mantenedor_global = "Proveedor";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrilla();
                DBUtil db = new DBUtil();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "grid", "<script>javascript:Datatables();</script>", false);
            }
        }

        public void LlenarGrilla()
        {
            DBUtil db = new DBUtil();
            G_PRINCIPAL.DataSource = db.consultar("Select * from  V_REPUESTOS ");
            G_PRINCIPAL.DataBind();
        }


        protected void G_PRINCIPAL_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //editar
                if (e.CommandName == "Editar")
                {
                    T_CANTIDAD.Text = "";
                    T_CANTIDAD2.Text = "";
                    T_NUM_DOCUMENTO.Text = "";
                    T_MOTIVO.Text = "";
                    T_VALOR_COMPRA.Text = "";

                    int id_stock = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString()));
                    int id_producto = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString()));
                    int cantidad = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[2].ToString()));
                    string nombre_repuesto = G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[3].ToString();

                    t_id_stock.Text = id_stock.ToString();
                    T_STOCK_ACTUAL.Text = cantidad.ToString();
                    T_ID_PRODUCTO.Text = id_producto.ToString();
                    TITULO_MODAL.InnerHtml = "<h4 class='title text-purple'>Repuesto: " + nombre_repuesto + "</h4>";
                    AbreModalGasto();
                    UP_GASTO_GENERAL.Update();

                }
                if (e.CommandName == "verstock")
                {
                    int id = int.Parse((G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString()));
                    string nombre = (G_PRINCIPAL.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[3].ToString());
                    
                    ModalLogStock(id, nombre);

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ModalLogStock(int id_repuesto, string nombre)
        {
            DBUtil db = new DBUtil();
            DataTable dt = new DataTable();

            dt = db.consultar("select * from V_REPUESTOS_LOG where id_producto = " + id_repuesto);
            G_LOG_STOCK.DataSource = dt;
            G_LOG_STOCK.DataBind();
            UP_GASTO_GENERAL2.Update();
            stock_nombre_producto.InnerHtml = "Log Stock del repuesto: " + nombre;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "abremodalgasto2", "<script>javascript:GASTOGENERAL2();</script>", false);
        }

        public void LlenaLog(int id_producto)
        {

        }

        public void AbreModalGasto()
        {
            // ABRE MODAL
            ScriptManager.RegisterStartupScript(this, typeof(Page), "abremodalgasto2", "<script>javascript:GASTOGENERAL();</script>", false);
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
                    if (control.ID != "FITRO_FECHA")
                    {
                        ((TextBox)control).Text = string.Empty;
                    }
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

        protected void B_GUARDAR_STOCK_Click(object sender, EventArgs e)
        {
            DBUtil db = new DBUtil();
            OBJ_USUARIOS usuario = new OBJ_USUARIOS();
            usuario.usuario = HttpContext.Current.User.Identity.Name;
            FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
            db.Scalar("update mant_producto_stock set cantidad = " + T_STOCK_ACTUAL.Text + " where id_stock = " + t_id_stock.Text);
            // AGREGAR AL LOG
            db.Scalar("insert into mant_stock_log (entra_sale, id_producto, cantidad, precio_compra, id_ot, doc_compra, fecha, usuario, motivo) values (-1, " + T_ID_PRODUCTO.Text + ", " + T_STOCK_ACTUAL.Text + ", -1, '-1', 'N/A', getdate(), " + usuario.ID_USUARIO + ", 'EDICION MANUAL'); ");
            //
            alert("Modificado con éxito", 1);
            LlenarGrilla();
          
        }

        protected void B_SUBIR_Click(object sender, EventArgs e)
        {

        }

        protected void B_BAJAR_Click(object sender, EventArgs e)
        {

        }

        protected void B_AUMENTAR_STOCK_Click(object sender, EventArgs e)
        {
            try
            {
                if (T_CANTIDAD.Text == "")
                {
                    alert("Ingrese la cantidad", 0);
                }
                else if (T_VALOR_COMPRA.Text == "")
                {
                    alert("Ingrese valor de compra", 0);
                }
                else if (T_NUM_DOCUMENTO.Text == "")
                {
                    alert("Ingrese un numero de documento", 0);
                }
                else
                {
                    DataTable dt = new DataTable();
                    DBUtil db = new DBUtil();

                    dt = db.consultar("select * from mant_producto_stock where id_producto = " + T_ID_PRODUCTO.Text);
                    if (dt.Rows.Count > 0)
                    {
                        // EDITAR STOCK
                        int stock_actual = int.Parse(dt.Rows[0]["cantidad"].ToString());
                        int sumar = int.Parse(T_CANTIDAD.Text);
                        OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                        usuario.usuario = HttpContext.Current.User.Identity.Name;
                        FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
                        db.Scalar("update mant_producto_stock set cantidad = " + (stock_actual + sumar) + " where id_producto = " + T_ID_PRODUCTO.Text);
                        db.Scalar("insert into mant_stock_log (entra_sale, id_producto, cantidad, cantidad_inicial, cantidad_final, precio_compra, id_ot, doc_compra, fecha, usuario, motivo) values (1, " + T_ID_PRODUCTO.Text + ", " + T_CANTIDAD.Text + ", " + stock_actual + ", " + (stock_actual + sumar) + ", " + T_VALOR_COMPRA.Text + ", '-1', '" + T_NUM_DOCUMENTO.Text + "', getdate(), " + usuario.ID_USUARIO + ", 'INGRESO DE STOCK CON DOCUMENTO'); ");
                        alert("Modificado con éxito", 1);
                        LlenarGrilla();
                    }                   
                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void B_DISMINUIR_STOCK_Click(object sender, EventArgs e)
        {
            try
            {
                if (T_CANTIDAD2.Text == "")
                {
                    alert("Ingrese la cantidad", 0);
                }
                else if (T_MOTIVO.Text == "")
                {
                    alert("Ingrese un motivo", 0);
                }
                else
                {
                    DataTable dt = new DataTable();
                    DBUtil db = new DBUtil();
                    OBJ_USUARIOS usuario = new OBJ_USUARIOS();
                    usuario.usuario = HttpContext.Current.User.Identity.Name;
                    FN_USUARIOS.BUSCARCONUSUARIO(ref usuario);
                    dt = db.consultar("select * from mant_producto_stock where id_producto = " + T_ID_PRODUCTO.Text);
                    if (dt.Rows.Count > 0)
                    {
                        // EDITAR STOCK
                        int stock_actual = int.Parse(dt.Rows[0]["cantidad"].ToString());
                        int restar = int.Parse(T_CANTIDAD2.Text);

                        db.Scalar("update mant_producto_stock set cantidad = " + (stock_actual - restar) + " where id_producto = " + T_ID_PRODUCTO.Text);
                        db.Scalar("insert into mant_stock_log (entra_sale, id_producto, cantidad, cantidad_inicial, cantidad_final, precio_compra, id_ot, doc_compra, fecha, usuario, motivo) values (2, " + T_ID_PRODUCTO.Text + ", " + T_CANTIDAD2.Text + ", " + stock_actual + ", " + (stock_actual - restar) + ", -1, '-1', 'N/A', getdate(), " + usuario.ID_USUARIO + ", '" + T_MOTIVO.Text + "'); ");
                        alert("Modificado con éxito", 1);
                        LlenarGrilla();

                    }     
              
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}